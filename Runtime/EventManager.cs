using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    /// <summary>
    /// A global publish/subscribe event bus keyed by type. An event is any type - usually a small
    /// <c>readonly struct</c> declared by the system that raises it - so every system brings its own
    /// events and nothing ever has to be added to a shared list:
    /// <code>
    /// public readonly struct PlayerDied { }
    ///
    /// public readonly struct HealthChanged
    /// {
    ///     public readonly int Value;
    ///     public HealthChanged(int value) => Value = value;
    /// }
    ///
    /// EventManager.Register&lt;HealthChanged&gt;(OnHealthChanged);
    /// EventManager.Invoke(new HealthChanged(80));
    /// EventManager.Unregister&lt;HealthChanged&gt;(OnHealthChanged);
    /// </code>
    /// </summary>
    /// <remarks>
    /// Every event type gets its own static channel, so <see cref="Invoke{T}(T)"/> is a static field read
    /// and a delegate call: no dictionary lookup, no boxing, and no allocation when the event is a struct.
    /// For every MonoBehaviour listener, register in <c>OnEnable</c> and unregister in <c>OnDisable</c> -
    /// the EventManager knows nothing about Unity's object lifecycle.
    /// </remarks>
    public static class EventManager
    {
        // One "clear" per channel ever used, so Clear() can reach every channel without knowing their types.
        private static readonly List<Action> ChannelClears = new();

        /// <summary>Starts calling <paramref name="handler"/> whenever a <typeparamref name="T"/> is invoked.</summary>
        public static void Register<T>(Action<T> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            Channel<T>.Handlers += handler;
        }

        /// <summary>Stops calling <paramref name="handler"/>. A no-op if it was never registered.</summary>
        public static void Unregister<T>(Action<T> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            Channel<T>.Handlers -= handler;
        }

        /// <summary>Calls every handler registered for <typeparamref name="T"/> with <paramref name="evt"/>.</summary>
        public static void Invoke<T>(T evt) => Channel<T>.Handlers?.Invoke(evt);

        /// <summary>Invokes an event that carries no data, e.g. <c>EventManager.Invoke&lt;PlayerDied&gt;()</c>.</summary>
        public static void Invoke<T>() where T : struct => Channel<T>.Handlers?.Invoke(default);

        /// <summary>Whether anything listens to <typeparamref name="T"/> - e.g. to skip building an expensive event.</summary>
        public static bool HasListeners<T>() => Channel<T>.Handlers != null;

        /// <summary>Unregisters every handler of <typeparamref name="T"/>.</summary>
        public static void Clear<T>() => Channel<T>.Handlers = null;

        /// <summary>Unregisters every handler of every event.</summary>
        public static void Clear()
        {
            foreach (var clear in ChannelClears)
            {
                clear();
            }
        }

        // Keeps stale handlers out when "Enter Play Mode Options" skips the domain reload.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics() => Clear();

        private static class Channel<T>
        {
            public static Action<T> Handlers;

            static Channel() => ChannelClears.Add(() => Handlers = null);
        }
    }
}

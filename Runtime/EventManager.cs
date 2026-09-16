using System;
using System.Collections.Generic;

namespace EventSystem
{
    /// <summary>
    /// A global publish/subscribe event manager, keyed by <see cref="EventTypes"/>. Covers two shapes:
    /// a plain event that carries no data, and an event that carries one piece of data (any of the
    /// built-in <c>Args</c> types, or a custom one you generate via
    /// <c>Assets/Create/Event System/Args Script</c>).
    /// <code>
    /// // No data:
    /// EventManager.RegisterEvent(EventTypes.PlayerDead, OnPlayerDead);
    /// EventManager.InvokeEvent(EventTypes.PlayerDead);
    /// EventManager.UnregisterEvent(EventTypes.PlayerDead, OnPlayerDead);
    ///
    /// // With data:
    /// EventManager.RegisterEvent&lt;BoolArgs&gt;(EventTypes.InteractableUndetected, OnInteractableUndetected);
    /// EventManager.InvokeEvent(EventTypes.InteractableUndetected, new BoolArgs(true));
    /// EventManager.UnregisterEvent&lt;BoolArgs&gt;(EventTypes.InteractableUndetected, OnInteractableUndetected);
    /// </code>
    /// </summary>
    /// <remarks>
    /// For every MonoBehaviour listener, register in <c>OnEnable</c> and unregister in
    /// <c>OnDisable</c> - EventManager has no knowledge of Unity's object lifecycle, so a listener
    /// that forgets to unregister keeps receiving events (and can throw on a destroyed object) after
    /// it should be gone.
    /// </remarks>
    public static class EventManager
    {
        private static readonly Dictionary<EventTypes, Action> ParameterlessHandlers = new();
        private static readonly Dictionary<EventTypes, Delegate> ArgHandlers = new();

        /// <summary>Registers a handler for a parameterless event.</summary>
        public static void RegisterEvent(EventTypes type, Action handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            ParameterlessHandlers[type] = ParameterlessHandlers.TryGetValue(type, out var existing)
                ? existing + handler
                : handler;
        }

        /// <summary>Unregisters a previously registered parameterless handler. A no-op if it was never registered.</summary>
        public static void UnregisterEvent(EventTypes type, Action handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            if (!ParameterlessHandlers.TryGetValue(type, out var existing))
            {
                return;
            }

            var remaining = existing - handler;
            if (remaining == null)
            {
                ParameterlessHandlers.Remove(type);
            }
            else
            {
                ParameterlessHandlers[type] = remaining;
            }
        }

        /// <summary>Invokes every handler registered for the parameterless event <paramref name="type"/>.</summary>
        public static void InvokeEvent(EventTypes type)
        {
            if (ParameterlessHandlers.TryGetValue(type, out var handler))
            {
                handler.Invoke();
            }
        }

        /// <summary>Registers a handler for an event that carries a <typeparamref name="T"/> payload.</summary>
        public static void RegisterEvent<T>(EventTypes type, Action<T> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            ArgHandlers[type] = ArgHandlers.TryGetValue(type, out var existing)
                ? Delegate.Combine(existing, handler)
                : handler;
        }

        /// <summary>Unregisters a previously registered handler for a data-carrying event. A no-op if it was never registered.</summary>
        public static void UnregisterEvent<T>(EventTypes type, Action<T> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            if (!ArgHandlers.TryGetValue(type, out var existing))
            {
                return;
            }

            var remaining = Delegate.Remove(existing, handler);
            if (remaining == null)
            {
                ArgHandlers.Remove(type);
            }
            else
            {
                ArgHandlers[type] = remaining;
            }
        }

        /// <summary>Invokes every handler registered for <paramref name="type"/> with <paramref name="args"/>.</summary>
        public static void InvokeEvent<T>(EventTypes type, T args)
        {
            if (ArgHandlers.TryGetValue(type, out var handler))
            {
                ((Action<T>)handler).Invoke(args);
            }
        }

        /// <summary>Unregisters every handler for every event.</summary>
        public static void Clear()
        {
            ParameterlessHandlers.Clear();
            ArgHandlers.Clear();
        }

        /// <summary>Unregisters every handler - parameterless or data-carrying - for one specific event.</summary>
        public static void ClearEvent(EventTypes type)
        {
            ParameterlessHandlers.Remove(type);
            ArgHandlers.Remove(type);
        }
    }
}

using UnityEngine;

namespace EventSystem
{
    /// <summary>Event data carrying a single <see cref="Vector2"/>.</summary>
    public sealed class Vector2Args
    {
        public Vector2 Value;

        public Vector2Args(Vector2 value)
        {
            Value = value;
        }
    }
}

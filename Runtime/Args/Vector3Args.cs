using UnityEngine;

namespace EventSystem
{
    /// <summary>Event data carrying a single <see cref="Vector3"/>.</summary>
    public sealed class Vector3Args
    {
        public Vector3 Value;

        public Vector3Args(Vector3 value)
        {
            Value = value;
        }
    }
}

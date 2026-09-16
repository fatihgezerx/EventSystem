using UnityEngine;

namespace EventSystem
{
    /// <summary>Event data carrying a single <see cref="GameObject"/> reference.</summary>
    public sealed class GameObjectArgs
    {
        public GameObject Value;

        public GameObjectArgs(GameObject value)
        {
            Value = value;
        }
    }
}

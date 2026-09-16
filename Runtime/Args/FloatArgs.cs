namespace EventSystem
{
    /// <summary>Event data carrying a single <see cref="float"/>.</summary>
    public sealed class FloatArgs
    {
        public float Value;

        public FloatArgs(float value)
        {
            Value = value;
        }
    }
}

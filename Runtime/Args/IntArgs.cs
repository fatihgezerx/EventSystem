namespace EventSystem
{
    /// <summary>Event data carrying a single <see cref="int"/>.</summary>
    public sealed class IntArgs
    {
        public int Value;

        public IntArgs(int value)
        {
            Value = value;
        }
    }
}

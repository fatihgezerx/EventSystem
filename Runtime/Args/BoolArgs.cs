namespace EventSystem
{
    /// <summary>Event data carrying a single <see cref="bool"/>.</summary>
    public sealed class BoolArgs
    {
        public bool Value;

        public BoolArgs(bool value)
        {
            Value = value;
        }
    }
}

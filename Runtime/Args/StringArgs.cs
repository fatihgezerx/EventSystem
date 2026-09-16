namespace EventSystem
{
    /// <summary>Event data carrying a single <see cref="string"/>.</summary>
    public sealed class StringArgs
    {
        public string Value;

        public StringArgs(string value)
        {
            Value = value;
        }
    }
}

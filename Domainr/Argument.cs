namespace Domainr
{
    public struct Argument
    {
        public readonly string Key;
        public readonly object Value;

        internal Argument(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
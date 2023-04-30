namespace VersOne.Epub.Test.Integration.JsonUtils.Configuration
{
    [Flags]
    internal enum PropertyDefaultValue
    {
        NULL = 1,
        FALSE = 2,
        EMPTY_ARRAY = 4,
        EMPTY_OBJECT = 8,
        EMPTY_DICTIONARY = 16
    }
}

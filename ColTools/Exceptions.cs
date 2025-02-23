using System;

namespace ColTools.Exceptions
{
    public class InputParsingException : Exception
    {
        public InputParsingException(string input, string mode)
            : base($"Input parsing failed on value \"{input}\" with mode \"{mode}\"") { }
    }

    public class GeneratorException : Exception
    {
        public GeneratorException(string minimumColor, string maximumColor)
            : base($"Generating random values failed on minimum \"{minimumColor}\" and maximum \"{maximumColor}\"") { }
    }
}

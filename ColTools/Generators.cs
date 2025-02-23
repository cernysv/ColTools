using System;
using ColTools.Exceptions;
using ColTools.Helpers;

namespace ColTools.Generators
{
    public class Generators
    {
        public string RandomColor(string minimumColor, string maximumColor, string mode)
        {
            byte[] internalMinimumColor = new byte[3];
            byte[] internalMaximumColor = new byte[3];
            try
            {
                internalMinimumColor = Helpers.Helpers.ParseToInternal(minimumColor, mode);
                internalMaximumColor = Helpers.Helpers.ParseToInternal(maximumColor, mode);
            }
            catch
            {
                throw;
            }
            Random random = new Random();
            for (int i = 0; i < 3; i++) if (internalMinimumColor[i] > internalMaximumColor[i]) throw new GeneratorException(minimumColor, maximumColor);
            byte[] internalRandomColor = new byte[3];
            for (int i = 0; i < 3; i++) if (internalMinimumColor[i] == internalMaximumColor[i]) internalRandomColor[i] = internalMinimumColor[i]; else internalRandomColor[i] = (byte)random.Next(internalMinimumColor[i], internalMaximumColor[i] + 1);
            return Helpers.Helpers.ParseBack(internalRandomColor, mode);
        }

        public string[] RandomColors(string minimumColor, string maximumColor, string mode, int amount)
        {
            string[] outputArray = new string[amount];
            for (int i = 0; i < amount; i++)
            {
                outputArray[i] = RandomColor(minimumColor, maximumColor, mode);
            }
            return outputArray;
        }
    }
}

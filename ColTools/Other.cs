using System;
using ColTools.Exceptions;
using ColTools.Helpers;

namespace ColTools.Other
{
    public class Other
    {
        public string ConvertColor(string color, string mode, string outputMode)
        {
            return Helpers.Helpers.ParseBack(Helpers.Helpers.ParseToInternal(color, mode), outputMode);
        }

        public string InvertColor(string color, string mode)
        {
            byte[] internalColor = Helpers.Helpers.ParseToInternal(color, mode);
            byte[] internalInvertedColor = [Convert.ToByte(255 - internalColor[0]), Convert.ToByte(255 - internalColor[1]), Convert.ToByte(255 - internalColor[2])];
            return Helpers.Helpers.ParseBack(internalInvertedColor, mode);
        }

        public string ColorShade(string color, string mode)
        {
            byte[] internalColor = Helpers.Helpers.ParseToInternal(color, mode);
            if (internalColor[0] - internalColor[1] >= 100 && internalColor[0] - internalColor[2] >= 100) return "red";
            else if (internalColor[1] - internalColor[0] >= 100 && internalColor[1] - internalColor[2] >= 100) return "green";
            else if (internalColor[2] - internalColor[0] >= 100 && internalColor[2] - internalColor[1] >= 100) return "blue";
            else return "grayscale/unrecognized";
        }
    }
}

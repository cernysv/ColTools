#pragma warning disable 8603
using System;
using ColTools.Exceptions;

namespace ColTools.Helpers
{
    internal class Helpers
    {
        internal static byte[] ParseToInternal(string color, string mode)
        {
            byte[] internalColor = new byte[3];
            switch (mode)
            {
                case "hex":
                    try
                    {
                        internalColor[0] = Convert.ToByte(color.Substring(1, 2), 16);
                        internalColor[1] = Convert.ToByte(color.Substring(3, 2), 16);
                        internalColor[2] = Convert.ToByte(color.Substring(5, 2), 16);
                    }
                    catch
                    {
                        throw new InputParsingException(color, mode);
                    }
                    break;
                case "rgb":
                    try
                    {
                        string[] reparse = color.Replace("rgb(", "").TrimEnd(')').Replace(" ", "").Split(',');
                        internalColor[0] = Convert.ToByte(reparse[0]);
                        internalColor[1] = Convert.ToByte(reparse[1]);
                        internalColor[2] = Convert.ToByte(reparse[2]);
                    }
                    catch
                    {
                        throw new InputParsingException(color, mode);
                    }
                    break;
                case "cmyk":
                    try
                    {
                        float[] cmykColor = Array.ConvertAll(color.Replace("cmyk(", "").TrimEnd(')').Replace(" ", "").Split(','), float.Parse);
                        internalColor[0] = Convert.ToByte((1 - cmykColor[0]) * (1 - cmykColor[3]) * 255);
                        internalColor[1] = Convert.ToByte((1 - cmykColor[1]) * (1 - cmykColor[3]) * 255);
                        internalColor[2] = Convert.ToByte((1 - cmykColor[2]) * (1 - cmykColor[3]) * 255);
                    }
                    catch
                    {
                        throw new InputParsingException(color, mode);
                    }
                    break;
                default:
                    throw new InputParsingException(color, mode);
            }
            return internalColor;
        }

        internal static string ParseBack(byte[] color, string mode)
        {
            switch (mode)
            {
                case "hex":
                    return $"#{color[0].ToString("X2")}{color[1].ToString("X2")}{color[2].ToString("X2")}";
                case "rgb":
                    return $"rgb({color[0]}, {color[1]}, {color[2]})";
                case "cmyk":
                    float[] normalizedColor = [color[0] / 255f, color[1] / 255f, color[2] / 255f];
                    float key = 1 - Math.Max(Math.Max(normalizedColor[0], normalizedColor[1]), normalizedColor[2]);
                    float[] reparse = new float[3];
                    if (key < 1)
                    {
                        reparse[0] = (1 - normalizedColor[0] - key) / (1 - key);
                        reparse[1] = (1 - normalizedColor[1] - key) / (1 - key);
                        reparse[2] = (1 - normalizedColor[2] - key) / (1 - key);
                    }
                    else reparse[0] = reparse[1] = reparse[2] = 0f;
                    return $"cmyk({reparse[0]:0.0}, {reparse[1]:0.0}, {reparse[2]:0.0}, {key:0.0})";
            }
            return null;
        }
    }
}

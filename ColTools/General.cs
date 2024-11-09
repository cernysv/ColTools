using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.InteropServices.Marshalling;

namespace ColTools
{
    public class Generators
    {
        // Generates a random color within specified ranges and in the specified mode
        public static string RandomColor(string colorMin, string colorMax, string mode)
        {
            switch (mode)
            {
                case "hex": // Hex mode
                    if (colorMin.Length != 7 || colorMax.Length != 7) { return "E7"; }

                    if (!colorMin.StartsWith("#") || !colorMax.StartsWith("#")) { return "E7"; }

                    string hexColorMin = colorMin.TrimStart('#');
                    string hexColorMax = colorMax.TrimStart('#');

                    if (!System.Text.RegularExpressions.Regex.IsMatch(colorMin, @"^#[0-9A-Fa-f]{6}$") || !System.Text.RegularExpressions.Regex.IsMatch(colorMax, @"^[0-9A-Fa-f]{6}$")) { return "E7"; }

                    int hexColorRedMin = int.Parse(hexColorMin.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    int hexColorGreenMin = int.Parse(hexColorMin.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    int hexColorBlueMin = int.Parse(hexColorMin.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

                    int hexColorRedMax = int.Parse(hexColorMax.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    int hexColorGreenMax = int.Parse(hexColorMax.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    int hexColorBlueMax = int.Parse(hexColorMax.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

                    if (hexColorRedMin > hexColorRedMax || hexColorGreenMin > hexColorGreenMax || hexColorBlueMin > hexColorBlueMax) { return "B2"; }

                    int hexColorRed = new Random().Next(hexColorRedMin, hexColorRedMax);
                    int hexColorGreen = new Random().Next(hexColorGreenMin, hexColorGreenMax);
                    int hexColorBlue = new Random().Next(hexColorBlueMin, hexColorBlueMax);

                    return $"#{hexColorRed:X2}{hexColorGreen:X2}{hexColorBlue:X2}";

                case "rgb": // Rgb mode
                    string rgbRegEx = @"^rgb\(\s*(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\s*,\s*(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\s*,\s*(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\s*\)$";

                    if (colorMin.Length > 18 || colorMin.Length < 12 || colorMax.Length > 18 || colorMax.Length < 12) { return "E7"; }
                    if (!colorMin.StartsWith("rgb(") || !colorMax.StartsWith("rgb(") || !colorMin.EndsWith(')') || !colorMax.EndsWith(')')) { return "E7"; }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(colorMin, rgbRegEx) || !System.Text.RegularExpressions.Regex.IsMatch(colorMax, rgbRegEx)) { return "E7"; }

                    string[] rgbColorMin = colorMin.Replace("rgb(", "").TrimEnd(')').Split(", ");
                    string[] rgbColorMax = colorMax.Replace("rgb(", "").TrimEnd(')').Split(", ");

                    int rgbColorRedMin = int.Parse(rgbColorMin[0].Trim());
                    int rgbColorGreenMin = int.Parse(rgbColorMin[1].Trim());
                    int rgbColorBlueMin = int.Parse(rgbColorMin[2].Trim());

                    int rgbColorRedMax = int.Parse(rgbColorMax[0].Trim());
                    int rgbColorGreenMax = int.Parse(rgbColorMax[1].Trim());
                    int rgbColorBlueMax = int.Parse(rgbColorMax[2].Trim());

                    if (rgbColorRedMin > rgbColorRedMax || rgbColorGreenMin > rgbColorGreenMax || rgbColorBlueMin > rgbColorBlueMax) { return "B2"; }

                    int rgbColorRed = new Random().Next(rgbColorRedMin, rgbColorRedMax);
                    int rgbColorGreen = new Random().Next(rgbColorGreenMin, rgbColorGreenMax);
                    int rgbColorBlue = new Random().Next(rgbColorBlueMin, rgbColorBlueMax);

                    return $"rgb({rgbColorRed}, {rgbColorGreen}, {rgbColorBlue})";

                case "hsl": // Hsl mode
                    string hslRegex = @"^hsl\(\s*(360|[0-9]?[1-9]?[0-9])\s*,\s*(100|[1-9]?\d)\s*,\s*(100|[1-9]?\d)\s*\)$";

                    if (colorMin.Length > 19 || colorMin.Length < 13 || colorMax.Length > 19 || colorMax.Length < 13) { return "E7"; }
                    if (!colorMin.StartsWith("hsl(") || !colorMax.StartsWith("hsl(") || !colorMin.EndsWith(')') || !colorMax.EndsWith(')')) { return "E7"; }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(colorMin, hslRegex) || !System.Text.RegularExpressions.Regex.IsMatch(colorMax, hslRegex)) { return "E7"; }

                    string[] hslColorMin = colorMin.Replace("hsl(", "").TrimEnd(')').Split(", ");
                    string[] hslColorMax = colorMax.Replace("hsl(", "").TrimEnd(')').Split(", ");

                    int hslColorHueMin = int.Parse(hslColorMin[0].Trim());
                    int hslColorSaturationMin = int.Parse(hslColorMin[1].Trim());
                    int hslColorLightMin = int.Parse(hslColorMin[2].Trim());

                    int hslColorHueMax = int.Parse(hslColorMax[0].Trim());
                    int hslColorSaturationMax = int.Parse(hslColorMax[1].Trim());
                    int hslColorLightMax = int.Parse(hslColorMax[2].Trim());

                    if (hslColorHueMin > hslColorHueMax || hslColorSaturationMin > hslColorSaturationMax || hslColorLightMin > hslColorLightMax) { return "B2"; }

                    int hslColorHue = new Random().Next(hslColorHueMin, hslColorHueMax);
                    int hslColorSaturation = new Random().Next(hslColorSaturationMin, hslColorSaturationMax);
                    int hslColorLight = new Random().Next(hslColorLightMin, hslColorLightMax);

                    return $"hsl({hslColorHue}, {hslColorSaturation}, {hslColorLight})";

                case "cmyk": // Cmyk mode
                    string cmykRegex = @"^cmyk\(\s*(100|[1-9]?[0-9])\s*,\s*(100|[1-9]?[0-9])\s*,\s*(100|[1-9]?[0-9])\s*,\s*(100|[1-9]?[0-9])\s*\)$";

                    if (colorMin.Length > 24 || colorMin.Length < 18 || colorMin.Length > 24 || colorMin.Length < 18) { return "E7"; }
                    if (!colorMin.StartsWith("cmyk(") || !colorMax.StartsWith("cmyk(") || !colorMin.EndsWith(")") || !colorMax.EndsWith(")")) { return "E7"; }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(colorMin, cmykRegex) || !System.Text.RegularExpressions.Regex.IsMatch(colorMax, cmykRegex)) { return "E7"; }

                    string[] cmykColorMin = colorMin.Replace("cmyk(", "").TrimEnd(')').Split(", ");
                    string[] cmykColorMax = colorMax.Replace("cmyk(", "").TrimEnd(')').Split(", ");

                    int cmykColorCyanMin = int.Parse(cmykColorMin[0].Trim());
                    int cmykColorMagentaMin = int.Parse(cmykColorMin[1].Trim());
                    int cmykColorYellowMin = int.Parse(cmykColorMin[2].Trim());
                    int cmykColorBlackMin = int.Parse(cmykColorMin[3].Trim());

                    int cmykColorCyanMax = int.Parse(cmykColorMax[0].Trim());
                    int cmykColorMagentaMax = int.Parse(cmykColorMax[1].Trim());
                    int cmykColorYellowMax = int.Parse(cmykColorMax[2].Trim());
                    int cmykColorBlackMax = int.Parse(cmykColorMax[3].Trim());

                    if (cmykColorCyanMin > cmykColorCyanMax || cmykColorMagentaMin > cmykColorMagentaMax || cmykColorYellowMin > cmykColorYellowMax || cmykColorBlackMin > cmykColorBlackMax) { return "B2"; }

                    int cmykColorCyan = new Random().Next(cmykColorCyanMin, cmykColorCyanMax);
                    int cmykColorMagenta = new Random().Next(cmykColorMagentaMin, cmykColorMagentaMax);
                    int cmykColorYellow = new Random().Next(cmykColorYellowMin, cmykColorYellowMax);
                    int cmykColorBlack = new Random().Next(cmykColorBlackMin, cmykColorBlackMax);

                    return $"cmyk({cmykColorCyan}, {cmykColorMagenta}, {cmykColorYellow}, {cmykColorBlack}";

                case "hsv": // Hsv mode
                    string hsvRegex = @"^hsv\(\s*(360|[1-9]?\d{1,2})\s*,\s*(100(\.\d+)?|[1-9]?\d)\s*,\s*(100(\.\d+)?|[1-9]?\d)\s*\)$";

                    if (colorMin.Length > 19 || colorMin.Length < 13 || colorMax.Length > 19 || colorMax.Length < 12) { return "E7"; }
                    if (!colorMin.StartsWith("hsv(") || !colorMax.StartsWith("hsv(") || !colorMin.EndsWith(')') || !colorMax.EndsWith(')')) { return "E7"; }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(colorMin, hsvRegex) || !System.Text.RegularExpressions.Regex.IsMatch(colorMax, hsvRegex)) { return "E7"; }

                    string[] hsvColorMin = colorMin.Replace("hsv(", "").TrimEnd(')').Split(", ");
                    string[] hsvColorMax = colorMax.Replace("hsv(", "").TrimEnd(')').Split(", ");

                    int hsvColorHueMin = int.Parse(hsvColorMin[0].Trim());
                    int hsvColorSaturationMin = int.Parse(hsvColorMin[1].Trim());
                    int hsvColorValueMin = int.Parse(hsvColorMin[2].Trim());

                    int hsvColorHueMax = int.Parse(hsvColorMax[0].Trim());
                    int hsvColorSaturationMax = int.Parse(hsvColorMax[1].Trim());
                    int hsvColorValueMax = int.Parse(hsvColorMax[2].Trim());

                    if (hsvColorHueMin > hsvColorHueMax || hsvColorSaturationMin > hsvColorSaturationMax || hsvColorValueMin > hsvColorValueMax) { return "B2"; }

                    int hsvColorHue = new Random().Next(hsvColorHueMin, hsvColorHueMax);
                    int hsvColorSaturation = new Random().Next(hsvColorSaturationMin, hsvColorSaturationMax);
                    int hsvColorValue = new Random().Next(hsvColorValueMin, hsvColorValueMax);

                    return $"hsv({hsvColorHue}, {hsvColorSaturation}, {hsvColorValue}";

                default:
                     return "F8";
            }
        }

        // Generates a specified amount of random colors within specified ranges and in the specified mode
        public static string MultipleRandomColors(string colorMin, string colorMax, string mode, int colorCount)
        {
            var colorList = new List<string>();

            for (int i = 0; i < colorCount; i++)
            {
                string color = RandomColor(colorMin, colorMax, mode);

                if (color == "E7") { return "E7"; }
                if (color == "B2") { return "B2"; }
                if (color == "F8") { return "F8"; }

                colorList.Add(color);
            }

            return string.Join(",", colorList);
        }
    }

    public class Converters
    {
        // Converts hex to hsl
        public static string HexToHsl(string hexColor)
        {
            string hexToHslRgbColor = HexToRgb(hexColor);
            string hexToHslColor = RgbToHsl(hexToHslRgbColor);

            return hexToHslColor;
        }

        // Converts hsl to hex
        public static string HslToHex(string hslColor)
        {
            string hslToHexRgbColor = HslToRgb(hslColor);
            string hslToHexColor = RgbToHex(hslToHexRgbColor);

            return hslToHexColor;
        }
        // Converts hex to rgb
        public static string HexToRgb(string hexColor)
        {
            int rgbColorRed = int.Parse(hexColor.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            int rgbColorGreen = int.Parse(hexColor.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            int rgbColorBlue = int.Parse(hexColor.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

            return $"rgb({rgbColorRed}, {rgbColorGreen}, {rgbColorBlue})";
        }

        // Converts rgb to hex
        public static string RgbToHex(string rgbColor)
        {
            string[] parts = rgbColor.Trim().TrimStart("rgb(".ToCharArray()).TrimEnd(')').Split(',');

            int hexColorRed = int.Parse(parts[0].Trim());
            int hexColorGreen = int.Parse(parts[1].Trim());
            int hexColorBlue = int.Parse(parts[2].Trim());

            return $"#{hexColorRed:X2}{hexColorGreen:X2}{hexColorBlue:X2}";
        }

        // Converts rgb to hsl
        public static string RgbToHsl(string colorInput)
        {
            string[] rgbValues = colorInput.Trim().TrimStart("rgb(".ToCharArray()).TrimEnd(')').Split(',');

            int rgbRed = int.Parse(rgbValues[0].Trim());
            int rgbGreen = int.Parse(rgbValues[1].Trim());
            int rgbBlue = int.Parse(rgbValues[2].Trim());

            double hslHueRatio = rgbRed / 255.0;
            double hslSaturationRatio = rgbGreen / 255.0;
            double hslLightRatio = rgbBlue / 255.0;

            double maxVal = Math.Max(hslHueRatio, Math.Max(hslSaturationRatio, hslLightRatio));
            double minVal = Math.Min(hslHueRatio, Math.Min(hslSaturationRatio, hslLightRatio));
            double delta = maxVal - minVal;

            double lightness = (maxVal + minVal) / 2.0;

            double saturation = 0;
            if (delta != 0)
            {
                saturation = delta / (1 - Math.Abs(2 * lightness - 1));
            }

            double hue = 0;
            if (delta != 0)
            {
                if (maxVal == hslHueRatio)
                {
                    hue = 60 * (((hslSaturationRatio - hslLightRatio) / delta) % 6);
                }
                else if (maxVal == hslSaturationRatio)
                {
                    hue = 60 * (((hslLightRatio - hslHueRatio) / delta) + 2);
                }
                else if (maxVal == hslLightRatio)
                {
                    hue = 60 * (((hslHueRatio - hslSaturationRatio) / delta) + 4);
                }
            }

            if (hue < 0)
            {
                hue += 360;
            }

            hue = Math.Round(hue, 0);
            saturation = Math.Round(saturation * 100, 0);
            lightness = Math.Round(lightness * 100, 0);

            return $"hsl({hue}, {saturation}, {lightness})";
        }

        // Converts hsl to rgb
        public static string HslToRgb(string colorInput)
        {
            colorInput = colorInput.Replace("hsl(", "").Replace(")", "");
            string[] hslValues = colorInput.Split(',');

            double hslHue = double.Parse(hslValues[0], CultureInfo.InvariantCulture);
            double hslSaturation = double.Parse(hslValues[1], CultureInfo.InvariantCulture) / 100.0;
            double hslLightness = double.Parse(hslValues[2], CultureInfo.InvariantCulture) / 100.0;

            double rgbRedRatio = 0, rgbGreenRatio = 0, rgbBlueRatio = 0;

            if (hslSaturation == 0)
            {
                rgbRedRatio = rgbGreenRatio = rgbBlueRatio = hslLightness;
            }
            else
            {
                double chroma = (1 - Math.Abs(2 * hslLightness - 1)) * hslSaturation;
                double intermediate = chroma * (1 - Math.Abs((hslHue / 60) % 2 - 1));
                double match = hslLightness - chroma / 2;

                if (0 <= hslHue && hslHue < 60)
                {
                    rgbRedRatio = chroma;
                    rgbGreenRatio = intermediate;
                    rgbBlueRatio = 0;
                }
                else if (60 <= hslHue && hslHue < 120)
                {
                    rgbRedRatio = intermediate;
                    rgbGreenRatio = chroma;
                    rgbBlueRatio = 0;
                }
                else if (120 <= hslHue && hslHue < 180)
                {
                    rgbRedRatio = 0;
                    rgbGreenRatio = chroma;
                    rgbBlueRatio = intermediate;
                }
                else if (180 <= hslHue && hslHue < 240)
                {
                    rgbRedRatio = 0;
                    rgbGreenRatio = intermediate;
                    rgbBlueRatio = chroma;
                }
                else if (240 <= hslHue && hslHue < 300)
                {
                    rgbRedRatio = intermediate;
                    rgbGreenRatio = 0;
                    rgbBlueRatio = chroma;
                }
                else if (300 <= hslHue && hslHue < 360)
                {
                    rgbRedRatio = chroma;
                    rgbGreenRatio = 0;
                    rgbBlueRatio = intermediate;
                }

                rgbRedRatio += match;
                rgbGreenRatio += match;
                rgbBlueRatio += match;
            }

            int rgbRed = (int)(rgbRedRatio * 255);
            int rgbGreen = (int)(rgbGreenRatio * 255);
            int rgbBlue = (int)(rgbBlueRatio * 255);

            return $"rgb({rgbRed}, {rgbGreen}, {rgbBlue})";
        }
    }

    public static class Other
    {
        // Testing, not implemented yet
        /*private static string ReverseColor(string color, string mode)
        {
            if (mode != "hex" || mode != "rgb" || System.Text.RegularExpressions.Regex.IsMatch(color, "^#[A-Fa-f0-9]{6}$") || System.Text.RegularExpressions.Regex.IsMatch(color, "^rgb\\(\\s*(\\d{1,3})\\s*,\\s*(\\d{1,3})\\s*,\\s*(\\d{1,3})\\s*\\)$")) { return "E2";}

            if (mode == "hex")
            {
                string hexColorTrim = color.TrimStart('#');

                int hexColorRed = int.Parse(hexColorTrim.Substring(0, 2), NumberStyles.HexNumber);
                int hexColorGreen = int.Parse(hexColorTrim.Substring(2, 2), NumberStyles.HexNumber);
                int hexColorBlue = int.Parse(hexColorTrim.Substring(4, 2), NumberStyles.HexNumber);

                if (hexColorRed > 255 || hexColorGreen > 255 || hexColorBlue > 255 || hexColorRed < 0 || hexColorGreen < 0 || hexColorBlue < 0) { return "A3"; }

                int hexColorRedInvert =  255 - hexColorRed;
                int hexColorGreenInvert = 255 - hexColorGreen;
                int hexColorBlueInvert = 255 - hexColorBlue;

                return $"#{hexColorRedInvert:X2}{hexColorGreenInvert:X2}{hexColorBlueInvert:X2}";
            }
            else if (mode == "rgb")
            {
                string[] rgbColorTrimArray = color.Replace("rgb(", "").TrimEnd(')').Split(", ");

                int rgbColorRed = int.Parse(rgbColorTrimArray[0].Trim());
                int rgbColorGreen = int.Parse(rgbColorTrimArray[1].Trim());
                int rgbColorBlue = int.Parse(rgbColorTrimArray[2].Trim());

                if (rgbColorRed > 255 || rgbColorGreen > 255 || rgbColorBlue > 255 || rgbColorRed < 0 || rgbColorGreen < 0 || rgbColorBlue < 0) { return "A3"; }

                int rgbColorRedInvert = 255 - rgbColorRed;
                int rgbColorGreenInvert = 255 - rgbColorGreen;
                int rgbColorBlueInvert = 255 - rgbColorBlue;

                return $"rgb({rgbColorRedInvert}, {rgbColorGreenInvert}, {rgbColorBlueInvert})";
            }
            else
            {
                return "F8";
            }
        }*/

        // Detects the shade of a hex color
        public static string GetColorShade(string hexColor)
        {
            string colorValue = hexColor.TrimStart('#');
            int redValue = int.Parse(colorValue.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int greenValue = int.Parse(colorValue.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int blueValue = int.Parse(colorValue.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            if (redValue > 255 || greenValue > 255 || blueValue > 255 || redValue < 0 || greenValue < 0 || blueValue < 0) { return "A3"; }
            else if (redValue > 200 && greenValue < 150 && blueValue < 150) { return "Red"; }
            else if (redValue < 150 && greenValue > 200 && blueValue < 150) { return "Green"; }
            else if (redValue < 150 && greenValue < 150 && blueValue > 200) { return "Blue"; }
            else { return "D6"; }
        }
    }
}
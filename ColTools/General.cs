using System.Globalization;

namespace ColTools
{
    public class Generators
    {
        public static string RandomColor(string rR, string gR, string bR)
        {
            var rVs = rR.Split('-');
            int rS = int.Parse(rVs[0]);
            int rB = int.Parse(rVs[1]);

            var gVs = gR.Split('-');
            int gS = int.Parse(gVs[0]);
            int gB = int.Parse(gVs[1]);

            var bVs = bR.Split('-');
            int bS = int.Parse(bVs[0]);
            int bB = int.Parse(bVs[1]);

            int rV = new Random().Next(rS, rB + 1);
            int gV = new Random().Next(gS, gB + 1);
            int bV = new Random().Next(bS, bB + 1);

            string hC = $"#{rV:X2}{gV:X2}{bV:X2}";
            return hC;
        }

        public static string RandomColorPalette(string rR, string gR, string bR, int cC)
        {
            var cL = new List<string>();

            for (int i = 0; i < cC; i++)
            {
                string hC = RandomColor(rR, gR, bR);

                cL.Add(hC);
            }

            return string.Join(",", cL);
        }
    }

    public class Converters
    {
        public static string HexToRgb(string hC)
        {
            int rV = int.Parse(hC.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            int gV = int.Parse(hC.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            int bV = int.Parse(hC.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

            return $"rgb({rV}, {gV}, {bV})";
        }

        public static string RgbToHex(string rgbC)
        {
            string[] parts = rgbC.Trim().TrimStart("rgb(".ToCharArray()).TrimEnd(')').Split(',');

            int r = int.Parse(parts[0].Trim());
            int g = int.Parse(parts[1].Trim());
            int b = int.Parse(parts[2].Trim());

            return $"#{r:X2}{g:X2}{b:X2}";
        }

        public static string RgbToHsl(string rgbC)
        {
            rgbC = rgbC.Trim().TrimStart("rgb(".ToCharArray()).TrimEnd(')');

            string[] parts = rgbC.Split(',');

            int rV = int.Parse(parts[0].Trim());
            int gV = int.Parse(parts[1].Trim());
            int bV = int.Parse(parts[2].Trim());

            double rH = rV / 255.0;
            double gH = gV / 255.0;
            double bH = bV / 255.0;

            double m = Math.Max(rH, Math.Max(gH, bH));
            double mn = Math.Min(rH, Math.Min(gH, bH));
            double d = m - mn;

            double l = (m + mn) / 2.0;

            double s = 0;
            if (d != 0)
            {
                s = d / (1 - Math.Abs(2 * l - 1));
            }

            double h = 0;
            if (d != 0)
            {
                if (m == rH)
                {
                    h = 60 * (((gH - bH) / d) % 6);
                }
                else if (m == gH)
                {
                    h = 60 * (((bH - rH) / d) + 2);
                }
                else if (m == bH)
                {
                    h = 60 * (((rH - gH) / d) + 4);
                }
            }

            if (h < 0)
            {
                h += 360;
            }

            h = Math.Round(h, 0);
            s = Math.Round(s * 100, 0);
            l = Math.Round(l * 100, 0);

            return $"hsl({h}, {s}, {l})";
        }
        public static string HslToRgb(string hslString)
        {
            hslString = hslString.Replace("hsl(", "").Replace(")", "").Replace("%", "");
            string[] hslParts = hslString.Split(',');

            double h = double.Parse(hslParts[0], CultureInfo.InvariantCulture);
            double s = double.Parse(hslParts[1], CultureInfo.InvariantCulture) / 100.0;
            double l = double.Parse(hslParts[2], CultureInfo.InvariantCulture) / 100.0;

            double r = 0, g = 0, b = 0;

            if (s == 0)
            {
                r = g = b = l;
            }
            else
            {
                double C = (1 - Math.Abs(2 * l - 1)) * s;
                double X = C * (1 - Math.Abs((h / 60) % 2 - 1));
                double m = l - C / 2;

                if (0 <= h && h < 60)
                {
                    r = C;
                    g = X;
                    b = 0;
                }
                else if (60 <= h && h < 120)
                {
                    r = X;
                    g = C;
                    b = 0;
                }
                else if (120 <= h && h < 180)
                {
                    r = 0;
                    g = C;
                    b = X;
                }
                else if (180 <= h && h < 240)
                {
                    r = 0;
                    g = X;
                    b = C;
                }
                else if (240 <= h && h < 300)
                {
                    r = X;
                    g = 0;
                    b = C;
                }
                else if (300 <= h && h < 360)
                {
                    r = C;
                    g = 0;
                    b = X;
                }

                r += m;
                g += m;
                b += m;
            }

            int R = (int)(r * 255);
            int G = (int)(g * 255);
            int B = (int)(b * 255);

            return $"rgb({R}, {G}, {B})";
        }
    }

    public static class Shades
    {
        public static string GetColorShade(string hC)
        {
            string hCr = hC.Replace("#", "");
            int rV = int.Parse(hCr.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            int gV = int.Parse(hCr.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            int bV = int.Parse(hCr.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

            int brightness = (rV + gV + bV) / 3;
            bool isDark = brightness < 96;

            if (Math.Abs(rV - gV) < 40 && Math.Abs(gV - bV) < 40 && Math.Abs(rV - bV) < 40)
            {
                if (brightness > 200) return "Light Gray";
                if (brightness < 50) return "Dark Gray";
                return "Gray";
            }

            if (rV >= gV && rV >= bV)
            {
                if (rV > 200 && gV < 100 && bV < 100) return isDark ? "Dark Red" : "Light Red";
                return "Red";
            }
            else if (gV >= rV && gV >= bV)
            {
                if (gV > 200 && rV < 100 && bV < 100) return isDark ? "Dark Green" : "Light Green";
                return "Green";
            }
            else if (bV >= rV && bV >= gV)
            {
                if (bV > 200 && rV < 100 && gV < 100) return isDark ? "Dark Blue" : "Light Blue";
                return "Blue";
            }
            else if (rV > 200 && gV > 200 && bV < 100)
            {
                return isDark ? "Dark Yellow" : "Light Yellow";
            }
            else if (gV > 200 && bV > 200 && rV < 100)
            {
                return isDark ? "Dark Cyan" : "Light Cyan";
            }
            else if (rV > 200 && bV > 200 && gV < 100)
            {
                return isDark ? "Dark Magenta" : "Light Magenta";
            }
            else if (rV > gV && gV > bV)
            {
                return isDark ? "Dark Orange" : "Light Orange";
            }
            else if (rV < 100 && gV < 100 && bV < 100)
            {
                return "Black";
            }
            else if (rV > 200 && gV > 200 && bV > 200)
            {
                return "White";
            }
            else if (rV > 150 && gV < 100 && bV < 100)
            {
                return isDark ? "Dark Brown" : "Light Brown";
            }
            else if (rV < 100 && gV < 100 && bV > 100)
            {
                return isDark ? "Dark Violet" : "Light Violet";
            }

            return $"Color {hC} doesn't match any shade";
        }
    }
}
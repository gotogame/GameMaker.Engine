namespace GameMaker.Engine
{
    /// <summary>
    /// 颜色
    /// </summary>
    [JsonConverter(typeof(JsonConverterColor))]
    public struct Color
    {
        #region 字段

        public byte A;
        public byte R;
        public byte G;
        public byte B;

        #endregion

        #region 构造函数

        public Color(byte a, byte r, byte g, byte b)
        {
            A = a; R = r; G = g; B = b;
        }

        public Color(byte r, byte g, byte b)
        {
            A = 255; R = r; G = g; B = b;
        }

        #endregion

        #region 转换方法

        /// <summary>
        /// "A,R,G,B"
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool TryParse(string data, out Color color)
        {
            string[] values = data?.Split(',');

            if (values != null && values.Length == 4 &&
                byte.TryParse(values[0], out byte a) &&
                byte.TryParse(values[1], out byte r) &&
                byte.TryParse(values[2], out byte g) &&
                byte.TryParse(values[3], out byte b))
            {
                color = new Color(a, r, g, b);
                return true;
            }
            else
            {
                color = new Color();
                return false;
            }
        }

        /// <summary>
        /// "A,R,G,B"
        /// </summary>
        public static Color Parse(string data)
        {
            if (TryParse(data, out Color color))
                return color;
            else
                throw new FormatException();
        }

        /// <summary>
        /// "A,R,G,B"
        /// </summary>
        public override string ToString()
        {
            return string.Join(',', A, R, G, B);
        }

        /// <summary>
        /// 0xAARRGGBB
        /// </summary>
        public static Color FromArgb(int argb)
        {
            byte[] bytes = BitConverter.GetBytes(argb);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return new Color(bytes[0], bytes[1], bytes[2], bytes[3]);
        }

        /// <summary>
        /// 0xAARRGGBB
        /// </summary>
        public int ToArgb()
        {
            byte[] bytes = new byte[] { A, R, G, B };

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        #endregion

        #region 常用颜色

        /// <summary>
        /// 透明 (A:0, R:0, G:0, B:0)
        /// </summary>
        /// <returns></returns>
        public static Color Transparent { get; } = new Color(0, 0, 0, 0);

        /// <summary>
        /// 黑 (A:255, R:0, G:0, B:0)
        /// </summary>
        /// <returns></returns>
        public static Color Black { get; } = new Color(255, 0, 0, 0);

        /// <summary>
        /// 银 (A:255, R:192, G:192, B:192)
        /// </summary>
        /// <returns></returns>
        public static Color Silver { get; } = new Color(255, 192, 192, 192);

        /// <summary>
        /// 灰 (A:255, R:128, G:128, B:128)
        /// </summary>
        /// <returns></returns>
        public static Color Gray { get; } = new Color(255, 128, 128, 128);

        /// <summary>
        /// 白 (A:255, R:255, G:255, B:255)
        /// </summary>
        /// <returns></returns>
        public static Color White { get; } = new Color(255, 255, 255, 255);

        /// <summary>
        /// 褐 (A:255, R:128, G:0, B:0)
        /// </summary>
        /// <returns></returns>
        public static Color Maroon { get; } = new Color(255, 128, 0, 0);

        /// <summary>
        /// 红 (A:255, R:255, G:0, B:0)
        /// </summary>
        /// <returns></returns>
        public static Color Red { get; } = new Color(255, 255, 0, 0);

        /// <summary>
        /// 紫 (A:255, R:128, G:0, B:128)
        /// </summary>
        /// <returns></returns>
        public static Color Purple { get; } = new Color(255, 128, 0, 128);

        /// <summary>
        /// 紫红 (A:255, R:255, G:0, B:255)
        /// </summary>
        /// <returns></returns>
        public static Color Fuchsia { get; } = new Color(255, 255, 0, 255);

        /// <summary>
        /// 绿 (A:255, R:0, G:128, B:0)
        /// </summary>
        /// <returns></returns>
        public static Color Green { get; } = new Color(255, 0, 128, 0);

        /// <summary>
        /// 绿黄 (A:255, R:0, G:255, B:0)
        /// </summary>
        /// <returns></returns>
        public static Color Lime { get; } = new Color(255, 0, 255, 0);

        /// <summary>
        /// 橄榄绿 (A:255, R:128, G:128, B:0)
        /// </summary>
        /// <returns></returns>
        public static Color Olive { get; } = new Color(255, 128, 128, 0);

        /// <summary>
        /// 黄 (A:255, R:255, G:255, B:0)
        /// </summary>
        /// <returns></returns>
        public static Color Yellow { get; } = new Color(255, 255, 255, 0);

        /// <summary>
        /// 藏青 (A:255, R:0, G:0, B:128)
        /// </summary>
        /// <returns></returns>
        public static Color Navy { get; } = new Color(255, 0, 0, 128);

        /// <summary>
        /// 蓝 (A:255, R:0, G:0, B:255)
        /// </summary>
        /// <returns></returns>
        public static Color Blue { get; } = new Color(255, 0, 0, 255);

        /// <summary>
        /// 青 (A:255, R:0, G:128, B:128)
        /// </summary>
        /// <returns></returns>
        public static Color Teal { get; } = new Color(255, 0, 128, 128);

        /// <summary>
        /// 水绿 (A:255, R:0, G:255, B:255)
        /// </summary>
        /// <returns></returns>
        public static Color Aqua { get; } = new Color(255, 0, 255, 255);

        #endregion
    }

    #region Json转换器

    public class JsonConverterColor : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Color.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    #endregion

}

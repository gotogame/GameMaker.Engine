namespace GameMaker.Engine
{
    /// <summary>
    /// 大小
    /// </summary>
    [JsonConverter(typeof(JsonConverterSize))]
    public struct Size : IEquatable<Size>
    {
        #region 静态属性

        /// <summary>
        /// 获取一个宽度与高度都为0的大小
        /// </summary>
        public static Size Empty { get; } = new Size(0, 0);

        #endregion

        #region 字段

        /// <summary>
        /// 宽度
        /// (当宽度或者高度小于等于0时为空)
        /// </summary>
        public int Width;

        /// <summary>
        /// 高度
        /// (当宽度或者高度小于等于0时为空)
        /// </summary>
        public int Height;

        #endregion

        #region 属性

        /// <summary>
        /// 判断是否为空
        /// (当宽度或者高度小于等于0时为空)
        /// </summary>
        public bool IsEmpty { get { return Width <= 0 || Height <= 0; } }

        #endregion

        #region 构造函数

        /// <summarheight>
        /// 分别设置宽度与高度
        /// </summarheight>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summarheight>
        /// 同时设置宽度与高度为相同的值
        /// </summarheight>
        /// <param name="value">宽度与高度的值</param>
        public Size(int value)
        {
            Width = value;
            Height = value;
        }

        #endregion

        #region 转换方法

        /// <summary>
        /// "Width,Height"
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool TryParse(string data, out Size size)
        {
            string[] values = data?.Split(',');

            if (values != null && values.Length == 2 &&
                int.TryParse(values[0], out int width) &&
                int.TryParse(values[1], out int height))
            {
                size = new Size(width, height);
                return true;
            }
            else
            {
                size = new Size();
                return false;
            }
        }

        /// <summary>
        /// "Width,Height"
        /// </summary>
        public static Size Parse(string data)
        {
            if (TryParse(data, out Size size))
                return size;
            else
                throw new FormatException();
        }

        /// <summary>
        /// "Width,Height"
        /// </summary>
        public override string ToString()
        {
            return string.Join(',', Width, Height);
        }

        #endregion

        #region 运算符重载

        public static Size operator +(Size a, Size b)
        {
            return new Size(a.Width + b.Width, a.Height + b.Height);
        }

        public static Size operator -(Size a, Size b)
        {
            return new Size(a.Width - b.Width, a.Height - b.Height);
        }

        public static Size operator *(Size a, Size b)
        {
            return new Size(a.Width * b.Width, a.Height * b.Height);
        }

        public static Size operator /(Size a, Size b)
        {
            return new Size(a.Width / b.Width, a.Height / b.Height);
        }

        //----------------------------------------------------------------------------------------------------

        public static Size operator +(Size a, int b)
        {
            return new Size(a.Width + b, a.Height + b);
        }

        public static Size operator -(Size a, int b)
        {
            return new Size(a.Width - b, a.Height - b);
        }

        public static Size operator *(Size a, int b)
        {
            return new Size(a.Width * b, a.Height * b);
        }

        public static Size operator /(Size a, int b)
        {
            return new Size(a.Width / b, a.Height / b);
        }

        #endregion

        #region 相等性比较

        public override bool Equals(object obj)
        {
            return obj is Size size && Equals(size);
        }

        public bool Equals(Size other)
        {
            return Width == other.Width &&
                   Height == other.Height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }

        public static bool operator ==(Size left, Size right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }

        #endregion

    }

    #region Json转换器

    public class JsonConverterSize : JsonConverter<Size>
    {
        public override Size Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Size.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Size value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    #endregion

}

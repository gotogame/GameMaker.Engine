namespace GameMaker.Engine
{
    /// <summary>
    /// 厚度
    /// </summary>
    [JsonConverter(typeof(JsonConverterThickness))]
    public struct Thickness : IEquatable<Thickness>
    {
        #region 字段

        /// <summary>
        /// 上边
        /// </summary>
        public int Top;
        /// <summary>
        /// 下边
        /// </summary>
        public int Bottom;
        /// <summary>
        /// 左边
        /// </summary>
        public int Left;
        /// <summary>
        /// 右边
        /// </summary>
        public int Right;

        #endregion

        #region 构造函数

        /// <summary>
        /// 分别设置四条边
        /// </summary>
        /// <param name="top">上边</param>
        /// <param name="bottom">下边</param>
        /// <param name="left">左边</param>
        /// <param name="right">右边</param>
        public Thickness(int top, int bottom, int left, int right)
        {
            Top = top; Bottom = bottom;
            Left = left; Right = right;
        }

        /// <summary>
        /// 分别设置同方向的两条边
        /// </summary>
        /// <param name="vertical">垂直(上边和下边)</param>
        /// <param name="horizontal">水平(左边和右边)</param>
        public Thickness(int vertical, int horizontal)
        {
            Top = vertical; Bottom = vertical;
            Left = horizontal; Right = horizontal;
        }

        /// <summary>
        /// 同时设置所有边为相同的值
        /// </summary>
        /// <param name="all">所有边</param>
        public Thickness(int all)
        {
            Top = all; Bottom = all;
            Left = all; Right = all;
        }

        #endregion

        #region 转换方法

        /// <summary>
        /// "Top,Bottom,Left,Right"
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool TryParse(string data, out Thickness thickness)
        {
            string[] values = data?.Split(',');

            if (values != null && values.Length == 4 &&
                int.TryParse(values[0], out int top) &&
                int.TryParse(values[1], out int bottom) &&
                int.TryParse(values[2], out int left) &&
                int.TryParse(values[3], out int right))
            {
                thickness = new Thickness(top, bottom, left, right);
                return true;
            }
            else
            {
                thickness = new Thickness();
                return false;
            }
        }

        /// <summary>
        /// "Top,Bottom,Left,Right"
        /// </summary>
        public static Thickness Parse(string data)
        {
            if (TryParse(data, out Thickness thickness))
                return thickness;
            else
                throw new FormatException();
        }

        /// <summary>
        /// "Top,Bottom,Left,Right"
        /// </summary>
        public override string ToString()
        {
            return string.Join(',', Top, Bottom, Left, Right);
        }

        #endregion

        #region 运算符重载

        public static Thickness operator +(Thickness a, Thickness b)
        {
            return new Thickness(a.Top + b.Top, a.Bottom + b.Bottom, a.Left + b.Left, a.Right + b.Right);
        }

        public static Thickness operator -(Thickness a, Thickness b)
        {
            return new Thickness(a.Top - b.Top, a.Bottom - b.Bottom, a.Left - b.Left, a.Right - b.Right);
        }

        #endregion

        #region 相等性比较

        public override bool Equals(object obj)
        {
            return obj is Thickness thickness && Equals(thickness);
        }

        public bool Equals(Thickness other)
        {
            return Top == other.Top &&
                   Bottom == other.Bottom &&
                   Left == other.Left &&
                   Right == other.Right;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Top, Bottom, Left, Right);
        }

        public static bool operator ==(Thickness left, Thickness right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Thickness left, Thickness right)
        {
            return !(left == right);
        }

        #endregion

    }

    #region Json转换器

    public class JsonConverterThickness : JsonConverter<Thickness>
    {
        public override Thickness Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Thickness.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Thickness value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    #endregion

}

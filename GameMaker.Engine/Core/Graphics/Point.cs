namespace GameMaker.Engine
{
    /// <summary>
    /// 点
    /// </summary>
    [JsonConverter(typeof(JsonConverterPoint))]
    public struct Point : IEquatable<Point>
    {
        #region 静态属性

        /// <summary>
        /// 获取一个X与Y都为0的点
        /// </summary>
        public static Point Zero { get; } = new Point(0, 0);

        /// <summary>
        /// 获取一个X与Y都为1的点
        /// </summary>
        public static Point One { get; } = new Point(1, 1);

        #endregion

        #region 字段

        /// <summary>
        /// X坐标
        /// </summary>
        public int X;

        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y;

        #endregion

        #region 构造函数

        /// <summary>
        /// 分别设置X与Y
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 同时设置X与Y为相同的值
        /// </summary>
        /// <param name="value">X与Y的值</param>
        public Point(int value)
        {
            X = value;
            Y = value;
        }

        #endregion

        #region 转换方法

        /// <summary>
        /// "X,Y"
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool TryParse(string data, out Point point)
        {
            string[] values = data?.Split(',');

            if (values != null && values.Length == 2 &&
                int.TryParse(values[0], out int x) &&
                int.TryParse(values[1], out int y))
            {
                point = new Point(x, y);
                return true;
            }
            else
            {
                point = new Point();
                return false;
            }
        }

        /// <summary>
        /// "X,Y"
        /// </summary>
        public static Point Parse(string data)
        {
            if (TryParse(data, out Point point))
                return point;
            else
                throw new FormatException();
        }

        /// <summary>
        /// "X,Y"
        /// </summary>
        public override string ToString()
        {
            return string.Join(',', X, Y);
        }

        #endregion

        #region 运算符重载

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static Point operator *(Point a, Point b)
        {
            return new Point(a.X * b.X, a.Y * b.Y);
        }

        public static Point operator /(Point a, Point b)
        {
            return new Point(a.X / b.X, a.Y / b.Y);
        }

        //----------------------------------------------------------------------------------------------------

        public static Point operator +(Point a, int b)
        {
            return new Point(a.X + b, a.Y + b);
        }

        public static Point operator -(Point a, int b)
        {
            return new Point(a.X - b, a.Y - b);
        }

        public static Point operator *(Point a, int b)
        {
            return new Point(a.X * b, a.Y * b);
        }

        public static Point operator /(Point a, int b)
        {
            return new Point(a.X / b, a.Y / b);
        }

        #endregion

        #region 相等性比较

        public override bool Equals(object obj)
        {
            return obj is Point point && Equals(point);
        }

        public bool Equals(Point other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        #endregion

    }

    #region Json转换器

    public class JsonConverterPoint : JsonConverter<Point>
    {
        public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Point.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    #endregion

}

namespace GameMaker.Engine
{
    /// <summary>
    /// 矩形
    /// </summary>
    [JsonConverter(typeof(JsonConverterRectangle))]
    public struct Rectangle : IEquatable<Rectangle>
    {
        #region 静态属性

        /// <summary>
        /// 获取一个X,Y,宽度,高度都为0的矩形
        /// </summary>
        public static Rectangle Empty { get; } = new Rectangle(0, 0, 0, 0);

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

        /// <summary>
        /// 宽度
        /// (当宽度或者高度小于等于0时为空矩形)
        /// </summary>
        public int Width;

        /// <summary>
        /// 高度
        /// (当宽度或者高度小于等于0时为空矩形)
        /// </summary>
        public int Height;

        #endregion

        #region 属性

        /// <summary>
        /// 判断是否为空矩形
        /// (当宽度或者高度小于等于0时为空矩形)
        /// </summary>
        public bool IsEmpty { get { return Width <= 0 || Height <= 0; } }

        /// <summary>
        /// 获取矩形的左上角坐标(X)
        /// </summary>
        public int Left { get { return X; } }

        /// <summary>
        /// 获取矩形的左上角坐标(Y)
        /// </summary>
        public int Top { get { return Y; } }

        /// <summary>
        /// 获取矩形的右下角坐标(X + Width)
        /// </summary>
        public int Right { get { return X + Width; } }

        /// <summary>
        /// 获取矩形的右下角坐标(Y + Height)
        /// </summary>
        public int Bottom { get { return Y + Height; } }

        /// <summary>
        /// 获取或设置矩形的位置(X, Y)
        /// </summary>
        public Point Location { get { return new Point(X, Y); } set { X = value.X; Y = value.Y; } }

        /// <summary>
        /// 获取或设置矩形的大小(Width, Height)
        /// </summary>
        public Size Size { get { return new Size(Width, Height); } set { Width = value.Width; Height = value.Height; } }

        /// <summary>
        /// 获取矩形的中心点(X + (Width / 2), Y + (Height / 2))
        /// </summary>
        public Point Center { get { return new Point(X + (Width / 2), Y + (Height / 2)); } }

        #endregion

        #region 构造函数

        /// <summary>
        /// 分别设置X,Y,宽度,高度
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 分别设置位置与大小
        /// </summary>
        /// <param name="location">位置(X,Y)</param>
        /// <param name="size">大小(宽,高)</param>
        public Rectangle(Point location, Size size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// 分别设置X,Y,大小
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="size">大小(宽,高)</param>
        public Rectangle(int x, int y, Size size)
        {
            X = x;
            Y = y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// 分别设置位置,宽度,高度
        /// </summary>
        /// <param name="location">位置(X,Y)</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public Rectangle(Point location, int width, int height)
        {
            X = location.X;
            Y = location.Y;
            Width = width;
            Height = height;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="point">点</param>
        /// <returns>指定的点在该矩形内返回true,否则返回false(当该矩形为空矩形时返回false)</returns>
        public bool Contains(Point point)
        {
            return IsEmpty ? false : point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom;
        }

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <returns>指定的矩形在该矩形内返回true,否则返回false(当该矩形或者指定的矩形为空矩形时返回false)</returns>
        public bool Contains(Rectangle rect)
        {
            return IsEmpty || rect.IsEmpty ? false : rect.Left >= Left && rect.Right <= Right && rect.Top >= Top && rect.Bottom <= Bottom;
        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 相交
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <returns>与指定的矩形相交返回true,否则返回false(当该矩形或者指定的矩形为空矩形时返回false)</returns>
        public bool Intersects(Rectangle rect)
        {
            return IsEmpty || rect.IsEmpty ? false : rect.Left <= Right && rect.Right >= Left && rect.Top <= Bottom && rect.Bottom >= Top;
        }

        /// <summary>
        /// 获取两个矩形的重叠区域的矩形
        /// </summary>
        /// <param name="rectA">矩形A</param>
        /// <param name="rectB">矩形B</param>
        /// <returns>如果两个矩形相交,返回重叠区域的矩形,否则返回一个位置与大小都为0的空矩形(当任意一个矩形为空矩形时返回一个位置与大小都为0的空矩形)</returns>
        public static Rectangle Intersect(Rectangle rectA, Rectangle rectB)
        {
            if (!rectA.Intersects(rectB))
                return new Rectangle(0, 0, 0, 0);

            int left = Math.Max(rectA.Left, rectB.Left);
            int top = Math.Max(rectA.Top, rectB.Top);
            int right = Math.Min(rectA.Right, rectB.Right);
            int bottom = Math.Min(rectA.Bottom, rectB.Bottom);
            return new Rectangle(left, top, right - left, bottom - top);
        }

        #endregion

        #region 转换方法

        /// <summary>
        /// "X,Y,Width,Height"
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool TryParse(string data, out Rectangle rectangle)
        {
            string[] values = data?.Split(',');

            if (values != null && values.Length == 4 &&
                int.TryParse(values[0], out int x) &&
                int.TryParse(values[1], out int y) &&
                int.TryParse(values[2], out int width) &&
                int.TryParse(values[3], out int height))
            {
                rectangle = new Rectangle(x, y, width, height);
                return true;
            }
            else
            {
                rectangle = new Rectangle();
                return false;
            }
        }

        /// <summary>
        /// "X,Y,Width,Height"
        /// </summary>
        public static Rectangle Parse(string data)
        {
            if (TryParse(data, out Rectangle rectangle))
                return rectangle;
            else
                throw new FormatException();
        }

        /// <summary>
        /// "X,Y,Width,Height"
        /// </summary>
        public override string ToString()
        {
            return string.Join(',', X, Y, Width, Height);
        }

        internal SDL2.SDL.SDL_Rect ToSDLRectangle()
        {
            return new SDL2.SDL.SDL_Rect { x = X, y = Y, w = Width, h = Height };
        }

        #endregion

        #region 运算符重载

        /// <summary>
        /// 扩大
        /// </summary>
        public static Rectangle operator +(Rectangle a, Thickness b)
        {
            return new Rectangle(a.X - b.Left, a.Y - b.Top, a.Width + b.Left + b.Right, a.Height + b.Top + b.Bottom);
        }

        /// <summary>
        /// 缩小
        /// </summary>
        public static Rectangle operator -(Rectangle a, Thickness b)
        {
            return new Rectangle(a.X + b.Left, a.Y + b.Top, a.Width - b.Left - b.Right, a.Height - b.Top - b.Bottom);
        }

        #endregion

        #region 相等性比较

        public override bool Equals(object obj)
        {
            return obj is Rectangle rectangle && Equals(rectangle);
        }

        public bool Equals(Rectangle other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Width == other.Width &&
                   Height == other.Height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        #endregion

    }

    #region Json转换器

    public class JsonConverterRectangle : JsonConverter<Rectangle>
    {
        public override Rectangle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Rectangle.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Rectangle value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    #endregion

}

namespace GameMaker.Engine
{
    public static partial class GameRenderer
    {
        #region 绘制字符串

        /// <summary>
        /// 绘制字符串
        /// (当字体等于null或者字体大小小于等于0时无操作)
        /// (当字符串为null或空字符时无操作)
        /// (当最大宽度大于0时自动换行)
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="str">字符串</param>
        /// <param name="position">目标位置</param>
        /// <param name="color">颜色</param>
        /// <param name="maxWidth">最大宽度</param>
        public static void DrawString(Font font, int fontSize, string str, Point position, Color color, int maxWidth)
        {
            if (font == null || fontSize <= 0 || string.IsNullOrEmpty(str))
                return;

            int lineHeight = font.GetLineHeight(fontSize);

            Point cursor = position;    //光标位置
            foreach (char chr in str)
            {
                //处理控制字符
                if (chr == '\r')
                {
                    cursor.X = position.X;
                }
                else if (chr == '\n')
                {
                    cursor.X = position.X;
                    cursor.Y += lineHeight;
                }

                if (char.IsControl(chr))
                    continue;

                //获取字符信息
                CharSetTexture charSetTexture = font.GetCharSetTexture(fontSize, chr);
                Rectangle charBounds = charSetTexture != null ? charSetTexture.CharBoundsDictionary[chr] : new Rectangle(0, 0, fontSize, fontSize);

                //自动换行
                if (maxWidth > 0 && cursor.X + charBounds.Width > position.X + maxWidth)
                {
                    cursor.X = position.X;
                    cursor.Y += lineHeight;
                }

                //绘制字符
                if (charSetTexture != null)
                    DrawTexture(charSetTexture, charBounds, new Rectangle(cursor, charBounds.Size), color);
                else
                    DrawBorder(new Rectangle(cursor, charBounds.Size), color);

                //移动光标
                cursor.X += charBounds.Width;
            }
        }

        #endregion

        #region 测量字符串

        /// <summary>
        /// 测量字符串包围盒大小
        /// (当字体等于null或者字体大小小于等于0时无操作)
        /// (当字符串为null或空字符时无操作)
        /// (当最大宽度大于0时自动换行)
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="str">字符串</param>
        /// <param name="maxWidth">最大宽度</param>
        public static Size MeasureString(Font font, int fontSize, string str, int maxWidth)
        {
            if (font == null || fontSize <= 0 || string.IsNullOrEmpty(str))
                return new Size(0, 0);

            Size size = new Size(0, 0);

            int lineHeight = font.GetLineHeight(fontSize);

            Point position = new Point(0, 0);
            Point cursor = position;    //光标位置
            foreach (char chr in str)
            {
                //处理控制字符
                if (chr == '\r')
                {
                    cursor.X = position.X;
                }
                else if (chr == '\n')
                {
                    cursor.X = position.X;
                    cursor.Y += lineHeight;
                }

                if (char.IsControl(chr))
                    continue;

                //获取字符信息
                CharSetTexture charSetTexture = font.GetCharSetTexture(fontSize, chr);
                Rectangle charBounds = charSetTexture != null ? charSetTexture.CharBoundsDictionary[chr] : new Rectangle(0, 0, fontSize, fontSize);

                //自动换行
                if (maxWidth > 0 && cursor.X + charBounds.Width > position.X + maxWidth)
                {
                    cursor.X = position.X;
                    cursor.Y += lineHeight;
                }

                //移动光标
                cursor.X += charBounds.Width;

                if (cursor.X > size.Width)
                    size.Width = cursor.X;
            }

            size.Height = cursor.Y + lineHeight;
            return size;
        }

        #endregion

    }
}

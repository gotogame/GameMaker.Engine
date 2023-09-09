namespace GameMaker.Engine
{
    public static partial class MapGraphics
    {
        #region 绘制精灵

        /// <summary>
        /// 绘制精灵
        /// (当精灵等于null或者精灵子图像索引越界时无操作)
        /// </summary>
        /// <param name="spriteName">精灵名称</param>
        /// <param name="index">精灵子图像索引</param>
        /// <param name="srcRect">源矩形,相对于精灵子图像包围盒</param>
        /// <param name="dstRect">目标矩形</param>
        /// <param name="color">颜色</param>
        /// <param name="angle">旋转角度,以度为单位顺时针旋转</param>
        /// <param name="center">旋转中心点,相对于目标矩形,目标矩形围绕此点旋转</param>
        /// <param name="flipHorizontally">水平翻转</param>
        /// <param name="flipVertically">垂直翻转</param>
        public static void DrawSprite(string spriteName, int index, Rectangle srcRect, Rectangle dstRect, Color color, double angle, Point center, bool flipHorizontally, bool flipVertically)
        {
            Sprite sprite = Assets.SpriteAsset.Load(spriteName);
            if (sprite == null || sprite.Subimages == null || index < 0 || index >= sprite.Subimages.Length)
                return;

            srcRect.Location += sprite.Subimages[index].Bounds.Location;
            DrawTexture(sprite.TextureName, Rectangle.Intersect(sprite.Subimages[index].Bounds, srcRect), dstRect, color, angle, center, flipHorizontally, flipVertically);
        }

        /// <summary>
        /// 绘制精灵
        /// (当精灵等于null或者精灵子图像索引越界时无操作)
        /// </summary>
        /// <param name="spriteName">精灵名称</param>
        /// <param name="index">精灵子图像索引</param>
        /// <param name="srcRect">源矩形,相对于精灵子图像包围盒</param>
        /// <param name="dstRect">目标矩形</param>
        /// <param name="color">颜色</param>
        public static void DrawSprite(string spriteName, int index, Rectangle srcRect, Rectangle dstRect, Color color)
        {
            Sprite sprite = Assets.SpriteAsset.Load(spriteName);
            if (sprite == null || sprite.Subimages == null || index < 0 || index >= sprite.Subimages.Length)
                return;

            srcRect.Location += sprite.Subimages[index].Bounds.Location;
            DrawTexture(sprite.TextureName, Rectangle.Intersect(sprite.Subimages[index].Bounds, srcRect), dstRect, color);
        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 绘制精灵
        /// (当精灵等于null或者精灵子图像索引越界时无操作)
        /// </summary>
        /// <param name="spriteName">精灵名称</param>
        /// <param name="index">精灵子图像索引</param>
        /// <param name="dstRect">目标矩形</param>
        /// <param name="color">颜色</param>
        /// <param name="angle">旋转角度,以度为单位顺时针旋转</param>
        /// <param name="center">旋转中心点,相对于目标矩形,目标矩形围绕此点旋转</param>
        /// <param name="flipHorizontally">水平翻转</param>
        /// <param name="flipVertically">垂直翻转</param>
        public static void DrawSprite(string spriteName, int index, Rectangle dstRect, Color color, double angle, Point center, bool flipHorizontally, bool flipVertically)
        {
            Sprite sprite = Assets.SpriteAsset.Load(spriteName);
            if (sprite == null || sprite.Subimages == null || index < 0 || index >= sprite.Subimages.Length)
                return;

            DrawTexture(sprite.TextureName, sprite.Subimages[index].Bounds, dstRect, color, angle, center, flipHorizontally, flipVertically);
        }

        /// <summary>
        /// 绘制精灵
        /// (当精灵等于null或者精灵子图像索引越界时无操作)
        /// </summary>
        /// <param name="spriteName">精灵名称</param>
        /// <param name="index">精灵子图像索引</param>
        /// <param name="dstRect">目标矩形</param>
        /// <param name="color">颜色</param>
        public static void DrawSprite(string spriteName, int index, Rectangle dstRect, Color color)
        {
            Sprite sprite = Assets.SpriteAsset.Load(spriteName);
            if (sprite == null || sprite.Subimages == null || index < 0 || index >= sprite.Subimages.Length)
                return;

            DrawTexture(sprite.TextureName, sprite.Subimages[index].Bounds, dstRect, color);
        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 绘制精灵
        /// (当精灵等于null或者精灵子图像索引越界时无操作)
        /// </summary>
        /// <param name="spriteName">精灵名称</param>
        /// <param name="index">精灵子图像索引</param>
        /// <param name="position">目标位置</param>
        /// <param name="color">颜色</param>
        /// <param name="angle">以原点为中心点进行旋转的角度,以度为单位顺时针旋转</param>
        /// <param name="scaleX">缩放系数X</param>
        /// <param name="scaleY">缩放系数Y</param>
        /// <param name="flipHorizontally">水平翻转</param>
        /// <param name="flipVertically">垂直翻转</param>
        public static void DrawSprite(string spriteName, int index, Point position, Color color, double angle, double scaleX, double scaleY, bool flipHorizontally, bool flipVertically)
        {
            Sprite sprite = Assets.SpriteAsset.Load(spriteName);
            if (sprite == null || sprite.Subimages == null || index < 0 || index >= sprite.Subimages.Length)
                return;

            Size dstRectSize = new Size(Convert.ToInt32(sprite.Subimages[index].Bounds.Width * scaleX), Convert.ToInt32(sprite.Subimages[index].Bounds.Height * scaleY));
            Point dstRectOrigin = new Point(Convert.ToInt32(sprite.Subimages[index].Origin.X * scaleX), Convert.ToInt32(sprite.Subimages[index].Origin.Y * scaleY));
            Rectangle dstRect = new Rectangle(position - dstRectOrigin, dstRectSize);

            DrawTexture(sprite.TextureName, sprite.Subimages[index].Bounds, dstRect, color, angle, dstRectOrigin, flipHorizontally, flipVertically);
        }

        /// <summary>
        /// 绘制精灵
        /// (当精灵等于null或者精灵子图像索引越界时无操作)
        /// </summary>
        /// <param name="spriteName">精灵名称</param>
        /// <param name="index">精灵子图像索引</param>
        /// <param name="position">目标位置</param>
        /// <param name="color">颜色</param>
        public static void DrawSprite(string spriteName, int index, Point position, Color color)
        {
            Sprite sprite = Assets.SpriteAsset.Load(spriteName);
            if (sprite == null || sprite.Subimages == null || index < 0 || index >= sprite.Subimages.Length)
                return;

            Rectangle dstRect = new Rectangle(position - sprite.Subimages[index].Origin, sprite.Subimages[index].Bounds.Size);
            DrawTexture(sprite.TextureName, sprite.Subimages[index].Bounds, dstRect, color);
        }

        #endregion

        #region 获取精灵

        /// <summary>
        /// 获取精灵
        /// </summary>
        /// <param name="spriteName">精灵名称</param>
        /// <returns>精灵</returns>
        public static Sprite GetSprite(string spriteName)
        {
            return Assets.SpriteAsset.Load(spriteName);
        }

        /// <summary>
        /// 获取精灵子图像数量
        /// (当精灵等于null或者精灵子图像数组等于null时返回0)
        /// </summary>
        /// <param name="spriteName">精灵名称</param>
        /// <returns>子图像数量</returns>
        public static int GetSpriteSubimageCount(string spriteName)
        {
            Sprite sprite = Assets.SpriteAsset.Load(spriteName);
            return (sprite == null || sprite.Subimages == null) ? 0 : sprite.Subimages.Length;
        }

        #endregion

    }
}

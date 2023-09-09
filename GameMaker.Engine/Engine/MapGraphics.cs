namespace GameMaker.Engine
{
    public static partial class MapGraphics
    {
        #region 绘制纹理

        /// <summary>
        /// 绘制纹理
        /// (当传入的纹理等于null时无操作)
        /// (目标矩形自动转换为相对于视野的坐标)
        /// </summary>
        /// <param name="textureName">纹理名称</param>
        /// <param name="srcRect">源矩形</param>
        /// <param name="dstRect">目标矩形</param>
        /// <param name="color">颜色</param>
        /// <param name="angle">旋转角度,以度为单位顺时针旋转</param>
        /// <param name="center">旋转中心点,相对于目标矩形,目标矩形围绕此点旋转</param>
        /// <param name="flipHorizontally">水平翻转</param>
        /// <param name="flipVertically">垂直翻转</param>
        public static void DrawTexture(string textureName, Rectangle srcRect, Rectangle dstRect, Color color, double angle, Point center, bool flipHorizontally, bool flipVertically)
        {
            Texture texture = Assets.TextureAsset.Load(textureName);
            if (texture == null)
                return;

            dstRect.Location = Engine.CurrentMap.MapToViewPoint(dstRect.Location);
            GameRenderer.DrawTexture(texture, srcRect, dstRect, color, angle, center, flipHorizontally, flipVertically);
        }

        /// <summary>
        /// 绘制纹理
        /// (当传入的纹理等于null时无操作)
        /// (目标矩形自动转换为相对于视野的坐标)
        /// </summary>
        /// <param name="textureName">纹理名称</param>
        /// <param name="srcRect">源矩形</param>
        /// <param name="dstRect">目标矩形</param>
        /// <param name="color">颜色</param>
        public static void DrawTexture(string textureName, Rectangle srcRect, Rectangle dstRect, Color color)
        {
            Texture texture = Assets.TextureAsset.Load(textureName);
            if (texture == null)
                return;

            dstRect.Location = Engine.CurrentMap.MapToViewPoint(dstRect.Location);
            GameRenderer.DrawTexture(texture, srcRect, dstRect, color);
        }

        #endregion

    }
}

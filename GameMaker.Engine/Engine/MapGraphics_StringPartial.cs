namespace GameMaker.Engine
{
    public static partial class MapGraphics
    {
        #region 绘制字符串

        /// <summary>
        /// 绘制字符串
        /// (当字体等于null或者字体大小小于等于0时无操作)
        /// (当字符串为null或空字符时无操作)
        /// (当最大宽度大于0时自动换行)
        /// </summary>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="str">字符串</param>
        /// <param name="position">目标位置</param>
        /// <param name="color">颜色</param>
        /// <param name="maxWidth">最大宽度</param>
        public static void DrawString(string fontName, int fontSize, string str, Point position, Color color, int maxWidth)
        {
            GameRenderer.DrawString(Assets.FontAsset.Load(fontName), fontSize, str, Engine.CurrentMap.MapToViewPoint(position), color, maxWidth);
        }

        #endregion

        #region 测量字符串

        /// <summary>
        /// 测量字符串包围盒大小
        /// (当字体等于null或者字体大小小于等于0时无操作)
        /// (当字符串为null或空字符时无操作)
        /// (当最大宽度大于0时自动换行)
        /// </summary>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="str">字符串</param>
        /// <param name="maxWidth">最大宽度</param>
        public static Size MeasureString(string fontName, int fontSize, string str, int maxWidth)
        {
            return GameRenderer.MeasureString(Assets.FontAsset.Load(fontName), fontSize, str, maxWidth);
        }

        #endregion

    }
}

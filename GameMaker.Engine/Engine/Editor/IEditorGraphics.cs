namespace GameMaker.Engine
{
    /// <summary>
    /// 编辑器图形接口
    /// </summary>
    public interface IEditorGraphics
    {
        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="color">颜色</param>
        void DrawPoint(Point point, Color color);

        /// <summary>
        /// 绘制线
        /// </summary>
        /// <param name="startPoint">起点</param>
        /// <param name="endPoint">终点</param>
        /// <param name="color">颜色</param>
        void DrawLine(Point startPoint, Point endPoint, Color color);

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="rectangle">目标矩形</param>
        /// <param name="color">颜色</param>
        void DrawRectangle(Rectangle rectangle, Color color);

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="rectangle">目标矩形,边框围绕在此矩形内部</param>
        /// <param name="color">颜色</param>
        void DrawBorder(Rectangle rectangle, Color color);

        /// <summary>
        /// 绘制纹理
        /// (当纹理等于null时无操作)
        /// </summary>
        /// <param name="textureName">纹理名称</param>
        /// <param name="srcRect">源矩形</param>
        /// <param name="dstRect">目标矩形</param>
        /// <param name="color">颜色</param>
        void DrawTexture(string textureName, Rectangle srcRect, Rectangle dstRect, Color color);

        /// <summary>
        /// 绘制纹理
        /// (当纹理等于null时无操作)
        /// </summary>
        /// <param name="textureName">纹理名称</param>
        /// <param name="position">目标位置</param>
        /// <param name="color">颜色</param>
        void DrawTexture(string textureName, Point position, Color color);

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 绘制精灵
        /// (当精灵等于null或者精灵子图像索引越界时无操作)
        /// </summary>
        /// <param name="spriteName">精灵名称</param>
        /// <param name="index">精灵子图像索引</param>
        /// <param name="position">目标位置</param>
        /// <param name="color">颜色</param>
        void DrawSprite(string spriteName, int index, Point position, Color color);

        //----------------------------------------------------------------------------------------------------

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
        void DrawString(string fontName, int fontSize, string str, Point position, Color color, int maxWidth);

    }
}

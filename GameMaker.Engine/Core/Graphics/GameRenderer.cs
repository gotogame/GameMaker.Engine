namespace GameMaker.Engine
{
    /// <summary>
    /// 游戏渲染器
    /// </summary>
    public static partial class GameRenderer
    {
        #region 属性

        /// <summary>
        /// SDL渲染器
        /// </summary>
        internal static IntPtr SDLRenderer { get; set; } = IntPtr.Zero;

        /// <summary>
        /// 逻辑大小
        /// </summary>
        public static Size LogicalSize
        {
            get
            {
                SDL.SDL_RenderGetLogicalSize(SDLRenderer, out int w, out int h);
                return new Size(w, h);
            }
            set
            {
                SDL.SDL_RenderSetLogicalSize(SDLRenderer, value.Width, value.Height);
            }
        }

        /// <summary>
        /// 剪切矩形
        /// (当矩形等于空时表示禁用剪切功能)
        /// </summary>
        public static Rectangle ClipRect
        {
            get
            {
                if (SDL.SDL_RenderIsClipEnabled(SDLRenderer) == SDL.SDL_bool.SDL_TRUE)
                {
                    SDL.SDL_RenderGetClipRect(SDLRenderer, out SDL.SDL_Rect sdlRect);
                    return new Rectangle(sdlRect.x, sdlRect.y, sdlRect.w, sdlRect.h);
                }
                else
                {
                    return new Rectangle(0, 0, 0, 0);
                }
            }
            set
            {
                if (value.IsEmpty)
                {
                    SDL.SDL_RenderSetClipRect(SDLRenderer, IntPtr.Zero);
                }
                else
                {
                    SDL.SDL_Rect sdlRect = value.ToSDLRectangle();
                    SDL.SDL_RenderSetClipRect(SDLRenderer, ref sdlRect);
                }
            }
        }

        /// <summary>
        /// 视图接口
        /// </summary>
        public static Rectangle Viewport
        {
            get
            {
                SDL.SDL_RenderGetViewport(SDLRenderer, out SDL.SDL_Rect rect);
                return new Rectangle(rect.x, rect.y, rect.w, rect.h);
            }
            set
            {
                SDL.SDL_Rect sdlRect = value.ToSDLRectangle();
                SDL.SDL_RenderSetViewport(SDLRenderer, ref sdlRect);
            }
        }

        /// <summary>
        /// 缩放比例
        /// </summary>
        public static (float X, float Y) Scale
        {
            get
            {
                SDL.SDL_RenderGetScale(SDLRenderer, out float x, out float y);
                return (x, y);
            }
            set
            {
                (float x, float y) = value;
                SDL.SDL_RenderSetScale(SDLRenderer, x, y);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 使用指定的颜色清除绘制目标
        /// </summary>
        /// <param name="color">颜色</param>
        public static void Clear(Color color)
        {
            SDL.SDL_SetRenderDrawColor(SDLRenderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderClear(SDLRenderer);
        }

        /// <summary>
        /// 将后备缓冲区翻转至游戏窗口
        /// </summary>
        public static void Present()
        {
            SDL.SDL_RenderPresent(SDLRenderer);
        }

        /// <summary>
        /// 读取后备缓冲区像素
        /// </summary>
        /// <param name="size">后备缓冲区大小</param>
        /// <returns>获取成功返回Color[size.Height, size.Width],获取失败返回null</returns>
        public static Color[,] ReadPixels(out Size size)
        {
            size = new Size(0, 0);
            IntPtr pixels = IntPtr.Zero;

            try
            {
                if (SDL.SDL_GetRendererOutputSize(SDLRenderer, out int width, out int height) == 0)
                {
                    Rectangle rect = new Rectangle(0, 0, width, height);
                    SDL.SDL_Rect sdlRect = rect.ToSDLRectangle();

                    int pitch = rect.Width * 4;
                    pixels = Marshal.AllocHGlobal(rect.Height * pitch);

                    if (SDL.SDL_RenderReadPixels(SDLRenderer, ref sdlRect, SDLTexturePixelFormat, pixels, pitch) == 0)
                    {
                        Color[,] colors = new Color[rect.Height, rect.Width];

                        for (int y = rect.Top; y < rect.Bottom; y++)
                        {
                            for (int x = rect.Left; x < rect.Right; x++)
                            {
                                int i = x * 4 + y * pitch;
                                colors[y - rect.Top, x - rect.Left] = Color.FromArgb(Marshal.ReadInt32(pixels, i));
                            }
                        }

                        size = rect.Size;
                        return colors;
                    }
                }

                return null;
            }
            catch { return null; }
            finally
            {
                if (pixels != IntPtr.Zero)
                    Marshal.FreeHGlobal(pixels);
            }
        }

        #endregion

        #region 绘制点

        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="color">颜色</param>
        public static void DrawPoint(Point point, Color color)
        {
            SDL.SDL_SetRenderDrawColor(SDLRenderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderDrawPoint(SDLRenderer, point.X, point.Y);
        }

        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="points">点</param>
        /// <param name="color">颜色</param>
        public static void DrawPoints(Point[] points, Color color)
        {
            if (points == null || points.Length <= 0)
                return;

            SDL.SDL_Point[] sdlPoints = new SDL.SDL_Point[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                sdlPoints[i].x = points[i].X;
                sdlPoints[i].y = points[i].Y;
            }

            SDL.SDL_SetRenderDrawColor(SDLRenderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderDrawPoints(SDLRenderer, sdlPoints, sdlPoints.Length);
        }

        #endregion

        #region 绘制线

        /// <summary>
        /// 绘制线
        /// </summary>
        /// <param name="startPoint">起点</param>
        /// <param name="endPoint">终点</param>
        /// <param name="color">颜色</param>
        public static void DrawLine(Point startPoint, Point endPoint, Color color)
        {
            SDL.SDL_SetRenderDrawColor(SDLRenderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderDrawLine(SDLRenderer, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        }

        /// <summary>
        /// 绘制线
        /// </summary>
        /// <param name="points">点</param>
        /// <param name="color">颜色</param>
        public static void DrawLines(Point[] points, Color color)
        {
            if (points == null || points.Length <= 0)
                return;

            SDL.SDL_Point[] sdlPoints = new SDL.SDL_Point[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                sdlPoints[i].x = points[i].X;
                sdlPoints[i].y = points[i].Y;
            }

            SDL.SDL_SetRenderDrawColor(SDLRenderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderDrawLines(SDLRenderer, sdlPoints, sdlPoints.Length);
        }

        #endregion

        #region 绘制矩形

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="rectangle">目标矩形</param>
        /// <param name="color">颜色</param>
        public static void DrawRectangle(Rectangle rectangle, Color color)
        {
            SDL.SDL_Rect sdlRect = rectangle.ToSDLRectangle();

            SDL.SDL_SetRenderDrawColor(SDLRenderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderFillRect(SDLRenderer, ref sdlRect);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="rectangles">目标矩形</param>
        /// <param name="color">颜色</param>
        public static void DrawRectangles(Rectangle[] rectangles, Color color)
        {
            if (rectangles == null || rectangles.Length <= 0)
                return;

            SDL.SDL_Rect[] sdlRects = new SDL.SDL_Rect[rectangles.Length];
            for (int i = 0; i < rectangles.Length; i++)
            {
                sdlRects[i] = rectangles[i].ToSDLRectangle();
            }

            SDL.SDL_SetRenderDrawColor(SDLRenderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderFillRects(SDLRenderer, sdlRects, sdlRects.Length);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="rectangle">目标矩形,边框围绕在此矩形内部</param>
        /// <param name="color">颜色</param>
        public static void DrawBorder(Rectangle rectangle, Color color)
        {
            SDL.SDL_Rect sdlRect = rectangle.ToSDLRectangle();

            SDL.SDL_SetRenderDrawColor(SDLRenderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderDrawRect(SDLRenderer, ref sdlRect);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="rectangles">目标矩形</param>
        /// <param name="color">颜色</param>
        public static void DrawBorders(Rectangle[] rectangles, Color color)
        {
            if (rectangles == null || rectangles.Length <= 0)
                return;

            SDL.SDL_Rect[] sdlRects = new SDL.SDL_Rect[rectangles.Length];
            for (int i = 0; i < rectangles.Length; i++)
            {
                sdlRects[i] = rectangles[i].ToSDLRectangle();
            }

            SDL.SDL_SetRenderDrawColor(SDLRenderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderDrawRects(SDLRenderer, sdlRects, sdlRects.Length);
        }

        #endregion

        #region 绘制纹理

        /// <summary>
        /// 绘制纹理
        /// (当传入的纹理等于null时无操作)
        /// </summary>
        /// <param name="texture">要绘制的纹理</param>
        /// <param name="srcRect">源矩形</param>
        /// <param name="dstRect">目标矩形</param>
        /// <param name="color">颜色</param>
        /// <param name="angle">旋转角度,以度为单位顺时针旋转</param>
        /// <param name="center">旋转中心点,相对于目标矩形,目标矩形围绕此点旋转</param>
        /// <param name="flipHorizontally">水平翻转</param>
        /// <param name="flipVertically">垂直翻转</param>
        public static void DrawTexture(Texture texture, Rectangle srcRect, Rectangle dstRect, Color color, double angle, Point center, bool flipHorizontally, bool flipVertically)
        {
            if (texture == null || texture.SDLTexture == IntPtr.Zero)
                return;

            SDL.SDL_Rect sdlSrcRect = srcRect.ToSDLRectangle();
            SDL.SDL_Rect sdlDstRect = dstRect.ToSDLRectangle();
            SDL.SDL_Point sdlCenter = new SDL.SDL_Point() { x = center.X, y = center.Y };
            SDL.SDL_RendererFlip sdlFlip = SDL.SDL_RendererFlip.SDL_FLIP_NONE | (flipHorizontally ? SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL : 0) | (flipVertically ? SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL : 0);

            SDL.SDL_SetTextureColorMod(texture.SDLTexture, color.R, color.G, color.B);
            SDL.SDL_SetTextureAlphaMod(texture.SDLTexture, color.A);

            SDL.SDL_RenderCopyEx(SDLRenderer, texture.SDLTexture, ref sdlSrcRect, ref sdlDstRect, angle, ref sdlCenter, sdlFlip);
        }

        /// <summary>
        /// 绘制纹理
        /// (当传入的纹理等于null时无操作)
        /// </summary>
        /// <param name="texture">要绘制的纹理</param>
        /// <param name="srcRect">源矩形</param>
        /// <param name="dstRect">目标矩形</param>
        /// <param name="color">颜色</param>
        public static void DrawTexture(Texture texture, Rectangle srcRect, Rectangle dstRect, Color color)
        {
            if (texture == null || texture.SDLTexture == IntPtr.Zero)
                return;

            SDL.SDL_Rect sdlSrcRect = srcRect.ToSDLRectangle();
            SDL.SDL_Rect sdlDstRect = dstRect.ToSDLRectangle();

            SDL.SDL_SetTextureColorMod(texture.SDLTexture, color.R, color.G, color.B);
            SDL.SDL_SetTextureAlphaMod(texture.SDLTexture, color.A);

            SDL.SDL_RenderCopy(SDLRenderer, texture.SDLTexture, ref sdlSrcRect, ref sdlDstRect);
        }

        #endregion

    }
}

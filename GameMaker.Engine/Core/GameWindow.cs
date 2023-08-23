namespace GameMaker.Engine
{
    /// <summary>
    /// 游戏窗口
    /// </summary>
    public static class GameWindow
    {
        #region 属性

        /// <summary>
        /// SDL窗口
        /// </summary>
        internal static IntPtr SDLWindow { get; set; } = IntPtr.Zero;

        /// <summary>
        /// 游戏窗口标题
        /// </summary>
        public static string Title
        {
            get { return SDL.SDL_GetWindowTitle(SDLWindow); }
            set { SDL.SDL_SetWindowTitle(SDLWindow, value ?? ""); }
        }

        /// <summary>
        /// 是否显示游戏窗口标题栏
        /// </summary>
        public static bool IsShowTitleBar
        {
            get { return !((SDL.SDL_GetWindowFlags(SDLWindow) & (uint)SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS) == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS); }
            set { SDL.SDL_SetWindowBordered(SDLWindow, value ? SDL.SDL_bool.SDL_TRUE : SDL.SDL_bool.SDL_FALSE); }
        }

        /// <summary>
        /// 当前桌面大小
        /// </summary>
        public static Size DesktopSize
        {
            get
            {
                SDL.SDL_GetDisplayBounds(0, out SDL.SDL_Rect sdlRect);
                return new Size(sdlRect.w, sdlRect.h);
            }
        }

        /// <summary>
        /// 游戏窗口在桌面上的位置
        /// </summary>
        public static Point Location
        {
            get
            {
                SDL.SDL_GetWindowPosition(SDLWindow, out int x, out int y);
                return new Point(x, y);
            }
            set { SDL.SDL_SetWindowPosition(SDLWindow, value.X, value.Y); }
        }

        /// <summary>
        /// 游戏窗口大小
        /// </summary>
        public static Size Size
        {
            get
            {
                SDL.SDL_GetWindowSize(SDLWindow, out int w, out int h);
                return new Size(w, h);
            }
            set
            {
                SDL.SDL_SetWindowSize(SDLWindow, value.Width, value.Height);
            }
        }

        /// <summary>
        /// 游戏窗口是否全屏
        /// </summary>
        public static bool IsFullscreen
        {
            get { return (SDL.SDL_GetWindowFlags(SDLWindow) & (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN) == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN; }
            set { SDL.SDL_SetWindowFullscreen(SDLWindow, value ? (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN : 0); }
        }

        /// <summary>
        /// 是否显示鼠标指针
        /// </summary>
        public static bool IsShowCursor
        {
            get { return SDL.SDL_ShowCursor(SDL.SDL_QUERY) == SDL.SDL_ENABLE; }
            set { SDL.SDL_ShowCursor(value ? SDL.SDL_ENABLE : SDL.SDL_DISABLE); }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取当前显示器支持的游戏窗口大小
        /// </summary>
        /// <returns></returns>
        public static Size[] GetSizes()
        {
            List<Size> sizes = new List<Size>();

            int displayIndex = 0;
            int displayModeCount = SDL.SDL_GetNumDisplayModes(displayIndex);

            for (int i = 0; i < displayModeCount; i++)
            {
                if (SDL.SDL_GetDisplayMode(displayIndex, i, out SDL.SDL_DisplayMode mode) == 0)
                {
                    Size size = new Size(mode.w, mode.h);
                    if (!sizes.Contains(size))
                        sizes.Add(size);
                }
            }

            return sizes.ToArray();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 关闭事件
        /// (游戏窗口的关闭按钮被点击时引发一次)
        /// </summary>
        public static Action Closing;

        #endregion

    }
}

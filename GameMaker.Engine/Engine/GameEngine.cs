namespace GameMaker.Engine
{
    /// <summary>
    /// 游戏引擎
    /// </summary>
    public static partial class GameEngine
    {
        #region 方法

        /// <summary>
        /// 获取自初始化以来经过的毫秒数
        /// </summary>
        /// <returns></returns>
        public static ulong GetTicks()
        {
            return SDL.SDL_GetTicks64();
        }

        /// <summary>
        /// 延时指定的毫秒数
        /// </summary>
        /// <param name="ms">毫秒</param>
        public static void Delay(uint ms)
        {
            SDL.SDL_Delay(ms);
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            //初始化SDL
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0)
            {
                throw new InitException("SDL初始化失败: " + SDL.SDL_GetError());
            }

            //初始化SDL_image库
            var flagsSDL_image = SDL_image.IMG_InitFlags.IMG_INIT_PNG;
            int inittedSDL_image = SDL_image.IMG_Init(flagsSDL_image);
            if ((inittedSDL_image & (int)flagsSDL_image) != (int)flagsSDL_image)
            {
                throw new InitException("SDL_image库初始化失败");
            }

            //初始化SDL_ttf库
            if (SDL_ttf.TTF_Init() != 0)
            {
                throw new InitException("SDL_ttf库初始化失败");
            }

            //初始化SDL_mixer库
            var flagsSDL_mixer = SDL_mixer.MIX_InitFlags.MIX_INIT_OGG;
            int inittedSDL_mixer = SDL_mixer.Mix_Init(flagsSDL_mixer);
            if ((inittedSDL_mixer & (int)flagsSDL_mixer) != (int)flagsSDL_mixer)
            {
                throw new InitException("SDL_mixer库初始化失败");
            }

        }

        #endregion

        #region 显示

        /// <summary>
        /// 创建窗口与渲染器
        /// (仅支持一个窗口与渲染器)
        /// </summary>
        /// <param name="handle">窗口句柄(未指定窗口句柄时将创建新窗口)</param>
        public static void CreateWindowAndRenderer(IntPtr handle = default)
        {
            //创建窗口
            if (handle == IntPtr.Zero)
                GameWindow.SDLWindow = SDL.SDL_CreateWindow("Game", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 1280, 720, 0);
            else
                GameWindow.SDLWindow = SDL.SDL_CreateWindowFrom(handle);

            if (GameWindow.SDLWindow == IntPtr.Zero)
            {
                throw new InitException("SDL创建窗口失败: " + SDL.SDL_GetError());
            }

            //创建渲染器
            GameRenderer.SDLRenderer = SDL.SDL_CreateRenderer(GameWindow.SDLWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (GameRenderer.SDLRenderer == IntPtr.Zero)
            {
                throw new InitException("SDL创建渲染器失败: " + SDL.SDL_GetError());
            }

            //设置渲染器绘制模式
            if (SDL.SDL_SetRenderDrawBlendMode(GameRenderer.SDLRenderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND) != 0)
            {
                throw new InitException("设置渲染器绘制模式失败");
            }
        }

        /// <summary>
        /// 销毁窗口与渲染器
        /// </summary>
        public static void DestroyWindowAndRenderer()
        {
            //销毁渲染器
            if (GameRenderer.SDLRenderer != IntPtr.Zero)
                SDL.SDL_DestroyRenderer(GameRenderer.SDLRenderer);
            GameRenderer.SDLRenderer = IntPtr.Zero;

            //销毁窗口
            if (GameWindow.SDLWindow != IntPtr.Zero)
                SDL.SDL_DestroyWindow(GameWindow.SDLWindow);
            GameWindow.SDLWindow = IntPtr.Zero;
        }

        #endregion

        #region 退出

        /// <summary>
        /// 退出
        /// </summary>
        public static void Quit()
        {
            //退出SDL_mixer库
            SDL_mixer.Mix_Quit();
            //退出SDL_ttf库
            SDL_ttf.TTF_Quit();
            //退出SDL_image库
            SDL_image.IMG_Quit();

            //退出SDL库
            SDL.SDL_Quit();
        }

        #endregion
    }
}

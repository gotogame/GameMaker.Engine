namespace GameMaker.Engine
{
    /// <summary>
    /// 纹理
    /// </summary>
    public class Texture : IDisposable
    {
        #region 属性

        /// <summary>
        /// SDL纹理
        /// (像素格式ARGB8888)
        /// </summary>
        internal IntPtr SDLTexture { get; private set; } = IntPtr.Zero;

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// 包围盒
        /// (0, 0, Width, Height)
        /// </summary>
        public Rectangle Bounds { get; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建纹理
        /// </summary>
        /// <param name="width">宽度(大于0)</param>
        /// <param name="height">高度(大于0)</param>
        public Texture(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentException("宽度小于等于0", "width");
            if (height <= 0)
                throw new ArgumentException("高度小于等于0", "height");

            SDLTexture = GameRenderer.CreateSDLTexture(width, height, SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING);
            if (SDLTexture == IntPtr.Zero)
                throw new InvalidOperationException("创建纹理失败");

            Width = width;
            Height = height;
            Bounds = new Rectangle(0, 0, Width, Height);
        }

        /// <summary>
        /// 创建纹理
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        public Texture(string fileFullPath)
        {
            SDLTexture = GameRenderer.CreateSDLTextureFromFile(fileFullPath, SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, out int width, out int height);
            if (SDLTexture == IntPtr.Zero)
                throw new InvalidOperationException("创建纹理失败");

            Width = width;
            Height = height;
            Bounds = new Rectangle(0, 0, Width, Height);
        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 创建纹理
        /// </summary>
        /// <param name="sdlSurface">sdl表面</param>
        internal Texture(IntPtr sdlSurface)
        {
            SDLTexture = GameRenderer.CreateSDLTextureFromSDLSurface(sdlSurface, SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, out int width, out int height);
            if (SDLTexture == IntPtr.Zero)
                throw new InvalidOperationException("创建纹理失败");

            Width = width;
            Height = height;
            Bounds = new Rectangle(0, 0, Width, Height);
        }

        #endregion

        #region 释放资源(包含析构函数和 IDisposable 接口实现)
        ~Texture()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsDisposed { get { return _isDisposed; } }
        private bool _isDisposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    //释放托管资源

                }

                //释放非托管资源
                if (SDLTexture != IntPtr.Zero)
                {
                    SDL.SDL_DestroyTexture(SDLTexture);
                    SDLTexture = IntPtr.Zero;
                }

                //
                _isDisposed = true;
            }
        }
        #endregion 释放资源

        #region 方法

        /// <summary>
        /// 获取指定矩形范围内的像素颜色
        /// (当指定的矩形没有包含在包围盒矩形内时获取失败)
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <returns>获取成功返回Color[rect.Height, rect.Width],获取失败返回null</returns>
        public Color[,] GetColors(Rectangle rect)
        {
            if (Bounds.Contains(rect) == false)
                return null;

            //像素格式ARGB8888
            if (SDL.SDL_LockTexture(SDLTexture, IntPtr.Zero, out IntPtr pixels, out int pitch) == 0)
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

                SDL.SDL_UnlockTexture(SDLTexture);

                return colors;
            }

            return null;
        }

        /// <summary>
        /// 设置指定矩形范围内的像素颜色
        /// (当指定的矩形没有包含在包围盒矩形内时无操作)
        /// (当指定的颜色二维数组等于null或者小于指定的矩形大小时无操作)
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="colors">颜色二维数组Color[rect.Height, rect.Width]</param>
        public void SetColors(Rectangle rect, Color[,] colors)
        {
            if (Bounds.Contains(rect) == false || colors == null || colors.GetLength(0) < rect.Height || colors.GetLength(1) < rect.Width)
                return;

            //像素格式ARGB8888
            if (SDL.SDL_LockTexture(SDLTexture, IntPtr.Zero, out IntPtr pixels, out int pitch) == 0)
            {
                for (int y = rect.Top; y < rect.Bottom; y++)
                {
                    for (int x = rect.Left; x < rect.Right; x++)
                    {
                        int i = x * 4 + y * pitch;
                        Color color = colors[y - rect.Top, x - rect.Left];
                        Marshal.WriteInt32(pixels, i, color.ToArgb());
                    }
                }

                SDL.SDL_UnlockTexture(SDLTexture);
            }
        }

        /// <summary>
        /// 获取指定像素的颜色
        /// </summary>
        /// <param name="point">像素坐标</param>
        /// <returns>指定像素的颜色</returns>
        public Color GetColor(Point point)
        {
            Color[,] colors = GetColors(new Rectangle(point.X, point.Y, 1, 1));
            return colors == null ? new Color(0, 0, 0, 0) : colors[0, 0];
        }

        /// <summary>
        /// 设置指定像素的颜色
        /// </summary>
        /// <param name="point">像素坐标</param>
        /// <param name="color">像素颜色</param>
        public void SetColor(Point point, Color color)
        {
            SetColors(new Rectangle(point.X, point.Y, 1, 1), new Color[,] { { color } });
        }

        #endregion
    }
}

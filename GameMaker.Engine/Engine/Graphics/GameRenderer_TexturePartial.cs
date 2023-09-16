namespace GameMaker.Engine
{
    public static partial class GameRenderer
    {
        #region SDL纹理属性

        /// <summary>
        /// SDL纹理像素格式
        /// (默认像素格式:SDL.SDL_PIXELFORMAT_ARGB8888)
        /// </summary>
        internal static uint SDLTexturePixelFormat { get; } = SDL.SDL_PIXELFORMAT_ARGB8888;

        /// <summary>
        /// SDL纹理混合模式
        /// (默认混合模式:SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND)
        /// </summary>
        internal static SDL.SDL_BlendMode SDLTextureBlendMode { get; } = SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND;

        #endregion

        #region 创建SDL纹理

        /// <summary>
        /// 创建SDL纹理
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="textureAccess">sdl纹理类型</param>
        /// <returns>失败返回IntPtr.Zero</returns>
        internal static IntPtr CreateSDLTexture(int width, int height, SDL.SDL_TextureAccess textureAccess)
        {
            IntPtr texture = SDL.SDL_CreateTexture(SDLRenderer, SDLTexturePixelFormat, (int)textureAccess, width, height);
            if (texture != IntPtr.Zero)
                SDL.SDL_SetTextureBlendMode(texture, SDLTextureBlendMode);
            return texture;
        }

        /// <summary>
        /// 创建SDL纹理
        /// </summary>
        /// <param name="sdlSurface">sdl表面</param>
        /// <param name="textureAccess">sdl纹理类型</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>失败返回IntPtr.Zero</returns>
        internal static IntPtr CreateSDLTextureFromSDLSurface(IntPtr sdlSurface, SDL.SDL_TextureAccess textureAccess, out int width, out int height)
        {
            IntPtr surface = IntPtr.Zero;
            IntPtr texture = IntPtr.Zero;

            try
            {
                surface = SDL.SDL_ConvertSurfaceFormat(sdlSurface, SDLTexturePixelFormat, 0);

                SDL.SDL_Surface sf = Marshal.PtrToStructure<SDL.SDL_Surface>(surface);
                byte[] surfacePixels = new byte[sf.pitch * sf.h];
                Marshal.Copy(sf.pixels, surfacePixels, 0, surfacePixels.Length);

                width = sf.w;
                height = sf.h;
                texture = CreateSDLTexture(width, height, textureAccess);
                if (texture != IntPtr.Zero)
                {
                    SDL.SDL_LockTexture(texture, IntPtr.Zero, out IntPtr pixels, out int pitch);
                    Marshal.Copy(surfacePixels, 0, pixels, surfacePixels.Length);
                    SDL.SDL_UnlockTexture(texture);
                }

                return texture;
            }
            finally
            {
                if (surface != IntPtr.Zero)
                    SDL.SDL_FreeSurface(surface);
            }
        }

        /// <summary>
        /// 创建SDL纹理
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <param name="textureAccess">sdl纹理类型</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>失败返回IntPtr.Zero</returns>
        internal static IntPtr CreateSDLTextureFromFile(string fileFullPath, SDL.SDL_TextureAccess textureAccess, out int width, out int height)
        {
            IntPtr surface = IntPtr.Zero;

            try
            {
                surface = SDL_image.IMG_Load(fileFullPath);
                return CreateSDLTextureFromSDLSurface(surface, textureAccess, out width, out height);
            }
            finally
            {
                if (surface != IntPtr.Zero)
                    SDL.SDL_FreeSurface(surface);
            }
        }

        #endregion

    }
}

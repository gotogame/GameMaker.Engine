namespace GameMaker.Engine
{
    /// <summary>
    /// 字体
    /// </summary>
    public class Font : IDisposable
    {
        #region 属性

        /// <summary>
        /// 字体文件数据
        /// </summary>
        private IntPtr _fontFile;
        private int _fontFile_Length;

        /// <summary>
        /// 字符集纹理字典(!=null)
        /// (键: 字体大小)
        /// (值: 字符集纹理列表)
        /// </summary>
        public Dictionary<int, List<CharSetTexture>> CharSetTextureDictionary { get; } = new Dictionary<int, List<CharSetTexture>>();

        /// <summary>
        /// 行高字典(!=null)
        /// (键: 字体大小)
        /// (值: 行高)
        /// </summary>
        public Dictionary<int, int> LineHeightDictionary { get; } = new Dictionary<int, int>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        public Font(string fileFullPath)
        {
            byte[] data = File.ReadAllBytes(fileFullPath);
            _fontFile_Length = data.Length;
            _fontFile = Marshal.AllocHGlobal(_fontFile_Length);
            Marshal.Copy(data, 0, _fontFile, _fontFile_Length);
        }

        #endregion

        #region 释放资源(包含析构函数和 IDisposable 接口实现)
        ~Font()
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
                if (_fontFile != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(_fontFile);
                    _fontFile = IntPtr.Zero;
                }

                if (CharSetTextureDictionary != null)
                {
                    foreach (var kvp in CharSetTextureDictionary)
                    {
                        if (kvp.Value != null)
                        {
                            foreach (var texture in kvp.Value)
                            {
                                texture?.Dispose();
                            }
                            kvp.Value.Clear();
                        }
                    }
                    CharSetTextureDictionary.Clear();
                }

                //
                _isDisposed = true;
            }
        }
        #endregion 释放资源

        #region 方法

        /// <summary>
        /// 新建字符纹理
        /// </summary>
        /// <param name="fontSize">字体大小</param>
        /// <param name="chr">字符</param>
        /// <returns>失败返回null</returns>
        public Texture NewCharTexture(int fontSize, char chr)
        {
            if (_fontFile == IntPtr.Zero)
                return null;

            IntPtr sdlFont = IntPtr.Zero;
            IntPtr charSurface = IntPtr.Zero;
            Texture charTexture = null;
            try
            {
                //创建SDL字体对象
                IntPtr sdl_rw = SDL.SDL_RWFromMem(_fontFile, _fontFile_Length);
                sdlFont = SDL_ttf.TTF_OpenFontRW(sdl_rw, 1, fontSize);

                if (sdlFont != IntPtr.Zero)
                {
                    //创建字符纹理
                    charSurface = SDL_ttf.TTF_RenderGlyph_Blended(sdlFont, chr, new SDL.SDL_Color() { r = 255, g = 255, b = 255, a = 255 });
                    charTexture = new Texture(charSurface);
                }
            }
            catch { }
            finally
            {
                if (sdlFont != IntPtr.Zero)
                    SDL_ttf.TTF_CloseFont(sdlFont);

                if (charSurface != IntPtr.Zero)
                    SDL.SDL_FreeSurface(charSurface);
            }
            return charTexture;
        }

        /// <summary>
        /// 获取字符集纹理
        /// </summary>
        /// <param name="fontSize">字体大小</param>
        /// <param name="chr">字符</param>
        /// <returns>返回包含指定字符的字符集纹理,获取失败返回null</returns>
        public CharSetTexture GetCharSetTexture(int fontSize, char chr)
        {
            if (CharSetTextureDictionary.ContainsKey(fontSize))
            {
                if (CharSetTextureDictionary[fontSize] != null)
                {
                    foreach (CharSetTexture texture in CharSetTextureDictionary[fontSize])
                    {
                        if (texture != null && texture.CharBoundsDictionary.ContainsKey(chr))
                            return texture;
                    }
                }
                else
                {
                    CharSetTextureDictionary[fontSize] = new List<CharSetTexture>();
                }
            }
            else
            {
                CharSetTextureDictionary[fontSize] = new List<CharSetTexture>();
            }

            //不等于null并且不包含当前需要的字符纹理
            List<CharSetTexture> charSetTextureList = CharSetTextureDictionary[fontSize];

            //
            Texture charTexture = null;
            CharSetTexture charSetTexture = null;
            try
            {
                charTexture = NewCharTexture(fontSize, chr);
                if (charTexture == null)
                    return null;

                foreach (CharSetTexture texture in charSetTextureList)
                {
                    if (texture != null && !texture.IsFull && texture.AddCharTexture(chr, charTexture))
                    {
                        return texture;
                    }
                }

                //
                charSetTexture = new CharSetTexture();
                if (charSetTexture.AddCharTexture(chr, charTexture))
                {
                    charSetTextureList.Add(charSetTexture);
                    return charSetTexture;
                }
            }
            catch
            {
                charSetTexture?.Dispose();
            }
            finally
            {
                charTexture?.Dispose();
            }

            return null;
        }

        /// <summary>
        /// 获取行高
        /// </summary>
        /// <param name="fontSize">字体大小</param>
        /// <returns>失败返回-1</returns>
        public int GetLineHeight(int fontSize)
        {
            if (LineHeightDictionary.ContainsKey(fontSize))
                return LineHeightDictionary[fontSize];

            if (_fontFile == IntPtr.Zero)
                return -1;

            int lineHeight = -1;
            IntPtr sdlFont = IntPtr.Zero;
            try
            {
                //创建SDL字体对象
                IntPtr sdl_rw = SDL.SDL_RWFromMem(_fontFile, _fontFile_Length);
                sdlFont = SDL_ttf.TTF_OpenFontRW(sdl_rw, 1, fontSize);

                if (sdlFont != IntPtr.Zero)
                {
                    lineHeight = SDL_ttf.TTF_FontHeight(sdlFont);
                    LineHeightDictionary[fontSize] = lineHeight;
                }
            }
            catch { }
            finally
            {
                if (sdlFont != IntPtr.Zero)
                    SDL_ttf.TTF_CloseFont(sdlFont);
            }

            return lineHeight;
        }

        #endregion

    }
}

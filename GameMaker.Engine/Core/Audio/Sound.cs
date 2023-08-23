namespace GameMaker.Engine
{
    /// <summary>
    /// 声音
    /// </summary>
    public class Sound : IDisposable
    {
        #region 属性

        /// <summary>
        /// SDL_mixer库中的Mix_Chunk对象
        /// </summary>
        internal IntPtr SDLMixChunk { get; private set; } = IntPtr.Zero;

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建声音
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        public Sound(string fileFullPath)
        {
            SDLMixChunk = SDL_mixer.Mix_LoadWAV(fileFullPath);
            if (SDLMixChunk == IntPtr.Zero)
                throw new InvalidOperationException("创建声音失败");
        }

        #endregion

        #region 释放资源(包含析构函数和 IDisposable 接口实现)
        ~Sound()
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
                if (SDLMixChunk != IntPtr.Zero)
                {
                    SDL_mixer.Mix_FreeChunk(SDLMixChunk);
                    SDLMixChunk = IntPtr.Zero;
                }

                //
                _isDisposed = true;
            }
        }
        #endregion 释放资源

    }
}

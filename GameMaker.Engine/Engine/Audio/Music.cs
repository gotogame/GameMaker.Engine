namespace GameMaker.Engine
{
    /// <summary>
    /// 音乐
    /// </summary>
    public class Music : IDisposable
    {
        #region 属性

        /// <summary>
        /// SDL_mixer库中的Mix_Music对象
        /// </summary>
        internal IntPtr SDLMixMusic { get; private set; } = IntPtr.Zero;

        /// <summary>
        /// 时长(秒)
        /// </summary>
        public double Duration { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建音乐
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        public Music(string fileFullPath)
        {
            SDLMixMusic = SDL_mixer.Mix_LoadMUS(fileFullPath);
            if (SDLMixMusic == IntPtr.Zero)
                throw new InvalidOperationException("创建音乐失败");

            Duration = SDL_mixer.Mix_MusicDuration(SDLMixMusic);
        }

        #endregion

        #region 释放资源(包含析构函数和 IDisposable 接口实现)
        ~Music()
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
                if (SDLMixMusic != IntPtr.Zero)
                {
                    SDL_mixer.Mix_FreeMusic(SDLMixMusic);
                    SDLMixMusic = IntPtr.Zero;
                }

                //
                _isDisposed = true;
            }
        }
        #endregion 释放资源

    }
}

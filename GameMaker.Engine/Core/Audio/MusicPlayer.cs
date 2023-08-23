namespace GameMaker.Engine
{
    /// <summary>
    /// 音乐播放器
    /// </summary>
    public static class MusicPlayer
    {
        #region 属性

        /// <summary>
        /// 音量(0 -- 128)
        /// </summary>
        public static int Volume { get { return SDL_mixer.Mix_VolumeMusic(-1); } set { SDL_mixer.Mix_VolumeMusic(value); } }

        /// <summary>
        /// 是否播放中
        /// </summary>
        public static bool IsPlaying { get { return SDL_mixer.Mix_PlayingMusic() == 1; } }

        /// <summary>
        /// 是否已暂停
        /// </summary>
        public static bool IsPaused { get { return SDL_mixer.Mix_PausedMusic() == 1; } }

        /// <summary>
        /// 当前音乐
        /// </summary>
        public static Music CurrentMusic { get; private set; }

        /// <summary>
        /// 当前音乐的播放位置(秒)
        /// </summary>
        public static double CurrentMusicPosition
        {
            get
            {
                return (CurrentMusic != null && CurrentMusic.SDLMixMusic != IntPtr.Zero && IsPlaying) ? SDL_mixer.Mix_GetMusicPosition(CurrentMusic.SDLMixMusic) : 0;
            }
            set
            {
                if (CurrentMusic != null && CurrentMusic.SDLMixMusic != IntPtr.Zero && IsPlaying && value >= 0 && value < CurrentMusic.Duration)
                {
                    SDL_mixer.Mix_SetMusicPosition(value);
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 播放
        /// (当音乐等于null时无操作)
        /// (自动停止正在播放中的音乐)
        /// </summary>
        /// <param name="music">音乐</param>
        /// <param name="isLoop">是否循环</param>
        public static void PlayMusic(Music music, bool isLoop = false)
        {
            if (music == null || music.SDLMixMusic == IntPtr.Zero)
                return;

            SDL_mixer.Mix_HaltMusic();
            CurrentMusic = null;

            if (SDL_mixer.Mix_PlayMusic(music.SDLMixMusic, isLoop ? -1 : 0) == 0)
                CurrentMusic = music;
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public static void Pause()
        {
            SDL_mixer.Mix_PauseMusic();
        }

        /// <summary>
        /// 继续
        /// </summary>
        public static void Resume()
        {
            SDL_mixer.Mix_ResumeMusic();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            SDL_mixer.Mix_HaltMusic();
        }

        #endregion

    }
}

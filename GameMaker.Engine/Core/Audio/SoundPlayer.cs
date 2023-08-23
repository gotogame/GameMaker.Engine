namespace GameMaker.Engine
{
    /// <summary>
    /// 声音播放器
    /// </summary>
    public class SoundPlayer
    {
        #region 静态属性与方法

        [DllImport("SDL2_mixer", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Mix_MasterVolume(int volume);

        /// <summary>
        /// 主音量(0 -- 128)
        /// </summary>
        public static int MasterVolume { get { return Mix_MasterVolume(-1); } set { Mix_MasterVolume(value); } }

        /// <summary>
        /// 停止播放全部声音
        /// </summary>
        public static void StopAll()
        {
            SDL_mixer.Mix_HaltChannel(-1);
        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// [通道]--声音播放器
        /// </summary>
        private static SoundPlayer[] SoundPlayerChannels;

        /// <summary>
        /// 获取当前可供指定的声音播放器使用的通道
        /// (失败返回-1)
        /// </summary>
        private static int GetChannel(SoundPlayer soundPlayer)
        {
            if (soundPlayer == null)
                return -1;

            if (SoundPlayerChannels == null)
                SoundPlayerChannels = new SoundPlayer[SDL_mixer.Mix_AllocateChannels(-1)];

            for (int i = 0; i < SoundPlayerChannels.Length; i++)
            {
                if (SoundPlayerChannels[i] == soundPlayer)
                    return i;
            }

            for (int i = 0; i < SoundPlayerChannels.Length; i++)
            {
                if (SDL_mixer.Mix_Playing(i) == 0)
                {
                    SoundPlayerChannels[i] = soundPlayer;
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 音量(0 -- 128)
        /// </summary>
        public int Volume
        {
            get { return _volume; }
            set
            {
                _volume = value < 0 ? 0 : value > 128 ? 128 : value;

                int channel = GetChannel(this);
                if (channel >= 0)
                {
                    SDL_mixer.Mix_Volume(channel, value);
                }
            }
        }
        private int _volume = 128;

        /// <summary>
        /// 是否播放中
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                int channel = GetChannel(this);
                return channel >= 0 ? SDL_mixer.Mix_Playing(channel) == 1 : false;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 播放
        /// (当声音等于null时无操作)
        /// (自动停止该播放器正在播放中的声音)
        /// </summary>
        /// <param name="sound">声音</param>
        /// <param name="isLoop">是否循环</param>
        public void PlaySound(Sound sound, bool isLoop = false)
        {
            if (sound == null || sound.SDLMixChunk == IntPtr.Zero)
                return;

            int channel = GetChannel(this);
            if (channel >= 0)
            {
                SDL_mixer.Mix_Volume(channel, Volume);
                SDL_mixer.Mix_PlayChannel(channel, sound.SDLMixChunk, isLoop ? -1 : 0);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            int channel = GetChannel(this);
            if (channel >= 0)
            {
                SDL_mixer.Mix_HaltChannel(channel);
            }
        }

        #endregion

    }
}

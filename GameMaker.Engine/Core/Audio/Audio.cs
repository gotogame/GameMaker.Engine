namespace GameMaker.Engine
{
    public static class Audio
    {
        /// <summary>
        /// 打开混音器
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool OpenAudio()
        {
            return SDL_mixer.Mix_OpenAudio(SDL_mixer.MIX_DEFAULT_FREQUENCY, SDL_mixer.MIX_DEFAULT_FORMAT, SDL_mixer.MIX_DEFAULT_CHANNELS, 4096) == 0;
        }

        /// <summary>
        /// 关闭混音器
        /// </summary>
        public static void CloseAudio()
        {
            SDL_mixer.Mix_CloseAudio();
        }
    }
}

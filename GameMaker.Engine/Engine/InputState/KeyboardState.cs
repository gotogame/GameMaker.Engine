namespace GameMaker.Engine
{
    /// <summary>
    /// 键盘状态
    /// </summary>
    public static class KeyboardState
    {
        /// <summary>
        /// 上一帧按住的键盘按键(!=null)
        /// </summary>
        public static List<Key> PreviousKeyboardPressedKeys { get; } = new List<Key>();

        /// <summary>
        /// 当前帧按住的键盘按键(!=null)
        /// </summary>
        public static List<Key> CurrentKeyboardPressedKeys { get; } = new List<Key>();

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 判断自上一帧以来按下的键盘按键
        /// </summary>
        /// <param name="key">键盘按键</param>
        /// <returns>按下返回true,未按下返回false</returns>
        public static bool IsDown(Key key)
        {
            return PreviousKeyboardPressedKeys.Contains(key) == false && CurrentKeyboardPressedKeys.Contains(key) == true;
        }

        /// <summary>
        /// 判断当前帧按住的键盘按键
        /// </summary>
        /// <param name="key">键盘按键</param>
        /// <returns>按住返回true,未按住返回false</returns>
        public static bool IsPressed(Key key)
        {
            return CurrentKeyboardPressedKeys.Contains(key);
        }

        /// <summary>
        /// 判断自上一帧以来放开的键盘按键
        /// </summary>
        /// <param name="key">键盘按键</param>
        /// <returns>放开返回true,未放开返回false</returns>
        public static bool IsUp(Key key)
        {
            return PreviousKeyboardPressedKeys.Contains(key) == true && CurrentKeyboardPressedKeys.Contains(key) == false;
        }
    }
}

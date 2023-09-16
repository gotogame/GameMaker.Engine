namespace GameMaker.Engine
{
    /// <summary>
    /// 鼠标状态
    /// </summary>
    public static class MouseState
    {
        /// <summary>
        /// 上一帧鼠标位置
        /// </summary>
        public static Point PreviousMousePosition { get; internal set; }

        /// <summary>
        /// 当前帧鼠标位置
        /// </summary>
        public static Point CurrentMousePosition { get; internal set; }

        /// <summary>
        /// 鼠标滚轮值(X表示水平滚轮值,Y表示垂直滚轮值)
        /// (当X大于0时表示向右滚动,X小于0时表示向左滚动)
        /// (当Y大于0时表示向前滚动,Y小于0时表示向后滚动)
        /// </summary>
        public static Point MouseScrollWheel { get; internal set; }

        /// <summary>
        /// 上一帧按住的鼠标按钮(!=null)
        /// </summary>
        public static List<MouseButton> PreviousMousePressedButtons { get; } = new List<MouseButton>();

        /// <summary>
        /// 当前帧按住的鼠标按钮(!=null)
        /// </summary>
        public static List<MouseButton> CurrentMousePressedButtons { get; } = new List<MouseButton>();

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 判断自上一帧以来按下的鼠标按钮
        /// </summary>
        /// <param name="mouseButton">鼠标按钮</param>
        /// <returns>按下返回true,未按下返回false</returns>
        public static bool IsDown(MouseButton mouseButton)
        {
            return PreviousMousePressedButtons.Contains(mouseButton) == false && CurrentMousePressedButtons.Contains(mouseButton) == true;
        }

        /// <summary>
        /// 判断当前帧按住的鼠标按钮
        /// </summary>
        /// <param name="mouseButton">鼠标按钮</param>
        /// <returns>按住返回true,未按住返回false</returns>
        public static bool IsPressed(MouseButton mouseButton)
        {
            return CurrentMousePressedButtons.Contains(mouseButton);
        }

        /// <summary>
        /// 判断自上一帧以来放开的鼠标按钮
        /// </summary>
        /// <param name="mouseButton">鼠标按钮</param>
        /// <returns>放开返回true,未放开返回false</returns>
        public static bool IsUp(MouseButton mouseButton)
        {
            return PreviousMousePressedButtons.Contains(mouseButton) == true && CurrentMousePressedButtons.Contains(mouseButton) == false;
        }
    }
}

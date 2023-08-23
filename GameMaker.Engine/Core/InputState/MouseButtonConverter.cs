namespace GameMaker.Engine
{
    /// <summary>
    /// 鼠标按钮转换器
    /// </summary>
    internal static class MouseButtonConverter
    {
        private static Dictionary<uint, MouseButton> _map = new Dictionary<uint, MouseButton>()
        {
            { SDL.SDL_BUTTON_LEFT, MouseButton.Left },
            { SDL.SDL_BUTTON_MIDDLE, MouseButton.Middle },
            { SDL.SDL_BUTTON_RIGHT, MouseButton.Right },
            { SDL.SDL_BUTTON_X1, MouseButton.X1 },
            { SDL.SDL_BUTTON_X2, MouseButton.X2 }
        };



        /// <summary>
        /// SDL鼠标码转换鼠标按键
        /// </summary>
        /// <param name="sdlMouseButton"></param>
        /// <returns></returns>
        public static MouseButton SDLMouseButtonToMouseButton(uint sdlMouseButton)
        {
            return _map.ContainsKey(sdlMouseButton) ? _map[sdlMouseButton] : MouseButton.None;
        }

        /// <summary>
        /// 鼠标按键转换SDL鼠标码
        /// </summary>
        /// <param name="mouseButton"></param>
        /// <returns></returns>
        public static uint MouseButtonToSDLMouseButton(MouseButton mouseButton)
        {
            foreach (var kvp in _map)
            {
                if (kvp.Value == mouseButton)
                    return kvp.Key;
            }

            return 0;
        }
    }
}

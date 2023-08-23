namespace GameMaker.Engine
{
    /// <summary>
    /// 键盘按键转换器
    /// </summary>
    internal static class KeyConverter
    {
        private static Dictionary<SDL.SDL_Keycode, Key> _map = new Dictionary<SDL.SDL_Keycode, Key>()
        {
            { SDL.SDL_Keycode.SDLK_ESCAPE, Key.Escape },



            { SDL.SDL_Keycode.SDLK_F1, Key.F1 },
            { SDL.SDL_Keycode.SDLK_F2, Key.F2 },
            { SDL.SDL_Keycode.SDLK_F3, Key.F3 },
            { SDL.SDL_Keycode.SDLK_F4, Key.F4 },
            { SDL.SDL_Keycode.SDLK_F5, Key.F5 },
            { SDL.SDL_Keycode.SDLK_F6, Key.F6 },
            { SDL.SDL_Keycode.SDLK_F7, Key.F7 },
            { SDL.SDL_Keycode.SDLK_F8, Key.F8 },
            { SDL.SDL_Keycode.SDLK_F9, Key.F9 },
            { SDL.SDL_Keycode.SDLK_F10, Key.F10 },
            { SDL.SDL_Keycode.SDLK_F11, Key.F11 },
            { SDL.SDL_Keycode.SDLK_F12, Key.F12 },



            { SDL.SDL_Keycode.SDLK_1, Key.D1 },
            { SDL.SDL_Keycode.SDLK_2, Key.D2 },
            { SDL.SDL_Keycode.SDLK_3, Key.D3 },
            { SDL.SDL_Keycode.SDLK_4, Key.D4 },
            { SDL.SDL_Keycode.SDLK_5, Key.D5 },
            { SDL.SDL_Keycode.SDLK_6, Key.D6 },
            { SDL.SDL_Keycode.SDLK_7, Key.D7 },
            { SDL.SDL_Keycode.SDLK_8, Key.D8 },
            { SDL.SDL_Keycode.SDLK_9, Key.D9 },
            { SDL.SDL_Keycode.SDLK_0, Key.D0 },



            { SDL.SDL_Keycode.SDLK_a, Key.A },
            { SDL.SDL_Keycode.SDLK_b, Key.B },
            { SDL.SDL_Keycode.SDLK_c, Key.C },
            { SDL.SDL_Keycode.SDLK_d, Key.D },
            { SDL.SDL_Keycode.SDLK_e, Key.E },
            { SDL.SDL_Keycode.SDLK_f, Key.F },
            { SDL.SDL_Keycode.SDLK_g, Key.G },
            { SDL.SDL_Keycode.SDLK_h, Key.H },
            { SDL.SDL_Keycode.SDLK_i, Key.I },
            { SDL.SDL_Keycode.SDLK_j, Key.J },
            { SDL.SDL_Keycode.SDLK_k, Key.K },
            { SDL.SDL_Keycode.SDLK_l, Key.L },
            { SDL.SDL_Keycode.SDLK_m, Key.M },
            { SDL.SDL_Keycode.SDLK_n, Key.N },
            { SDL.SDL_Keycode.SDLK_o, Key.O },
            { SDL.SDL_Keycode.SDLK_p, Key.P },
            { SDL.SDL_Keycode.SDLK_q, Key.Q },
            { SDL.SDL_Keycode.SDLK_r, Key.R },
            { SDL.SDL_Keycode.SDLK_s, Key.S },
            { SDL.SDL_Keycode.SDLK_t, Key.T },
            { SDL.SDL_Keycode.SDLK_u, Key.U },
            { SDL.SDL_Keycode.SDLK_v, Key.V },
            { SDL.SDL_Keycode.SDLK_w, Key.W },
            { SDL.SDL_Keycode.SDLK_x, Key.X },
            { SDL.SDL_Keycode.SDLK_y, Key.Y },
            { SDL.SDL_Keycode.SDLK_z, Key.Z },



            { SDL.SDL_Keycode.SDLK_SPACE, Key.Space },



            { SDL.SDL_Keycode.SDLK_BACKQUOTE, Key.Backquote },
            { SDL.SDL_Keycode.SDLK_TAB, Key.Tab },
            { SDL.SDL_Keycode.SDLK_CAPSLOCK, Key.CapsLock },
            { SDL.SDL_Keycode.SDLK_LSHIFT, Key.LeftShift },
            { SDL.SDL_Keycode.SDLK_LCTRL, Key.LeftControl },
            { SDL.SDL_Keycode.SDLK_LGUI, Key.LeftWindows },
            { SDL.SDL_Keycode.SDLK_LALT, Key.LeftAlt },



            { SDL.SDL_Keycode.SDLK_MINUS, Key.Minus },
            { SDL.SDL_Keycode.SDLK_EQUALS, Key.Equals },
            { SDL.SDL_Keycode.SDLK_BACKSPACE, Key.Backspace },
            { SDL.SDL_Keycode.SDLK_LEFTBRACKET, Key.LeftBracket },
            { SDL.SDL_Keycode.SDLK_RIGHTBRACKET, Key.RightBracket },
            { SDL.SDL_Keycode.SDLK_BACKSLASH, Key.Backslash },
            { SDL.SDL_Keycode.SDLK_SEMICOLON, Key.Semicolon },
            { SDL.SDL_Keycode.SDLK_QUOTE, Key.Quote },
            { SDL.SDL_Keycode.SDLK_RETURN, Key.Enter },
            { SDL.SDL_Keycode.SDLK_COMMA, Key.Comma },
            { SDL.SDL_Keycode.SDLK_PERIOD, Key.Period },
            { SDL.SDL_Keycode.SDLK_SLASH, Key.Slash },
            { SDL.SDL_Keycode.SDLK_RSHIFT, Key.RightShift },
            { SDL.SDL_Keycode.SDLK_RALT, Key.RightAlt },
            { SDL.SDL_Keycode.SDLK_RGUI, Key.RightWindows },
            { SDL.SDL_Keycode.SDLK_APPLICATION, Key.ContextMenu },
            { SDL.SDL_Keycode.SDLK_RCTRL, Key.RightControl },



            { SDL.SDL_Keycode.SDLK_PRINTSCREEN, Key.PrintScreen },
            { SDL.SDL_Keycode.SDLK_SCROLLLOCK, Key.ScrollLock },
            { SDL.SDL_Keycode.SDLK_PAUSE, Key.Pause },
            { SDL.SDL_Keycode.SDLK_INSERT, Key.Insert },
            { SDL.SDL_Keycode.SDLK_HOME, Key.Home },
            { SDL.SDL_Keycode.SDLK_PAGEUP, Key.PageUp },
            { SDL.SDL_Keycode.SDLK_DELETE, Key.Delete },
            { SDL.SDL_Keycode.SDLK_END, Key.End },
            { SDL.SDL_Keycode.SDLK_PAGEDOWN, Key.PageDown },



            { SDL.SDL_Keycode.SDLK_UP, Key.Up },
            { SDL.SDL_Keycode.SDLK_DOWN, Key.Down },
            { SDL.SDL_Keycode.SDLK_LEFT, Key.Left },
            { SDL.SDL_Keycode.SDLK_RIGHT, Key.Right },



            { SDL.SDL_Keycode.SDLK_KP_0, Key.NumPad0 },
            { SDL.SDL_Keycode.SDLK_KP_1, Key.NumPad1 },
            { SDL.SDL_Keycode.SDLK_KP_2, Key.NumPad2 },
            { SDL.SDL_Keycode.SDLK_KP_3, Key.NumPad3 },
            { SDL.SDL_Keycode.SDLK_KP_4, Key.NumPad4 },
            { SDL.SDL_Keycode.SDLK_KP_5, Key.NumPad5 },
            { SDL.SDL_Keycode.SDLK_KP_6, Key.NumPad6 },
            { SDL.SDL_Keycode.SDLK_KP_7, Key.NumPad7 },
            { SDL.SDL_Keycode.SDLK_KP_8, Key.NumPad8 },
            { SDL.SDL_Keycode.SDLK_KP_9, Key.NumPad9 },



            { SDL.SDL_Keycode.SDLK_NUMLOCKCLEAR, Key.NumLock },
            { SDL.SDL_Keycode.SDLK_KP_PLUS, Key.NumPadAdd },
            { SDL.SDL_Keycode.SDLK_KP_MINUS, Key.NumPadSubtract },
            { SDL.SDL_Keycode.SDLK_KP_MULTIPLY, Key.NumPadMultiply },
            { SDL.SDL_Keycode.SDLK_KP_DIVIDE, Key.NumPadDivide },
            { SDL.SDL_Keycode.SDLK_KP_PERIOD, Key.NumPadDecimal },
            { SDL.SDL_Keycode.SDLK_KP_ENTER, Key.NumPadEnter },



            { SDL.SDL_Keycode.SDLK_AC_BACK, Key.AndroidBack }
        };



        /// <summary>
        /// SDL键盘码转键盘按键
        /// </summary>
        /// <param name="sdlKeycode"></param>
        /// <returns></returns>
        public static Key SDLKeycodeToKey(SDL.SDL_Keycode sdlKeycode)
        {
            return _map.ContainsKey(sdlKeycode) ? _map[sdlKeycode] : Key.None;
        }

        /// <summary>
        /// 键盘按键转SDL键盘码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static SDL.SDL_Keycode KeyToSDLKeycode(Key key)
        {
            foreach (var kvp in _map)
            {
                if (kvp.Value == key)
                    return kvp.Key;
            }

            return 0;
        }
    }
}

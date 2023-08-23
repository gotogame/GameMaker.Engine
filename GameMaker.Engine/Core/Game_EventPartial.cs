namespace GameMaker.Engine
{
    public static partial class Game
    {
        /// <summary>
        /// 更新输入状态与游戏窗口事件
        /// </summary>
        public static void UpdateInputState()
        {
            KeyboardState.PreviousKeyboardPressedKeys.Clear();
            KeyboardState.PreviousKeyboardPressedKeys.AddRange(KeyboardState.CurrentKeyboardPressedKeys);

            MouseState.PreviousMousePressedButtons.Clear();
            MouseState.PreviousMousePressedButtons.AddRange(MouseState.CurrentMousePressedButtons);
            MouseState.PreviousMousePosition = MouseState.CurrentMousePosition;
            MouseState.MouseScrollWheel = Point.Zero;

            //
            while (SDL.SDL_PollEvent(out SDL.SDL_Event sdlEvent) == 1)
            {
                switch (sdlEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        {
                            GameWindow.Closing?.Invoke();
                            break;
                        }
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        {
                            Key key = KeyConverter.SDLKeycodeToKey(sdlEvent.key.keysym.sym);

                            if (!KeyboardState.CurrentKeyboardPressedKeys.Contains(key))
                            {
                                KeyboardState.CurrentKeyboardPressedKeys.Add(key);
                            }

                            break;
                        }
                    case SDL.SDL_EventType.SDL_KEYUP:
                        {
                            Key key = KeyConverter.SDLKeycodeToKey(sdlEvent.key.keysym.sym);

                            if (KeyboardState.CurrentKeyboardPressedKeys.Contains(key))
                            {
                                KeyboardState.CurrentKeyboardPressedKeys.Remove(key);
                            }

                            break;
                        }
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        {
                            MouseButton mouseButton = MouseButtonConverter.SDLMouseButtonToMouseButton(sdlEvent.button.button);

                            if (!MouseState.CurrentMousePressedButtons.Contains(mouseButton))
                            {
                                MouseState.CurrentMousePressedButtons.Add(mouseButton);
                            }

                            break;
                        }
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        {
                            MouseButton mouseButton = MouseButtonConverter.SDLMouseButtonToMouseButton(sdlEvent.button.button);

                            if (MouseState.CurrentMousePressedButtons.Contains(mouseButton))
                            {
                                MouseState.CurrentMousePressedButtons.Remove(mouseButton);
                            }

                            break;
                        }
                    case SDL.SDL_EventType.SDL_MOUSEMOTION:
                        {
                            //SDL事件中获取的鼠标坐标为相对于游戏窗口的坐标
                            MouseState.CurrentMousePosition = new Point(sdlEvent.motion.x, sdlEvent.motion.y);

                            break;
                        }
                    case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                        {
                            MouseState.MouseScrollWheel = new Point(sdlEvent.wheel.x, sdlEvent.wheel.y);

                            break;
                        }

                }
            }
        }

    }
}

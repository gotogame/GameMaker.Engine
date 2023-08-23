namespace GameMaker.Engine
{
    /// <summary>
    /// 引擎
    /// </summary>
    public static class Engine
    {
        #region 属性

        /// <summary>
        /// 当前地图(!=null)
        /// </summary>
        public static Map CurrentMap { get; private set; } = new Map();

        /// <summary>
        /// 下一帧要切换的地图
        /// (null表示不切换)
        /// (切换完成后该属性被设置为null)
        /// </summary>
        public static Map NextMap { get; set; }

        /// <summary>
        /// 下一帧退出引擎主循环
        /// </summary>
        public static bool NextExit { get; set; }

        #endregion

        #region 帧率

        /// <summary>
        /// 帧率
        /// </summary>
        private static ulong _fps;
        private static ulong _fpsCounter;
        private static ulong _fpsTicks;

        /// <summary>
        /// 每帧最小毫秒数
        /// </summary>
        private static ulong _minFrameTime;

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 获取帧率
        /// </summary>
        public static ulong GetFPS()
        {
            return _fps;
        }

        /// <summary>
        /// 设置预期帧率
        /// </summary>
        /// <param name="fps">帧率(小于等于0时无操作)</param>
        public static void SetFPS(ulong fps)
        {
            if (fps > 0)
                _minFrameTime = 1000 / fps;
        }

        /// <summary>
        /// 更新帧率
        /// </summary>
        private static void UpdateFPS()
        {
            if ((Game.GetTicks() - _fpsTicks) >= 1000)
            {
                _fps = _fpsCounter;
                _fpsCounter = 0;
                _fpsTicks = Game.GetTicks();
            }
            else
            {
                _fpsCounter += 1;
            }
        }

        #endregion

        #region 运行

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="map">当前地图(!=null)</param>
        public static void Run(Map map)
        {
            //初始化相关变量
            CurrentMap = map ?? throw new ArgumentNullException();

            if (GameWindow.Closing == null)
                GameWindow.Closing = () => { NextExit = true; };

            SetFPS(60);

            ulong previousTicks = 0;     //上一帧开始时间戳
            ulong currentTicks = 0;      //当前帧开始时间戳
            ulong ms = 0;                //自上一帧以来经过的毫秒数
            ulong delay = 0;             //当前帧需要延时的时间

            //引发游戏开始事件
            CurrentMap.GameStart();
            CurrentMap.MapLoad();

            //开始主循环
            while (!NextExit)
            {
                //计算帧时间
                previousTicks = currentTicks;
                currentTicks = Game.GetTicks();
                ms = currentTicks - previousTicks;

                //更新帧率
                UpdateFPS();

                //更新输入状态与游戏窗口事件
                Game.UpdateInputState();

                //切换地图
                if (NextMap != null)
                {
                    CurrentMap.MapUnload();

                    CurrentMap = NextMap;
                    NextMap = null;

                    CurrentMap.MapLoad();
                }

                //引发帧事件
                CurrentMap.Frame(ms);

                //控制帧时间
                delay = _minFrameTime - (Game.GetTicks() - currentTicks);
                if (delay > 0 && delay < uint.MaxValue)
                    Game.Delay((uint)delay);
            }

            //引发游戏结束事件
            CurrentMap.MapUnload();
            CurrentMap.GameOver();

            //
            CurrentMap = new Map();
            NextMap = null;
            NextExit = false;
        }

        #endregion
    }
}

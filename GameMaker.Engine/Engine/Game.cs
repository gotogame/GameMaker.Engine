namespace GameMaker.Engine
{
    public class Game : IDisposable
    {
        #region 属性

        /// <summary>
        /// 下一帧退出主循环
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
            if ((GameEngine.GetTicks() - _fpsTicks) >= 1000)
            {
                _fps = _fpsCounter;
                _fpsCounter = 0;
                _fpsTicks = GameEngine.GetTicks();
            }
            else
            {
                _fpsCounter += 1;
            }
        }

        #endregion

        #region 构造函数

        public Game()
        {
            GameEngine.Init();
            GameEngine.CreateWindowAndRenderer();
            Audio.OpenAudio();
        }

        #endregion

        #region 释放资源(包含析构函数和 IDisposable 接口实现)
        ~Game()
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
                    Audio.CloseAudio();
                    GameEngine.DestroyWindowAndRenderer();
                    GameEngine.Quit();
                }

                //释放非托管资源


                //
                _isDisposed = true;
            }
        }
        #endregion 释放资源

        #region 事件

        /// <summary>
        /// 游戏开始事件,游戏开始时引发一次
        /// </summary>
        public virtual void GameStart() { }

        /// <summary>
        /// 游戏结束事件,游戏结束时引发一次
        /// </summary>
        public virtual void GameOver() { }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 帧事件,每帧引发一次
        /// </summary>
        /// <param name="ms">自上一帧以来经过的毫秒数</param>
        public virtual void Frame(ulong ms) { }

        #endregion

        #region 运行

        /// <summary>
        /// 运行
        /// </summary>
        public void Run()
        {
            //初始化相关变量

            if (GameWindow.Closing == null)
                GameWindow.Closing = () => { NextExit = true; };

            SetFPS(60);

            ulong previousTicks = 0;     //上一帧开始时间戳
            ulong currentTicks = 0;      //当前帧开始时间戳
            ulong ms = 0;                //自上一帧以来经过的毫秒数
            ulong delay = 0;             //当前帧需要延时的时间

            //引发游戏开始事件
            GameStart();

            //开始主循环
            while (!NextExit)
            {
                //计算帧时间
                previousTicks = currentTicks;
                currentTicks = GameEngine.GetTicks();
                ms = currentTicks - previousTicks;

                //更新帧率
                UpdateFPS();

                //更新输入状态与游戏窗口事件
                GameEngine.UpdateInputState();

                //引发帧事件
                Frame(ms);

                //控制帧时间
                delay = _minFrameTime - (GameEngine.GetTicks() - currentTicks);
                if (delay > 0 && delay < uint.MaxValue)
                    GameEngine.Delay((uint)delay);
            }

            //引发游戏结束事件
            GameOver();

        }

        #endregion

    }
}

namespace GameMaker.Engine
{
    /// <summary>
    /// 地图
    /// </summary>
    public class Map
    {
        #region 属性

        /// <summary>
        /// 视野位置
        /// </summary>
        public Point ViewLocation { get; set; }

        /// <summary>
        /// 视野大小
        /// </summary>
        public Size ViewSize
        {
            get { return _viewSize; }
            set
            {
                _viewSize = value;
                if (Engine.CurrentMap == this && _viewSize.IsEmpty == false)
                    GameRenderer.LogicalSize = _viewSize;
            }
        }
        private Size _viewSize;

        #endregion

        #region 坐标转换

        /// <summary>
        /// 地图坐标转视野坐标
        /// </summary>
        /// <param name="mapPoint">相对于地图的坐标</param>
        /// <returns>相对于视野的坐标</returns>
        public Point MapToViewPoint(Point mapPoint)
        {
            return mapPoint - ViewLocation;
        }

        /// <summary>
        /// 视野坐标转地图坐标
        /// </summary>
        /// <param name="viewPoint">相对于视野的坐标</param>
        /// <returns>相对于地图的坐标</returns>
        public Point ViewToMapPoint(Point viewPoint)
        {
            return viewPoint + ViewLocation;
        }

        #endregion

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
        /// 地图加载事件,切换当前地图时引发一次
        /// </summary>
        public virtual void MapLoad()
        {
            ViewSize = ViewSize.IsEmpty ? GameWindow.Size : ViewSize;
        }

        /// <summary>
        /// 地图卸载事件,切换当前地图时引发一次
        /// </summary>
        public virtual void MapUnload()
        {
            MusicPlayer.Stop();
            SoundPlayer.StopAll();
            Assets.MusicAsset.Clear();
            Assets.SoundAsset.Clear();

            Assets.TextureAsset.Clear();
            Assets.FontAsset.Clear();

            Assets.SpriteAsset.Clear();
            Assets.MapAsset.Clear();
        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 帧事件,每帧引发一次
        /// </summary>
        /// <param name="ms">自上一帧以来经过的毫秒数</param>
        public virtual void Frame(ulong ms) { }

        #endregion

    }
}

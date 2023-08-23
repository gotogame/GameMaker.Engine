namespace GameMaker.Engine
{
    /// <summary>
    /// 元素
    /// </summary>
    public abstract partial class Element
    {
        #region 属性

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        public virtual int Depth { get; set; }

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
        public virtual void MapLoad() { }

        /// <summary>
        /// 地图卸载事件,切换当前地图时引发一次
        /// </summary>
        public virtual void MapUnload() { }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 开始更新事件,每帧引发一次
        /// </summary>
        /// <param name="ms">自上一帧以来经过的毫秒数</param>
        public virtual void BeginUpdate(ulong ms) { }

        /// <summary>
        /// 更新事件,每帧引发一次
        /// </summary>
        /// <param name="ms">自上一帧以来经过的毫秒数</param>
        public virtual void Update(ulong ms) { }

        /// <summary>
        /// 结束更新事件,每帧引发一次
        /// </summary>
        /// <param name="ms">自上一帧以来经过的毫秒数</param>
        public virtual void EndUpdate(ulong ms) { }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 绘制事件,每帧引发一次
        /// </summary>
        /// <param name="ms">自上一帧以来经过的毫秒数</param>
        public virtual void Draw(ulong ms) { }

        #endregion

    }
}

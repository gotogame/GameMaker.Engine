namespace GameMaker.Engine
{
    /// <summary>
    /// 地图
    /// </summary>
    public class Map
    {
        #region 属性

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.Black;

        /// <summary>
        /// 背景音乐名称
        /// </summary>
        public string BackgroundMusicName
        {
            get { return _backgroundMusicName; }
            set
            {
                _backgroundMusicName = value;
                if (Engine.CurrentMap == this)
                    MusicPlayer.PlayMusic(Assets.MusicAsset.Load(_backgroundMusicName), true);
            }
        }
        private string _backgroundMusicName;

        //----------------------------------------------------------------------------------------------------

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

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 地图大小
        /// </summary>
        [JsonIgnore]
        public Size Size { get { return CellSize * GridSize; } }

        /// <summary>
        /// 单元格大小
        /// </summary>
        public Size CellSize { get; set; }

        /// <summary>
        /// 网格大小
        /// </summary>
        public Size GridSize { get; set; }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 元素集合(!=null)
        /// </summary>
        [JsonConverter(typeof(JsonConverterElementList)), JsonInclude]
        public List<Element> Elements { get; private set; } = new List<Element>();

        #endregion

        #region 排序

        /// <summary>
        /// 获取已排序的元素集合
        /// (根据元素的深度值升序排序)
        /// (在排序结果中去除了null,NullElement与重复元素)
        /// </summary>
        /// <returns>元素集合(!=null)</returns>
        public Element[] GetSortedElements()
        {
            SortedSet<Element> elements = new SortedSet<Element>(new ElementComparer());

            foreach (Element element in Elements)
            {
                if (element != null && element is not NullElement)
                    elements.Add(element);
            }

            return elements.ToArray();
        }

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

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 地图坐标转网格坐标
        /// </summary>
        /// <param name="mapPoint">相对于地图的坐标</param>
        /// <returns>相对于网格的坐标(当单元格大小等于空时始终返回Point.Zero)</returns>
        public Point MapToGridPoint(Point mapPoint)
        {
            return CellSize.IsEmpty ? Point.Zero : new Point((int)Math.Floor(mapPoint.X / (double)CellSize.Width), (int)Math.Floor(mapPoint.Y / (double)CellSize.Height));
        }

        /// <summary>
        /// 网格坐标转地图坐标
        /// </summary>
        /// <param name="gridPoint">相对于网格的坐标</param>
        /// <returns>网格左上角相对于地图的坐标</returns>
        public Point GridToMapPoint(Point gridPoint)
        {
            return new Point(gridPoint.X * CellSize.Width, gridPoint.Y * CellSize.Height);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 游戏开始事件,游戏开始时引发一次
        /// </summary>
        public virtual void GameStart()
        {
            Element[] elements = GetSortedElements();
            foreach (Element element in elements)
            {
                element.GameStart();
            }
        }

        /// <summary>
        /// 游戏结束事件,游戏结束时引发一次
        /// </summary>
        public virtual void GameOver()
        {
            Element[] elements = GetSortedElements();
            foreach (Element element in elements)
            {
                element.GameOver();
            }
        }

        /// <summary>
        /// 地图加载事件,切换当前地图时引发一次
        /// </summary>
        public virtual void MapLoad()
        {
            Element[] elements = GetSortedElements();
            foreach (Element element in elements)
            {
                element.MapLoad();
            }

            //
            BackgroundMusicName = BackgroundMusicName;
            ViewSize = ViewSize.IsEmpty ? GameWindow.Size : ViewSize;
        }

        /// <summary>
        /// 地图卸载事件,切换当前地图时引发一次
        /// </summary>
        public virtual void MapUnload()
        {
            Element[] elements = GetSortedElements();
            foreach (Element element in elements)
            {
                element.MapUnload();
            }

            //
            MusicPlayer.Stop();
            SoundPlayer.StopAll();
            Assets.MusicAsset.Clear();
            Assets.SoundAsset.Clear();
            Assets.SpriteAsset.Clear();
            Assets.TextureAsset.Clear();
        }

        /// <summary>
        /// 帧事件,每帧引发一次
        /// </summary>
        /// <param name="ms">自上一帧以来经过的毫秒数</param>
        public virtual void Frame(ulong ms)
        {
            //更新
            Element[] elements = GetSortedElements();
            foreach (Element element in elements)
            {
                element.BeginUpdate(ms);
            }
            foreach (Element element in elements)
            {
                element.Update(ms);
            }
            foreach (Element element in elements)
            {
                element.EndUpdate(ms);
            }

            //绘制
            GameRenderer.Clear(BackgroundColor);

            Element[] elements_draw = GetSortedElements();
            for (int i = elements_draw.Length - 1; i >= 0; i--)
            {
                elements_draw[i].Draw(ms);
            }

            GameRenderer.Present();
        }

        #endregion

    }
}

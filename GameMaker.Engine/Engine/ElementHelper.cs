namespace GameMaker.Engine
{
    /// <summary>
    /// 元素助手
    /// </summary>
    public static partial class ElementHelper
    {
        #region 助手

        /// <summary>
        /// 获取当前地图(!=null)
        /// </summary>
        public static Map GetCurrentMap()
        {
            return Engine.CurrentMap;
        }

        /// <summary>
        /// 跳转到指定的地图
        /// </summary>
        /// <param name="mapName">地图名称</param>
        public static void GotoMap(string mapName)
        {
            Engine.NextMap = Assets.MapAsset.Deserialize(mapName);
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        public static void ExitGame()
        {
            Engine.NextExit = true;
        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 将当前地图的视野移动到指定的位置
        /// (视野矩形中心点与指定的位置对齐)
        /// (当视野矩形超出地图大小时自动对齐)
        /// </summary>
        /// <param name="mapPoint">相对于地图的坐标</param>
        public static void ViewMoveTo(Point mapPoint)
        {
            Map currentMap = Engine.CurrentMap;

            //视野矩形中心点对齐
            Rectangle mapRect = new Rectangle(Point.Zero, currentMap.Size);
            Rectangle viewRect = new Rectangle(new Point(mapPoint.X - currentMap.ViewSize.Width / 2, mapPoint.Y - currentMap.ViewSize.Height / 2), currentMap.ViewSize);

            //水平对齐
            if (viewRect.Width >= mapRect.Width)
            {
                viewRect.X = (mapRect.Width - viewRect.Width) / 2;
            }
            else if (viewRect.Left < mapRect.Left)
            {
                viewRect.X = 0;
            }
            else if (viewRect.Right > mapRect.Right)
            {
                viewRect.X = mapRect.Width - viewRect.Width;
            }

            //垂直对齐
            if (viewRect.Height >= mapRect.Height)
            {
                viewRect.Y = (mapRect.Height - viewRect.Height) / 2;
            }
            else if (viewRect.Top < mapRect.Top)
            {
                viewRect.Y = 0;
            }
            else if (viewRect.Bottom > mapRect.Bottom)
            {
                viewRect.Y = mapRect.Height - viewRect.Height;
            }

            //
            currentMap.ViewLocation = viewRect.Location;
        }

        #endregion

    }
}

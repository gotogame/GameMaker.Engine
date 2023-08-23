namespace GameMaker.Engine
{
    /// <summary>
    /// 精灵
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// 包围盒
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// 原点
        /// </summary>
        public Point Origin { get; set; }

        /// <summary>
        /// 标志
        /// </summary>
        public int Flag { get; set; }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 纹理名称
        /// </summary>
        public string TextureName { get; set; }

        /// <summary>
        /// 子图像数组
        /// </summary>
        public SpriteSubimage[] Subimages { get; set; }

    }
}

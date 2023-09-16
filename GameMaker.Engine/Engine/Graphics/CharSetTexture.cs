namespace GameMaker.Engine
{
    /// <summary>
    /// 字符集纹理
    /// </summary>
    public class CharSetTexture : Texture
    {
        #region 属性

        /// <summary>
        /// 字符包围盒集合(!=null)
        /// </summary>
        public Dictionary<char, Rectangle> CharBoundsDictionary { get; } = new Dictionary<char, Rectangle>();

        /// <summary>
        /// 是否已满
        /// </summary>
        public bool IsFull { get; set; }

        /// <summary>
        /// 当前添加字符纹理的位置
        /// </summary>
        private Point _position;

        private int _maxHeight;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认大小512x512
        /// </summary>
        public CharSetTexture() : base(512, 512) { }

        public CharSetTexture(int width, int height) : base(width, height) { }

        #endregion

        #region 方法

        /// <summary>
        /// 添加字符纹理
        /// (当字符集纹理已满时添加失败)
        /// (当字符纹理等于null时添加失败)
        /// </summary>
        /// <param name="chr">字符</param>
        /// <param name="charTexture">字符纹理</param>
        /// <returns>添加成功返回true,添加失败返回false</returns>
        public bool AddCharTexture(char chr, Texture charTexture)
        {
            if (IsFull)
                return false;

            if (charTexture == null)
                return false;

            Rectangle charBounds = new Rectangle(_position, charTexture.Bounds.Size);
            if (charBounds.Right > Width)
            {
                charBounds.X = 0;
                charBounds.Y += _maxHeight;
            }

            if (Bounds.Contains(charBounds))
            {
                Color[,] colors = charTexture.GetColors(charTexture.Bounds);
                SetColors(charBounds, colors);

                CharBoundsDictionary.Add(chr, charBounds);
                _position.X = charBounds.Right;
                _position.Y = charBounds.Y;

                if (charTexture.Height > _maxHeight)
                    _maxHeight = charTexture.Height;

                return true;
            }
            else
            {
                IsFull = true;
                return false;
            }
        }

        #endregion

    }
}

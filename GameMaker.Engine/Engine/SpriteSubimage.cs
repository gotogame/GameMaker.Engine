namespace GameMaker.Engine
{
    /// <summary>
    /// 精灵子图像
    /// </summary>
    [JsonConverter(typeof(JsonConverterSpriteSubimage))]
    public struct SpriteSubimage
    {
        #region 字段

        /// <summary>
        /// 包围盒X
        /// </summary>
        public short BoundsX;

        /// <summary>
        /// 包围盒Y
        /// </summary>
        public short BoundsY;

        /// <summary>
        /// 包围盒宽度
        /// </summary>
        public short BoundsWidth;

        /// <summary>
        /// 包围盒高度
        /// </summary>
        public short BoundsHeight;

        /// <summary>
        /// 原点X
        /// </summary>
        public short OriginX;

        /// <summary>
        /// 原点Y
        /// </summary>
        public short OriginY;

        /// <summary>
        /// 标志0
        /// </summary>
        public short Flag0;

        /// <summary>
        /// 标志1
        /// </summary>
        public short Flag1;

        #endregion

        #region 属性

        /// <summary>
        /// 包围盒(相对于精灵纹理)
        /// </summary>
        public Rectangle Bounds { get { return new Rectangle(BoundsX, BoundsY, BoundsWidth, BoundsHeight); } }

        /// <summary>
        /// 原点(相对于精灵子图像包围盒)
        /// </summary>
        public Point Origin { get { return new Point(OriginX, OriginY); } }

        #endregion

        #region 转换方法

        /// <summary>
        /// "BoundsX,BoundsY,BoundsWidth,BoundsHeight,OriginX,OriginY,Flag0,Flag1"
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool TryParse(string data, out SpriteSubimage subimage)
        {
            string[] values = data?.Split(',');

            if (values != null && values.Length == 8 &&
                short.TryParse(values[0], out short boundsX) &&
                short.TryParse(values[1], out short boundsY) &&
                short.TryParse(values[2], out short boundsWidth) &&
                short.TryParse(values[3], out short boundsHeight) &&
                short.TryParse(values[4], out short originX) &&
                short.TryParse(values[5], out short originY) &&
                short.TryParse(values[6], out short flag0) &&
                short.TryParse(values[7], out short flag1))
            {
                subimage = new SpriteSubimage()
                {
                    BoundsX = boundsX,
                    BoundsY = boundsY,
                    BoundsWidth = boundsWidth,
                    BoundsHeight = boundsHeight,
                    OriginX = originX,
                    OriginY = originY,
                    Flag0 = flag0,
                    Flag1 = flag1
                };
                return true;
            }
            else
            {
                subimage = new SpriteSubimage();
                return false;
            }
        }

        /// <summary>
        /// "BoundsX,BoundsY,BoundsWidth,BoundsHeight,OriginX,OriginY,Flag0,Flag1"
        /// </summary>
        public static SpriteSubimage Parse(string data)
        {
            if (TryParse(data, out SpriteSubimage subimage))
                return subimage;
            else
                throw new FormatException();
        }

        /// <summary>
        /// "BoundsX,BoundsY,BoundsWidth,BoundsHeight,OriginX,OriginY,Flag0,Flag1"
        /// </summary>
        public override string ToString()
        {
            return string.Join(',', BoundsX, BoundsY, BoundsWidth, BoundsHeight, OriginX, OriginY, Flag0, Flag1);
        }

        #endregion

    }

    #region Json转换器

    public class JsonConverterSpriteSubimage : JsonConverter<SpriteSubimage>
    {
        public override SpriteSubimage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return SpriteSubimage.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, SpriteSubimage value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    #endregion

}

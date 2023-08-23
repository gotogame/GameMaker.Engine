namespace GameMaker.Engine
{
    /// <summary>
    /// 元素比较器
    /// (根据元素的深度值升序排序)
    /// </summary>
    public class ElementComparer : IComparer<Element>
    {
        public int Compare(Element x, Element y)
        {
            if (x == null || y == null)
                return 0;

            if (x.Depth == y.Depth)
                return 1;

            return x.Depth < y.Depth ? -1 : 1;
        }
    }
}

namespace GameMaker.Engine
{
    /// <summary>
    /// 空元素
    /// </summary>
    public sealed class NullElement : Element
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">文本</param>
        internal NullElement(string text)
        {
            Text = text;
        }
    }
}

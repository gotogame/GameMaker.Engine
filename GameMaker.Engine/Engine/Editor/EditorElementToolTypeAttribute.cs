namespace GameMaker.Engine
{
    /// <summary>
    /// 编辑器元素工具类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EditorElementToolTypeAttribute : Attribute
    {
        /// <summary>
        /// 类型全名称
        /// </summary>
        public string FullName { get; }

        public EditorElementToolTypeAttribute(string fullName)
        {
            FullName = fullName;
        }
    }
}

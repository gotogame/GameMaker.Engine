namespace GameMaker.Engine
{
    public abstract partial class Element
    {
        /// <summary>
        /// 编辑器绘制事件
        /// </summary>
        /// <param name="graphics">用于绘制的图形对象</param>
        /// <param name="map">当前地图</param>
        /// <param name="selectedIndex">当前选中元素的索引</param>
        public virtual void EditorDraw(IEditorGraphics graphics, Map map, int selectedIndex) { }

    }
}

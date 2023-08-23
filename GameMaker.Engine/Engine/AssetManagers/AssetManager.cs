namespace GameMaker.Engine
{
    /// <summary>
    /// 资产管理器
    /// </summary>
    public class AssetManager<T> where T : class
    {
        /// <summary>
        /// 资产文件路径
        /// </summary>
        public string AssetPath { get; set; }

        /// <summary>
        /// 资产文件扩展名
        /// </summary>
        public string AssetExtensionName { get; set; }

        /// <summary>
        /// 缓存(!=null)
        /// </summary>
        private Dictionary<string, T> Cache { get; } = new Dictionary<string, T>();

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 加载指定的资产并缓存
        /// (当资产名称等于null或仅由空白字符组成时返回null)
        /// (当资产已在缓存中时直接返回该资产对象)
        /// (当资产不在缓存中时通过调用Deserialize方法获取资产对象,如果获取到的对象不等于null将缓存该对象)
        /// </summary>
        /// <param name="assetName">资产名称</param>
        /// <returns>成功返回资产对象,失败返回null</returns>
        public T Load(string assetName)
        {
            if (string.IsNullOrWhiteSpace(assetName))
                return null;

            if (Cache.ContainsKey(assetName))
                return Cache[assetName];

            T asset = Deserialize(assetName);
            if (asset != null)
                Cache.Add(assetName, asset);

            return asset;
        }

        /// <summary>
        /// 卸载指定的缓存
        /// (当资产名称等于null或仅由空白字符组成时无操作)
        /// </summary>
        /// <param name="assetName">资产名称</param>
        public void Unload(string assetName)
        {
            if (string.IsNullOrWhiteSpace(assetName))
                return;

            if (Cache.ContainsKey(assetName))
            {
                (Cache[assetName] as IDisposable)?.Dispose();
                Cache.Remove(assetName);
            }
        }

        /// <summary>
        /// 清除全部缓存
        /// </summary>
        public void Clear()
        {
            foreach (var asset in Cache)
            {
                (asset as IDisposable)?.Dispose();
            }
            Cache.Clear();
        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="assetName">资产名称</param>
        /// <returns>成功返回对象,失败返回null</returns>
        public virtual T Deserialize(string assetName)
        {
            return null;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="assetName">资产名称</param>
        /// <returns>成功返回true,失败返回false</returns>
        public virtual bool Serialize(T obj, string assetName)
        {
            return false;
        }

    }
}

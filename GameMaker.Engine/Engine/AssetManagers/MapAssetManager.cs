namespace GameMaker.Engine
{
    /// <summary>
    /// 地图资产管理器
    /// </summary>
    public class MapAssetManager : AssetManager<Map>
    {
        /// <summary>
        /// 该类型内部的序列化器默认使用该选项
        /// </summary>
        public static JsonSerializerOptions Options { get; set; } = new JsonSerializerOptions();

        public override bool Serialize(Map obj, string assetName)
        {
            try
            {
                if (obj == null)
                    return false;

                string fileFullPath = Path.IsPathRooted(assetName) ? assetName : Path.Combine(AssetPath, assetName + AssetExtensionName);
                string jsonString = JsonSerializer.Serialize<Map>(obj, Options);
                File.WriteAllText(fileFullPath, jsonString);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override Map Deserialize(string assetName)
        {
            try
            {
                string fileFullPath = Path.IsPathRooted(assetName) ? assetName : Path.Combine(AssetPath, assetName + AssetExtensionName);
                string jsonString = File.ReadAllText(fileFullPath);
                Map map = JsonSerializer.Deserialize<Map>(jsonString, Options);

                return map;
            }
            catch
            {
                return null;
            }
        }

    }
}

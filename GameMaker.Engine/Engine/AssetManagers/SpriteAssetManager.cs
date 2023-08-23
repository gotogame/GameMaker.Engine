namespace GameMaker.Engine
{
    /// <summary>
    /// 精灵资产管理器
    /// </summary>
    public class SpriteAssetManager : AssetManager<Sprite>
    {
        /// <summary>
        /// 该类型内部的序列化器默认使用该选项
        /// </summary>
        public static JsonSerializerOptions Options { get; set; } = new JsonSerializerOptions();

        public override bool Serialize(Sprite obj, string assetName)
        {
            try
            {
                if (obj == null)
                    return false;

                string fileFullPath = Path.IsPathRooted(assetName) ? assetName : Path.Combine(AssetPath, assetName + AssetExtensionName);
                string jsonString = JsonSerializer.Serialize<Sprite>(obj, Options);
                File.WriteAllText(fileFullPath, jsonString);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override Sprite Deserialize(string assetName)
        {
            try
            {
                string fileFullPath = Path.IsPathRooted(assetName) ? assetName : Path.Combine(AssetPath, assetName + AssetExtensionName);
                string jsonString = File.ReadAllText(fileFullPath);
                Sprite sprite = JsonSerializer.Deserialize<Sprite>(jsonString, Options);

                return sprite;
            }
            catch
            {
                return null;
            }
        }

    }
}

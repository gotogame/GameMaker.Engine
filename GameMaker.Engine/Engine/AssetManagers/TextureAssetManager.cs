namespace GameMaker.Engine
{
    /// <summary>
    /// 纹理资产管理器
    /// </summary>
    public class TextureAssetManager : AssetManager<Texture>
    {
        public override Texture Deserialize(string assetName)
        {
            try
            {
                string fileFullPath = Path.IsPathRooted(assetName) ? assetName : Path.Combine(AssetPath, assetName + AssetExtensionName);
                return File.Exists(fileFullPath) ? new Texture(fileFullPath) : null;
            }
            catch
            {
                return null;
            }
        }
    }
}

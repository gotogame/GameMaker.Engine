namespace GameMaker.Engine
{
    /// <summary>
    /// 声音资产管理器
    /// </summary>
    public class SoundAssetManager : AssetManager<Sound>
    {
        public override Sound Deserialize(string assetName)
        {
            try
            {
                string fileFullPath = Path.IsPathRooted(assetName) ? assetName : Path.Combine(AssetPath, assetName + AssetExtensionName);
                return File.Exists(fileFullPath) ? new Sound(fileFullPath) : null;
            }
            catch
            {
                return null;
            }
        }
    }
}

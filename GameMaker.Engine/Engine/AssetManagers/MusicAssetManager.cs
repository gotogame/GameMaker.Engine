namespace GameMaker.Engine
{
    /// <summary>
    /// 音乐资产管理器
    /// </summary>
    public class MusicAssetManager : AssetManager<Music>
    {
        public override Music Deserialize(string assetName)
        {
            try
            {
                string fileFullPath = Path.IsPathRooted(assetName) ? assetName : Path.Combine(AssetPath, assetName + AssetExtensionName);
                return File.Exists(fileFullPath) ? new Music(fileFullPath) : null;
            }
            catch
            {
                return null;
            }
        }
    }
}

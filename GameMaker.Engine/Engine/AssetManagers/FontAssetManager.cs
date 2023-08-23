namespace GameMaker.Engine
{
    /// <summary>
    /// 字体资产管理器
    /// </summary>
    public class FontAssetManager : AssetManager<Font>
    {
        public override Font Deserialize(string assetName)
        {
            try
            {
                string fileFullPath = Path.IsPathRooted(assetName) ? assetName : Path.Combine(AssetPath, assetName + AssetExtensionName);
                return File.Exists(fileFullPath) ? new Font(fileFullPath) : null;
            }
            catch
            {
                return null;
            }
        }
    }
}

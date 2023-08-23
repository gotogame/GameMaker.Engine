namespace GameMaker.Engine
{
    /// <summary>
    /// 资产管理器
    /// </summary>
    public static class Assets
    {
        /// <summary>
        /// 纹理(!=null)
        /// </summary>
        public static TextureAssetManager TextureAsset { get { return _textureAsset; } set { _textureAsset = value ?? throw new ArgumentNullException(); } }
        private static TextureAssetManager _textureAsset;

        /// <summary>
        /// 字体(!=null)
        /// </summary>
        public static FontAssetManager FontAsset { get { return _fontAsset; } set { _fontAsset = value ?? throw new ArgumentNullException(); } }
        private static FontAssetManager _fontAsset;

        /// <summary>
        /// 音乐(!=null)
        /// </summary>
        public static MusicAssetManager MusicAsset { get { return _musicAsset; } set { _musicAsset = value ?? throw new ArgumentNullException(); } }
        private static MusicAssetManager _musicAsset;

        /// <summary>
        /// 声音(!=null)
        /// </summary>
        public static SoundAssetManager SoundAsset { get { return _soundAsset; } set { _soundAsset = value ?? throw new ArgumentNullException(); } }
        private static SoundAssetManager _soundAsset;

        /// <summary>
        /// 精灵(!=null)
        /// </summary>
        public static SpriteAssetManager SpriteAsset { get { return _spriteAsset; } set { _spriteAsset = value ?? throw new ArgumentNullException(); } }
        private static SpriteAssetManager _spriteAsset;

        /// <summary>
        /// 地图(!=null)
        /// </summary>
        public static MapAssetManager MapAsset { get { return _mapAsset; } set { _mapAsset = value ?? throw new ArgumentNullException(); } }
        private static MapAssetManager _mapAsset;

        /// <summary>
        /// 脚本(!=null)
        /// </summary>
        public static ScriptAssetManager ScriptAsset { get { return _scriptAsset; } set { _scriptAsset = value ?? throw new ArgumentNullException(); } }
        private static ScriptAssetManager _scriptAsset;

        //----------------------------------------------------------------------------------------------------

        static Assets()
        {
            //查找资产根路径
            string currentDirectory = Directory.GetCurrentDirectory();
            string assetFolderName = "Assets";
            string assetRootPath = "";

            while (currentDirectory != null)
            {
                assetRootPath = Path.Combine(currentDirectory, assetFolderName);
                if (Directory.Exists(assetRootPath))
                    break;

                currentDirectory = Path.GetDirectoryName(currentDirectory);
            }

            //创建资产管理器
            TextureAsset = new TextureAssetManager() { AssetPath = Path.Combine(assetRootPath, "TextureAsset"), AssetExtensionName = ".png" };
            FontAsset = new FontAssetManager() { AssetPath = Path.Combine(assetRootPath, "FontAsset"), AssetExtensionName = ".ttf" };
            MusicAsset = new MusicAssetManager() { AssetPath = Path.Combine(assetRootPath, "MusicAsset"), AssetExtensionName = ".ogg" };
            SoundAsset = new SoundAssetManager() { AssetPath = Path.Combine(assetRootPath, "SoundAsset"), AssetExtensionName = ".wav" };
            SpriteAsset = new SpriteAssetManager() { AssetPath = Path.Combine(assetRootPath, "SpriteAsset"), AssetExtensionName = ".sprite" };
            MapAsset = new MapAssetManager() { AssetPath = Path.Combine(assetRootPath, "MapAsset"), AssetExtensionName = ".map" };
            ScriptAsset = new ScriptAssetManager() { AssetPath = Path.Combine(assetRootPath, "ScriptAsset"), AssetExtensionName = ".dll" };
        }
    }
}

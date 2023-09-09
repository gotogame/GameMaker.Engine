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
        private static TextureAssetManager _textureAsset = new TextureAssetManager() { AssetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "TextureAsset"), AssetExtensionName = ".png" };

        /// <summary>
        /// 字体(!=null)
        /// </summary>
        public static FontAssetManager FontAsset { get { return _fontAsset; } set { _fontAsset = value ?? throw new ArgumentNullException(); } }
        private static FontAssetManager _fontAsset = new FontAssetManager() { AssetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "FontAsset"), AssetExtensionName = ".ttf" };

        /// <summary>
        /// 音乐(!=null)
        /// </summary>
        public static MusicAssetManager MusicAsset { get { return _musicAsset; } set { _musicAsset = value ?? throw new ArgumentNullException(); } }
        private static MusicAssetManager _musicAsset = new MusicAssetManager() { AssetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "MusicAsset"), AssetExtensionName = ".ogg" };

        /// <summary>
        /// 声音(!=null)
        /// </summary>
        public static SoundAssetManager SoundAsset { get { return _soundAsset; } set { _soundAsset = value ?? throw new ArgumentNullException(); } }
        private static SoundAssetManager _soundAsset = new SoundAssetManager() { AssetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "SoundAsset"), AssetExtensionName = ".wav" };

        /// <summary>
        /// 精灵(!=null)
        /// </summary>
        public static SpriteAssetManager SpriteAsset { get { return _spriteAsset; } set { _spriteAsset = value ?? throw new ArgumentNullException(); } }
        private static SpriteAssetManager _spriteAsset = new SpriteAssetManager() { AssetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "SpriteAsset"), AssetExtensionName = ".sprite" };

        /// <summary>
        /// 地图(!=null)
        /// </summary>
        public static MapAssetManager MapAsset { get { return _mapAsset; } set { _mapAsset = value ?? throw new ArgumentNullException(); } }
        private static MapAssetManager _mapAsset = new MapAssetManager() { AssetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "MapAsset"), AssetExtensionName = ".map" };

    }
}

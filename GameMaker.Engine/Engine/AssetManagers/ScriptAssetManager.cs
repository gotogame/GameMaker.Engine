namespace GameMaker.Engine
{
    /// <summary>
    /// 脚本资产管理器
    /// </summary>
    public class ScriptAssetManager
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
        /// 脚本程序集加载上下文
        /// </summary>
        public ScriptAssemblyLoadContext ScriptALC { get; set; }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 加载全部
        /// </summary>
        public void LoadAll()
        {
            try
            {
                if (ScriptALC == null)
                {
                    ScriptALC = new ScriptAssemblyLoadContext();

                    foreach (string scriptFile in Directory.GetFiles(AssetPath, "*" + AssetExtensionName))
                    {
                        ScriptALC.LoadFromAssemblyPath(scriptFile);
                    }
                }
            }
            catch { }
        }
    }
}

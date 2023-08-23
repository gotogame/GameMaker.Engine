namespace GameMaker.Engine
{
    /// <summary>
    /// 脚本程序集加载上下文
    /// </summary>
    public class ScriptAssemblyLoadContext : AssemblyLoadContext
    {
        public ScriptAssemblyLoadContext() : base("ScriptALC", true) { }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}

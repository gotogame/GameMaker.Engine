global using GameMaker.Engine;

namespace GameMakerEngineTestWindows
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game.Init();
                Game.CreateWindowAndRenderer();
                Audio.OpenAudio();
                Assets.ScriptAsset.LoadAll();

                Map map = new Map();
                map.Elements.Add(new Element1());

                Engine.Run(map);
            }
            finally
            {
                Audio.CloseAudio();
                Game.DestroyWindowAndRenderer();
                Game.Quit();
            }
        }
    }
}

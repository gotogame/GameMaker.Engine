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

                Engine.Run(new TestMap());
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

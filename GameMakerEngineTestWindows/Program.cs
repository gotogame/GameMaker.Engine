global using GameMaker.Engine;

namespace GameMakerEngineTestWindows
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Game1())
            {
                game.Run();
            }
        }
    }
}

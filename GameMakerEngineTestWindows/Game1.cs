using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMakerEngineTestWindows
{
    public class Game1 : Game
    {
        Font font0;
        Texture texture0;

        Point point = new Point(300, 300);
        double angle;
        int scale = 1;

        public override void GameStart()
        {
            //base.GameStart();

            font0 = new Font(@"D:\CSharp\Assets\FontAsset\字体0.ttf");
            texture0 = new Texture(@"D:\CSharp\Assets\TextureAsset\瓷砖0.png");
        }

        public override void GameOver()
        {
            //base.GameOver();

            font0?.Dispose();
            texture0?.Dispose();
        }

        public override void Frame(ulong ms)
        {
            //base.Frame(ms);

            if (KeyboardState.IsPressed(Key.Up))
            {
                point.Y -= 2;
            }
            if (KeyboardState.IsPressed(Key.Down))
            {
                point.Y += 2;
            }
            if (KeyboardState.IsPressed(Key.Left))
            {
                point.X -= 2;
            }
            if (KeyboardState.IsPressed(Key.Right))
            {
                point.X += 2;
            }

            if (KeyboardState.IsPressed(Key.NumPadAdd))
            {
                scale += 1;
            }
            if (KeyboardState.IsPressed(Key.NumPadSubtract))
            {
                scale -= 1;
            }
            if (KeyboardState.IsPressed(Key.NumPad4))
            {
                angle -= 1;
            }
            if (KeyboardState.IsPressed(Key.NumPad6))
            {
                angle += 1;
            }

            //
            GameRenderer.Clear(Color.Blue);

            GameRenderer.DrawString(font0, 20, $"FPS: {Game.GetFPS()}", new Point(0, 0), Color.White, 0);
            GameRenderer.DrawString(font0, 20, $"鼠标位置: {MouseState.CurrentMousePosition}", new Point(0, 48), Color.White, 0);
            GameRenderer.DrawString(font0, 20, $"位置: {point}; 旋转:{angle}; 缩放:{scale}", new Point(0, 96), Color.White, 0);


            GameRenderer.DrawTexture(texture0, texture0.Bounds, new Rectangle(point, texture0.Bounds.Size * scale), Color.White, angle, Point.Zero, false, false);

            GameRenderer.Present();
        }
    }
}

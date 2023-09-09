using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMakerEngineTestWindows
{
    public class TestMap : Map
    {
        int index;
        Point point = new Point(300, 300);
        double angle;
        int scale = 1;

        public override void GameStart()
        {
            //base.GameStart();

            Assets.TextureAsset.AssetPath = @"D:\CSharp\Assets\TextureAsset";
            Assets.FontAsset.AssetPath = @"D:\CSharp\Assets\FontAsset";
            Assets.MusicAsset.AssetPath = @"D:\CSharp\Assets\MusicAsset";
            Assets.SoundAsset.AssetPath = @"D:\CSharp\Assets\SoundAsset";
            Assets.SpriteAsset.AssetPath = @"D:\CSharp\Assets\SpriteAsset";
            Assets.MapAsset.AssetPath = @"D:\CSharp\Assets\MapAsset";
        }

        public override void Frame(ulong ms)
        {
            //base.Frame(ms);

            if (KeyboardState.IsPressed(Key.W))
            {
                ViewLocation += new Point(0, -2);
            }
            if (KeyboardState.IsPressed(Key.S))
            {
                ViewLocation += new Point(0, 2);
            }
            if (KeyboardState.IsPressed(Key.A))
            {
                ViewLocation += new Point(-2, 0);
            }
            if (KeyboardState.IsPressed(Key.D))
            {
                ViewLocation += new Point(2, 0);
            }

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

            if (KeyboardState.IsDown(Key.NumPad1))
            {
                index -= 1;
            }
            if (KeyboardState.IsDown(Key.NumPad2))
            {
                index += 1;
            }

            //绘制
            GameRenderer.Clear(Color.Blue);

            MapGraphics.DrawString("字体0", 20, $"FPS: {Engine.GetFPS()}", new Point(0, 0), Color.White, 0);
            MapGraphics.DrawString("字体0", 20, $"鼠标位置: {MouseState.CurrentMousePosition}", new Point(0, 48), Color.White, 0);
            MapGraphics.DrawString("字体0", 20, $"索引:{index}; 位置: {point}; 旋转:{angle}; 缩放:{scale}", new Point(0, 96), Color.White, 0);


            MapGraphics.DrawSprite("精灵1", index, point, Color.White, angle, scale, scale, false, false);

            GameRenderer.Present();
        }

    }
}

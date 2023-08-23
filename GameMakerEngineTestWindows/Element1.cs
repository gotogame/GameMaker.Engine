using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMakerEngineTestWindows
{
    public class Element1 : Element
    {
        Point point;

        public override void MapLoad()
        {
            base.MapLoad();
        }

        public override void MapUnload()
        {
            base.MapUnload();
        }

        public override void Update(ulong ms)
        {
            base.Update(ms);

            if (KeyboardState.IsPressed(Key.Left))
            {
                point.X -= 2;
            }

            if (KeyboardState.IsPressed(Key.Right))
            {
                point.X += 2;
            }
        }

        public override void Draw(ulong ms)
        {
            base.Draw(ms);

            ElementHelper.DrawSprite("精灵1", 0, point, Color.White);
            ElementHelper.DrawString("字体0", 22, $"FPS:{Engine.GetFPS()}", new Point(0, 0), Color.White, 0);
        }
    }
}

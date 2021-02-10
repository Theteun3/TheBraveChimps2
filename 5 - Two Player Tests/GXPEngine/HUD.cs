using System;
using System.Drawing;
using GXPEngine;

class HUD : EasyDraw
{
    Font _font = new Font("Verdana", 16, FontStyle.Regular);

    public HUD() : base(MyGame.main.width, MyGame.main.height, false)
    {

    }

    private void Update()
    {
        width = (int) (game.width / game.scale);
        height = (int)(game.width / game.scale);

        graphics.Clear(Color.Empty);
        graphics.DrawString("FPS: " + (int) game.currentFps, _font, new SolidBrush(Color.Black), (int) (game.width * .85f), 0);
        
    }
}


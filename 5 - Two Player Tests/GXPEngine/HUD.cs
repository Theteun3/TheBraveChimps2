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
        scale = 1/game.scale;

        graphics.Clear(Color.Empty);
        graphics.DrawString("FPS: " + (int) game.currentFps, _font, new SolidBrush(Color.Black), (int) (game.width * .85f), 0);
        graphics.DrawString("TIME LEFT: " + (int)((MyGame)game).gameTime, _font, new SolidBrush(Color.Black), (int)(0), 0);

    }
}


using System;
using System.Drawing;
using GXPEngine;

class HUD : EasyDraw
{
    Font _font = new Font("Stay and Shine", 16, FontStyle.Regular);
    Font _bigFont = new Font("Stay and Shine", 40, FontStyle.Regular);



    public HUD() : base(MyGame.main.width, MyGame.main.height, false)
    {

    }

    private void Update()
    {
        scale = 1/game.scale;

        graphics.Clear(Color.Empty);
        graphics.DrawString("FPS: " + (int) game.currentFps, _font, new SolidBrush(Color.Cyan), (int) (game.width * .85f), 0);
        if ((int)((MyGame)game).gameTime > 0 )graphics.DrawString("TIME: " + Math.Round(((MyGame)game).gameTime,2), _font, new SolidBrush(Color.Cyan), (int)(0), 0);
        if (((MyGame)game).isFinished) graphics.DrawString("PLAYER " + ((MyGame)game).winner + " HAS WON", _bigFont, new SolidBrush(Color.Red), width / 2 - 200, height / 3);
        if (((MyGame)game).isFinished) graphics.DrawString("WITH A TIME OF " + Math.Round(((MyGame)game).gameTime, 2), _bigFont, new SolidBrush(Color.Red), width / 2 - 200, height / 2.5f);
    }
}


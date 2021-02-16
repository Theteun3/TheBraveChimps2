using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using GXPEngine;

class HUD : EasyDraw
{
    PrivateFontCollection pfc = new PrivateFontCollection();

    Font _font;
    Font _bigFont;

    public HUD() : base(MyGame.main.width, MyGame.main.height, false)
    {
        pfc.AddFontFile("StayAndShine.ttf");
        System.Drawing.Font font = new Font(pfc.Families[0], 16);
        System.Drawing.Font bigfont = new Font(pfc.Families[0], 36);
        _font = font;
        _bigFont = bigfont;
    }

    private void Update()
    {
        scale = 1/game.scale;

        graphics.Clear(Color.Empty);
        graphics.DrawString("FPS: " + (int) game.currentFps, _font, new SolidBrush(Color.Cyan), (int) (game.width * .85f), 0);
        if ((int)((MyGame)game).gameTime > 0 )graphics.DrawString("TIME: " + Math.Round(((MyGame)game).gameTime,2), _font, new SolidBrush(Color.Cyan), (int)(0), 0);
        if (((MyGame)game).isFinished) graphics.DrawString("PLAYER " + ((MyGame)game).winner + " HAS WON", _bigFont, new SolidBrush(Color.Red), width / 2 - 200, height / 3);
        if (((MyGame)game).isFinished) graphics.DrawString("WITH A TIME OF " + Math.Round(((MyGame)game).gameTime, 2), _bigFont, new SolidBrush(Color.Red), width / 2 - 200, height / 2.5f);
        if (((MyGame)game).getCurrentLevel == 5)
        {
            TextFont(_bigFont);
            TextAlign(CenterMode.Center, CenterMode.Center);
            Fill(Color.Cyan);
            Text(((MyGame)game).tutorialText, game.width/2, game.height*.8f);
            if (((MyGame)game).tutorialText == "SHOOT LEVERS AND TRAPS TO ACTIVATE THEM. REACH THE END TO FINISH THE LEVEL!")
                Text("DO NOT FALL BEHIND, OR YOU WILL BE RESORTED TO USE PRECIOUS FUEL", game.width / 2, game.height * .85f);
        }

        ShapeAlign(CenterMode.Min, CenterMode.Min);
        StrokeWeight(5);
        Stroke(Color.DarkCyan);
        Fill(Color.Cyan);
        Rect(game.width / 40, game.height / 20, 4 * 50, 50);
        Fill(Color.LightCyan);
        Rect(game.width / 40, game.height / 20, 4 * 50, 50);

        Stroke(Color.DeepPink);
        Fill(Color.Magenta);
        Rect(game.width * .875f, game.height / 20, 4 * 50, 50);
        Fill(Color.HotPink);
        Rect(game.width *.875f, game.height / 20, 4 * 50, 50);
    }


   
}


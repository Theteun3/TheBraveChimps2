using System;
using System.Drawing;
using System.Drawing.Text;
using GXPEngine;
using TiledMapParser;



class Button : EasyDraw
{
    PrivateFontCollection pfc = new PrivateFontCollection();

    private Sound _menuClick = new Sound("Sounds/SFX/MenuClick.wav");
    private Sound _menuHover = new Sound("Sounds/SFX/MenuHover.wav");

    private bool _hasPlayedClickSound;
    private bool _hasPlayedHoverSound;

    public Font drawFont;
    public string displayText;

    public Button() : base(256, 64)
    {
        pfc.AddFontFile("StayAndShine.ttf");
        System.Drawing.Font bigfont = new Font(pfc.Families[0], 36);
        drawFont = bigfont;
    }

    public void Sounds()
    {
        if (IsClicked())
        {
            if (!_hasPlayedClickSound)
            {
                _menuClick.Play();
                _hasPlayedClickSound = true;
            }
        }

        if (IsHover())
        {
            if (!_hasPlayedHoverSound)
            {
                _menuHover.Play(false, 0, 2);
                _hasPlayedHoverSound = true;
            }
        }
        else _hasPlayedHoverSound = false;
    }

    public void setString(string str)
    {
        displayText = str;
    }

    public bool IsClicked()
    {
        return HitTestPoint(Input.mouseX, Input.mouseY) && Input.GetMouseButtonUp(0);
    }

    public bool IsHover()
    {
        return HitTestPoint(Input.mouseX, Input.mouseY);
    }


}



class LevelButton : Button
{
    
    

    private string _toLevel;
    private int _toLevelNum;

    public LevelButton(TiledObject obj) : base()
    {
        _toLevel = obj.GetStringProperty("Level");
        _toLevelNum = obj.GetIntProperty("LevelNum", 0);
        setString(obj.GetStringProperty("DisplayString"));
    }

    private void Update()
    {
        Sounds();

        if (IsClicked()) onClickEvent();

        graphics.Clear(Color.Empty);
        if (IsHover())
        {
            graphics.DrawString(displayText, drawFont, new SolidBrush(Color.CadetBlue), 0, 0);
            
        }
        else
        {
            graphics.DrawString(displayText, drawFont, new SolidBrush(Color.Cyan), 0, 0);
            
        }
    }

    private void onClickEvent()
    {
       ((MyGame)game).gameTime = 0;
       ((MyGame)game).LoadLevel(_toLevelNum);
        ((MyGame)game).getCurrentLevel = _toLevelNum;
        ((MyGame)game).isFinished = false;
       ((MyGame)game).winner = null;
    }

    
}


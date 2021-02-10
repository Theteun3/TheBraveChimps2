using System;
using System.Drawing;
using GXPEngine;
using TiledMapParser;



class Button : EasyDraw
{
    public Font drawFont = new Font("Stay and Shine", 36, FontStyle.Regular);
    public string displayText;

    public Button() : base(256, 64)
    {

    }

    private void Update()
    {

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

    public LevelButton(TiledObject obj) : base()
    {
        _toLevel = obj.GetStringProperty("Level");
        setString(obj.GetStringProperty("DisplayString"));
    }

    private void Update()
    {
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
        ((MyGame)game).LoadLevel(_toLevel);
    }

    
}


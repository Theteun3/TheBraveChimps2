using System;
using GXPEngine;
using TiledMapParser;


class Finish : Sprite
{
    Player _player1;
    Player _player2;

    private int _goToLevel;

    public Finish(TiledObject obj) : base("SpriteSheets/PlayerCollisionT.png", false, false)
    {
        SetOrigin(0, 0);
        _goToLevel = obj.GetIntProperty("ToLevel", 0);
    }

    public void AddPlayer(Player player1)
    {
        _player1 = player1;
    }

    public void AddPlayer(Player player1, Player player2)
    {
        _player1 = player1;
        _player2 = player2;
    }

    private void Update()
    {
        if (_player1 != null)
        {
            if (checkOverlap(_player1))
            {
                Console.WriteLine("Player one has Won!");
                if (_goToLevel == 0 && ((MyGame)game).getCurrentLevel != 5)
                {
                    ((MyGame)game).isFinished = true;
                    ((MyGame)game).winner = "ONE";
                }
                ((MyGame)game).LoadLevel(_goToLevel);
            }
        }
        
        if (_player2 != null)
        {
            if (checkOverlap(_player2))
            {
                Console.WriteLine("Player two has Won!");
                if (_goToLevel == 0 && ((MyGame)game).getCurrentLevel != 5)
                {
                    ((MyGame)game).isFinished = true;
                    ((MyGame)game).winner = "TWO";
                }
                ((MyGame)game).LoadLevel(_goToLevel);
            }
        }
    }

    private bool checkOverlap(GameObject other)
    {
        return other.x > x - width && other.x < x + width && other.y > y && other.y < y + height;
    }

}


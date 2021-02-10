using System;
using GXPEngine;
using TiledMapParser;


class Level : GameObject
{
    private string _levelName;
    private Player1 _player1;
    private Player2 _player2;



    public Level(string levelName) : base()
    {
        _levelName = levelName;
        generateLevel(_levelName);

    }

    private void generateLevel(string levelName)
    {
        TiledLoader level = new TiledLoader(levelName, this, false);
        level.OnObjectCreated += Level_OnObjectCreated;
        level.autoInstance = true;

        level.LoadTileLayers(0);
        level.addColliders = true;
        for (int layers = 1; layers < level.NumTileLayers-1; layers++)
        {
            level.LoadTileLayers(layers);
        }
        level.LoadObjectGroups();
        level.addColliders = false;
        level.LoadTileLayers(level.NumTileLayers-1);
    }

    private void Level_OnObjectCreated(Sprite sprite, TiledObject obj)
    {
        if (sprite is Player1 player1)
        {
            _player1 = player1;
            _player1.AddParents(this);
        }
        if (sprite is Player2 player2)
        {
            _player2 = player2;
            player2.AddParents(this);
        }
    }

    private void Update()
    {
        if (_player1 != null)
        {
            if (_player2 != null)
            {
                float frontPlayerX = -Math.Max(_player1.x, _player2.x) + game.width / 2 / game.scale;
                x = Mathf.Clamp(frontPlayerX, -5760 + (game.width / game.scale), 0);
            }
            else x = -_player1.x + game.width / 2 / game.scale;
            y = Mathf.Clamp(-_player1.y + game.height / 2 / game.scale, -500, 0);
        }


        
    }

    public float PlayerDistance()
    {
        float distance = Math.Abs(_player1.x - _player2.x) / 12000;
        float scale = 0.5f - distance;

        if (_player1 != null && _player2 != null && scale > 0.4f) return scale;
        else if (_player1 != null && _player2 != null) return 0.4f;
        else if (_player1 != null) return 0.5f;
        else return .6f;
    }


    public void CreateRocket(Player p, int direction)
    {
        float side;
        if (direction < 0) side = p.x - p.width;
        else side = p.x + p.width;
        Rocket rocket = new Rocket(side, p.y - p.height / 2, p, direction);
        AddChild(rocket);
    }
}


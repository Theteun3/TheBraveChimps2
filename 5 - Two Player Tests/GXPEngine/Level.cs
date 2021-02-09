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
        }
        if (sprite is Player2 player2)
        {
            _player2 = player2;
        }
    }

    private void Update()
    {
        if (_player1 != null)
        {
            if (_player2 != null) x = -Math.Max(_player1.x, _player2.x) + game.width;
            else x = -_player1.x + game.width / 2;
            y = Mathf.Clamp(-_player1.y + game.height / 2, -600, 0);
        }
    }
}


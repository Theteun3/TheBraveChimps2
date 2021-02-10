﻿using System;
using GXPEngine;
using TiledMapParser;


class Level : GameObject
{
    private string _levelName;
    private Player1 _player1;
    private Player2 _player2;

    public int time;

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
        for (int layers = 1; layers < level.NumTileLayers - 1; layers++)
        {
            level.LoadTileLayers(layers);
        }
        level.LoadObjectGroups();
        level.addColliders = false;
        level.LoadTileLayers(level.NumTileLayers - 1);
    }

    private void Level_OnObjectCreated(Sprite sprite, TiledObject obj)
    {
        if (sprite is Player1 player1)
        {
            _player1 = player1;
            _player1.AddParents(this);
            time = _player1.gameTime();
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
            moveCamera();
            if (_player2 != null)
            {
                killPlayer();
            }
        }
    }

    private void moveCamera()
    {
        if (_player2 != null)
        {
            float frontPlayerX = -Math.Max(_player1.x, _player2.x) + game.width / 2 / game.scale;
            x = Mathf.Clamp(frontPlayerX, -12800 + (game.width / game.scale), 0);
        }
        else x = Mathf.Clamp(-_player1.x + game.width / 2 / game.scale, -19200 + (game.width / game.scale), 0);
        y = Mathf.Clamp(-_player1.y + game.height / 2 / game.scale, -500, 0);
    }

    public float PlayerDistance()
    {
        if (_player1 != null && _player2 != null)
        {
            float distance = Math.Abs(_player1.x - _player2.x) / 12000;
            float scale = 0.5f - distance;


            if (scale > 0.4f) return scale;
            else return 0.4f;
        }
        else if (_player1 != null) return 0.5f;
        else return .6f;
    }

    private void killPlayer()
    {
        if (PlayerDistance() == 0.4f)
        {
            if (_player1.x > _player2.x)
            {
                _player2.TakeDamage();
                _player2.x = _player1.x - _player1.width * 2;
                _player2.y = _player1.y;
            }
            else
            {
                _player1.TakeDamage();
                _player1.x = _player2.x - _player2.width * 2;
                _player1.y = _player2.y;
            }
        }
    }


    public void CreateRocket(Player p, int direction)
    {
        float side;
        if (direction < 0) side = p.x - p.width * 2;
        else side = p.x + p.width * 2;
        Rocket rocket = new Rocket(side, p.y - p.height / 2, p, direction);
        AddChild(rocket);
    }

    public int totalGameTime()
    {
        return time;
    }
}


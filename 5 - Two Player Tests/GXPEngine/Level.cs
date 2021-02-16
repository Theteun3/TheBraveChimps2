using System;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.OpenGL;
using TiledMapParser;


class Level : GameObject
{
    private string _levelName;
    private Player1 _player1;
    private Player2 _player2;

    public List<Sprite> _tileObjects = new List<Sprite>();
    private List<Lever> _levers = new List<Lever>();
    private List<Trapdoor> _doors= new List<Trapdoor>();

    private const int REVIVE_SPEED = 4;

    public int time;

    public bool _isPlayer1MovingToOtherPlayer;
    public bool _isPlayer2MovingToOtherPlayer;
    private bool _isTrapsInitialized;

    private bool _hasPlayedSound;

    public Level(string levelName) : base()
    {
        _levelName = levelName;
        generateLevel(_levelName);

    }

    private void generateLevel(string levelName)
    {
        TiledLoader level = new TiledLoader(levelName, this, false);
        level.OnObjectCreated += Level_OnObjectCreated;
        level.OnTileCreated += Level_OnTileCreated;
        level.autoInstance = true;
        

        level.LoadTileLayers(0);

        
        level.LoadImageLayers();
        level.addColliders = true;
        for (int layers = 1; layers < level.NumTileLayers - 1; layers++)
        {
            level.LoadTileLayers(layers);
        }
        level.LoadObjectGroups();
        level.addColliders = false;
        level.LoadTileLayers(level.NumTileLayers - 1);


    }

    private void Level_OnTileCreated(AnimationSprite sprite)
    {
        _tileObjects.Add(sprite);
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

        if (sprite is Finish finish && _player1 != null)
        {
            if (_player2 != null) finish.AddPlayer(_player1, _player2);
            else finish.AddPlayer(_player1);
        }

        if (sprite is Lever lever)
        {
            _levers.Add(lever);
        }

        if (sprite is Trapdoor trapdoor)
        {
            _doors.Add(trapdoor);
            trapdoor.addLevel(this);
            _tileObjects.Add(trapdoor);
        }
    }

    private void Update()
    {
        if (_player1 != null)
        {
            if (_player1.moveCamera) moveCamera();
            if (_player2 != null)
            {
                if (_player2.isTutorial) handleTutorialLevel();
                killPlayer();
                boostLastPlayer();
            }

            
        }
        else
        {
            GL.glfwEnable(GL.GLFW_MOUSE_CURSOR);
        }

        if (!_isTrapsInitialized)
        {
            for (int i = 0; i < _levers.Count; i++)
            {
                if (_levers.ToArray()[i].id == _doors.ToArray()[i].id)
                {
                    _levers.ToArray()[i].addDoor(_doors.ToArray()[i]);
                }
            }

            _isTrapsInitialized = true;
        }
    }

    private void moveCamera()
    {
        if (_player2 != null)
        {
            float frontPlayerX = -Math.Max(_player1.x, _player2.x) + game.width / 2 / game.scale;
            x = Mathf.Clamp(frontPlayerX, -19200 + (game.width / game.scale), 0);
        }
        else x = Mathf.Clamp(-_player1.x + game.width / 2 / game.scale, -19200 + (game.width / game.scale), 0);
        y = Mathf.Clamp(-_player1.y + game.height / 2 / game.scale, -100, 500);
    }

    public float PlayerDistance()
    {
        if (_player1 != null && _player2 != null)
        {
            float distance = Math.Abs(_player1.x - _player2.x) / 36000;
            float scale = 0.5f - distance;
            if (scale > 0.475f) return 0.475f;
            if (scale > 0.4f) return scale;
            else return 0.4f;
        }
        else if (_player1 != null) return 0.5f;
        else return 1f;
    }

    private void killPlayer()
    {
        if (Math.Abs(_player1.x - _player2.x) > game.width / game.scale / 2)
        {
            if (_player1.x > _player2.x)
            {
                _player2.TakeDamage();
                _isPlayer2MovingToOtherPlayer = true;
                
            }
            else
            {
                _player1.TakeDamage();
                _isPlayer1MovingToOtherPlayer = true;
            }
            _hasPlayedSound = false;
        }

        if (_isPlayer1MovingToOtherPlayer)
        {
            _player1.x += REVIVE_SPEED * Time.deltaTime;
            _player1.y = _player2.y;
            _player1.scaleX = 1;
            if (!_hasPlayedSound)
            {
                _player1.boosting.Play();
                _hasPlayedSound = true;
            }
        }
        else if (_isPlayer2MovingToOtherPlayer)
        {
            _player2.x += REVIVE_SPEED * Time.deltaTime;
            _player2.y = _player1.y;
            _player2.scaleX = 1;
            if (!_hasPlayedSound)
            {
                _player2.boosting.Play();
                _hasPlayedSound = true;
            }
        }

        if (Math.Abs(_player1.x - _player2.x) < 300 )
        {
            _isPlayer1MovingToOtherPlayer = false;
            _isPlayer2MovingToOtherPlayer = false;
        }
    }

    private void boostLastPlayer()
    {
        if (_player1.x > _player2.x)
        {
            _player1.boostSpeed = 1f;
            _player2.boostSpeed = 1.05f;
        }
        else
        {
            _player1.boostSpeed = 1.05f;
            _player2.boostSpeed = 1f;
        }
    }

    public string handleTutorialLevel()
    {
        if (_player1 != null && _player2 != null) 
        {
            if (_player1.isTutorial)
            {
                if (_player1._tutorialState == Player.TutorialState.RUNNING) 
                    return "PLAYER ONE CAN MOVE WITH A & D, PLAYER TWO WITH LEFT & RIGHT";
                if (_player1._tutorialState == Player.TutorialState.JUMPING)
                    return "PLAYER ONE CAN JUMP WITH W, PLAYER TWO WITH UP";
                if (_player1._tutorialState == Player.TutorialState.SHOOTING)
                    return "PLAYER ONE CAN SHOOT WITH SPACE, PLAYER TWO WITH LEFT MOUSE CLICK";
                if (_player1._tutorialState == Player.TutorialState.LEVER)
                    return "SHOOT LEVERS AND TRAPS TO ACTIVATE THEM. REACH THE END TO FINISH THE LEVEL!";
                else return "";
            }
            else return "";
        }
        else return "";
    }

    public void CreateRocket(Player p, int direction)
    {
        float side;
        if (direction < 0) side = p.x - p.width * 2;
        else side = p.x + p.width * 2;
        Rocket rocket = new Rocket(side, p.y - p.height / 2, p, direction, this);
        AddChild(rocket);
    }

    public int totalGameTime()
    {
        return time;
    }

    public List<Sprite> getLevelColliders(Sprite subject)
    {
        List<Sprite> query = new List<Sprite>();
        foreach (Sprite other in _tileObjects)
        {
            float deltaX = Mathf.Abs(other.x - subject.x);
            if (deltaX < 200)
            {
                query.Add(other);
            }
        }
        return query;
    }

    public float playerSpeed()
    {
        if (_player1 != null)
        {
            if (_player2 != null && _player2.runSpeed > _player1.runSpeed && _player2.runSpeed > 2.5f) return _player2.runSpeed / 12;
            else if (_player1.runSpeed > 2.5f) return _player1.runSpeed / 12;
            else return 0;
        }
        else return 0;

    }
}


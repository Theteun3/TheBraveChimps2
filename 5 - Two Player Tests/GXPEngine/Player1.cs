using System;
using GXPEngine;
using TiledMapParser;
class Player1 : Player
{

    private AnimationSprite _graphics;

    private float _rocketTimer;
    private int _rocketCooldown = 1;

    private int _time;

    public Player1(TiledObject obj) : base()
    {
        _time = obj.GetIntProperty("Time", 100);
        _graphics = new AnimationSprite("SpriteSheets/Character1.png", 8, 3, -1, false, false);
        _graphics.scaleX = 1;
        _graphics.x = -width;
        _graphics.y = -height * 1.25f;
        AddChild(_graphics);
    }

    private void Update()
    {
        float oldX = x;
        handleJump();
        if (!level._isPlayer1MovingToOtherPlayer) HandlePlayerStuff();
        handleInput();
        handleShooting();
        handleAnimation();
        float deltaX = x - oldX;

        if (deltaX == 0) runSpeed = 1;
    }

    private void handleAnimation()
    {
        switch (playerState)
        {
            case State.IDLE:
                _graphics.SetCycle(0, 1);
                break;

            case State.WALKING:
                _graphics.SetCycle(0, 8, 8);
                break;

            case State.RUNNING:
                _graphics.SetCycle(8, 8, (byte)(16 / runSpeed));
                break;

            case State.JUMPING:
                if (_deltaY < 0) _graphics.SetCycle(18, 1);
                if (_deltaY > 0) _graphics.SetCycle(19, 1);
                break;
        }
        _graphics.Animate();
    }

    

    private void handleShooting()
    {
        int rot;
        if (scaleX > 0) rot = 0;
        else rot = -180;


        if (Input.GetKeyDown(Key.SPACE) && _rocketTimer <= 0)
        {
            _rocketTimer = _rocketCooldown;
            level.CreateRocket(this, rot);
        }

        if (_rocketTimer > 0) _rocketTimer -= .032f; ;
    }

    private void handleInput()
    {


        if (Input.GetKeyDown(Key.D) || Input.GetKeyUp(Key.D))
        {
            runSpeed = 1;
        }

        if (Input.GetKey(Key.A))
        {
            speedX = -MOVEMENTSPEED;
        }

        if (Input.GetKey(Key.D))
        {
            speedX = MOVEMENTSPEED;
            if (runSpeed < 3) runSpeed += .015f;
        }


    }

    private void handleJump()
    {
        if (Input.GetKeyDown(Key.W) && !isJumping && isGrounded)
        {
            isJumping = true;
            
        }

        if (speedY > JUMPHEIGHT)
        {
            if (isJumping) speedY -= JUMPFORCE;
            isGrounded = false;
        }
        else isJumping = false;
    }

    public int gameTime()
    {
        return _time;
    }


    public void TakeDamage()
    {
        _health--;

        if (_health <= 0)
        {
            Console.WriteLine("A player has been killed");
            ((MyGame)game).isFinished = true;
            ((MyGame)game).winner = "TWO";
            ((MyGame)game).LoadLevel(0);
        }
    }
}


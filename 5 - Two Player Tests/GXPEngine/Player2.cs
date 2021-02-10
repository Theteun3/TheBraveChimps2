using System;
using GXPEngine;
using TiledMapParser;

class Player2 : Player
{

    private AnimationSprite _graphics;

    private Level _level;

    private float _rocketTimer;
    private int _rocketCooldown = 1;

    public Player2(TiledObject obj) : base()
    {
        _graphics = new AnimationSprite("SpriteSheets/Player1.png", 8, 2, -1, false, false);
        _graphics.scaleX = 1;
        _graphics.x = -width;
        _graphics.y = -height * 1.25f;
        AddChild(_graphics);
    }

    private void Update()
    {
        handleJump();
        HandlePlayerStuff();
        handleInput();
        handleAnimation();
        handleShooting();
    }

    public void AddParents(Level l)
    {
        _level = l;
    }

    private void handleShooting()
    {
        int rot;
        if (scaleX > 0) rot = 0;
        else rot = -180;


        if (Input.GetMouseButtonDown(0) && _rocketTimer <= 0)
        {
            _rocketTimer = _rocketCooldown;
            _level.CreateRocket(this, rot);
        }

        if (_rocketTimer > 0) _rocketTimer -= .032f; ;
    }

    private void handleAnimation()
    {
        switch (playerState)
        {
            case State.IDLE:
                _graphics.SetCycle(0, 1);
                break;

            case State.WALKING:
                _graphics.SetCycle(8, 8, 8);
                break;

            case State.RUNNING:
                _graphics.SetCycle(1, 7, (byte)(16 / runSpeed));
                break;

            case State.JUMPING:
                _graphics.SetCycle(0, 1);
                break;
        }

        _graphics.Animate();
    }

    private void handleInput()
    {

        if (Input.GetKeyDown(Key.RIGHT) || Input.GetKeyUp(Key.RIGHT))
        {
            runSpeed = 1;
        }

        if (Input.GetKey(Key.LEFT))
        {
            speedX = -MOVEMENTSPEED;
        }

        if (Input.GetKey(Key.RIGHT))
        {
            speedX = MOVEMENTSPEED;
            if (runSpeed < 3) runSpeed += .07f;
        }
    }

    private void handleJump()
    {
        if (Input.GetKeyDown(Key.UP) && !isJumping && isGrounded)
        {
            isJumping = true;
        }

        if (speedY > JUMPHEIGHT)
        {
            if (isJumping) speedY -= JUMPFORCE;
        }
        else isJumping = false;
    }

}


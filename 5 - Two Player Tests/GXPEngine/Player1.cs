using System;
using GXPEngine;
using TiledMapParser;
class Player1 : Player
{

    private Level _level;

    private float _rocketTimer;
    private int _rocketCooldown = 1;

    private int _time;

    public Player1(TiledObject obj) : base()
    {
        _time = obj.GetIntProperty("Time", 100);
        
    }

    public void AddParents(Level l)
    {
        _level = l;
    }

    private void Update()
    {
        handleJump();
        HandlePlayerStuff();
        handleInput();
        handleShooting();
    }

    private void handleShooting()
    {
        int rot;
        if (scaleX > 0) rot = 0;
        else rot = -180;


        if (Input.GetKeyDown(Key.SPACE) && _rocketTimer <= 0)
        {
            _rocketTimer = _rocketCooldown;
            _level.CreateRocket(this, rot);
        }

        if (_rocketTimer > 0) _rocketTimer -= .032f;;
    }

    private void handleInput()
    {
        speedX = MOVEMENTSPEED;

        if (Input.GetKeyDown(Key.D) || Input.GetKeyUp(Key.D))
        {
            runSpeed = 1;
        }

        if (Input.GetKey(Key.D) && runSpeed < 3)
        {
            runSpeed += .1f;
        }

        if (Input.GetKey(Key.A))
        {
            speedX = -MOVEMENTSPEED;
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
        }
        else isJumping = false;
    }

    public int gameTime()
    {
        return _time;
    }

}


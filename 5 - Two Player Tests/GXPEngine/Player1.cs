using System;
using GXPEngine;
using TiledMapParser;
class Player1 : Player
{

    private Level _level;

    private float _rocketTimer;
    private int _rocketCooldown = 1;

    public Player1(TiledObject obj) : base()
    {
        
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
        if (Input.GetKey(Key.D))
        {
            speedX = MOVEMENTSPEED;
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

}


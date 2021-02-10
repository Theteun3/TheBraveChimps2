using System;
using GXPEngine;
using TiledMapParser;

class Player2 : Player
{

    private Level _level;

    private float _rocketTimer;
    private int _rocketCooldown = 1;
    public Player2(TiledObject obj) : base()
    {

    }

    private void Update()
    {
        handleJump();
        HandlePlayerStuff();
        handleInput();
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


        if (Input.GetKeyDown(Key.NUMPAD_5) && _rocketTimer <= 0)
        {
            _rocketTimer = _rocketCooldown;
            _level.CreateRocket(this, rot);
        }

        if (_rocketTimer > 0) _rocketTimer -= .032f; ;
    }

    private void handleInput()
    {
        if (Input.GetKey(Key.RIGHT))
        {
            speedX = MOVEMENTSPEED;
        }

        if (Input.GetKey(Key.LEFT))
        {
            speedX = -MOVEMENTSPEED;
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


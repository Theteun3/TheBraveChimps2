using System;
using GXPEngine;
using TiledMapParser;

class Player2 : Player
{
    public Player2(TiledObject obj) : base()
    {

    }

    private void Update()
    {
        handleJump();
        HandlePlayerStuff();
        handleInput();
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


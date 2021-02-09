using System;
using GXPEngine;
using TiledMapParser;
class Player1 : Player
{
    public Player1(TiledObject obj) : base()
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


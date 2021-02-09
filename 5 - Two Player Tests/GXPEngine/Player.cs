using System;
using GXPEngine;
using TiledMapParser;

class Player : Sprite
{
    public const float MOVEMENTSPEED = 0.8f;
    public const float JUMPFORCE = .5f;
    public const int JUMPHEIGHT = -1;
    private const float GRAVITY = 0.02f;

    public float speedX;
    public float speedY;
    public bool isJumping;
    public bool isGrounded;

    private float _deltaY;

    public Player() : base("SpriteSheets/PlayerCollision.png")
    {
        SetOrigin(width / 2, height / 2);
    }

    public void HandlePlayerStuff()
    {
        float oldY = y;

        handleGravity();
        handleMovement();

        _deltaY = y - oldY;
    }


    private void handleMovement()
    {
        MoveUntilCollision(speedX * Time.deltaTime, 0);
        MoveUntilCollision(0, speedY * Time.deltaTime);

        speedX *= .9f;
        if (_deltaY == 0)
        {
            speedY = 0;
            isGrounded = true;
        }
        else isGrounded = false;
    }


    private void handleGravity()
    {
        speedY += GRAVITY;
    }



}


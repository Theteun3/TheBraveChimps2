﻿using System;
using GXPEngine;
using TiledMapParser;

class Player : Sprite
{
    public const float MOVEMENTSPEED = 1.2f;
    public const float JUMPFORCE = .5f;
    public const float JUMPHEIGHT = -1.2f;
    private const float GRAVITY = 0.08f;
    private const float LEFT = -.5f;
    private const float RIGHT = .5f;

    public float speedX;
    public float speedY;
    public bool isJumping;
    public bool isGrounded;

    private float _deltaY;

    private Sprite RocketLauncher;

    public Player() : base("SpriteSheets/PlayerCollision.png")
    {
        SetOrigin(width / 2, height / 2);
        RocketLauncher = new Sprite("Sprites/Rocketlauncher.png", false, false);
        RocketLauncher.scale = 6;
        RocketLauncher.y = -height / 4;
        AddChild(RocketLauncher);
    }

    public void HandlePlayerStuff()
    {
        float oldY = y;

        handleGravity();
        handleFacing();
        handleMovement();

        

        _deltaY = y - oldY;
    }

    private void handleFacing()
    {
        if (speedX < 0) scaleX = LEFT;
        else scaleX = RIGHT;
    }

    private void handleMovement()
    {
        MoveUntilCollision(speedX * Time.deltaTime, 0);
        MoveUntilCollision(0, speedY * Time.deltaTime);

        speedX *= .8f;
        if (_deltaY == 0)
        {
            speedY = 0;
            isGrounded = true;
        }
        else isGrounded = false;
    }

    public float Facing()
    {
        return scaleX;
    }


    private void handleGravity()
    {
        speedY += GRAVITY;
    }



}


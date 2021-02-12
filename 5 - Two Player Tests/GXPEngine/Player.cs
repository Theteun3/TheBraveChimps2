using System;
using System.Collections.Generic;
using GXPEngine;
using TiledMapParser;

class Player : Sprite
{
    public const float MOVEMENTSPEED = .6f;
    public const float JUMPFORCE = .5f;
    public const float JUMPHEIGHT = -1.8f;
    private const float GRAVITY = 0.1f;
    private const float LEFT = -1f;
    private const float RIGHT = 1f;

    public Level level;

    public enum State { IDLE, WALKING, RUNNING, JUMPING };
    public State playerState = State.IDLE;

    public float speedX;
    public float speedY; 
    public float runSpeed = 1f;
    public bool isJumping;
    public bool isGrounded;

    public float boostSpeed = 1f;

    public float _deltaY;
    public int _health = 4;
    

    private Sprite RocketLauncher;

    public Player() : base("SpriteSheets/PlayerCollisionT.png")
    {
        SetOrigin(width / 2, height / 2);
        RocketLauncher = new Sprite("Sprites/Rocketlauncher.png", false, false);
        RocketLauncher.scale = 3;
        RocketLauncher.y = -height / 4;
        AddChild(RocketLauncher);
    }

    public void AddParents(Level l)
    {
        level = l;
    }

    public void HandlePlayerStuff()
    {
        float oldY = y;
        handleMovement();
        handleStates();
        handleGravity();
        handleFacing();
        
        dieInVoid();

        _deltaY = y - oldY;
    }

    private void dieInVoid()
    {
        if (y > 2000)
        {
            y = 1200;
            x += 400;
        }
    }

    private void handleMovement()
    {
        MoveAndCollide(speedX * Time.deltaTime * runSpeed * boostSpeed, 0, level.getLevelColliders(this));
        if (MoveAndCollide(0, speedY * Time.deltaTime, level.getLevelColliders(this)) == false)
        {
            speedY = 0f;
            isGrounded = true;
        }


        speedX *= .90f;

    }

    private void handleStates()
    {
        if ((speedX < 0.1 && speedX > -0.1)) playerState = State.IDLE;
        else if (runSpeed < 1.5) playerState = State.WALKING;
        else playerState = State.RUNNING;

        if (_deltaY != 0) playerState = State.JUMPING;
    }

    private void handleFacing()
    {
        if (speedX < 0) scaleX = LEFT;
        else scaleX = RIGHT;
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


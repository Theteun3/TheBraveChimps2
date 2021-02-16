using System;
using System.Collections.Generic;
using GXPEngine;
using TiledMapParser;

class Player : Sprite
{
    public const float MOVEMENTSPEED = .6f;
    public const float JUMPFORCE = .5f;
    public const float JUMPHEIGHT = -1.8f;
    public const float RUNSPEED = 3.5f;
    public const float MOMENTUMGAIN = 0.05f;
    private const float GRAVITY = 0.1f;
    private const float LEFT = -1f;
    private const float RIGHT = 1f;

    public Level level;

    public enum State { IDLE, WALKING, RUNNING, JUMPING };
    public State playerState = State.IDLE;

    public enum TutorialState { RUNNING, JUMPING, SHOOTING, LEVER };
    public TutorialState _tutorialState = TutorialState.RUNNING;

    public float speedX;
    public float speedY; 
    public float runSpeed = 1f;
    public bool isJumping;
    public bool isGrounded;
    public bool isFalling = true;
    public bool justDied;

    public float boostSpeed = 1f;

    public float _deltaY;
    public int _health = 4;

    private float slowTimer;
    private int slowTime = 1;
    

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
        if (justDied) slowPlayer();
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

    private void slowPlayer()
    {
        if (slowTimer <= 0) slowTimer = slowTime;
        else
        {
            slowTimer -= (float)(1f / (float)game.currentFps);
            runSpeed = 0.5f;
        }

        if (slowTimer <= 0) justDied = false;
    }

    private void handleMovement()
    {
        if (_deltaY >= 0) isFalling = true;
        if (_deltaY < 0) isFalling = false;

        if (isFalling && _deltaY == 0)
        {
            isGrounded = true;
            isFalling = false;
        }

        MoveAndCollide(speedX * Time.deltaTime * runSpeed * boostSpeed, 0, level.getLevelColliders(this));
        MoveAndCollide(0, speedY * Time.deltaTime, level.getLevelColliders(this));


        if (_deltaY != 0) isGrounded = false;

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
        if (isGrounded) speedY = 0.1f;

        speedY += GRAVITY;
    }



}


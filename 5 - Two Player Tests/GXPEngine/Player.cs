using System;
using GXPEngine;
using TiledMapParser;

class Player : Sprite
{
    public const float MOVEMENTSPEED = .6f;
    public const float JUMPFORCE = .5f;
    public const float JUMPHEIGHT = -1.6f;
    private const float GRAVITY = 0.08f;
    private const float LEFT = -1f;
    private const float RIGHT = 1f;
    
    public enum State { IDLE, WALKING, RUNNING, JUMPING };
    public State playerState = State.IDLE;

    public float speedX;
    public float speedY;
    public float runSpeed = 1f;
    public bool isJumping;
    public bool isGrounded;

    private float _deltaY;
    private int _health = 3;
    

    private Sprite RocketLauncher;

    public Player() : base("SpriteSheets/PlayerCollisionT.png")
    {
        SetOrigin(width / 2, height / 2);
        RocketLauncher = new Sprite("Sprites/Rocketlauncher.png", false, false);
        RocketLauncher.scale = 3;
        RocketLauncher.y = -height / 4;
        AddChild(RocketLauncher);
    }

    public void HandlePlayerStuff()
    {
        float oldY = y;

        handleStates();
        handleGravity();
        handleFacing();
        handleMovement();

        

        _deltaY = y - oldY;

        Console.WriteLine(_deltaY);
    }

    private void handleStates()
    {
        if ((speedX < 0.1 && speedX > -0.1)) playerState = State.IDLE;
        else if (runSpeed < 1.5) playerState = State.WALKING;
        else playerState = State.RUNNING;

        if (_deltaY < 0) playerState = State.JUMPING;
    }

    private void handleFacing()
    {
        if (speedX < 0) scaleX = LEFT;
        else scaleX = RIGHT;
    }

    private void handleMovement()
    {
        MoveUntilCollision(speedX * Time.deltaTime * runSpeed, 0);
        MoveUntilCollision(0, speedY * Time.deltaTime);

        speedX *= .93f;
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

    public void TakeDamage()
    {
        _health--;

        if (_health <= 0)
        {
            Console.WriteLine("A player has been killed");
            LateDestroy();
        }
    }



}


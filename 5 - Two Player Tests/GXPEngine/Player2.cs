using System;
using GXPEngine;
using TiledMapParser;

class Player2 : Player
{

    private AnimationSprite _graphics;


    private float _rocketTimer;
    private int _rocketCooldown = 1;

    public bool isTutorial;
    private bool _hasStepped;

    public Player2(TiledObject obj) : base()
    {
        _graphics = new AnimationSprite("SpriteSheets/Character2.png", 8, 3, -1, false, false);
        isTutorial = obj.GetBoolProperty("IsTutorial", false);
        _graphics.scaleX = 1;
        _graphics.x = -width;
        _graphics.y = -height * 1.25f;
        AddChild(_graphics);
    }


    private void Update()
    {
        float oldX = x;

        if (!isTutorial)
        {

            handleJump();
            if (!level._isPlayer2MovingToOtherPlayer) HandlePlayerStuff();
            handleInput();
            handleShooting();
            handleAnimation();



        }
        else handleTutorial();

        handleSound();

        float deltaX = x - oldX;
        if (deltaX == 0 && runSpeed > 2) runSpeed = 2;
        else if (deltaX == 0) runSpeed = 1;
    }

    private void handleSound()
    {
        if (_graphics.currentFrame == 4 || _graphics.currentFrame == 12 || _graphics.currentFrame == 0 || _graphics.currentFrame == 8)
            _hasStepped = false;

        if (_graphics.currentFrame == 5 || _graphics.currentFrame == 13 || _graphics.currentFrame == 1 || _graphics.currentFrame == 9)
        {
            if (!_hasStepped)
            {
                PlayWalking(Utils.Random(1, 4));
                _hasStepped = true;
            }
        }
    }

    private void handleShooting()
    {
        int rot;
        if (scaleX > 0) rot = 0;
        else rot = -180;


        if (Input.GetMouseButtonDown(0) && _rocketTimer <= 0)
        {
            _rocketTimer = _rocketCooldown;
            level.CreateRocket(this, rot);
        }

        if (_rocketTimer > 0) _rocketTimer -= .032f; ;
    }

    private void handleAnimation()
    {
        switch (playerState)
        {
            case State.IDLE:
                _graphics.SetCycle(0, 1);
                break;

            case State.WALKING:
                _graphics.SetCycle(0, 8, 8);
                break;

            case State.RUNNING:
                _graphics.SetCycle(8, 8, (byte)(16 / runSpeed));
                break;

            case State.JUMPING:
                if (_deltaY < 0) _graphics.SetCycle(19, 1);
                if (_deltaY > 0) _graphics.SetCycle(20, 1);
                break;
        }

        _graphics.Animate();
    }

    private void handleInput()
    {

        if (Input.GetKeyDown(Key.RIGHT) || Input.GetKeyUp(Key.RIGHT))
        {
            runSpeed = 1;
        }

        if (Input.GetKey(Key.LEFT))
        {
            speedX = -MOVEMENTSPEED;
        }

        if (Input.GetKey(Key.RIGHT))
        {
            speedX = MOVEMENTSPEED;
            if (runSpeed < RUNSPEED && isGrounded) runSpeed += MOMENTUMGAIN;
        }
    }

    private void handleJump()
    {
        if (Input.GetKeyDown(Key.UP) && !isJumping && isGrounded)
        {
            isJumping = true;
            _deltaY = -.1f;
        }

        if (isJumping)
        {
            if (speedY > JUMPHEIGHT && _deltaY != 0)
            {
                speedY -= JUMPFORCE;
            }
            else isJumping = false;
        }

        if (_graphics.currentFrame == 18) playerState = State.JUMPING;
    }

    public void TakeDamage()
    {
        if (!isTutorial) _health--;
        justDied = true;

        if (_health <= 0)
        {
            Console.WriteLine("A player has been killed");
            ((MyGame)game).isFinished = true;
            ((MyGame)game).winner = "ONE";
            ((MyGame)game).LoadLevel(0);
        }
    }

    private void handleTutorial()
    {
        switch (_tutorialState)
        {
            case TutorialState.RUNNING:
                HandlePlayerStuff();
                handleInput();
                if (Input.GetKeyDown(Key.A) || Input.GetKeyDown(Key.D) || Input.GetKeyDown(Key.LEFT) || Input.GetKeyDown(Key.RIGHT))
                    _tutorialState = TutorialState.JUMPING;
                break;

            case TutorialState.JUMPING:
                handleJump();
                HandlePlayerStuff();
                handleInput();
                if (Input.GetKeyDown(Key.W) || Input.GetKeyDown(Key.UP))
                    _tutorialState = TutorialState.SHOOTING;
                break;

            case TutorialState.SHOOTING:
                handleJump();
                HandlePlayerStuff();
                handleInput();
                handleShooting();
                if (Input.GetKeyDown(Key.SPACE) || Input.GetMouseButtonDown(0))
                    _tutorialState = TutorialState.SHOOTING;
                break;

            case TutorialState.LEVER:
                handleJump();
                HandlePlayerStuff();
                handleInput();
                handleShooting();
                break;
        }

        handleAnimation();
    }

}


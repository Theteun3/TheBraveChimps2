using System;
using GXPEngine;


class Rocket : AnimationSprite
{
    private float _speed = 3f;
    public bool isExploding;
    private Player _player;
    private Level _level;

    public Rocket(float newX, float newY, Player p, int rot, Level l) : base("SpriteSheets/RocketSheet.png", 4, 1)
    {
        SetOrigin(width / 2, height / 2);
        _player = p;
        SetCycle(0, 1);
        x = newX;
        y = newY;
        rotation = rot;
        if (rot == -180) _speed *= -1;
        _level = l;
    }

    private void Update()
    {
        float oldX = x;
        if (!isExploding) x += _speed * Time.deltaTime ;
        float dX = oldX - x;

        if (dX == 0 )
        {
            Explode();
            
        }
    }

    public void Explode()
    {
        if (isExploding) Animate();
        isExploding = true;
        SetCycle(1, 3);
        if (currentFrame == 1) ((MyGame)game).StartScreenShaking(1);


        if (currentFrame == 3 || Math.Abs(_player.x - x) > game.width / game.scale / 2)
        {
            LateDestroy();
        }
    }

    public void OnCollision(GameObject other)
    {
        if (other is Trap trap)
        {
            trap.isActivated = true;
            isExploding = true;
        }

        else if (other is Player p && !isExploding)
        {
            p.runSpeed = 1;
            isExploding = true;
        }

        else
        {
            isExploding = true;
        }
    }
}


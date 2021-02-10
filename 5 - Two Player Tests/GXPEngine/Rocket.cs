using System;
using GXPEngine;


class Rocket : AnimationSprite
{
    private float _speed = 3f;
    private bool _isExploding;
    private Player _player;

    public Rocket(float newX, float newY, Player p, int rot) : base("SpriteSheets/RocketSheet.png", 4, 1)
    {
        SetOrigin(width / 2, height / 2);
        _player = p;
        SetCycle(0, 1);
        x = newX;
        y = newY;
        rotation = rot;
        if (rot == -180) _speed *= -1;
    }

    private void Update()
    {
        float oldX = x;
        MoveUntilCollision(_speed * Time.deltaTime,0);
        float dX = oldX - x;

        if (dX == 0 )
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (_isExploding) Animate();
        _isExploding = true;
        SetCycle(1, 3);

        if (currentFrame == 3 || Math.Abs(_player.x - x) > game.width / game.scale / 2)
        {
            LateDestroy();
        }
    }
}


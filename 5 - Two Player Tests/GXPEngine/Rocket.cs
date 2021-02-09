using System;
using GXPEngine;


class Rocket : AnimationSprite
{
    private float SPEED = 1.2f;

    public Rocket(float newX, float newY) : base("SpriteSheets/RocketSheet.png", 4, 1)
    {
        SetCycle(0, 1);
        x = newX;
        y = newY;
    }

    private void Update()
    {
        Move(SPEED * Time.deltaTime, 0);
        Animate();
    }

    private void OnCollision(GameObject other)
    {
        SPEED = 0f;
        SetCycle(1, 3);
    }
}


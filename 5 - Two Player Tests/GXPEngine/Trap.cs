using System;
using GXPEngine;
using TiledMapParser;


class Trap : AnimationSprite
{

    public bool isActivated;

    public Trap(string Sprite, int col, int row, bool addCollider) : base(Sprite, col, row, -1, false, addCollider)
    {

    }


    public void OnCollision(GameObject other)
    {
        if (other is Rocket rocket)
        {
            isActivated = true;
            rocket.isExploding = true;
        }
    }


}

class Lever : Trap
{
    Trapdoor door;
    public int id;

    public Lever(TiledObject obj) : base("SpriteSheets/Lever.png", 2, 1, true)
    {
        id = obj.GetIntProperty("ID", 1);
    }

    public void addDoor(Trapdoor trapdoor)
    {
        door = trapdoor;
    }

    private void Update()
    {
        if (isActivated)
        {
            handleEffect();
        }
    }

    private void handleEffect()
    {
        currentFrame = 2;
        door.LateDestroy();
    }


}

class Trapdoor : AnimationSprite
{

    public int id;

    public Trapdoor(TiledObject obj) : base("SpriteSheets/PlayerCollision.png", 1, 1, -1, false, true)
    {
        id = obj.GetIntProperty("ID", 1);
    }
}



using System;
using GXPEngine;
using TiledMapParser;


class Trap : AnimationSprite
{

    public bool isActivated;

    public Trap(string Sprite, int col, int row, bool addCollider) : base(Sprite, col, row, -1, false, addCollider)
    {

    }
}

class Lever : Trap
{
    Trapdoor door;
    public int id;

    private Sound _doorOpen = new Sound("Sounds/SFX/DoorClose.wav");
    private Sound _activateClick = new Sound("Sounds/SFX/LeverSwitch.wav");
    private bool _hasPlayedSound;

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
        if (!_hasPlayedSound)
        {
            _activateClick.Play(false, 0, 10);
            _doorOpen.Play();
            _hasPlayedSound = true;
        }
        door.removeFromList();
        door.LateDestroy();
    }


}

class Trapdoor : AnimationSprite
{

    public int id;
    private Level _level;

    

    public Trapdoor(TiledObject obj) : base("SpriteSheets/PlayerCollision.png", 1, 1, -1, false, true)
    {
        id = obj.GetIntProperty("ID", 1);
    }

    public void addLevel(Level level)
    {
        _level = level;
    }

    public void removeFromList()
    {
        if (_level != null) _level._tileObjects.Remove(this);
    }
}



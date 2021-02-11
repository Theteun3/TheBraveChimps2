using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{

	private Level _level;
	private HUD _hud;
	private Sprite _fastEffect;

	public float gameTime;

	private float _screenShakeTimer = 0;

	private string _currentLevel;

	private string[] levelName = new string[4]
	{
		"TitleScreen.tmx",
		"SingleplayerPrototype.tmx",
		"PrototypeMap.tmx",
		"PrototypeMap2.tmx"
	};

	public MyGame() : base(1920, 1080, false, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		_hud = new HUD();
		LoadLevel(levelName[3]);
		scale = 0.5f;
		targetFps = 60;
		_fastEffect = new Sprite("Sprites/SpeedEffect.png", false, false);
		_fastEffect.alpha = 0;
		AddChild(_fastEffect);
    }

	public GameObject GetLevel()
    {
		return _level;
    }

	public void LoadLevel(string level)
    {
		_currentLevel = level;
		if (_level != null) _level.LateDestroy();
		_hud.LateDestroy();
		_level = new Level(_currentLevel);
		_hud = new HUD();
		LateAddChild(_level);
		LateAddChild(_hud);
    }

    private void Update()
	{
		updateScale();
		updateTime();
		screenShake();
		if (_level != null)
		{
			_fastEffect.alpha = _level.playerSpeed();
            /*_fastEffect.scale = 1 / scale;*/
        }
	}

	private void updateScale()
    {
		scale = _level.PlayerDistance();
    }

	private void updateTime()
    {
		if (_level != null && gameTime == 0)
		{
			gameTime = _level.totalGameTime();
		}

		gameTime -= (float) (1f / (float) currentFps);

	}

	public void StartScreenShaking(int time)
    {
		_screenShakeTimer = time;
    }

	private void screenShake()
    {
		if (_screenShakeTimer > 0)
        {
			y += Utils.Random(-5, 5);
			x += Utils.Random(-5, 5);
			_screenShakeTimer -= 1f;
		}
        else
        {
			y = 0;
			x = 0;
        }
    }

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}
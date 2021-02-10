using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{

	private Level _level;
	private HUD _hud;

	public float gameTime;

	private string _currentLevel;

	private string[] levelName = new string[3]
	{
		"TitleScreen.tmx",
		"SingleplayerPrototype.tmx",
		"PrototypeMap.tmx"
	};

	public MyGame() : base(1920, 1080, false, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		_hud = new HUD();
		LoadLevel(levelName[0]);
		scale = 0.5f;
		targetFps = 60;
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

    void Update()
	{
		updateScale();
		updateTime();
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

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}
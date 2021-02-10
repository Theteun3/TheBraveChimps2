using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{

	private Level _level;
	private HUD _hud;

	public MyGame() : base(800, 600, false, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		_hud = new HUD();
		loadLevel("PrototypeMap.tmx");
		scale = 0.5f;
		targetFps = 60;
    }

	public GameObject GetLevel()
    {
		return _level;
    }

	private void loadLevel(string level)
    {
		_hud.LateDestroy();
		_level = new Level(level);
		_hud = new HUD();
		LateAddChild(_level);
		LateAddChild(_hud);
    }

    void Update()
	{
		updateScale();
	}

	private void updateScale()
    {
		scale = _level.PlayerDistance();
    }

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}
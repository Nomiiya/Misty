using Godot;
using System;

public partial class game_settings : Node
{
	[Signal]
	public delegate void GameOverEventHandler();

	[Signal]
	public delegate void WaveStartEventHandler(int monstersCount);

	public Godot.Collections.Array<Godot.Vector2> monsterSpawns {get;set;} = new Godot.Collections.Array<Godot.Vector2>();
	public bool InputPaused{get; set;} = false;	
}

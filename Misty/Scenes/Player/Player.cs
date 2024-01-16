using Godot;
using System;

public partial class Player : CharacterBody2D
{
	game_settings gameSettings;
	building_globals buildingGlobals;
	public float speed {get; set;} = 500;

	KinematicCollision2D collision2D;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameSettings = GetNode<game_settings>("/root/GameSettings");
		buildingGlobals = GetNode<building_globals>("/root/BuildingGlobals");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	 public override void _Process(double delta)
    {
		Movement_Check(delta);
		
    }

	public void Movement_Check(double delta) {
		if(gameSettings.InputPaused) { return; }
		// Movement
		this.Velocity = new Godot.Vector2(0, speed);
		if(Input.IsActionPressed("movement")){
			if(Input.IsKeyPressed(Key.W)){
				collision2D = MoveAndCollide(-Velocity * (float)delta);
			}
			if(Input.IsKeyPressed(Key.S)){
				collision2D = MoveAndCollide(new Godot.Vector2(0,speed) * (float)delta);
			}
			if(Input.IsKeyPressed(Key.A)){
				collision2D = MoveAndCollide(new Godot.Vector2(-speed,0) * (float)delta);
			}
			if(Input.IsKeyPressed(Key.D)){
				collision2D = MoveAndCollide(new Godot.Vector2(speed,0) * (float)delta);
			}
		}
	}

    public override void _Input(InputEvent @event)
    {

		// "B" Keyboard Button
        if(Input.IsActionPressed("open_build_menu")){
			if(buildingGlobals.selectedTilePath == null) 
			{
				return;
			}
			if(gameSettings.InputPaused) 
			{ 
				// if input is paused, unpause and return player to main map
				gameSettings.InputPaused = false; 
				GetNode<Control>("BuildingMenu").Visible = false;
			}
			else
			{
				// Pause Keyboard inputs but NOT the game
				gameSettings.InputPaused = true;
				GetNode<Control>("BuildingMenu").Visible = true;
				
			}
		}
    }
}

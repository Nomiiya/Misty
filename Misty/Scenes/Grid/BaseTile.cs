using Godot;
using System;

public partial class BaseTile : Sprite2D
{
	public building_globals buildingGlobals;
	public bool IsBuilding {get; set;} = false;
	public bool IsPlayerOnTile { get; private set; } = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		buildingGlobals = GetNode<building_globals>("/root/BuildingGlobals");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	public void OnBodyEntered(Node2D body){
		if(body is Player){
        IsPlayerOnTile = true;
    }
		if(!body.GetMetaList().Contains("Player")){
			return;
		}
		Texture = GD.Load<Texture2D>("res://Assets/Testing/tileSelect.png");
		buildingGlobals.selectedTilePath = this.GetPath();
		
	}

	public void OnBodyExited(Node2D body){
		if(body is Player){
        IsPlayerOnTile = false;
    }
		Texture = GD.Load<Texture2D>("res://Assets/Testing/tile.png");
		buildingGlobals.selectedTilePath = null;
	}
}

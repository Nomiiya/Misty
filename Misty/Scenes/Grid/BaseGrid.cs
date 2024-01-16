using Godot;
using System;

public partial class BaseGrid : Node2D
{
	PackedScene tile = GD.Load<PackedScene>("res://Scenes/Grid/BaseTile.tscn");
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		await ToSignal(GetTree().CreateTimer(1.1), "timeout");
		SpawnGrid();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SpawnGrid(){
		for(int column = 0; column < 3; column++){
			for(int row = 0; row < 3; row++){
				BaseTile newTile = (BaseTile)tile.Instantiate();
				newTile.GlobalPosition = new Godot.Vector2(column * 64 ,row * 64);
				GetNode("Tiles").AddChild(newTile);
			}
		}
	}

	public void MoveGrid(Godot.Vector2 newPos){
		GlobalPosition = newPos;
	}
}

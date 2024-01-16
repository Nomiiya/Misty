using Godot;
using System;

public partial class Main : Node2D
{
	PackedScene baseGrid = GD.Load<PackedScene>("res://Scenes/Grid/BaseGrid.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//SpawnMap();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SpawnMap(){
		for(int i = 0; i < 2; i ++){
			BaseGrid grid = (BaseGrid)baseGrid.Instantiate();
			grid.MoveGrid(new Godot.Vector2(i * 256, 0));
			AddChild(grid);
		}
	}
}

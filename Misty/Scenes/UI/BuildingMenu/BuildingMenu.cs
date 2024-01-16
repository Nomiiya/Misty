using Godot;
using System;

public partial class BuildingMenu : Control
{
	building_globals bg;
	game_settings gameSettings;

	//Buildings
	PackedScene workerBuilding = GD.Load<PackedScene>("res://Scenes/Buildings/WorkerBuilding/WorkerBuilding.tscn");
	PackedScene archerBuilding = GD.Load<PackedScene>("res://Scenes/Buildings/ArcherTower/ArcherTower.tscn");
	const int WorkerBuildingCost = 200; // cost
    const int ArcherBuildingCost = 250; // cost


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameSettings = GetNode<game_settings>("/root/GameSettings");
		bg = GetNode<building_globals>("/root/BuildingGlobals");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnButtonOnePressed(){
		session_data snData = GetNode<session_data>("/root/SessionData");
    	if (snData.CurrentLuminiteCrystals >= ArcherBuildingCost) {
        // Deduct the crystals
        snData.CurrentLuminiteCrystals -= ArcherBuildingCost;
		// Replace the empty tile with the Building on Button One
		ArcherTower	building = (ArcherTower)archerBuilding.Instantiate();
		building.GlobalPosition = GetNode<BaseTile>(bg.selectedTilePath).GlobalPosition;
		GetNode<BaseTile>(bg.selectedTilePath).GetParent().AddChild(building);
		//	Free the current node 
		GetNode<BaseTile>(bg.selectedTilePath).QueueFree();

		// Hide the menu and unpause movement
		Visible = false;
		gameSettings.InputPaused = false; 
		bg.selectedTilePath = null;
	}
	}
	public void OnButtonTwoPressed(){
		session_data snData = GetNode<session_data>("/root/SessionData");
    	if (snData.CurrentLuminiteCrystals >= WorkerBuildingCost) {
        // Deduct the crystals
        snData.CurrentLuminiteCrystals -= WorkerBuildingCost;
		// Replace the empty tile with the Building on Button One
		WorkerBuilding	building = (WorkerBuilding)workerBuilding.Instantiate();
		building.GlobalPosition = GetNode<BaseTile>(bg.selectedTilePath).GlobalPosition;
		GetNode<BaseTile>(bg.selectedTilePath).GetParent().AddChild(building);
		//	Free the current node 
		GetNode<BaseTile>(bg.selectedTilePath).QueueFree();
		// Hide the menu and unpause movement
		Visible = false;
		gameSettings.InputPaused = false; 
		bg.selectedTilePath = null;
	}
	}
}
	
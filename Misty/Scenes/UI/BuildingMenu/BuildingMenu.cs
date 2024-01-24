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

	// Children
	Timer displayCostTimer;
	Label notEnoughMoneyLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameSettings = GetNode<game_settings>("/root/GameSettings");
		bg = GetNode<building_globals>("/root/BuildingGlobals");
		displayCostTimer = GetNode<Timer>("CostLabel/DisplayTimer");
		notEnoughMoneyLabel = GetNode<Label>("CostLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(displayCostTimer.IsStopped()){
			notEnoughMoneyLabel.Visible = false;
		}
	}

	public void OnButtonOnePressed(){
		session_data snData = GetNode<session_data>("/root/SessionData");
		if(snData.CurrentLuminiteCrystals < WorkerBuildingCost){
			DisplayCostLabel();
			return;
		}
		// Deduct the crystals
		snData.CurrentLuminiteCrystals -= ArcherBuildingCost;
		// Replace the empty tile with the Building on Button One
		ArcherTower	building = (ArcherTower)archerBuilding.Instantiate();
		building.GlobalPosition = GetNode<BaseTile>(bg.selectedBuildableTilePath).GlobalPosition;
		GetNode<BaseTile>(bg.selectedBuildableTilePath).GetParent().AddChild(building);
		//	Free the current node 
		GetNode<BaseTile>(bg.selectedBuildableTilePath).QueueFree();

		// Hide the menu and unpause movement
		Visible = false;
		gameSettings.InputPaused = false; 
		bg.selectedBuildableTilePath = null;
	}
	public void OnButtonTwoPressed(){
		session_data snData = GetNode<session_data>("/root/SessionData");
		if(snData.CurrentLuminiteCrystals < WorkerBuildingCost){
			DisplayCostLabel();
			return;
		}
    	
		// Deduct the crystals
		snData.CurrentLuminiteCrystals -= WorkerBuildingCost;
		// Replace the empty tile with the Building on Button One
		WorkerBuilding	building = (WorkerBuilding)workerBuilding.Instantiate();
		building.GlobalPosition = GetNode<BaseTile>(bg.selectedBuildableTilePath).GlobalPosition;
		GetNode<BaseTile>(bg.selectedBuildableTilePath).GetParent().AddChild(building);
		//	Free the current node 
		GetNode<BaseTile>(bg.selectedBuildableTilePath).QueueFree();
		// Hide the menu and unpause movement
		Visible = false;
		gameSettings.InputPaused = false; 
		bg.selectedBuildableTilePath = null;
		
	}

	// Utility Function
	public void DisplayCostLabel(){
		notEnoughMoneyLabel.Visible = true;
		displayCostTimer.Start();
	}
}
	
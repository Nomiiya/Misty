using Godot;
using System;

public partial class WorkerBuilding : StaticBody2D
{

	// Autoloads
	session_data snData;
	// Nodes
	PackedScene baseTile = GD.Load<PackedScene>("res://Scenes/Grid/BaseTile.tscn");

	public int BuildingHealth {get; set;} = 200;
	private const int BuildingHealthMax = 200;
	private const int luminiteGatherRateIncrease = 5;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		snData = GetNode<session_data>("/root/SessionData");
		snData.LuminiteRate  += luminiteGatherRateIncrease;

		GetNode<Label>("HP").Text = BuildingHealth.ToString() + " / " + BuildingHealthMax.ToString();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(BuildingHealth <= 0){ OnDeath();}
		
	}

	// Utilities
	public void DamageTaken(int damage){
		BuildingHealth -= damage;
		GetNode<Label>("HP").Text = BuildingHealth.ToString() + " / " + BuildingHealthMax.ToString();
	}

	private void OnDeath(){
		// Replace the basic building tile
		BaseTile replacement = (BaseTile)baseTile.Instantiate();
		replacement.GlobalPosition = GlobalPosition;
		GetParent().AddChild(replacement);

		// Remove rate increases
		snData.LuminiteRate -= luminiteGatherRateIncrease;

		// Remove from tree
		QueueFree();
	}

}

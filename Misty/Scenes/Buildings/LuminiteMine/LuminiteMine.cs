using Godot;
using System;

public partial class LuminiteMine : StaticBody2D
{
	

	public int BuildingHealth {get; set;} = 3000;
	private const int BuildingHealthMax = 3000;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Label>("HP").Text = BuildingHealth.ToString() + " / " + BuildingHealthMax.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if( BuildingHealth <= 0){ OnDeath(); }
	}

	// Utlities
	public void DamageTaken(int damage){
		BuildingHealth -= damage;
		GetNode<Label>("HP").Text = BuildingHealth.ToString() + " / " + BuildingHealthMax.ToString();
	}
	private void OnDeath(){
		GetTree().ChangeSceneToFile("res://Scenes/LostMenu/GameOver.tscn");
		EmitSignal("Gameover");
	}
}

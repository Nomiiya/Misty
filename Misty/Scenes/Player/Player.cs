using Godot;
using System;
using System.Linq;

public partial class Player : CharacterBody2D
{
	game_settings gameSettings;
	building_globals buildingGlobals;
	session_data snData;
	public float speed {get; set;} = 500;

	// Tile map to check tiles for what type they are
	TileMap terrain;
	TileData buyableCell;
	Godot.Vector2I buyableCellCoords;

	KinematicCollision2D collision2D;

	// Child Nodes
	Label currentLuminite;
	Label luminiteGatherRate;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameSettings = GetNode<game_settings>("/root/GameSettings");
		buildingGlobals = GetNode<building_globals>("/root/BuildingGlobals");
		snData = GetNode<session_data>("/root/SessionData");

		// Tile maps
		terrain = GetNode<TileMap>("/root/Main/Terrains");
		buyableCell = null;
		buyableCellCoords = new Godot.Vector2I(0,0);

		// Children
		currentLuminite = GetNode<Label>("HUD/LuminiteCount");
		luminiteGatherRate = GetNode<Label>("HUD/LiminiteRate");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	 public override void _Process(double delta)
    {
		Movement_Check(delta);
		UpdateHUD();
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
		// "E" Keyboard Button
        if(Input.IsActionPressed("open_build_menu")){
			if(buildingGlobals.selectedBuildableTilePath == null) 
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

		if(Input.IsActionPressed("BuyTile")){
			if(buyableCell == null){ return; }
			
			terrain.SetCell(0, buyableCellCoords,0, new Godot.Vector2I(2,1));
			snData.CurrentLuminiteCrystals -= (int)buyableCell.GetCustomData("cost");

			buyableCell = null;
			buyableCellCoords =new Godot.Vector2I(0,0);
		}

		/* TESTING BUTTTON*/

		// Wave Start -- TESTING
		if(Input.IsActionPressed("WaveStart")){
			gameSettings.EmitSignal("WaveStart", 10);
		}
    }

	//  
	public void OnTileCheckerEntered(Rid body_rid, Node2D body, int body_shape_index, int local_shape_index){
		Vector2I coords = terrain.GetCoordsForBodyRid(body_rid);
		Godot.Collections.Array<Godot.Vector2I> neighbors = new Godot.Collections.Array<Vector2I>{
			terrain.GetNeighborCell(coords, TileSet.CellNeighbor.RightSide),
			terrain.GetNeighborCell(coords, TileSet.CellNeighbor.TopSide),
			terrain.GetNeighborCell(coords, TileSet.CellNeighbor.LeftSide),
			terrain.GetNeighborCell(coords, TileSet.CellNeighbor.BottomSide),
		};
		TileData tile = terrain.GetCellTileData(0, coords);
		buyableCell = tile;
		buyableCellCoords = coords;

		
		foreach(Godot.Vector2I neighbor in neighbors){
			if((bool)terrain.GetCellTileData(0,neighbor).GetCustomData("bought") == true){
				GetNode<Label>("HUD/Buy").Visible = true;
				GetNode<Label>("HUD/Buy").Text = "Buyable for : " + tile.GetCustomData("cost").ToString();
			};
		}
		// terrain.SetCell(0, buyableCellCoords, 0, new Godot.Vector2I(0,0));
	}

	public void OnTileCheckerExited(Rid body_rid, Node2D body, int body_shape_index, int local_shape_index){
		buyableCell = null;
		buyableCellCoords = new Godot.Vector2I(0,0);
		GetNode<Label>("HUD/Buy").Visible = false;
	}

	// Utilities / Update Methods
	public void UpdateHUD(){
		currentLuminite.Text = "Current Luminite: " + snData.CurrentLuminiteCrystals.ToString();
		luminiteGatherRate.Text = "Gather Rate: " + snData.LuminiteRate.ToString();
	}
}

using Godot;
using System;

public partial class building_globals : Node
{
	public Godot.NodePath selectedBuildableTilePath {get; set;}

	// Tile Map Layers
	public int buyableCellLayer = 0;
	public int buildableCellLayer = 1;
	public int buildingCellLayer = 2;
	public int terrainCellLayer = 3;
	
}

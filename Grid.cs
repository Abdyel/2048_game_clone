using Godot;
using System;
using System.Reflection;

public partial class Grid : Node2D
{
	private Tile[,] grid;
	private PackedScene sceneTile;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneTile = ResourceLoader.Load<PackedScene>("res://Tile.tscn");
		
		grid = new Tile[4,4];

		PopulateStartingTiles();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private Vector2 ArrayToTileCoords(Vector2 arrayCoords)
	{
		return new Godot.Vector2(arrayCoords.X * 115 + 15, arrayCoords.Y * 115 + 15);
	}

	private void PopulateStartingTiles() 
	{
		Random rand = new Random();

		Vector2 tile1coords = new Vector2(rand.Next(0,4), rand.Next(0,4));
		Vector2 tile2coords = new Vector2(rand.Next(0,4), rand.Next(0,4));

		while(tile1coords.X == tile2coords.X && tile1coords.Y == tile2coords.Y)
		{
			tile1coords = new Vector2(rand.Next(0,4), rand.Next(0,4));
			tile2coords = new Vector2(rand.Next(0,4), rand.Next(0,4));
		}

		Tile t1 = sceneTile.Instantiate() as Tile; // Loaded in at _On Ready as Tile.tscn creates a Tile node.
		t1.Position = ArrayToTileCoords(tile1coords);
		AddChild(t1);

		Tile t2 = sceneTile.Instantiate() as Tile; // Loaded in at _On Ready as Tile.tscn creates a Tile node.
		t2.Position = ArrayToTileCoords(tile2coords);
		AddChild(t2);
		
		grid[(int) tile1coords.X, (int) tile1coords.Y] = t1;
		grid[(int) tile2coords.X, (int) tile2coords.Y] = t2;
	}
}

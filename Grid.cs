using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;

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
	public override void _Input(InputEvent @event)
	{
		if(@event.IsActionPressed("up"))
		{
			MoveTiles("up");
		}
		if(@event.IsActionPressed("down"))
		{
			MoveTiles("down");
		}
		if(@event.IsActionPressed("left"))
		{
			MoveTiles("left");
		}
		if(@event.IsActionPressed("right"))
		{
			MoveTiles("right");
		}
	}

	private bool MoveTiles(String direction)
	{
		GD.Print("MoveTiles() called: " + direction);
		bool movementOccurred = false;

		bool isHorizontal = direction == "left" || direction == "right";
		bool isReverse = direction == "up" || direction == "left";

		for(int i = 0; i < 4; i++)
		{
			Stack<Tile> tiles = new Stack<Tile>();

			for( int j = 0; j < 4; j++)
			{
				int x = isHorizontal ? (isReverse ? 3 - j : j) : i;
				int y = isHorizontal ? i : (isReverse ? 3 - j : j);
				
				if (grid[x,y] != null)
				{
					tiles.Push(grid[x,y]);
					grid[x,y] = null; //clear grid as we go.
				}
			}
			
			int newIndex = isReverse ? 0 : 3;

			while (tiles.Count > 0)
			{
				Tile current = tiles.Pop();
				Tile next = tiles.Count > 0 ? tiles.Peek() : null;
				Tile merged = null;

				//check for merge
				if(next != null && current.GetValue() == next.GetValue())
				{
					merged = tiles.Pop();
					current.SetValue(current.GetValue() * 2);
				}

				if(isHorizontal)
				{
					grid[newIndex, i] = current;
				}
				else
				{
					grid[i,newIndex] = current;
				}
				
			newIndex += isReverse ? 1 : -1;
			
			}
		}

		for (int x = 0; x < 4; x++)
		{
			for(int y = 0; y < 4; y++)
			{
				if(grid[x,y] != null)
				{
					Tile t = grid[x,y];
					Tween tween = t.CreateTween();
					tween.TweenProperty(t, "position", ArrayToTileCoords(new Vector2(x,y)), 0.1f);
				}
			}
		}

		return movementOccurred;
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
		t1.SetValue(2);
		t1.Position = ArrayToTileCoords(tile1coords);
		AddChild(t1);

		Tile t2 = sceneTile.Instantiate() as Tile; // Loaded in at _On Ready as Tile.tscn creates a Tile node.
		t2.SetValue(2);
		t2.Position = ArrayToTileCoords(tile2coords);
		AddChild(t2);

		grid[(int) tile1coords.X, (int) tile1coords.Y] = t1;
		grid[(int) tile2coords.X, (int) tile2coords.Y] = t2;
	}
}

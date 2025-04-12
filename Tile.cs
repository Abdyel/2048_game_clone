using Godot;
using System;

public partial class Tile : Polygon2D
{
	private int value = 0;
	private Label label;
	private Color tilecolor;
	private int currentDigits;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlaySpawnAnimation();
		UpdateLabel();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public int GetValue()
	{
		return value;
	}

	public void SetValue(int newValue)
	{
		value = newValue;
		UpdateColor();
		UpdateLabel();
	}

	private void PlaySpawnAnimation()
	{
		Scale = new Vector2(0,0); 
		Position = Position + new Vector2(50, 50);

		Tween scaleAnim = CreateTween();
		Tween positionAnim = CreateTween();

		scaleAnim.TweenProperty(this,"scale", new Vector2(1,1), 0.2f);

		positionAnim.TweenProperty(this,"position", Position - new Vector2(50,50), 0.2f);
	}

	private void UpdateColor()
	{
		switch (value)
		{
			case(2):
				tilecolor = new Color("eee3da");
				break;
			case(4):
				tilecolor = new Color("eddfc8");
				break;
			case(8):
				tilecolor = new Color("f2b178");
				break;
			case(16):
				tilecolor = new Color("f59562");
				break;
			case(32):
				tilecolor = new Color("f57c5f");
				break;
			case(64):
				tilecolor = new Color("f65e3a");
				break;
			case(128):
				tilecolor = new Color("edcf73");
				break;
			case(256):
				tilecolor = new Color("edcc61");
				break;
			case(512):
				tilecolor = new Color("edc750");
				break;
			case(1024):
				tilecolor = new Color("edc53e");
				break;
			case(2048):
				tilecolor = new Color("edc22d");
				break;
		}

		Color = tilecolor;
	}

	private void UpdateLabel()
	{
		label = GetNode<Label>("Label");
		label.Text = value.ToString();

		/*	// CODE USE TO MANUALLY CENTER LABEL INSIDE TILE. EDITOR USED TO AUTO CONTROL CENTERING LABEL.
		switch(value.ToString().Length)
		{
			case 2:
				if(currentDigits < 2)
				{
					label.Position = new Vector2(label.Position.X, label.Position.Y);
					currentDigits = 2;
				}
				break;
			case 3:
				if(currentDigits < 3)
				{
					label.Position = new Vector2(label.Position.X, label.Position.Y);
					currentDigits = 3;
				}
				break;
			case 4:
				if(currentDigits < 4)
				{
					label.Position = new Vector2(label.Position.X, label.Position.Y);
					currentDigits = 4;
				}
				break;
		}
		*/
	}
}

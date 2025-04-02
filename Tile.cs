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

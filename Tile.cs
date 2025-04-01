using Godot;
using System;

public partial class Tile : Polygon2D
{
	private int value = 0;
	private Label label;

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
	}
}

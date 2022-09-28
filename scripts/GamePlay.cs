using Godot;
using System;

public class GamePlay : Node
{
	//https://www.youtube.com/watch?v=RjVel3Ms9wo
	[Signal]
	public delegate void Moved(float newx, float newy);

	private Player player;
	public override void _Ready()
	{
		player = GetNodeOrNull<Player>("Player");
	}

	public override void _Process(float delta)
	{

		if ((player.IsMoving() && player.GetSlideCount() > 0) || player.IsOnCeiling() == true) 
		{
			//https://www.youtube.com/watch?v=RjVel3Ms9wo
			this.EmitSignal("Moved", player.GlobalPosition.x, player.GlobalPosition.y);
		}
	}
}

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

		if (Input.IsActionPressed("mv_dash"))
		{
			player.Dash();
		}
		if (Input.IsActionJustPressed("mv_up"))
		{
			player.Jump();
		}
		else if (Input.IsActionJustReleased("mv_up"))
		{
			player.CancelJump();
		}

		if (Input.IsActionPressed("mv_left"))
		{
			player.MoveLeft();
		}
		else if (Input.IsActionPressed("mv_right"))
		{
			player.MoveRight();
		}
		else
		{
			player.Idle();
		}

		if ((player.IsMoving() && player.GetSlideCount() > 0) || player.IsOnCeiling() == true) 
		{
			//https://www.youtube.com/watch?v=RjVel3Ms9wo
			this.EmitSignal("Moved", player.GlobalPosition.x, player.GlobalPosition.y);
		}
	}
}

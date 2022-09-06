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
		var movementButtonClicked = false;
		if (Input.IsActionPressed("mv_dash"))
		{
			player.Dash();
			movementButtonClicked = true;
		}
		if (Input.IsActionJustPressed("mv_up"))
		{
			player.Jump();
			movementButtonClicked = true;
		}
		else if (Input.IsActionJustReleased("mv_up"))
		{
			player.CancelJump();
			movementButtonClicked = true;
		}

		if (Input.IsActionPressed("mv_left"))
		{
			player.MoveLeft();
			movementButtonClicked = true;
		}
		else if (Input.IsActionPressed("mv_right"))
		{
			player.MoveRight();
			movementButtonClicked = true;
		}
		else
		{
			player.Idle();
		}

		if (!movementButtonClicked && Input.IsActionPressed("mv_down"))
		{
			player.Stop();
		}

		var butter_spread = ResourceLoader.Load<PackedScene>("res://scenes/ButterSpread.tscn");

		if (player.IsMoving() && player.GetSlideCount() > 0)
		{
			//https://www.youtube.com/watch?v=RjVel3Ms9wo
			this.EmitSignal("Moved", player.GlobalPosition.x, player.GlobalPosition.y);

			// var butterSpread_instance = butter_spread.InstanceOrNull<ButterSpread>();
			// if (butterSpread_instance != null)
			// {
			// 	GetTree().CurrentScene.AddChild(butterSpread_instance);
			// 	butterSpread_instance.GlobalPosition = new Vector2(player.GlobalPosition.x, player.GlobalPosition.y);
			// 	butterSpread_instance.Emitting = true;
			// }
			// else
			// {
			// 	GD.Print("Couldnt instance butterspread!");
			// }
		}
	}
}

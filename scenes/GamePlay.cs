using Godot;
using System;

public class GamePlay : Node
{

	private Player player;
	public override void _Ready()
	{
		player = GetNodeOrNull<Player>("Player");
	}

	public override void _Process(float delta)
	{

		if (Input.IsActionJustPressed("mv_accelerate"))
		{
			player.SetAccelerationBoost(true);
		}
		else if (Input.IsActionJustReleased("mv_accelerate"))
		{
			player.SetAccelerationBoost(false);
		}

		if (Input.IsActionPressed("mv_up"))
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

		var butter_spread = ResourceLoader.Load("res://scenes/ButterSpread.tscn") as PackedScene;

		if (player.IsMoving() && player.GetSlideCount() > 0)
		{
			var butterSpread_instance = butter_spread.Instance() as Node2D;
			GetTree().CurrentScene.AddChild(butterSpread_instance);
			butterSpread_instance.GlobalPosition = new Vector2(player.GlobalPosition.x, player.GlobalPosition.y);
		}
	}
}

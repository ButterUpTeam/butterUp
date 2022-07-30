using Godot;
using System;

public class Player : GravityObject
{
	public Player() : base(JUMP_FORCE) { }
	const int MAX_SPEED = 150;
	const int MAX_SPEED_BOOST = (int)(MAX_SPEED * 1.5f);
	const int ACCELERATION_DEFAULT = 30;
	const int JUMP_FORCE = 700;
	enum Direction { right, left, down, up }

	private Vector2 motion = new Vector2();
	private Vector2_t<Direction> direction = new Vector2_t<Direction>(Direction.right, Direction.down);
	private JumpPhase jump_phase = JumpPhase.Idle;
	private int max_speed = MAX_SPEED;
	private bool horizontal_moving = false;

	override public void _Ready()
	{
		GD.Print("Hello from C# to Godot :)");
	}
	override public void _Process(float delta)
	{

		var butter_spread = ResourceLoader.Load("res://scenes/ButterSpread.tscn") as PackedScene;

		if (Input.IsActionJustPressed("mv_accelerate"))
		{
			max_speed = MAX_SPEED_BOOST;
		}
		else if (Input.IsActionJustReleased("mv_accelerate"))
		{
			max_speed = MAX_SPEED;
		}

		if (Input.IsActionPressed("mv_left"))
		{
			motion.x -= ACCELERATION_DEFAULT;
			direction.x = Direction.left;
			horizontal_moving = true;
		}
		else if (Input.IsActionPressed("mv_right"))
		{
			motion.x += ACCELERATION_DEFAULT;
			direction.x = Direction.right;
			horizontal_moving = true;
		}
		else
		{
			motion.x = Mathf.Lerp(motion.x, 0, 0.3f);
			horizontal_moving = false;
		}

		motion.x = Mathf.Clamp(motion.x, -max_speed, max_speed);

		if (Input.IsActionPressed("mv_up") && IsOnFloor())
		{
			Jump(ref motion, 100);
		}
		else if (Input.IsActionJustReleased("mv_up"))
		{
			CancelJump();
		}

		if (horizontal_moving == true && GetSlideCount() > 0)
		{
			var butterSpread_instance = butter_spread.Instance() as Node2D;
			GetTree().CurrentScene.AddChild(butterSpread_instance);
			butterSpread_instance.GlobalPosition = new Vector2(GlobalPosition.x, GlobalPosition.y);
		}

		Gravity(ref motion, delta);


		Sprite sprite = GetNodeOrNull<Sprite>("Sprite");
		if (direction.x == Direction.right)
		{
			sprite.Scale = new Vector2(1, 1);
		}
		else
		{
			sprite.Scale = new Vector2(-1, 1);
		}

		motion = MoveAndSlide(motion, Vector2.Up);
	}
}







// func _physics_process(_delta):


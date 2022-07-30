using Godot;
using System;

public class Player : GravityObject
{
	public Player() : base(JUMP_FORCE) { }
	const int MAX_SPEED = 100;
	const int ACCEL = 30;
	const int JUMP_FORCE = 700;
	enum Direction { right, left, down, up }

	private Vector2 motion = new Vector2();
	private Vector2_t<Direction> direction = new Vector2_t<Direction>(Direction.right, Direction.down);
	private JumpPhase jump_phase = JumpPhase.Idle;

	override public void _Ready()
	{
		GD.Print("Hello from C# to Godot :)");
	}
	override public void _Process(float delta)
	{
		if (Input.IsActionPressed("ui_right"))
		{
			direction.x = Direction.right;
		}

		if (Input.IsActionPressed("mv_left"))
		{
			motion.x -= ACCEL;
			direction.x = Direction.left;
		}
		else if (Input.IsActionPressed("mv_right"))
		{
			motion.x += ACCEL;
			direction.x = Direction.right;
		}
		else
		{
			motion.x = Mathf.Lerp(motion.x, 0, 0.2f);
		}

		motion.x = Mathf.Clamp(motion.x, -MAX_SPEED, MAX_SPEED);

		if (Input.IsActionPressed("mv_up") && IsOnFloor())
		{
			Jump(ref motion, 100);
		}
		else if (Input.IsActionJustReleased("mv_up"))
		{
			CancelJump();
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


using Godot;

public class Player : GravityObject
{
	public Player() : base(JUMP_FORCE) { }
	const int MAX_SPEED = 100;
	const int MAX_SPEED_BOOST = (int)(MAX_SPEED * 1.7f);
	const int ACCELERATION_DEFAULT = 30;
	const int JUMP_FORCE = 200;
	enum Direction { right, left, down, up }

	private Vector2 motion = new Vector2();
	private Vector2_t<Direction> direction = new Vector2_t<Direction>(Direction.right, Direction.down);
	private JumpPhase jump_phase = JumpPhase.Idle;
	private int max_speed = MAX_SPEED;

	public bool IsMoving()
	{
		return motion.x != 0 || motion.y != 0;
	}

	public void MoveLeft()
	{
		motion.x -= ACCELERATION_DEFAULT;
		direction.x = Direction.left;
	}
	public void MoveRight()
	{
		motion.x += ACCELERATION_DEFAULT;
		direction.x = Direction.right;
	}

	public void Idle()
	{
		motion.x = Mathf.Lerp(motion.x, 0, 0.3f);
		motion.x = motion.x < 0.1 ? 0 : motion.x;
	}

	public void SetAccelerationBoost(bool boost)
	{
		if (boost)
		{
			max_speed = MAX_SPEED_BOOST;
		}
		else
		{
			max_speed = MAX_SPEED;
		}
	}

	public void Jump()
	{
		if (IsOnFloor())
		{
			Jump(ref motion, 100);
		}
	}

	override public void _Ready()
	{
		GD.Print("Hello from C# to Godot :)");
	}
	override public void _Process(float delta)
	{
		motion.x = Mathf.Clamp(motion.x, -max_speed, max_speed);

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


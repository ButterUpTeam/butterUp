using Godot;

public class Player : GravityObject
{
	public Player() : base(JUMP_FORCE)
	{
	}
	const int MAX_SPEED = 150;
	const int MAX_SPEED_BOOST = (int)(MAX_SPEED * 1.7f);
	const int ACCELERATION_DEFAULT = 5;
	const int JUMP_FORCE = 150;
	const int MAX_NUMBER_OF_JUMPS_IN_AIR = 2;
	enum Direction { right, left, down, up }

	private Vector2 motion = new Vector2();
	private Vector2_t<Direction> direction = new Vector2_t<Direction>(Direction.right, Direction.down);
	private int max_speed = MAX_SPEED;
	private int numberOfJumps = 0;

	private Timer dash_timer;

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
		motion.x = Mathf.Lerp(motion.x, 0, 0.1f);
		motion.x = (motion.x < 0.001 && motion.x > -0.001) ? 0 : motion.x;
	}

	public void Dash()
	{
		if (dash_timer is null)
		{
			dash_timer = GetNodeOrNull<Timer>("dash_timer");
		}
		if (dash_timer != null && dash_timer.IsStopped())
		{

			dash_timer.Start();
			motion.x = direction.x == Direction.right ? MAX_SPEED * 2 : -MAX_SPEED * 2;
			if (Direction.right == direction.x)
			{
				DrawDashEffect(new Vector2(-1, 1));
			}
			if (Direction.left == direction.x)
			{
				DrawDashEffect(new Vector2(1, 1));
			}

		}
	}

	private void DrawDashEffect(Vector2 dash_direction)
	{
		var butter_dash = ResourceLoader.Load<PackedScene>("res://scenes/DashEffect.tscn");
		var butter_dash_instance = butter_dash.InstanceOrNull<DashEffect>();
		if (butter_dash_instance != null)
		{
			GetTree().CurrentScene.AddChild(butter_dash_instance);
			butter_dash_instance.GlobalPosition = new Vector2(GlobalPosition.x, GlobalPosition.y);
			butter_dash_instance.ZIndex = 3;
			butter_dash_instance.Scale = dash_direction;
			butter_dash_instance.Emitting = true;
		}
	}

	private void DrawJumpEffect()
	{
		var jumpEffectResource = ResourceLoader.Load<PackedScene>("res://scenes/JumpEffect.tscn");
		var jumpEffect = jumpEffectResource.InstanceOrNull<JumpEffect>();
		if (jumpEffect != null)
		{
			GetTree().CurrentScene.AddChild(jumpEffect);
			jumpEffect.GlobalPosition = new Vector2(GlobalPosition.x, GlobalPosition.y);
			jumpEffect.ZIndex = 3;
			jumpEffect.Emitting = true;
		}
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
			numberOfJumps = 1;
			Jump(ref motion, 69);
			AudioStreamPlayer audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
			audio.Play();
			DrawJumpEffect();
		}
		else if (numberOfJumps < MAX_NUMBER_OF_JUMPS_IN_AIR)
		{
			Jump(ref motion, 69);
			AudioStreamPlayer audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
			audio.Play();
			numberOfJumps++;
			DrawJumpEffect();
		}
	}

	override public void _Ready()
	{
		GD.Print("Hello from C# to Godot :)");
	}

	override public void _PhysicsProcess(float delta)
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


using Godot;

public class Player : GravityObject
{
	[Signal] public delegate void Moved(float newx, float newy, Vector2 player_motion);
	[Signal] public delegate void JustFalled(float newx, float newy);
	[Signal] public delegate void Jumped();
	public Player() : base(JUMP_FORCE)
	{
	}
	private const int MAX_SPEED_DEFAULT = 150;
	private const int MAX_SPEED_CROUCH = (int)(0.3 * MAX_SPEED_DEFAULT);
	private const int MAX_SPEED_BOOST = (int)(MAX_SPEED_DEFAULT * 1.7f);
	private const int ACCELERATION_DEFAULT = 5;
	private const int ACCELERATION_CROUCH = (int)(0.3 * ACCELERATION_DEFAULT);
	private const int JUMP_FORCE = 150;
	private const int MAX_NUMBER_OF_JUMPS_IN_AIR = 2;

	enum Direction { right, left, down, up }

	private enum State { Normal, Crouch }

	private Vector2 motion = new Vector2();
	private Vector2_t<Direction> direction = new Vector2_t<Direction>(Direction.right, Direction.down);
	private State _state = State.Normal;
	private int numberOfJumps = 0;
	private bool wasFalling = false;

	private Timer dash_timer;
	
	private int _maxSpeed = MAX_SPEED_DEFAULT;
	private int _acceleration = ACCELERATION_DEFAULT;
	private float _slowDownWeight = 0.1f;

	private bool IsMoving => Mathf.Abs(motion.x) > 0.2 || Mathf.Abs(motion.y) > 0.05;

	private bool IsMovingRight => motion.x > 0;

	private bool IsMovingLeft => motion.x < 0;

	public bool CheckIfJustFalled()
	{
		bool isFalling = motion.y > 0.05;
		if (wasFalling && !isFalling)
		{
			wasFalling = isFalling;
			return true;
		}
		else
		{
			wasFalling = isFalling;
			return false;
		}
	}

	private void MoveLeft()
	{
		motion.x -= _acceleration;
		direction.x = Direction.left;
	}
	private void MoveRight()
	{
		motion.x += _acceleration;
		direction.x = Direction.right;
	}

	private void SlowDown()
	{
		motion.x = Mathf.Lerp(motion.x, 0, _slowDownWeight);
		motion.x = (motion.x < 0.001 && motion.x > -0.001) ? 0 : motion.x;
	}

	private void Crouch()
	{
		_acceleration = ACCELERATION_CROUCH;
		_maxSpeed = MAX_SPEED_CROUCH;
		_slowDownWeight = 0.2f;
		_state = State.Crouch;
	}

	private void StandUp()
	{
		_acceleration = ACCELERATION_DEFAULT;
		_maxSpeed = MAX_SPEED_DEFAULT;
		_slowDownWeight = 0.1f;
		_state = State.Normal;
	}

	private void Dash()
	{
		if (dash_timer is null)
		{
			dash_timer = GetNodeOrNull<Timer>("dash_timer");
		}
		if (dash_timer == null)
		{
			GD.PrintErr("dash_timer is null");
			return;
		}
		if (!dash_timer.IsStopped())
			return;

		dash_timer.Start();
		motion.x = direction.x == Direction.right ? MAX_SPEED_BOOST : -MAX_SPEED_BOOST;
		if (direction.x == Direction.right)
		{
			DrawDashEffect(new Vector2(-1, 1));
		}
		if (direction.x == Direction.left)
		{
			DrawDashEffect(new Vector2(1, 1));
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

	public void Jump()
	{
		if (IsOnFloor())
		{
			numberOfJumps = 1;
			Jump(ref motion, 69);
			this.EmitSignal("Jumped");
			//AudioStreamPlayer audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
			//audio.Play();
			DrawJumpEffect();
		}
		else if (numberOfJumps < MAX_NUMBER_OF_JUMPS_IN_AIR)
		{
			Jump(ref motion, 69);
			this.EmitSignal("Jumped");
			//AudioStreamPlayer audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
			//audio.Play();
			numberOfJumps++;
			DrawJumpEffect();
		}
	}

	override public void _Ready()
	{
		GD.Print("Hello from C# to Godot :)");
	}

	public override void _PhysicsProcess(float delta)
	{
		motion.x = Mathf.Clamp(motion.x, -_maxSpeed, _maxSpeed);

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

	public override void _Process(float delta)
	{
		
		if (Input.IsActionJustPressed("mv_down"))
		{
			Crouch();
		}
		else if(Input.IsActionJustReleased("mv_down"))
		{
			StandUp();
		}
		if(_state != State.Crouch && Input.IsActionJustPressed("mv_dash"))
		{
			Dash();
		}
		
		if (Input.IsActionJustPressed("mv_up"))
		{
			Jump();
		}
		else if (Input.IsActionJustReleased("mv_up"))
		{
			CancelJump();
		}

		if (Input.IsActionPressed("mv_left"))
		{
			MoveLeft();
		}
		else if (Input.IsActionPressed("mv_right"))
		{
			MoveRight();
		}
		else
		{
			SlowDown();
		}



		if ((IsMoving && GetSlideCount() > 0) || IsOnCeiling() == true)
		{
			this.EmitSignal("Moved", GlobalPosition.x, GlobalPosition.y, motion);
		}

		if (CheckIfJustFalled())
		{
			this.EmitSignal("JustFalled", GlobalPosition.x, GlobalPosition.y);
		}
	}
}

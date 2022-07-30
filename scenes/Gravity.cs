using Godot;
using System;

public enum JumpPhase
{
	Idle,
	Acceleration,
	Deceleration
}

public class GravityObject : KinematicBody2D
{
	public GravityObject(int jump_force) : base()
	{
		this.jump_force = jump_force;
	}

	const int GRAVITY_FORCE = 1200;
	const int MAX_FALL_SPEED = 700;
	private Vector2 acceleration = new Vector2();
	private Vector2 velocity = new Vector2();

	private int jump_force = 0;
	private float jump_time = 0;


	public void Jump(ref Vector2 motion, float jump_time_ms)
	{
		this.jump_time = jump_time_ms / 1000;
		motion.y = -jump_force;
	}

	public void CancelJump()
	{
		jump_time = 0;
	}
	public void Gravity(ref Vector2 motion, float delta)
	{
		jump_time -= delta;
		if (jump_time <= 0)
		{
			motion.y += GRAVITY_FORCE * delta;
			motion.y = Mathf.Clamp(motion.y, 0, MAX_FALL_SPEED);
		}
	}
}

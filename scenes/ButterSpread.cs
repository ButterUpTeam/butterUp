using Godot;
using System;

public class ButterSpread : CPUParticles2D
{
	public override void _Ready()
	{
		base._Ready();
	}

	public void _on_Timer_timeout()
	{
		SpeedScale = 0;
	}
	public void _on_timer_fade_timeout()
	{
		ButterManager butter_manager = GetTree().CurrentScene.GetNodeOrNull<ButterManager>("ButterManager");
		butter_manager.RemoveButterSpread(this);
	}

}





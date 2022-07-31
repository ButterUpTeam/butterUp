using Godot;
using System;

public class ButterSpread3 : Particles2D
{
	public override void _Ready()
	{

	}

	public void _on_Timer_timeout()
	{
		SpeedScale = 0;
	}
}

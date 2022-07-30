using Godot;
using System;


using System.Collections;

public class ButterManager : Node
{
	private Queue butter_spread_to_remove = new Queue();
	private Timer timer;

	public void RemoveButterSpread(ButterSpread butter)
	{
		butter_spread_to_remove.Enqueue(butter);
		timer.Start();
	}

	public void _on_timer_fade_frame_timeout()
	{
		if (butter_spread_to_remove.Count > 0)
		{
			ButterSpread butter_spread = (ButterSpread)butter_spread_to_remove.Dequeue();
			butter_spread.GetParent().RemoveChild(butter_spread);
			butter_spread.QueueFree();
		}
		else
		{
			timer.Stop();
		}
	}

	public override void _Ready()
	{
		timer = (Timer)FindNode("timer_fade_frame", false);
	}

}
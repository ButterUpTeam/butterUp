using Godot;
using System;

public class Limits
{
	public int Left = int.MaxValue;
	public int Right = int.MinValue;
	public int Top = int.MaxValue;
	public int Bottom = int.MinValue;

}

public class Game : Node2D
{
	public override void _Ready()
	{
		var tile_map = GetNodeOrNull<TileMap>("TileMap");
		Camera2D camera = GetNodeOrNull<Camera2D>("GamePlay/Player/Camera2D");
		Limits limits = null;
		if (tile_map != null)
		{
			limits = GetTileMapLimits(tile_map);
			Vector2 size = GetSize(limits);
			GD.Print("TileMap size: " + size);
		}
		if (camera != null && limits != null)
		{
			camera.LimitLeft = limits.Left * 8;
			camera.LimitRight = (limits.Right + 1) * 8;
			camera.LimitTop = limits.Top * 8;
			camera.LimitBottom = (limits.Bottom + 1) * 8;
		}
		else
		{
			GD.Print("Camera2D is null");
		}

	}
	private Limits GetTileMapLimits(TileMap tileMap)
	{
		Limits limits = new Limits();
		Godot.Collections.Array cels = tileMap.GetUsedCells();
		foreach (Vector2 cel in cels)
		{
			if (cel.x < limits.Left)
			{
				limits.Left = (int)cel.x;
			}
			if (cel.x > limits.Right)
			{
				limits.Right = (int)cel.x;
			}
			if (cel.y < limits.Top)
			{
				limits.Top = (int)cel.y;
			}
			if (cel.y > limits.Bottom)
			{
				limits.Bottom = (int)cel.y;
			}
		}
		return limits;
	}
	private Vector2 GetSize(Limits limits)
	{
		return new Vector2(limits.Right - limits.Left, limits.Bottom - limits.Top);
	}
}

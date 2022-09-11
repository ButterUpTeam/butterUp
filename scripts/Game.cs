using Godot;
using System.Linq;


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
		var tile_map_mask = GetNodeOrNull<TileMap>("TileMapMask");
		Camera2D camera = GetNodeOrNull<Camera2D>("Player/Camera2D");
		Limits limits = null;
		if (tile_map != null)
		{
			Godot.Collections.Array cells = tile_map.GetUsedCells();
			limits = GetTileMapLimits(cells);
			Vector2 size = GetSize(limits);
			GD.Print("TileMap size: " + size);
			if (tile_map_mask != null)
			{
				int cells_iter = 0;
				System.Collections.Generic.List<Vector2> debug_cells = new System.Collections.Generic.List<Vector2>();

				foreach (int y in Enumerable.Range(limits.Top, limits.Bottom + 1))
				{
					Vector2 predicted_cell = new Vector2();
					predicted_cell.y = y;
					foreach (int x in Enumerable.Range(limits.Left, limits.Right + 1))
					{
						predicted_cell.x = x;
						if (cells_iter <= cells.Count)
						{
							Vector2 cell = (Vector2)cells[cells_iter];
							if (cell != predicted_cell)
							{
								tile_map_mask.SetCellv(predicted_cell, 0);
								debug_cells.Add(predicted_cell);
							}
							else if (predicted_cell.x >= cell.x || predicted_cell.y >= cell.y)
							{
								cells_iter++;
							}
						}
					}
				}
			}
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
	private Limits GetTileMapLimits(Godot.Collections.Array cells)
	{
		Limits limits = new Limits();
		foreach (Vector2 cell in cells)
		{
			if (cell.x < limits.Left)
			{
				limits.Left = (int)cell.x;
			}
			if (cell.x > limits.Right)
			{
				limits.Right = (int)cell.x;
			}
			if (cell.y < limits.Top)
			{
				limits.Top = (int)cell.y;
			}
			if (cell.y > limits.Bottom)
			{
				limits.Bottom = (int)cell.y;
			}
		}
		return limits;
	}
	private Vector2 GetSize(Limits limits)
	{
		return new Vector2(limits.Right - limits.Left, limits.Bottom - limits.Top);
	}
}

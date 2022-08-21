public class Vector2_t<Type>
{
	public Vector2_t(Type x, Type y)
	{
		this.x = x;
		this.y = y;
	}
	public Type x;
	public Type y;
}

public class Math
{
	public static float Linear(float begin, float end, float step)
	{
		return begin < end ? begin + step : end;
	}
}
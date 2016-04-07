using UnityEngine;
using System.Collections;

public class IntPosition2D
{
	int x;
	int y;

	public int X {
		get
		{
			return x;
		}
		set
		{
			x = value;
		}
	}

	public int Y {
		get
		{
			return y;
		}
		set
		{
			y = value;
		}
	}

	public IntPosition2D (int _x, int _y)
	{
		x = _x;
		y = _y;
	}

	public static IntPosition2D Vector2ToIntPos2D (Vector2 vector2)
	{
		int X = 0;
		int Y = 0;
		IntPosition2D retPos;

		X = Mathf.RoundToInt (vector2.x);
		Y = Mathf.RoundToInt (vector2.y);

		retPos = new IntPosition2D (X, Y);

		return retPos;
	}
}

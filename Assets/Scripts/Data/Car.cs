using UnityEngine;
using System.Collections;

public class Car
{
	World world;
	IntPosition2D intPos;
	Vector2 pos;

	public Car (World _world)
	{
		world = _world;
		pos = world.Tiles [0, 0].TilePos;
		intPos = new IntPosition2D (0, 0);
		intPos = IntPosition2D.Vector2ToIntPos2D (pos);

	}

}

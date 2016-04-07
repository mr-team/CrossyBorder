using UnityEngine;
using System.Collections;

public class Tile
{
	Vector2 tilePos;
	bool walkable;
	bool deadly;

	public bool Walkable {
		get
		{
			return walkable;
		}
		set
		{
			walkable = value;
		}
	}

	public bool Deadly {
		get
		{
			return deadly;
		}
		set
		{
			deadly = value;
		}
	}

	public Vector2 TilePos {
		get
		{
			return tilePos;
		}
	}

	public Tile (Vector2 TilePos)
	{
		tilePos = TilePos;
	}
}

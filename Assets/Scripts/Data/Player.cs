using UnityEngine;
using System.Collections;

public class Player
{
	World world;
	IntPosition2D intPos;
	Vector2 pos;
	bool alive = true;

	public IntPosition2D IntPos {
		get
		{
			return intPos;
		}
		set
		{
			intPos = value;
		}
	}

	public Vector2 Pos {
		get
		{
			return pos;
		}
		set
		{
			pos = value;
		}
	}

	public bool Alive {
		get
		{
			return alive;
		}
		set
		{
			alive = value;
		}
	}

	public Player (World _World)
	{
		world = _World;
		pos = world.Tiles [0, 0].TilePos;
		intPos = new IntPosition2D (0, 0);
		intPos = IntPosition2D.Vector2ToIntPos2D (pos);
	}

	public void MoveLeft ()
	{
		try
		{
			if (world.Tiles [intPos.X - 1, intPos.Y].Walkable)
			{
				pos = new Vector2 (intPos.X - 1, intPos.Y);
				intPos = IntPosition2D.Vector2ToIntPos2D (pos);
			} else if (!world.Tiles [intPos.X - 1, intPos.Y].Walkable)
			{
				Debug.Log ("could not move to the left");
			}
		} catch
		{
			Debug.Log ("could not move to the left");
		}
	}

	public void MoveRight ()
	{
		try
		{
			if (world.Tiles [intPos.X + 1, intPos.Y].Walkable)
			{
				pos = new Vector2 (intPos.X + 1, intPos.Y);
				intPos = IntPosition2D.Vector2ToIntPos2D (pos);
			} else if (!world.Tiles [intPos.X + 1, intPos.Y].Walkable)
			{
				Debug.Log ("could not move to the right");
			}
		} catch
		{
			Debug.Log ("could not move to the right");
		}
	}

	public void MoveUp ()
	{
		try
		{
			if (world.Tiles [intPos.X, intPos.Y + 1].Walkable)
			{
				pos = new Vector2 (intPos.X, intPos.Y + 1);
				intPos = IntPosition2D.Vector2ToIntPos2D (pos);
			} else if (!world.Tiles [intPos.X, intPos.Y + 1].Walkable)
			{
				Debug.Log ("could not move up");
			}
		} catch
		{
			Debug.Log ("could not move up");
		}
	}

	public void MoveDown ()
	{
		try
		{
			if (world.Tiles [intPos.X, intPos.Y - 1].Walkable)
			{
				pos = new Vector2 (intPos.X, intPos.Y - 1);
				intPos = IntPosition2D.Vector2ToIntPos2D (pos);
			} else if (!world.Tiles [intPos.X, intPos.Y - 1].Walkable)
			{
				Debug.Log ("could not move down");
			}
		} catch
		{
			Debug.Log ("could not move down");
		}
	}
}

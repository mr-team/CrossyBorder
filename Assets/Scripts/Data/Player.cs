using UnityEngine;
using System.Collections;

public class Player
{
	public delegate void LoseLifeCB ();

	LoseLifeCB onLoseLife;

	World world;
	IntPosition2D intPos;
	Vector2 pos;
	bool alive = true;
	int lives = 5;

	public LoseLifeCB OnLoseLife {
		get
		{
			return onLoseLife;
		}
		set
		{
			onLoseLife = value;
		}
	}

	public IntPosition2D IntPos {
		get
		{
			return intPos;
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
			intPos = IntPosition2D.Vector2ToIntPos2D (pos);
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

	public int Lives {
		get
		{
			return lives;
		}
		set
		{
			lives = value;
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
				//Debug.Log ("could not move to the left");
			}
		} catch
		{
			//Debug.Log ("could not move to the left");
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
				//Debug.Log ("could not move to the right");
			}
		} catch
		{
			//Debug.Log ("could not move to the right");
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
				
			}
		} catch
		{
			
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
				
			}
		} catch
		{
			
		}
	}

	public void LoseLife (int amount = 1)
	{
		lives -= amount;
		onLoseLife ();
	}

	public void GainLife (int amount = 1)
	{
		lives += amount;
	}
}

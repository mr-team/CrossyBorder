using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
	Player player;
	World world;
	public int worldWidth;
	public int worldHeigth;
	public bool gameLoopActive;
	public bool gamePaused;

	public Player Player {
		get
		{
			return player;
		}
	}

	public World World {
		get
		{
			return world;
		}
	}

	void Awake ()
	{
		world = new World (worldWidth, worldHeigth);
		world.GenerateWorld ();
		player = new Player (world);
	}

	void Update ()
	{
		if (gameLoopActive)
		{
			if (!player.Alive)
			{
				Debug.Log ("player is dead");
				gameLoopActive = false;
			}

			if (Input.GetKeyDown (KeyCode.P))
			{
				gamePaused = true;
			}
		}
	}

	public void StartGame ()
	{
		gameLoopActive = true;
		gamePaused = false;
	}

	public void RestartLevel ()
	{
		gameLoopActive = false;
		player.Pos = new Vector2 (5, 1);
		player.Alive = true;
		gameLoopActive = true;
	}

	public void RestartGame ()
	{
		gameLoopActive = false;
		player.Pos = new Vector2 (5, 1);
		player.Alive = true;
		gameLoopActive = true;
	}
}
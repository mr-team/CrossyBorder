using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
	Player player;
	World world;
	public int worldWidth;
	public int worldHeigth;
	public bool gameLoopActive;

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
		}
	}

	public void StartGame ()
	{
		
		gameLoopActive = true;

	}
}

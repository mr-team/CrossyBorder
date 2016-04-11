using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
	Player player;
	World world;
	public int worldWidth;
	public int worldHeigth;
    public float noiseScale;
    public string seed;
	public bool gameLoopActive;
	public bool gamePaused;
	public bool gameResetPause;
	int prevlife;

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
		world = new World (worldWidth, worldHeigth, noiseScale, seed);
		world.GenerateWorld ();
		player = new Player (world);
		prevlife = player.Lives;
	}

	void Update ()
	{
		if (gameLoopActive)
		{
			if (!player.Alive)
			{
				gameLoopActive = false;
			}

			if (Input.GetKeyDown (KeyCode.P))
			{
				gamePaused = true;
			}
			if (player.Lives < prevlife && player.Alive)
			{
				RestartLevel ();
				Debug.Log ("hello");
				prevlife = player.Lives;
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
		gameResetPause = true;
		player.Pos = new Vector2 (5, 1);

		//player.Alive = true;
	}

	public void RestartGame ()
	{
		Application.LoadLevel (0);	
	}
}
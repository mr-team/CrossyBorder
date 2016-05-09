using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour
{

	public delegate void OnNextRoun ();

	public delegate void OnPlayerLostLife ();

	public enum States
	{
		gameActive,
		gameDeActive,
		gamePaused,
		roundWon,
		gameWon,
		gameLost,

	}

	public States gameState;

	public OnNextRoun onNextRound;
	public OnPlayerLostLife onPlayerLostLife;

	Player player;
	World world;
	//world settings
	public int worldWidth;
	public int worldHeigth;
    public GameObject EndWall;
    public GameObject carSpawner;
    public GameObject catapultPrefab;
    public int ladderCount;
	public int maxLadder;
	public float noiseScale;
	public string seed;
    [HideInInspector]
    public List<int> carLanes = new List<int>();

	//game state stuff
	public bool gameLoopActive;
	public bool gamePaused;
	public bool gameReset;
	public bool roundWon;
	public bool gameLost;
	public bool gameTransition;

	int prevlife;


	//four abilities
	public bool CarpetBombing;
	public bool PredatorDrones;
	public bool fbiTroops;
	public bool bearTraps;
	public bool mines;
    public bool catapult;

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
        List<int> roads = CalculateRoadLocations(CalculateRoadAmount(worldHeigth), worldHeigth);
        PlaceRoads(roads);
        world = new World (worldWidth, worldHeigth, noiseScale);
		world.GenerateWorld (seed);
        player = new Player (world);
		player.OnLoseLife += RestartCounter;
		prevlife = player.Lives;
		gameState = States.gameDeActive;
        EndWall.transform.position = new Vector3(EndWall.transform.position.x, (worldHeigth - 24), EndWall.transform.position.z);
    }

    public int CalculateRoadAmount(int mapHeight) {
        return Mathf.RoundToInt(mapHeight / 10);
    }

    public List<int> CalculateRoadLocations(int amount, int mapHeight) {
        int left = amount;
        List<int> result = new List<int>();
        System.Random rand = new System.Random(seed.GetHashCode());
        for(int i = 0; i < mapHeight; i++) {
            if(i <= 3)
                continue;
            if(left == 0)
                break;
            int percentage = (100 / mapHeight) * rand.Next(i, mapHeight);
            if(percentage >= 50) {
                result.Add(i);
                left--;
                i += rand.Next(1, mapHeight / 5);
            }
        }
        return result;
    }

    public void PlaceRoads(List<int> r) {
        System.Random rand = new System.Random(seed.GetHashCode());
        for(int i = 0; i < r.Count; i++) {
            if(r[i] == 0)
                continue;
            float x = rand.Next(0, 100) <= 50 ? -8 : 17.25f;
            GameObject road = Instantiate(carSpawner, new Vector3(x, r[i] + .4f, -0.0859375f), Quaternion.identity) as GameObject;
            road.GetComponent<CarSpawner>().SpawnDir = (x == -8) ? CarSpawner.directions.left : CarSpawner.directions.right;
            road.name = "Car & Road Spawner " + r[i];
        }
    }

	void Update ()
	{
		switch (gameState)
		{
			case States.gameActive:
				UpdateGameActive ();
				break;

			case States.gameDeActive:
				UpdateGameDeActive ();
				break;

			case States.gamePaused:
				UpdateGamePaused ();
				break;

			case States.roundWon:
				UpdateRoundWon ();
				break;

			case States.gameWon:
				UpdateGameWon ();
				break;

			case States.gameLost:
				UpdateGameLost ();
				break;

		}

		if (!player.Alive)
		{
			gameLoopActive = false;
		}

	}
	//handle the game when active
	void UpdateGameActive ()
	{
		gameLoopActive = true;
		gamePaused = false;
		if (Input.GetKeyDown (KeyCode.P))
			gameState = States.gamePaused;

		if (roundWon)
			gameState = States.roundWon;
	}
	//handle the game when deactive
	void UpdateGameDeActive ()
	{
		gameLoopActive = false;
	}

	void UpdateRoundWon ()
	{
		gameLoopActive = false;

		gameTransition = true;
	}

	//handle the game when paused
	void UpdateGamePaused ()
	{
		gamePaused = true;
			
	}
	//handle the game when the player has won
	void UpdateGameWon ()
	{
		//if not last round
		//deactivate cars,lanes,player and everything which will interfeer with the transition
		gameLoopActive = false;

		//move the camera to the top of the wall
		//spawn in politicians
		//gameTransition = true;


		//after choose, spin wheel of abilities

		//start next round

		//if last round
		//play victory animation

		//show highscore

	}
	//handle the game when the player has lost
	void UpdateGameLost ()
	{
		//show highscore

		//prompt main menu

		//promt restart
		
	}

	public void ResumeGame ()
	{
		gameState = States.gameActive;
	}

	public void StartGame ()
	{
		gameState = States.gameActive;
	}

	public void RestartCounter ()
	{
		
		if (onPlayerLostLife != null)
			onPlayerLostLife ();
		GetComponent<CountDown> ().ResetTimer ();
	}

	public void RestartGame ()
	{
		Application.LoadLevel (0);	
	}

	public void WinRound ()
	{

	}

	public void NextRound ()
	{	
		
		world.GenerateWorld (seed);
		onNextRound ();
		RestartCounter ();
		roundWon = false;
		gameTransition = false;
		gameState = States.gameActive;

	}
		
}
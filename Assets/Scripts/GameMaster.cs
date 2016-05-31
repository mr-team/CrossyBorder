using UnityEngine;
using UnityEngine.UI;
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

	[Header ("Ability Cards")]
	public List<Texture> cards = new List<Texture> ();
	[HideInInspector]
	public bool showCards = true;
    [Space(20)]

    [Header("Score Animation")]
    public GameObject scorePopupPrefab;
    public GameObject UI;
    [Space(20)]

    public States gameState;

	public GetTextCutScene textCutScene;
	public OnNextRoun onNextRound;
	public OnPlayerLostLife onPlayerLostLife;
	public PlayerClimbLadder climbCutScene;

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
	public List<int> carLanes = new List<int> ();
	[HideInInspector]
	public List<GameObject> carLaneObjects;

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


	//scoring
	public int score;

	public void ReAwake ()
	{
		
		List<int> roads = CalculateRoadLocations (worldHeigth);
		PlaceRoads (roads);
		world = new World (worldWidth, worldHeigth, noiseScale);
		world.GenerateWorld (seed);
		player.world = world;
		player.OnLoseLife += RestartCounter;
		prevlife = player.Lives;
		EndWall.transform.position = new Vector3 (EndWall.transform.position.x, (worldHeigth - 24), EndWall.transform.position.z);
    }

	void Awake ()
	{
		textCutScene = GameObject.Find ("GetTextCutScene").GetComponent<GetTextCutScene> ();
		List<int> roads = CalculateRoadLocations (worldHeigth);
		PlaceRoads (roads);
		world = new World (worldWidth, worldHeigth, noiseScale);
		world.GenerateWorld (seed);
		player = new Player (world);
		player.OnLoseLife += RestartCounter;
		prevlife = player.Lives;
		gameState = States.gameDeActive;
		EndWall.transform.position = new Vector3 (EndWall.transform.position.x, (worldHeigth - 24), EndWall.transform.position.z);
	}

	public int CalculateRoadAmount (int mapHeight)
	{
		return Mathf.RoundToInt (mapHeight / 10);
	}

	public List<int> CalculateRoadLocations (int mapHeight)
	{
		int left = Mathf.RoundToInt ((mapHeight / 100f) * 20f);
		List<int> result = new List<int> ();
		System.Random rand = new System.Random (seed.GetHashCode ());
		while (left > 0)
		{
			int lane = rand.Next (4, (mapHeight - 4));
			if (!result.Contains (lane - 1) && !result.Contains (lane) && !result.Contains (lane + 1))
			{
				result.Add (lane);
				left--;
			}
		}
		result.Sort ();
		return result;
	}

	public List<int> CalculateRoadLocationsOLD (int amount, int mapHeight)
	{
		int left = amount;
		List<int> result = new List<int> ();
		System.Random rand = new System.Random (seed.GetHashCode ());
		for (int i = 0; i < mapHeight; i++)
		{
			if (i <= 3)
				continue;
			if (left == 0)
				break;
			int percentage = (100 / mapHeight) * rand.Next (i, mapHeight);
			if (percentage >= 50)
			{
				result.Add (i);
				left--;
				i += rand.Next (1, mapHeight / 5);
			}
		}
		return result;
	}

	public void PlaceRoads (List<int> r)
	{
		System.Random rand = new System.Random (seed.GetHashCode ());
		for (int i = 0; i < r.Count; i++)
		{
			if (r [i] == 0)
				continue;
			float x = rand.Next (0, 100) <= 50 ? -8 : 17.25f;
			GameObject road = Instantiate (carSpawner, new Vector3 (x, r [i] + .4f, -0.0859375f), Quaternion.identity) as GameObject;
			road.GetComponent<CarSpawner> ().SpawnDir = (x == -8) ? CarSpawner.directions.left : CarSpawner.directions.right;
			road.name = "Car & Road Spawner " + r [i];
			carLaneObjects.Add (road);
		}
	}

	void RemoveCards ()
	{
		foreach (Texture t in cards)
		{
			bool enabled = false;
			switch (t.name)
			{
				case "BearTrapCard":
					enabled = bearTraps;
					break;
				case "FBICard":
					enabled = fbiTroops;
					break;
				case "MineCard":
					enabled = mines;
					break;
				case "SkillCard":
					enabled = CarpetBombing;
					break;
			}
			if (enabled)
				cards.Remove (t);
		}
	}

	public Texture pickRandomCard (string _seed)
	{
		System.Random rand = new System.Random (_seed.GetHashCode ());
		int i = rand.Next (0, cards.Count);
		return cards [i];
	}

	void Update ()
	{
		RemoveCards ();
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
		climbCutScene.active = true;
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
		textCutScene.Activate (0);
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

		//world.GenerateWorld (seed);
		onNextRound ();
		ladderCount = 0;
		RestartCounter ();
		climbCutScene.active = false;
		roundWon = false;
		gameTransition = false;
		gameState = States.gameActive;
        AddScore(500, Screen.width / 2f, Screen.height / 2f);
    }

	public void AddScore (int amount, float screenPosX = -1337f, float screenPosY = -1337f)
	{
		score += amount;
        GameObject scorePopup = (GameObject) Instantiate(scorePopupPrefab, Vector3.zero, Quaternion.identity);
        scorePopup.GetComponent<Text>().text = "+" + amount;
        //scorePopup.transform.parent = UI.transform;
        RectTransform rt = scorePopup.GetComponent<RectTransform>();
        rt.SetParent(UI.transform);
        if(screenPosX == -1337f && screenPosY == -1337f) {
            rt.position = Camera.main.WorldToScreenPoint(new Vector3(player.IntPos.X, player.IntPos.Y, 0f));
        } else {
            rt.position = new Vector3(screenPosX, screenPosY, 0f);
        }
    }
		
}
using UnityEngine;
using System.Collections;

public class VerticalLanesController : MonoBehaviour
{
	GameMaster GM;
	public GameObject[] Lanes = new GameObject[3];
	public GameObject[] laneObjects = new GameObject[5];

	float timer;
	float spawnTime;
	bool somthingInLane;

	public bool SomthingInLane {
		get
		{
			return somthingInLane;
		}
		set
		{
			somthingInLane = value;
		}
	}

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	void Update ()
	{

		//carpetBombings
		if (GM.CarpetBombing && GM.gameLoopActive && !GM.gamePaused)
		{
			timer += Time.deltaTime;

			if (timer >	spawnTime)
			{
				SpawnpredatorDrone (Lanes [1]);
				timer = 0;
				spawnTime = Random.Range (5, 15);
			}
		}
	}

	void SpawnpredatorDrone (GameObject lane)
	{
		GameObject predDrone = Instantiate (laneObjects [0], lane.transform.position, Quaternion.identity) as GameObject;
		predDrone.GetComponent<PredatorDrone> ().LaneControl = this;
		Debug.Log ("spawned drone in lane: " + lane);
		somthingInLane = true;
	}
}

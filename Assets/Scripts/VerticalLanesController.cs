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
		if (GM.gameLoopActive && GM.CarpetBombing)
		{
			timer += Time.deltaTime;

			if (timer >	spawnTime)
			{
				SpawnB52Bomber (Lanes [Random.Range (0, 3)]);
				timer = 0;
				spawnTime = Random.Range (5, 15);

			}
		}

		if (GM.gameLoopActive && GM.PredatorDrones)
		{
			timer += Time.deltaTime;

			if (timer >	spawnTime)
			{
				SpawnPredatorDrone (Lanes [1]);
				timer = 0;
				spawnTime = Random.Range (5, 15);

			}
		}
	}

	void SpawnB52Bomber (GameObject lane)
	{
		GameObject B52Bomber = Instantiate (laneObjects [0], lane.transform.position, Quaternion.identity) as GameObject;
		B52Bomber.GetComponent<B52Bomber> ().LaneControl = this;
		Debug.Log ("spawned bomber in lane: " + lane);
		somthingInLane = true;
	}

	void SpawnPredatorDrone (GameObject lane)
	{
		GameObject PredDrone = Instantiate (laneObjects [1], lane.transform.position, Quaternion.identity) as GameObject;
		PredDrone.GetComponent<PredatorDrone> ().LaneControl = this;
		Debug.Log ("spawned Drone");
		somthingInLane = true;
	}
}

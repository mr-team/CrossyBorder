using UnityEngine;
using System.Collections;

public class PredatorDrone : MonoBehaviour
{
	GameMaster GM;

	public GameObject bomb;
	public GameObject[] spawnPoints = new GameObject[5];
	VerticalLanesController laneControl;

	float moveSpeed = 6;
	float bombTimer;
	bool bombDroped;

	IntPosition2D intPos;
	IntPosition2D lastIntPos;

	public VerticalLanesController LaneControl {
		get
		{
			return laneControl;
		}
		set
		{
			laneControl = value;
		}
	}

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		lastIntPos = new IntPosition2D (0, 0);
	}

	void Update ()
	{	//constant move down
		if (!GM.gamePaused)
		{
			transform.Translate (Vector2.down * Time.deltaTime * moveSpeed);

			intPos = IntPosition2D.Vector2ToIntPos2D (transform.position);


			//makes sure a bomb is dropped in every tile

			if (intPos != lastIntPos)
				bombDroped = false;

			if (intPos.Y == 23)
			{
				lastIntPos = intPos;
			}

			if (!bombDroped && intPos.Y == (lastIntPos.Y - 1))
			{
				for (int i = 0; i < spawnPoints.Length; i++)
				{
					DropBomb (spawnPoints [i]);
				}

				lastIntPos = intPos;
				bombDroped = true;
			}

			if (transform.position.y <= -3)
			{
				laneControl.SomthingInLane = false;
				DestroyImmediate (this.gameObject);
			}
		}
	}

	void DropBomb (GameObject spawnpos)
	{
		GameObject bombRef = Instantiate (bomb, spawnpos.transform.position, Quaternion.identity) as GameObject;

	}
}

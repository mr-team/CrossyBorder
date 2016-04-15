using UnityEngine;
using System.Collections;

public class PredatorDrone : MonoBehaviour
{

	public GameObject bomb;
	public GameObject[] spawnPoints = new GameObject[11];
	VerticalLanesController laneControl;

	float moveSpeed = 4;
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
		lastIntPos = new IntPosition2D (0, 0);
	}


	void Update ()
	{
		//constant move down
		transform.Translate (Vector2.down * Time.deltaTime * moveSpeed);

		intPos = IntPosition2D.Vector2ToIntPos2D (transform.position);

		if (intPos != lastIntPos)
			bombDroped = false;

		if (intPos.Y == 23)
		{
			lastIntPos = intPos;
		}

		if (!bombDroped && intPos.Y == (lastIntPos.Y - 1))
		{
			
			FireMissile ();


			lastIntPos = intPos;
			bombDroped = true;
		}

	}

	void FireMissile ()
	{
		int pos1;
		int pos2;
		int pos3;

		pos1 = Random.Range (0, 4);
		pos2 = Random.Range (4, 7);
		pos3 = Random.Range (7, 11);

		GameObject misilRef1 = Instantiate (bomb, spawnPoints [pos1].transform.position, Quaternion.identity) as GameObject;
		GameObject misilRef2 = Instantiate (bomb, spawnPoints [pos2].transform.position, Quaternion.identity) as GameObject;
		GameObject misilRef3 = Instantiate (bomb, spawnPoints [pos3].transform.position, Quaternion.identity) as GameObject;
	}
}

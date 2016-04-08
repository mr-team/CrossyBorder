using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	GameMaster GM;
	GameObject player;
	Camera mainCam;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		player = GameObject.Find ("Player");
		mainCam = Camera.main;
	}

	void Update ()
	{
		if (GM.gameLoopActive)
		{
			if (player.transform.position.y >= 4.5f)
				transform.position = Vector3.Slerp (transform.position, new Vector3 (5f, player.transform.position.y, -10), 0.2f);
			else
			{
				transform.position = Vector3.Slerp (transform.position, new Vector3 (5f, 4.5f, -10), 0.2f);
			}
		}



	}
}

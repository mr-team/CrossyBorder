using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

	GameMaster GM;
	GameObject player;
    Transform winCameraPoint;
	Camera mainCam;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		player = GameObject.Find ("Player");
		mainCam = Camera.main;
        winCameraPoint = GameObject.Find("CameraPoint").transform;
    }

	void Update ()
	{
		if (GM.gameLoopActive)
		{
			if (player.transform.position.y >= 5.4f)
				transform.position = Vector3.Slerp (transform.position, new Vector3 (5f, player.transform.position.y, -10), 0.2f);
			else
			{
				transform.position = Vector3.Slerp (transform.position, new Vector3 (5f, 5.4f, -10), 0.2f);
			}
		} else if (GM.gameTransition)
		{
			transform.position = Vector3.Slerp (transform.position, winCameraPoint.position, 0.02f);
		}
	}
}

using UnityEngine;
using System.Collections;

public class PlayerClimbLadder : MonoBehaviour
{
	public enum Cuts
	{
		firstCut,
		secondCut,
	}

	GameMaster GM;

	public Cuts cuts;
	public Camera cutSceneCamera;
	public GameObject player;

	Animator playerAnim;
	public Animator trumpAnim;
	public Animator playerMove;

	public GameObject[] panPoints = new GameObject[5];
	public GameObject[] playerPanPoints = new GameObject[5];

	public bool active;
	public bool activeKick;

	float timer;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		cutSceneCamera.transform.position = panPoints [0].transform.position;
		player.transform.position = playerPanPoints [0].transform.position;
		cutSceneCamera.cullingMask = -1;
		cutSceneCamera.enabled = false;
		playerAnim = player.GetComponent<Animator> ();
		playerAnim.SetBool ("Climbing", true);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.K))
			active = true;
		
		if (active)
		{
			switch (cuts)
			{
				case Cuts.firstCut:
					FirstCut ();
					break;

				case Cuts.secondCut:
					
					break;
			}
		}

		if (!active)
		{
			Reset ();
		}
	}

	void FirstCut ()
	{
		timer += Time.deltaTime;

		cutSceneCamera.enabled = true;
		playerMove.SetBool ("Climbing", true);
		playerAnim.SetBool ("Climbing", true);
		cutSceneCamera.transform.position = Vector3.MoveTowards (cutSceneCamera.transform.position, panPoints [1].transform.position, 0.02f);

		if (timer >= 6)
		{
			TransistToSecondCut ();
			timer = 0;
			//active = false;
		}

		//StartCoroutine (FirstMovement ());

	}

	void TransistToSecondCut ()
	{
		if (cuts != Cuts.secondCut)
		{
			//cutSceneCamera.cullingMask = 0;
			cutSceneCamera.transform.position = panPoints [2].transform.position;
			cutSceneCamera.orthographicSize = 9;
			playerAnim.SetBool ("Climbing", false);
			playerMove.SetBool ("Climbing", false);
			//cutSceneCamera.cullingMask = -1;
			player.GetComponent<SpriteRenderer> ().enabled = false;
			cuts = Cuts.secondCut;
		}
	}

	void Reset ()
	{
		cutSceneCamera.transform.position = panPoints [0].transform.position;
		player.transform.position = playerPanPoints [0].transform.position;
		cuts = Cuts.firstCut;
		cutSceneCamera.orthographicSize = 5;
		player.GetComponent<SpriteRenderer> ().enabled = true;
		playerAnim.SetBool ("Climbing", true);
		cutSceneCamera.enabled = false;
	}
}

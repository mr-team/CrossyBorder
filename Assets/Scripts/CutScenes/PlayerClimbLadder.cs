using UnityEngine;
using System.Collections;

public class PlayerClimbLadder : MonoBehaviour
{
	enum Cuts
	{
		firstCut,
		secondCut,
		End,
	}

	Cuts cuts;
	public Camera cutSceneCamera;
	public GameObject player;
	public GameObject[] panPoints = new GameObject[5];
	public GameObject[] playerPanPoints = new GameObject[5];

	Animator playerAnim;

	public bool active;

	void Start ()
	{
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
					SecondCut ();
					break;

				case Cuts.End:
					End ();
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
		cutSceneCamera.enabled = true;
		StartCoroutine (FirstMovement ());

	}

	void SecondCut ()
	{
		
		
	}

	void End ()
	{
		
	}

	void TransistToNextCut ()
	{
		//cutSceneCamera.cullingMask = 0;
		cutSceneCamera.transform.position = panPoints [2].transform.position;
		cutSceneCamera.orthographicSize = 9;
		playerAnim.SetBool ("Climbing", false);
		//cutSceneCamera.cullingMask = -1;

		cuts = Cuts.secondCut;

	}

	IEnumerator FirstMovement ()
	{
		Vector2 currPos = new Vector3 (cutSceneCamera.transform.position.x, cutSceneCamera.transform.position.y, -10);
		Vector2 pos = new Vector3 (panPoints [1].transform.position.x, panPoints [1].transform.position.y, -10);

		cutSceneCamera.transform.position = Vector3.MoveTowards (cutSceneCamera.transform.position, panPoints [1].transform.position, 0.02f);
		player.transform.position = Vector3.MoveTowards (player.transform.position, playerPanPoints [1].transform.position, 0.08f);
		yield return new WaitUntil (() => player.transform.position == playerPanPoints [1].transform.position);
	
		TransistToNextCut ();
	}

	void Reset ()
	{
		cutSceneCamera.transform.position = panPoints [0].transform.position;
		player.transform.position = playerPanPoints [0].transform.position;
		cuts = Cuts.firstCut;
		cutSceneCamera.orthographicSize = 5;
		playerAnim.SetBool ("Climbing", true);
		cutSceneCamera.enabled = false;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerClimbLadder : MonoBehaviour
{
	public enum Cuts
	{
		firstCut,
		secondCut,
		end,
	}

	GameMaster GM;
	PlayerKickedFromWall KickedFromWall;

	public Cuts cuts;
	public Camera cutSceneCamera;
	public GameObject player;

	//Sound
	public CustomAudioSource customAudioSource;
	public AudioClip walkUpLadderSong;
	public AudioClip standardSong;

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
		KickedFromWall = GetComponent<PlayerKickedFromWall> ();
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
		{
			active = true;
			cuts = Cuts.firstCut;
		}

		
		if (active)
		{
			GM.Player.Imortal = true;
			switch (cuts)
			{
				case Cuts.firstCut:
					FirstCut ();
					break;

				case Cuts.secondCut:
					SecondCut ();
					break;

				case Cuts.end:
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

		if (customAudioSource.source.clip != walkUpLadderSong)
		{
			customAudioSource.source.clip = walkUpLadderSong;
			customAudioSource.Play (walkUpLadderSong);
			
		}
		cutSceneCamera.enabled = true;
		playerMove.SetBool ("Climbing", true);
		playerAnim.SetBool ("Climbing", true);
		cutSceneCamera.transform.position = Vector3.MoveTowards (cutSceneCamera.transform.position, panPoints [1].transform.position, 0.02f);

		if (timer >= 2)
		{
			TransistToSecondCut ();


		}
	}

	void SecondCut ()
	{
		timer += Time.deltaTime;

		if (timer >= 2)
			TransistToEnd ();
	}


	void TransistToSecondCut ()
	{
		if (cuts != Cuts.secondCut)
		{
			timer = 0;
			playerMove.SetBool ("Climbing", false);
			playerMove.SetBool ("Climbing2", true);
			cutSceneCamera.transform.position = panPoints [2].transform.position;
			cutSceneCamera.orthographicSize = 9;
			cuts = Cuts.secondCut;
		}
	}

	void TransistToEnd ()
	{
		timer = 0;
		playerAnim.SetBool ("Climbing", false);
		playerMove.SetBool ("Climbing2", false);
		player.GetComponent<SpriteRenderer> ().enabled = false;
		KickedFromWall.active = true;
		cuts = Cuts.end;
	}

	void Reset ()
	{
		timer = 0;
		KickedFromWall.active = false;
		cutSceneCamera.transform.position = panPoints [0].transform.position;
		player.transform.position = playerPanPoints [0].transform.position;
		cuts = Cuts.firstCut;
		cutSceneCamera.orthographicSize = 5;
		player.GetComponent<SpriteRenderer> ().enabled = true;
		playerMove.SetBool ("Climbing2", false);
		playerAnim.SetBool ("Climbing", true);
		cutSceneCamera.enabled = false;
	}
}

using UnityEngine;
using System.Collections;

public class BearTrap : MonoBehaviour
{
	GameMaster GM;
	Animator anim;
	GameObject player;
	float timer;
	float maxCloseTime = 2;
	bool sprung;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		anim = GetComponent<Animator> ();
		GM.onPlayerLostLife += Reset;
	}

	void Update ()
	{
		//run only if the player is in the trap
		if (sprung)
		{
			timer += Time.deltaTime; 

			//Debug.Log ("timer");
			if (timer < maxCloseTime)
			{
				//Debug.Log ("the trap is closed on the player");
				player.gameObject.GetComponent<PlayerController> ().stunned = true;
				anim.SetBool ("Close", true);
			}
			if (timer > maxCloseTime - 0.6f)
			{
				anim.SetBool ("Close", false);
			}
			if (timer > maxCloseTime)
			{
				//Debug.Log ("the trap opened for the player");
				player.gameObject.GetComponent<PlayerController> ().stunned = false;

			}
		}
	}

	//called by a callback from the GM when the player loses a life.
	void Reset ()
	{
		if (player != null)
		{
			player.gameObject.GetComponent<PlayerController> ().stunned = false;
			anim.SetBool ("Close", false);
			timer = 0;
			sprung = false;
			player = null;
			
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			GetComponent<CustomAudioSource> ().Play ();
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			player = other.gameObject;
			sprung = true;	
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			Reset ();
		}
	}
}
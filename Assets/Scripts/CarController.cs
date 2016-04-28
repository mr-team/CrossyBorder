using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
	GameMaster GM;
	float moveSpeed = 6f;
	bool moveLeft;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	void Update ()
	{
		if (!GM.gamePaused)
		{
			if (!moveLeft)
			{
				transform.Translate (Vector2.left * Time.deltaTime * moveSpeed);
			} else
			{
				transform.Translate (Vector2.right * Time.deltaTime * moveSpeed);
			}
		}


		if (transform.position.x > 30 || transform.position.x < -20)
		{
			DestroyImmediate (this.gameObject);
		}
	}

	public void MoveLeft ()
	{
		moveLeft = true;
	}

	public void MoveRight ()
	{
		moveLeft = false;

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player" && !GM.gameReset)
		{
			GetComponent<CustomAudioSource>().Play();
            other.gameObject.GetComponent<PlayerController> ().Player.LoseLife ();
            
		}
	}
}

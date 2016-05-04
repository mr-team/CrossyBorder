using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour
{
	GameMaster GM;
	Animator anim;
	public GameObject explotion;
	public GameObject shovelPrefab;
	public GameObject stopWatchPrefab;
	float timer;
	float fuseTime = 1f;

	bool Sprung;
	bool playerInRange;
	int numExplotions;

	bool explode;
	bool hit;
	bool watch;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (GM.gameLoopActive && Sprung)
		{
			timer += Time.deltaTime;

			if (!watch)
			{
				GameObject stopWatch = Instantiate (stopWatchPrefab, transform.position, Quaternion.identity) as GameObject;
				stopWatch.GetComponent<FeedBack_StopWatch> ().oneSec = true;
				watch = true;

			}

			if (timer > fuseTime)
			{
				
				SpawnExplotions ();
				Destroy (this.gameObject, 0.5f);
				if (playerInRange && !hit)
				{
					GM.Player.LoseLife ();
					hit = true;
				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			
			Sprung = true;
			playerInRange = true;
		}
        if(other.tag == "Boom") {
            Sprung = true;
            timer = fuseTime;
            watch = true;
        }
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			
			playerInRange = false;
		}
	}

	void SpawnExplotions ()
	{
		if (numExplotions <= 9)
		{
			for (int i = -1; i < 2; i++)
			{
				for (int o = -1; o < 2; o++)
				{
					int ran = Random.Range (0, 900);

					Vector2 pos = new Vector2 (transform.position.x + i, transform.position.y + o);
					GameObject exp = Instantiate (explotion, pos, Quaternion.identity) as GameObject;
                    exp.tag = "Boom";
                    exp.AddComponent<Rigidbody2D>().isKinematic = true;
					numExplotions++;
					//Debug.Log (ran);
					if (ran > 0 && ran <= 3)
					{
						try
						{
							if (GM.World.Tiles [(int)pos.x, (int)pos.y].Walkable == true)
								Instantiate (shovelPrefab, pos, Quaternion.identity);
		
						} catch
						{
							Debug.Log ("did not spawn a shovel");
						}
					}
				}
			}
		}
	}
}

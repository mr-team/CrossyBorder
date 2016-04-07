using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	GameMaster GM;
	SpriteRenderer playerRenderer;
	Player player;
	int startX = 1;
	int startY = 1;
	float timer;
	float timer2;
	float waitBeforeRun = 0.5f;
	bool moving;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		playerRenderer = GetComponent<SpriteRenderer> ();
		player = GM.Player;
		player.Pos = new Vector2 (startX, startY);
		player.IntPos = IntPosition2D.Vector2ToIntPos2D (player.Pos);
		transform.position = player.Pos;

		playerRenderer.enabled = false;
	}

	public void Update ()
	{
		if (GM.gameLoopActive)
		{
			playerRenderer.enabled = true;
			MovePlayer ();
			CheckTile (player.IntPos);
		}
	}

	void MovePlayerLaggy ()
	{
		if (Input.GetKeyDown (KeyCode.W))
		{
			player.MoveUp ();
			transform.position = player.Pos;

		}
		if (Input.GetKeyDown (KeyCode.A))
		{

			player.MoveLeft ();
			transform.position = player.Pos;
		}
		if (Input.GetKeyDown (KeyCode.S))
		{
			player.MoveDown ();
			transform.position = player.Pos;

		}
		if (Input.GetKeyDown (KeyCode.D))
		{
			
			player.MoveRight ();
			transform.position = player.Pos;
		}

		//if key is held for more than 1.5 seconds
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D))
		{
			
			timer += Time.deltaTime;
			
			if (Input.GetKey (KeyCode.W) && timer > waitBeforeRun)
			{
				
				timer2 += Time.deltaTime;

				if (timer2 > 0.1f)
				{
					player.MoveUp ();
					transform.position = player.Pos;
					timer2 = 0;
				}
			}
			if (Input.GetKey (KeyCode.A) && timer > waitBeforeRun)
			{
				
				timer2 += Time.deltaTime;

				if (timer2 > 0.1f)
				{
					player.MoveLeft ();
					transform.position = player.Pos;
					timer2 = 0;
				}
			}
			if (Input.GetKey (KeyCode.S) && timer > waitBeforeRun)
			{
				
				timer2 += Time.deltaTime;

				if (timer2 > 0.1f)
				{
					player.MoveDown ();
					transform.position = player.Pos;
					timer2 = 0;
				}
			}
			if (Input.GetKey (KeyCode.D) && timer > waitBeforeRun)
			{
				
				timer2 += Time.deltaTime;

				if (timer2 > 0.1f)
				{
					player.MoveRight ();
					transform.position = player.Pos;
					timer2 = 0;
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.D))
		{
			timer = 0;
		}
	}

	void MovePlayer ()
	{
		if (Input.GetKeyDown (KeyCode.W) && !moving)
		{
			player.MoveUp ();

			moving = true;
		}
		if (Input.GetKeyDown (KeyCode.A) && !moving)
		{
			player.MoveLeft ();

			moving = true;
		}
		if (Input.GetKeyDown (KeyCode.S) && !moving)
		{
			player.MoveDown ();

			moving = true;
		}
		if (Input.GetKeyDown (KeyCode.D) && !moving)
		{
			player.MoveRight ();

			moving = true;
		}

		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D))
		{
			timer += Time.deltaTime;

			if (Input.GetKey (KeyCode.W) && timer > waitBeforeRun)
			{

				timer2 += Time.deltaTime;

				if (timer2 > 0.34f)
				{
					player.MoveUp ();
					moving = true;
					timer2 = 0;
				}
			}
			if (Input.GetKey (KeyCode.A) && timer > waitBeforeRun)
			{

				timer2 += Time.deltaTime;

				if (timer2 > 0.34f)
				{
					player.MoveLeft ();
					moving = true;
					timer2 = 0;
				}
			}
			if (Input.GetKey (KeyCode.S) && timer > waitBeforeRun)
			{

				timer2 += Time.deltaTime;

				if (timer2 > 0.34f)
				{
					player.MoveDown ();
					moving = true;
					timer2 = 0;
				}
			}
			if (Input.GetKey (KeyCode.D) && timer > waitBeforeRun)
			{

				timer2 += Time.deltaTime;

				if (timer2 > 0.34f)
				{
					player.MoveRight ();
					moving = true;
					timer2 = 0;
				}
			}

		}
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.D))
		{
			timer = 0;
		}

		MovePlayerSmoth (player.Pos);
	}

	void MovePlayerSmoth (Vector3 targetPos)
	{
		if (moving)
			transform.position = Vector2.MoveTowards (transform.position, targetPos, 0.05f);

		if (transform.position == targetPos)
			moving = false;
	}

	void CheckTile (IntPosition2D pos)
	{
		Tile tile = GM.World.Tiles [pos.X, pos.Y];

		if (tile.Deadly)
		{
			player.Alive = false;
		}
	}
}

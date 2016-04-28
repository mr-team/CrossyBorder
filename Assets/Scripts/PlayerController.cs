using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public enum Direction
	{
		up,
		down,
		left,
		right
	}

	public enum States
	{
		playerDeactive,
		playerAlive,
		PlayerLostLife,
		playerDead,

	}

	public enum ActionStates
	{

		idle,
		tunneling,
	}

	public ActionStates actionStates;
	public States playerStates;
	public  Direction direction;

	public delegate void OnPlayerChangePos ();

	public OnPlayerChangePos onPlayerChangePos;
	GameMaster GM;
	SpriteRenderer playerRenderer;
	Animator playerAnim;
	Player player;

	int startX = 5;
	int startY = 1;

	public int shovelCount;

	float timer;
	float timer2;
	float tpDelayTimer;
	float getOutOfHoleTimer;
	float waitBeforeRun = 0.5f;


	bool moving;
	bool tunnel;
	public bool canTunnel;
	public bool stunned;
	public Vector2 startPos;

	public Player Player {
		get
		{
			return player;
		}
	}

	void Start ()
	{
		
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		GM.onNextRound = ResetPlayer;
		player = GM.Player;
		startPos = new Vector2 (5, 1);
		playerStates = States.playerAlive;
		player.OnLoseLife += TransToLostLife;
		playerRenderer = GetComponent<SpriteRenderer> ();
		playerAnim = GetComponent<Animator> ();
		player.Pos = new Vector2 (startX, startY);
		transform.position = player.Pos;
		playerRenderer.enabled = false;
	}

	public void Update ()
	{
		switch (playerStates)
		{
			case States.playerDeactive:
				UpdatePlayerDeActive ();
				break;

			case States.playerAlive:
				UpdatePlayerAlive ();
				break;

			case States.PlayerLostLife:
				UpdatePlayerLostLife ();
				break;

			case States.playerDead:
				UpdatePlayerDead ();
				break;

		}
	}

	//state handlers
	void UpdatePlayerDeActive ()
	{
		playerRenderer.enabled = false;
		if (GM.gameLoopActive)
			playerStates = States.playerAlive;
	}

	void UpdatePlayerAlive ()
	{
		playerRenderer.enabled = true;
		
		if (!GM.gamePaused)
		{

			switch (actionStates)
			{
				case ActionStates.idle:
					UpdateIdle ();
					break;
				
				case ActionStates.tunneling:
					UpdateTunneling ();
					break;
			}

			MovePlayer ();
			if (player.Alive && player.Lives <= 0)
			{
				GetComponent<CustomAudioSource> ().Play ();
				playerStates = States.playerDead;
			}
		}

		if (!GM.gameLoopActive)
			playerStates = States.playerDeactive;
	}

	void UpdatePlayerLostLife ()
	{
		if (player.Lives > 0)
		{
			player.Pos = startPos;
			transform.position = (player.Pos);

			playerAnim.SetBool ("FaceUp", true);
			playerAnim.SetBool ("FaceLeft", false);
			playerAnim.SetBool ("FaceDown", false);
			playerAnim.SetBool ("FaceRight", false);

		}
		actionStates = ActionStates.idle;
		playerStates = States.playerAlive;
	}

	void UpdatePlayerDead ()
	{
		playerAnim.SetBool ("Dead", true);
		player.Alive = false;
	}


	//functions
	void MovePlayer ()
	{
		if (GM.gameLoopActive && !stunned)
		{
			if (Input.GetKeyDown (KeyCode.W) && !moving || Input.GetKeyDown (KeyCode.UpArrow) && !moving)
			{
				player.MoveUp ();
				SetFaceDirection (Direction.up);
				moving = true;
				onPlayerChangePos ();
			}
			if (Input.GetKeyDown (KeyCode.A) && !moving || Input.GetKeyDown (KeyCode.LeftArrow) && !moving)
			{
				player.MoveLeft ();
				SetFaceDirection (Direction.left);
				moving = true;
				onPlayerChangePos ();
			}
			if (Input.GetKeyDown (KeyCode.S) && !moving || Input.GetKeyDown (KeyCode.DownArrow) && !moving)
			{
				player.MoveDown ();
				SetFaceDirection (Direction.down);
				moving = true;
				onPlayerChangePos ();
			}
			if (Input.GetKeyDown (KeyCode.D) && !moving || Input.GetKeyDown (KeyCode.RightArrow) && !moving)
			{
				player.MoveRight ();
				SetFaceDirection (Direction.right);
				moving = true;
				onPlayerChangePos ();
			}
		}
	
		/*if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D))
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
		}*/

		MovePlayerSmoth (player.Pos);
	}

	void MovePlayerSmoth (Vector3 targetPos)
	{
		if (moving)
			transform.position = Vector2.MoveTowards (transform.position, targetPos, 0.13f);

		if (transform.position == targetPos)
			moving = false;

		onPlayerChangePos ();
	}

	void CheckTile (IntPosition2D pos)
	{
		Tile tile = GM.World.Tiles [pos.X, pos.Y];

		if (tile.Deadly)
		{
			player.LoseLife ();
		}
	}

	void TransToLostLife ()
	{
		
		playerStates = States.PlayerLostLife;
	}

	public void kill ()
	{
		playerStates = States.playerDead;
	}

	public void ResetPlayer ()
	{
		player.Pos = startPos;
		transform.position = (player.Pos);
		playerAnim.SetBool ("FaceUp", true);
		playerAnim.SetBool ("FaceLeft", false);
		playerAnim.SetBool ("FaceDown", false);
		playerAnim.SetBool ("FaceRight", false);
	}

	/// <summary>
	/// Tps the player.
	/// </summary>
	/// <param name="pos">Position.</param>
	public void tpPlayer (Vector2 pos)
	{
		player.Pos = pos;
		transform.position = (player.Pos);
		playerAnim.SetBool ("FaceUp", true);
		playerAnim.SetBool ("FaceLeft", false);
		playerAnim.SetBool ("FaceDown", false);
		playerAnim.SetBool ("FaceRight", false);
		
	}

	/// <summary>
	/// Tps the player with a delay.
	/// </summary>
	/// <returns><c>true</c>, if player was tped, <c>false</c> otherwise.</returns>
	/// <param name="pos">Position.</param>
	/// <param name="delay">Delay.</param>
	public bool tpPlayer (Vector2 pos, float delay)
	{
		tpDelayTimer += Time.deltaTime;

		if (tpDelayTimer >= delay)
		{
			player.Pos = pos;
			transform.position = (player.Pos);
			playerAnim.SetBool ("FaceUp", true);
			playerAnim.SetBool ("FaceLeft", false);
			playerAnim.SetBool ("FaceDown", false);
			playerAnim.SetBool ("FaceRight", false);
			tpDelayTimer = 0f;
			return true;
		}
		return false;
	}


	void SetFaceDirection (Direction dir)
	{
		if (dir == Direction.up)
		{
			playerAnim.SetBool ("FaceUp", true);
			playerAnim.SetBool ("FaceLeft", false);
			playerAnim.SetBool ("FaceDown", false);
			playerAnim.SetBool ("FaceRight", false);
		}
		if (dir == Direction.left)
		{
			playerAnim.SetBool ("FaceUp", false);
			playerAnim.SetBool ("FaceLeft", true);
			playerAnim.SetBool ("FaceDown", false);
			playerAnim.SetBool ("FaceRight", false);
		}
		if (dir == Direction.down)
		{
			playerAnim.SetBool ("FaceUp", false);
			playerAnim.SetBool ("FaceLeft", false);
			playerAnim.SetBool ("FaceDown", true);
			playerAnim.SetBool ("FaceRight", false);
		}
		if (dir == Direction.right)
		{
			playerAnim.SetBool ("FaceUp", false);
			playerAnim.SetBool ("FaceLeft", false);
			playerAnim.SetBool ("FaceDown", false);
			playerAnim.SetBool ("FaceRight", true);
		}
	}

	void UpdateIdle ()
	{
		if (Input.GetKeyDown (KeyCode.T))
		{
			if (shovelCount > 0)
			{
				actionStates = ActionStates.tunneling;
			} else
				Debug.Log ("you aint got no shovels to be shovveling no tunnel");
		}
	}

	void UpdateTunneling ()
	{
		

		//get the end tile based on hov many shovels the player is carrying
		int travelTilesAmount;
		travelTilesAmount = 5 * shovelCount;
		IntPosition2D endPos = new IntPosition2D (player.IntPos.X, player.IntPos.Y + travelTilesAmount);

		//check if end tile is walkable
		if (GM.World.Tiles [endPos.X, endPos.Y].Walkable == true)
		{
			Debug.Log ("i can tunnel to the end!");
			canTunnel = true;

		} else
		{
			Debug.Log ("i Can't tunnel to the end");

			canTunnel = false;
		}

		//transport player && removeshovels

		if (canTunnel && Input.GetKeyDown (KeyCode.T))
			tunnel = true;
		
		if (tunnel)
		{
			if (tpPlayer (new Vector2 (endPos.X, endPos.Y), 1f))
			{
				
				shovelCount = 0;
				tunnel = false;
				actionStates = ActionStates.idle;
			}
		}
	}
}
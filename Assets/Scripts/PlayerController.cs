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
		catapulting,
	}

	public ActionStates actionStates;
	public States playerStates;
	public  Direction direction;

	public delegate void OnPlayerChangePos ();

	public OnPlayerChangePos onPlayerChangePos;
	GameMaster GM;
	PlayerHudGizmos hudGizmos;
	SpriteRenderer playerRenderer;
	Animator playerAnim;
	Player player;

	int startX = 5;
	int startY = 1;

	int tunnelDistance = 6;
	public int shovelCount;

	float timer;
	float timer2;
	float flashTimer;
	public float tpDelayTimer;
	public float stunTimer;
	float getOutOfHoleTimer;
	float waitBeforeRun = 0.5f;

	//throwing player
	float distanceToThrow;
	float distanceToGo;
	float playerscale;
	bool distSet;

	bool moving;
	bool tunnel;
	bool flashSprites;

	public bool canTunnel;
	public bool stunned;

	public Vector2 startPos;

	public int TunnelDistance {
		get
		{
			return tunnelDistance;
		}
	}

	public Player Player {
		get
		{
			return player;
		}
	}

	void Start ()
	{
		//Assignments
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		player = GM.Player;
		hudGizmos = GetComponent<PlayerHudGizmos> ();
		playerRenderer = GetComponent<SpriteRenderer> ();
		playerAnim = GetComponent<Animator> ();
		playerStates = States.playerAlive;

		//CallBacks
		player.OnLoseLife += TransToLostLife;
		GM.onNextRound += ResetPlayer;

		//variables
		startPos = new Vector2 (startX, startY);
		player.Pos = new Vector2 (startX, startY);
		transform.position = player.Pos;
		playerRenderer.enabled = false;
		playerscale = transform.localScale.x;
	
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
			if (flashSprites)
				FlashSprite (30);

			switch (actionStates)
			{
				case ActionStates.idle:
					UpdateIdle ();
					break;
				
				case ActionStates.tunneling:
					UpdateTunneling ();
					break;

				case ActionStates.catapulting:
					UpdateCatapulting ();
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
			DeStunPlayer ();
			flashSprites = true;

			playerAnim.SetBool ("DigDown", false);
			canTunnel = false;
			tunnel = false;
		}
			
		actionStates = ActionStates.idle;
		playerStates = States.playerAlive;
	}

	void UpdatePlayerDead ()
	{
		playerAnim.SetBool ("Dead", true);
		player.Alive = false;
	}


	//Action States handlers
	void UpdateIdle ()
	{
		
		if (Input.GetKeyDown (KeyCode.Alpha1))
		{
			if (shovelCount > 0)
			{

				actionStates = ActionStates.tunneling;
			} 

		}
	}

	void UpdateTunneling ()
	{

		//get the end tile based on hov many shovels the player is carrying
		int travelTilesAmount;
		travelTilesAmount = tunnelDistance;
		IntPosition2D endPos = new IntPosition2D (player.IntPos.X, player.IntPos.Y + travelTilesAmount);

		//check if end tile is walkable

		try
		{
			if (GM.World.Tiles [endPos.X, endPos.Y].Walkable == true)
			{
				Debug.Log ("i can tunnel to the end!");
				canTunnel = true;

			} else
			{
				Debug.Log ("i Can't tunnel to the end");
				canTunnel = false;
			}
		} catch
		{
			canTunnel = false;
		}


		//transport player && remove shovels
		if (Input.GetKeyDown (KeyCode.Q))
		{
			actionStates = ActionStates.idle;
			canTunnel = false;
			tunnel = false;
		}
		if (canTunnel && !tunnel && Input.GetKeyDown (KeyCode.Alpha1))
		{
			hudGizmos.clearHUD = true;
			StunPlayer ();
			playerAnim.SetBool ("DigDown", true);
			tunnel = true;
			player.Imortal = true;
		}

		if (tunnel)
		{

			if (tpPlayer (new Vector2 (endPos.X, endPos.Y), 0.5f))
			{
				shovelCount--;
				playerAnim.SetBool ("DigDown", false);

			}

			if (DeStunPlayerDelay (0.9f))
			{
				canTunnel = false;
				tunnel = false;
				player.Imortal = false;
				tpDelayTimer = 0f;
				hudGizmos.clearHUD = false;
				actionStates = ActionStates.idle;

			}
		}
	}

	void UpdateCatapulting ()
	{
		
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

			}
			if (Input.GetKeyDown (KeyCode.A) && !moving || Input.GetKeyDown (KeyCode.LeftArrow) && !moving)
			{
				player.MoveLeft ();
				SetFaceDirection (Direction.left);
				moving = true;

			}
			if (Input.GetKeyDown (KeyCode.S) && !moving || Input.GetKeyDown (KeyCode.DownArrow) && !moving)
			{
				player.MoveDown ();
				SetFaceDirection (Direction.down);
				moving = true;

			}
			if (Input.GetKeyDown (KeyCode.D) && !moving || Input.GetKeyDown (KeyCode.RightArrow) && !moving)
			{
				player.MoveRight ();
				SetFaceDirection (Direction.right);
				moving = true;

			}
		}
	
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
		player.Imortal = false;
		transform.position = (player.Pos);

		playerAnim.SetBool ("FaceUp", true);
		playerAnim.SetBool ("FaceLeft", false);
		playerAnim.SetBool ("FaceDown", false);
		playerAnim.SetBool ("FaceRight", false);
	}

	public void FlashSprite (int amount)
	{
		flashTimer += Time.deltaTime;
		float basef = 0.2f;
		if (flashTimer <= basef)
		{
			GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (flashTimer <= basef + 0.2f && flashTimer > basef)
		{
			GetComponent<SpriteRenderer> ().enabled = true;
		}
		if (flashTimer <= basef + 0.4f && flashTimer > basef + 0.2f)
		{
			GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (flashTimer <= basef + 0.6f && flashTimer > basef + 0.4f)
		{
			GetComponent<SpriteRenderer> ().enabled = true;
		}
		if (flashTimer <= basef + 0.8f && flashTimer > basef + 0.6)
		{
			GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (flashTimer <= basef + 1 && flashTimer > basef + 0.8f)
		{
			GetComponent<SpriteRenderer> ().enabled = true;
		}
		if (flashTimer <= basef + 1.2f && flashTimer > basef + 1f)
		{
			GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (flashTimer <= basef + 1.4f && flashTimer > basef + 1.2f)
		{
			GetComponent<SpriteRenderer> ().enabled = true;
			flashTimer = 0;
			player.Imortal = false;
			flashSprites = false;
		
		}
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
			tpDelayTimer = 0f;
			player.Pos = pos;
			transform.position = (player.Pos);
			playerAnim.SetBool ("FaceUp", true);
			playerAnim.SetBool ("FaceLeft", false);
			playerAnim.SetBool ("FaceDown", false);
			playerAnim.SetBool ("FaceRight", false);
			return true;
		}
		return false;
	}

	public bool ThrowPlayer (Vector3 pos, float delay)
	{
		tpDelayTimer += Time.deltaTime;

		if (!distSet)
		{
			distanceToThrow = Vector2.Distance (transform.position, pos);
			distSet = true;
		}

		distanceToGo = Vector2.Distance (transform.position, pos);

		if (tpDelayTimer >= delay)
		{
			if (distanceToGo > distanceToThrow / 2)
			{
				playerscale = Mathf.Lerp (1, 1.5f, (0.35f * 2));
			}
			if (distanceToGo < distanceToThrow / 2)
			{
				playerscale = Mathf.Lerp (1.5f, 1, (0.35f * 2));
			}

			Vector2 scale = new Vector2 (playerscale, playerscale);
			transform.localScale = scale;


			GetComponent<BoxCollider2D> ().enabled = false;
			player.Imortal = true;
			transform.position = Vector2.MoveTowards (transform.position, pos, 0.35f);
			playerAnim.SetBool ("FaceUp", true);
			playerAnim.SetBool ("FaceLeft", false);
			playerAnim.SetBool ("FaceDown", false);
			playerAnim.SetBool ("FaceRight", false);

			if (transform.position == pos)
			{
				tpDelayTimer = 0f;
				player.Pos = pos;
				transform.localScale = new Vector2 (1, 1);
				GetComponent<BoxCollider2D> ().enabled = true;
				player.Imortal = false;
				return true;
			}
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

	public void StunPlayer ()
	{
		stunned = true;
	}

	public void DeStunPlayer ()
	{
		stunned = false;
	}

	public bool DeStunPlayerDelay (float delay)
	{
		stunTimer += Time.deltaTime;
		//Debug.Log (stunTimer);
		if (stunTimer >= delay)
		{
			stunTimer = 0;
			stunned = false;
			return true;
		}

		return false;
	}


}
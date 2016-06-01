using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
	public GameObject[] ShovelGraphics = new GameObject[3];
	public GameObject[] livesGraphics = new GameObject[5];
	GameMaster GM;
	PlayerController PC;
	public Text timeText;
	public Text livesLeft;
	public Text ladderCounter;
	public Text spadeCounter;
	public Text scoreText;

	float timeLeft;
	float timeLimit = 200;
	float timeDelta = 10;

	public bool debugStopCount;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		PC = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}

	void Update ()
	{
		
		if (GM.gameLoopActive)
		{
			//timeText.text = ("Time left: " + ParseSeconds (timeLimit - timeLeft));
			//livesLeft.text = ("Lives left: " + GM.Player.Lives);
			ladderCounter.text = (GM.ladderCount + " / " + GM.maxLadder);
			scoreText.text = ("" + GM.score);

			ShovelGraphics [0].GetComponent<RawImage> ().enabled = PC.shovelCount >= 1;
			ShovelGraphics [1].GetComponent<RawImage> ().enabled = PC.shovelCount >= 2;
			ShovelGraphics [2].GetComponent<RawImage> ().enabled = PC.shovelCount >= 3;
		}

		if (GM.gameLoopActive && !GM.gamePaused && !debugStopCount)
		{
			//timeLeft += Time.deltaTime * timeDelta;
			timeLeft = 0f;
			if (timeLeft >= timeLimit)
			{
				GM.Player.LoseLife ();
				timeLeft = 0;
			}
		}
	}

	public string ParseSeconds (float time)
	{
		string[] tid = time.ToString ().Split (".".ToCharArray ());
		return tid [0];
	}

	public void ResetTimer ()
	{
		timeLeft = 0;
	}
}

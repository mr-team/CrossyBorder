using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHudGizmos : MonoBehaviour
{

	public GameObject[] tunnelingHUD;

	public List<GameObject> HudObjects = new List<GameObject> ();


	GameMaster GM;
	PlayerController playerControl;
	int runs;
	bool spawned;

	void Start ()
	{
		GM = GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		playerControl.onPlayerChangePos += UpdateTunnelingHudPosition;
	}

	void Update ()
	{
		switch (playerControl.actionStates)
		{

			case  PlayerController.ActionStates.idle:
				break;
			case  PlayerController.ActionStates.tunneling:
				UpdateTunneling ();
				break;
		}
	}

	void UpdateTunneling ()
	{
		//draw the right sprite at an interval of 1 from the players positon +1
		if (!spawned)
		{
			SpawnTunnelingHud ();
			spawned = true;
		}


		//at the end draw the tip (5th, 10th, 15th osv)
	}

	void SpawnTunnelingHud ()
	{
		runs = 0;

		for (int i = 1; i < playerControl.shovelCount + 1; i++)
		{
			if (runs <= 6 * i)
			{
				for (int u = 1; u < 6 * i; u++)
				{
					if (u != 5)
					{
						Vector2 pos = new Vector2 (transform.position.x, transform.position.y + u);
						GameObject hudObj = Instantiate (tunnelingHUD [0], pos, Quaternion.identity)as GameObject;
						hudObj.name = ("Body: " + u);
						HudObjects.Add (hudObj);
					} else if (u == 5)
					{
						Vector2 pos = new Vector2 (transform.position.x, transform.position.y + u);
						GameObject hudTipObj = Instantiate (tunnelingHUD [1], pos, Quaternion.identity)as GameObject;
						hudTipObj.name = ("Tip: " + u);
						HudObjects.Add (hudTipObj);
					}
					runs++;
				}
			}
		}
	}

	void UpdateTunnelingHudPosition ()
	{
		if (playerControl.actionStates == PlayerController.ActionStates.tunneling)
		{
			for (int i = 0; i < tunnelingHUD.Length; i++)
			{
				DestroyImmediate (HudObjects [i]);
				HudObjects.RemoveAt (i);
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHudGizmos : MonoBehaviour
{

	public GameObject[] tunnelingHUD;

	public GameObject HudObjParentPrefab;
	public GameObject HudObjParent;
	GameMaster GM;
	PlayerController playerControl;
	int runs;
	bool spawned;
	public bool clearHUD;


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
				UpdateIdle ();
				break;
			case  PlayerController.ActionStates.tunneling:
				UpdateTunneling ();
				break;
		}
	}

	void UpdateIdle ()
	{
		ClearHUD ();
	}

	void UpdateTunneling ()
	{
		
		//draw the right sprite at an interval of 1 from the players positon +1
		if (!spawned)
		{
			SpawnTunnelingHud ();
			spawned = true;
		}
		if (clearHUD)
		{
			ClearHUD ();
		}

		//at the end draw the tip (5th, 10th, 15th osv)
	}

	void SpawnTunnelingHud ()
	{
		runs = 0;

		if (runs <= 6)
		{
			for (int u = 1; u < playerControl.TunnelDistance + 1; u++)
			{
				if (u != playerControl.TunnelDistance)
				{
					
					Vector2 pos = new Vector2 (transform.position.x, transform.position.y + u);
					GameObject hudObj = Instantiate (tunnelingHUD [0], pos, Quaternion.identity)as GameObject;
					hudObj.name = ("Body: " + u);
					hudObj.transform.parent = HudObjParent.transform;
					
				} else if (u == playerControl.TunnelDistance)
				{
					if (playerControl.canTunnel)
					{
						
						Vector2 pos = new Vector2 (transform.position.x, transform.position.y + u);
						GameObject hudTipObj = Instantiate (tunnelingHUD [1], pos, Quaternion.identity)as GameObject;
						hudTipObj.name = ("Tip: " + u);
						hudTipObj.transform.parent = HudObjParent.transform;

					} else if (!playerControl.canTunnel)
					{
						
						Vector2 pos = new Vector2 (transform.position.x, transform.position.y + u);
						GameObject hudTipObj = Instantiate (tunnelingHUD [2], pos, Quaternion.identity)as GameObject;
						hudTipObj.name = ("Tip: " + u);
						hudTipObj.transform.parent = HudObjParent.transform;
							
					}
				}
				runs++;
			}
		}
	}

	void UpdateTunnelingHudPosition ()
	{
		
		if (playerControl.actionStates == PlayerController.ActionStates.tunneling)
		{
			
			DestroyImmediate (HudObjParent);
			HudObjParent = Instantiate (HudObjParentPrefab, Vector2.zero, Quaternion.identity) as GameObject;
			SpawnTunnelingHud ();
		}
	}

	public void ClearHUD ()
	{
		DestroyImmediate (HudObjParent);
		HudObjParent = Instantiate (HudObjParentPrefab, Vector2.zero, Quaternion.identity) as GameObject;
	}
}

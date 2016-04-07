using UnityEngine;
using System.Collections;

public class CanvasHandler : MonoBehaviour
{
	GameMaster GM;
	public Canvas mainMenu;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	void Update ()
	{
		if (GM.gameLoopActive)
			mainMenu.enabled = false;
	}
}

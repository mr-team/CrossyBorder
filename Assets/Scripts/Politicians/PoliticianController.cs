using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoliticianController : MonoBehaviour
{
	public enum ChoseState
	{
		notchosen,
		chosen,
	
	}

	public ChoseState choseState;
	GameMaster GM;
	public politician sanders;
	public politician cruz;
	public politician trump;
	public politician clinton;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		
		sanders.clickCB = ClickOnpolitician;
		clinton.clickCB = ClickOnpolitician;
		cruz.clickCB = ClickOnpolitician;
		trump.clickCB = ClickOnpolitician;

	}

	void Update ()
	{
		switch (choseState)
		{
			case ChoseState.notchosen:
				UpdateNotChosen ();
				break;

			case ChoseState.chosen:
				UpdateChosen ();
				break;
		}
	}

	void UpdateNotChosen ()
	{
		
	}

	void UpdateChosen ()
	{
		GM.NextRound ();
		choseState = ChoseState.notchosen;
	}

	void ClickOnpolitician (GameObject politician)
	{
		choseState = ChoseState.chosen;

	}
}

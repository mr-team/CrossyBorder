using UnityEngine;
using System.Collections;

public class LivesCounter : MonoBehaviour
{
	public GameObject[] lifeHats = new GameObject[5];

	GameMaster GM;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	void Update ()
	{
		switch (GM.Player.Lives)
		{

			case 5:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					lifeHats [i].SetActive (true);

				}
				break;

			case 4:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					lifeHats [4].SetActive (false);
				}
				break;

			case 3:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					lifeHats [3].SetActive (false);
				}
				break;

			case 2:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					
					lifeHats [2].SetActive (false);
				}
				break;

			case 1:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					lifeHats [1].SetActive (false);
				}
				break;

			case 0:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					lifeHats [0].SetActive (false);
				}
				break;
		}
	}
}

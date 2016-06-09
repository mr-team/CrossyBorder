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
        lifeHats[4].SetActive(GM.Player.Lives >= 5);
        lifeHats[3].SetActive(GM.Player.Lives >= 4);
        lifeHats[2].SetActive(GM.Player.Lives >= 3);
        lifeHats[1].SetActive(GM.Player.Lives >= 2);
        lifeHats[0].SetActive(GM.Player.Lives >= 1);

        //wtf is dis?

        /*switch (GM.Player.Lives)
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
					lifeHats [3].SetActive (true);
					lifeHats [2].SetActive (true);
					lifeHats [1].SetActive (true);
					lifeHats [0].SetActive (true);
				}
				break;

			case 3:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					lifeHats [3].SetActive (false);
					lifeHats [2].SetActive (true);
					lifeHats [1].SetActive (true);
					lifeHats [0].SetActive (true);
				}
				break;

			case 2:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					
					lifeHats [2].SetActive (false);
					lifeHats [1].SetActive (true);
					lifeHats [0].SetActive (true);

				}
				break;

			case 1:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					lifeHats [1].SetActive (false);
					lifeHats [0].SetActive (true);
				}
				break;

			case 0:
				for (int i = 0; i < lifeHats.Length; i++)
				{
					lifeHats [0].SetActive (false);
				}
				break;
		}*/
    }
}

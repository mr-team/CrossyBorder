using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShovelCounter : MonoBehaviour
{
	GameObject[] shovelImages = new GameObject[3];
	GameMaster GM;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}


	void Update ()
	{
	
	}
}

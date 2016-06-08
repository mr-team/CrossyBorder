using UnityEngine;
using System.Collections;

public class PoliticianRandomAnim : MonoBehaviour
{

	Animator polAnim;
	float timer;
	float timer2;

	void Start ()
	{
		polAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= 4)
		{
			int ran = Random.Range (1, 5);

			if (ran == 2)
			{	
				polAnim.SetBool ("Action", true);
				timer = 0;
			} else
			{
				polAnim.SetBool ("Action", false);
				timer = 0;
			}

		}
	}
}

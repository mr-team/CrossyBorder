﻿using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
	Animator bombAnim;
	GameMaster GM;
	public GameObject explosion;
	float timer;
	float fuseTime = 1;

	bool explode;
	bool hit;
	bool overPlayer;
	bool once;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		bombAnim = GetComponent<Animator> ();
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= fuseTime)
		{
			explode = true;
			Destroy (this.gameObject, 1f);
		}

		HandleAnimation ();

		if (!hit && explode && overPlayer)
		{

			GM.Player.LoseLife ();
			hit = true;
		}
	}

	void HandleAnimation ()
	{
		if (explode && !once)
		{
			Instantiate (explosion, transform.position, Quaternion.identity);
			once = true;

			//bombAnim.SetBool ("Explode", true);
		}
		Destroy (this.gameObject, 1.5f);
	}

	//Triggers

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.transform.tag == "Player")
		{

			overPlayer = true;
		}
	}

	//Only for destroying cars, is buggy with the player
	void OnTriggerStay2D (Collider2D other)
	{
		if (explode && other.transform.tag == "Car")
		{
			Destroy (other.gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.transform.tag == "Player")
		{

			overPlayer = false;
		}
	}
}

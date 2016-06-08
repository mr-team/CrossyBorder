using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour
{
	GameMaster GM;
	public Sprite[] sprites;
	public float speed = 0.3f;

	private float timer = 0f;
	private int spriteIndex = 0;
	private SpriteRenderer renderer;

	void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		renderer = GetComponent<SpriteRenderer> ();
		if (sprites.Length == 0)
			Debug.LogError ("No animation sprites found");
	}

	void Update ()
	{
		if (GM.ladderCount < GM.maxLadder)
		{
			
			timer += Time.deltaTime; 
			if (timer >= speed)
			{
				if (spriteIndex == 6)
				{
					spriteIndex = 0;
					renderer.sprite = sprites [spriteIndex];
				} else
				{
					spriteIndex++;
					renderer.sprite = sprites [spriteIndex];
				}
				timer = 0f;

			}
		} else if (GM.ladderCount >= GM.maxLadder)
		{
			
			timer += Time.deltaTime; 
			if (timer >= speed + 0.04)
			{
				if (spriteIndex < 7)
				{
					spriteIndex = 7;
					renderer.sprite = sprites [spriteIndex];
				}
				if (spriteIndex == 10)
				{
					spriteIndex = 7;
					renderer.sprite = sprites [spriteIndex];
				} else
				{
					spriteIndex++;
					renderer.sprite = sprites [spriteIndex];
				}
				timer = 0f;
			}
		}
	}
}
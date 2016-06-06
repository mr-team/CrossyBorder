using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class politician : MonoBehaviour
{
	
	public Dictionary<string,bool> abiliiesSanders = new Dictionary<string,bool> ();

	public delegate void ClickCB (GameObject thisObj);

	//public Texture[] abilityCardTextures = new Texture[2];
	public ClickCB clickCB;
	public Canvas UI;
	public RawImage abilityCard;
	public Sprite[] quotes;

	protected GameMaster GM;
	protected GameObject speechBubble;

	protected bool cardChosen;
	//protected int cardNum;

	protected Texture texture;
	protected float timer = 0f;
	protected float timeDelay = 5f;

	protected virtual void Start ()
	{
		GM = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();

		speechBubble = transform.FindChild ("Speech Bubble").gameObject;
		speechBubble.SetActive (false);
		speechBubble.GetComponent<SpriteRenderer> ().sprite = quotes [Random.Range (0, quotes.Length)];
		abilityCard.enabled = false;
	}

	protected virtual void Update ()
	{
		if (GM.cards.Count != 0)
			texture = GM.pickRandomCard (gameObject.name + "@" + GM.cards.Count + "@" + GM.seed);
		if (GM.roundWon && !cardChosen)
		{
			//cardNum = Random.Range (0, 2);
			cardChosen = true;
			speechBubble.GetComponent<SpriteRenderer> ().sprite = quotes [Random.Range (0, quotes.Length)];
		}

	}

	protected virtual void OnMouseDown ()
	{
		//ActivateAbility (cardNum);
		//clickCB (this.gameObject);
		ActivateAbility (texture);
		GM.showCards = false;
		cardChosen = false;
		speechBubble.SetActive (false);
	}

	void OnMouseOver ()
	{
		Debug.Log ("im over card");
		abilityCard.enabled = GM.showCards;
		//abilityCard.texture = abilityCardTextures [cardNum];
		abilityCard.texture = texture;
		int offset = Input.mousePosition.x < (Screen.width / 2f) ? 100 : -100;
		abilityCard.transform.position = new Vector2 (Input.mousePosition.x + offset, Input.mousePosition.y + 110);
		speechBubble.SetActive (true);
	}

	void OnMouseExit ()
	{
		abilityCard.enabled = false;
		speechBubble.SetActive (false);
	}

	protected virtual void ActivateAbility (Texture t)
	{
		if (!GM.showCards)
			return;
		switch (t.name)
		{
			case "BearTrapCard":
				GM.bearTraps = true;
				break;
			case "FBICard":
				GM.fbiTroops = true;
				break;
			case "MineCard":
				GM.mines = true;
				break;
			case "SkillCard":
				GM.CarpetBombing = true;
				break;
		}
		GM.cards.Remove (t);
	}
	/*protected virtual void ActivateAbility (int cardNum)
	{
		if (cardNum == 0)
		{
			//Debug.Log ("card " + cardNum + " on " + gameObject.name + " was activated");
		}
		if (cardNum == 1)
		{
			//Debug.Log ("card " + cardNum + " on " + gameObject.name + " was activated");
		}
	}*/
}
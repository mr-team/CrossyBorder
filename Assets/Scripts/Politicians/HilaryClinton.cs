using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HilaryClinton : politician
{
	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();
	}

	protected override void OnMouseDown ()
	{
		base.OnMouseDown ();
		clickCB (this.gameObject);
	}

	protected override void ActivateAbility (int cardNum)
	{
		if (cardNum == 0)
		{
			GM.CarpetBombing = true;

		}
		if (cardNum == 1)
		{
			GM.fbiTroops = true;

		}
	}
}

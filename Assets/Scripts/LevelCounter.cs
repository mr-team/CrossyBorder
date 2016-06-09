using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelCounter : MonoBehaviour {
    public string prefix = "Level ";

    private GameMaster GM;
    private Text tekst;

	void Start() {
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        tekst = GetComponent<Text>();
	}
	
	void FixedUpdate() {
        tekst.text = prefix + GM.currentLevel;
	}
}

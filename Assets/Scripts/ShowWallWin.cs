using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowWallWin : MonoBehaviour {
    public GameObject win;
    public GameObject politicians;

    private GameMaster GM;

    void Start() {
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
	
	void Update() {
        if(GM.showCards) {
            win.SetActive(GM.cards.Count == 0);
            politicians.SetActive(GM.cards.Count != 0);
        }
    }
}

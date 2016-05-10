using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fadeOut : MonoBehaviour {
    public float fadeAfter = 3f;

    public float timer = 0f;
    private RawImage RI;
    private GameMaster GM;

	void Start() {
        RI = GetComponent<RawImage>();
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
	}
	
	void Update() {
        if(GM.gameState != GameMaster.States.gameActive)
            return;
	    if(timer < fadeAfter) {
            timer += Time.deltaTime;
        } else {
            if(RI.color.a > 0) {
                RI.color = new Color(255f, 255f, 255f, -1f);
            } else {
                Destroy(gameObject);
            }
        }
	}
}

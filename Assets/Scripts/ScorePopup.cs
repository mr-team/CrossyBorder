using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScorePopup : MonoBehaviour {
    public float killTime = 3f;

    private Text tekst;
    private RectTransform rectTransform;

	void Start() {
        tekst = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
        Destroy(this.gameObject, killTime);
	}
	
	void FixedUpdate() {
        tekst.color = Color.Lerp(tekst.color, new Color(tekst.color.r, tekst.color.g, tekst.color.b, 0f), (killTime / 2f) * Time.deltaTime);
        rectTransform.Translate(Vector3.up);
    }
}

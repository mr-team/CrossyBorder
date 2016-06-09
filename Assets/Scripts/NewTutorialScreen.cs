using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NewTutorialScreen : MonoBehaviour {
    public Texture[] tutImages;
    public int tutorialIndex = 0;
    public RawImage RI;

    private GameMaster GM;

    void Awake() {
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
	
	void Update() {
        RI.texture = tutImages[tutorialIndex];
    }

    public void nextImage() {
        if(tutorialIndex < tutImages.Length - 1) {
            tutorialIndex++;
        } else {
            toggleTutorial();
        }
    }

    public void lastImage() {
        if(tutorialIndex > 0) {
            tutorialIndex--;
        } else {
            toggleTutorial();
        }
    }

    public void toggleTutorial() {
        GM.inTutorial = !GM.inTutorial;
        if(GM.inTutorial)
            tutorialIndex = 0;
    }

}

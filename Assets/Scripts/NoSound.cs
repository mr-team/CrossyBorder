using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoSound : MonoBehaviour {
    public SoundType soundType;
    public enum SoundType {
        Sound, Music
    }
    
    private Image i;

    void Start() {
        i = GetComponent<Image>();
    }
	
	void Update() {
        i.enabled = PlayerPrefs.GetFloat(soundType == SoundType.Sound ? "soundVolume" : "musicVolume") != 0f;
    }
}

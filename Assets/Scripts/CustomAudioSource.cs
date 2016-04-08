using UnityEngine;
using System.Collections;

public class CustomAudioSource : MonoBehaviour {
    public enum AudioTypes { Music, Sound }
    public AudioTypes audioType;
    public AudioClip clip;
    public bool loop = false;

    private AudioSource source;
    private string[] typeText = { "musicVolume", "musicVolume" };
    private string currentType;
     
	void Start() {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = loop;
        currentType = audioType == AudioTypes.Music ? typeText[0] : typeText[1];
        source.volume = PlayerPrefs.GetFloat(currentType);
    }
	
	void Update() {
        currentType = audioType == AudioTypes.Music ? typeText[0] : typeText[1];
        setLoop(loop);
        source.volume = PlayerPrefs.GetFloat(currentType);
    }

    public void setLoop(bool _loop) {
        source.loop = _loop;
    }

    public void PlayOnce() {
        source.PlayOneShot(clip);
    }

    public bool isPlaying() {
        return source.isPlaying;
    }

    public void Play() {
        source.Play();
    }

    public AudioClip setClip(AudioClip clip) {
        source.clip = clip;
        return clip;
    }
}

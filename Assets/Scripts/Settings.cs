using UnityEngine;
using System.Collections;

public class Settings {

    private float music;
    private float sound;

    public float musicVolume {
        get {
            return music;
        }
        set {
            PlayerPrefs.SetFloat("musicVolume", value);
            music = value;
        }
    }

    public float soundVolume {
        get {
            return sound;
        }
        set {
            PlayerPrefs.SetFloat("soundVolume", value);
            sound = value;
        }
    }

    public void updateFromPrefs() {
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        soundVolume = PlayerPrefs.GetFloat("soundVolume");
    }

    private float lastMV = -1f;
    public bool muteMusic() {
        if(lastMV == -1f) {
            lastMV = musicVolume;
            musicVolume = 0f;
            return true;
        }
        musicVolume = lastMV;
        lastMV = -1f;
        return false;
    }

    private float lastSV = -1f;
    public bool muteSounds() {
        if(lastSV == -1f) {
            lastSV = soundVolume;
            soundVolume = 0f;
            return true;
        }
        soundVolume = lastSV;
        lastSV = -1f;
        return false;
    }

}

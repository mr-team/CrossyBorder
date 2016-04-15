using UnityEngine;
using System.Collections;

public static class GameSettings {

    private static float music;
    private static float sound;

    public static float musicVolume {
        get {
            return music;
        }
        set {
            PlayerPrefs.SetFloat("musicVolume", value);
            music = value;
        }
    }

    public static float soundVolume {
        get {
            return sound;
        }
        set {
            PlayerPrefs.SetFloat("soundVolume", value);
            sound = value;
        }
    }

    public static void updateFromPrefs() {
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        soundVolume = PlayerPrefs.GetFloat("soundVolume");
    }

    private static float lastMV = -1f;
    public static bool muteMusic() {
        if(lastMV == -1f) {
            lastMV = musicVolume;
            musicVolume = 0f;
            return true;
        }
        musicVolume = lastMV;
        lastMV = -1f;
        return false;
    }

    private static float lastSV = -1f;
    public static bool muteSounds() {
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

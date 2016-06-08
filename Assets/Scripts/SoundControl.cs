using UnityEngine;
using System.Collections;

public class SoundControl : MonoBehaviour {
    [Range(0.0001f, 1f)]
    public float volume = .06f;

    public void MuteSound() {
        GameSettings.soundVolume = GameSettings.soundVolume != 0f ? 0f : volume;
    }

    public void MuteMusic() {
        GameSettings.musicVolume = GameSettings.musicVolume != 0f ? 0f : volume;
    }

}

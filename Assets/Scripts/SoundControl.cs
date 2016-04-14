using UnityEngine;
using System.Collections;

public class SoundControl : MonoBehaviour {

    public void MuteSound() {
        GameSettings.soundVolume = GameSettings.soundVolume == 1f ? 0f : 1f;
    }

    public void MuteMusic() {
        GameSettings.musicVolume = GameSettings.musicVolume == 1f ? 0f : 1f;
    }

}

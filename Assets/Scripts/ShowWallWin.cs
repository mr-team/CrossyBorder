using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowWallWin : MonoBehaviour {
    public GameObject win;
    public GameObject politicians;
    public GameObject MainCamera;
    public RawImage tooltip;

    private GameMaster GM;
    private PlayerClimbLadder pcl;
    private CustomAudioSource cas;

    void Start() {
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        pcl = GetComponent<PlayerClimbLadder>();
        cas = win.GetComponent<CustomAudioSource>();
    }

    void Update() {
        if(GM.cards.Count == 0 && pcl.cuts == PlayerClimbLadder.Cuts.secondCut) {
            MainCamera.GetComponent<CustomAudioSource>().Stop();
            cas.Play();
        }
        if(GM.showCards) {
            win.SetActive(GM.cards.Count == 0);
            if(GM.cards.Count == 0)
                tooltip.enabled = false;
            politicians.SetActive(GM.cards.Count != 0);
        }
    }
}
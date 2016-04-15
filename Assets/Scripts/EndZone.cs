using UnityEngine;
using System.Collections;

public class EndZone : MonoBehaviour
{

    GameMaster GM;

    void Start()
    {
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == ("Player"))
        {
            GM.roundWon = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == ("Player"))
        {
            Debug.Log("player not in endzone");
        }
    }
}

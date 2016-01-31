using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class showScore : MonoBehaviour {

    public GameStatusController game;
    public GameObject me;
	
	// Update is called once per frame
	void Update () {
        me.GetComponent<Text>().text = "Score = " + game.score;
	}
}

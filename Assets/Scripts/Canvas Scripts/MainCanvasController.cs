using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainCanvasController: MonoBehaviour {

    public CanvasController canvs;

    public keyBindings keys;
    public Sprite[] reticles;
    public Image curReticle;
    public Image blur;

    public GameStatusController game;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keys.switchCam) && !game.paused)
        {

            curReticle.sprite = reticles[canvs.currentCam];
        }
        blur.enabled = game.paused || !game.gameOngoing;
	}
}

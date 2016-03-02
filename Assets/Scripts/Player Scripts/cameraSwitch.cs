using UnityEngine;
using System.Collections;

public class cameraSwitch : MonoBehaviour {

    public Camera cam1;
    public Camera cam2;

    public GameStatusController game;

    //TODO: make it an array of cameras

    public keyBindings keys;
    
    // Use this for initialization
	void Start () {
        cam1.enabled = true;
        cam2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(keys.switchCam) && !game.paused)
        {
            if (cam1.enabled)
            {
                cam1.enabled = false;
                cam2.enabled = true;
            }
            else
            {
                cam1.enabled = true;
                cam2.enabled = false;
            }
        }
	}
}

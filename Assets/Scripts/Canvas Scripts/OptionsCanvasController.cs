using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsCanvasController: MonoBehaviour {

    public CanvasController canvs;

    public Camera[] cams;
    public Canvas me;
    public keyBindings keys;
    public Sprite[] reticles;
    public Image curReticle;
    public Image blur;
    public Text pausedText;

    public Button[] menuButtons;
    public Button[] pausedButtons;
    public Text startText;
    public Image startBG;

    public int currentCam;

    public GameStatusController game;
    public bool last = false;
    public bool lastPaused = true;
    
    // Use this for initialization
	void Start () {
        me.worldCamera = cams[currentCam];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keys.switchCam) && !game.paused)
        {
            if (++currentCam==cams.Length)
            {
                currentCam = 0;
            }
            me.renderMode = RenderMode.ScreenSpaceCamera;  //This gets rid of the "render cam" field, making it really wierd when
            me.worldCamera = cams[currentCam];              // you pause the game in the editor. Still works in-game tho
            
            curReticle.sprite = reticles[currentCam];
        }
        
        if(last!=game.gameOngoing)
        {
            switchScreen();                 //So I don't have to call a switch case and loop every frame
        }
        if(lastPaused!=game.paused)
        {
            switchPaused();
        }
        
        last = game.gameOngoing;
        lastPaused = game.paused;
	}

    void switchScreen()
    {
        foreach(Button b in menuButtons)
        {
            b.enabled = !game.gameOngoing;
            b.image.enabled = !game.gameOngoing;
        }
        
        startText.enabled = !game.gameOngoing;

        switch(game.causeOfDeath)
        {
            case 0: startText.text = "Asteroids 3D"; break;
            case 1: startText.text = "Killed by: Asteroid"; break;
            case 2: startText.text = "Killed by: Enemy"; break;
            case -1: startText.text = "You won!"; break;
        }
        blur.enabled = game.paused || !game.gameOngoing;
    }
    void switchPaused()
    {
        foreach (Button b in pausedButtons)
        {
            b.enabled = game.paused;
            b.image.enabled = game.paused;
        }
        blur.enabled = game.paused || !game.gameOngoing;
        pausedText.enabled = game.paused;
    }
}

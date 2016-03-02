using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasController: MonoBehaviour {

    public Canvas me;

    public Camera[] cams;
    public int currentCam;

    public keyBindings keys;

    public Sprite[] reticles;
    public Image curReticle;
    public Image blur;

    public Button[] myMenuButtons;
    public Button[] myPausedButtons;
    public Button[] myOptionButtons;
    public Text[] myMenuText;
    public Text[] myPausedText;
    public Text[] myOptionText;
    public Toggle[] myOptionToggle;

    public GameStatusController game;
    public bool lastPaused;
    public bool lastMenu;
    public bool lastOptions;
    public bool options;

    
    // Use this for initialization
	void Start () {
        me.worldCamera = cams[currentCam];
        switchMenu();
        switchPaused();
        switchOptions();
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
        if ((Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftAlt)) && Input.GetKeyDown(keys.restart))
        {
            game.causeOfDeath = 0;
            game.stopGame();
        }
        if (lastMenu == !(!game.gameOngoing && !options))
        {
            switchMenu();
        }
        if(lastPaused == !(game.paused && !options))
        {
            switchPaused();
        }
        if(lastOptions != options)
        {
            switchOptions();
        }
        blur.enabled = game.paused || !game.gameOngoing;
    }

    void switchPaused()
    {
        
        lastPaused = game.paused && !options;
        foreach (Button b in myPausedButtons)
        {
            b.enabled = lastPaused;
            b.gameObject.GetComponent<Image>().enabled = lastPaused;
            b.gameObject.transform.rotation = me.transform.rotation;
        }
        foreach(Text t in myPausedText)
        {
            t.enabled = lastPaused;
            t.gameObject.transform.rotation = me.transform.rotation;
        }
    }

    void switchMenu()
    {
        
        lastMenu = !game.gameOngoing && !options;
        foreach (Button b in myMenuButtons)
        {
            b.enabled = lastMenu;
            b.gameObject.GetComponent<Image>().enabled = lastMenu;
        }
        foreach (Text t in myMenuText)
        {
            t.enabled = lastMenu;
        }
        System.String text;
        switch(game.causeOfDeath)
        {
            case -1: text = "You won!"; break;
            case 0: text = "Asteroids 3D"; break;
            case 1: text = "Killed by obstacle"; break;
            case 2: text = "Killed by enemy"; break;
            case 3: text = "Killed by player"; break;
            case 4: text = "Hacker"; break;
            default: text = ""; break;
        }
        myMenuText[0].text = text;
    }

    void switchOptions()
    {
        lastOptions = options;
        foreach (Button b in myOptionButtons)
        {
            b.enabled = lastOptions;
            b.gameObject.GetComponent<Image>().enabled = lastOptions;
        }
        foreach (Text t in myOptionText)
        {
            t.enabled = lastOptions;
        }
        foreach(Toggle o in myOptionToggle)
        {
            o.enabled = lastOptions;
            foreach(Image i in o.GetComponentsInChildren<Image>())
            {
                i.enabled = lastOptions;
            }
            o.GetComponentInChildren<Text>().enabled = lastOptions;
        }
    }
    public void setOptionsTrue()
    {
        options = true;
        readOptions();
    }
    public void setOptionsFalse()
    {
        options = false;
        game.causeOfDeath = 0;
    }
    public void writeOptions()
    {
        keys.toggleAim = myOptionToggle[0].isOn;
    }
    public void readOptions()
    {
        myOptionToggle[0].isOn = keys.toggleAim;
    }
}

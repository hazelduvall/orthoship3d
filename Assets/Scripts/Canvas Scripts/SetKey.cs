using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetKey : MonoBehaviour {

    private bool waiting;

    public keyBindings keys;
    public string toSet;

    public Text me;
    public GameObject eventSystem;

    void Start()
    {
        waiting = false;

    }

    public void waitForClick()
    {
        me.text = "Press a key";
        waiting = true;
    }
    void Update()
    {
        if(waiting&&Input.anyKeyDown)
        {
            KeyCode keyPressed = KeyCode.Z;
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if(Input.GetKeyDown(kcode))
                {
                    keyPressed = kcode;
                }
            }
            switch(toSet)
            {
                case "rollLeft": keys.rollLeft = keyPressed;  break;
                case "rollRight": keys.rollRight = keyPressed; break;
                case "shoot": keys.shoot = keyPressed; break;
                case "aim": keys.aim = keyPressed; break;
                case "switchCam": keys.switchCam = keyPressed; break;
                case "switchWeapon": keys.switchWeapon = keyPressed; break;
                case "pause": keys.pause=keyPressed; break;
            }
            waiting = false;
            setText();
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
    public void setText()
    {
        string s = "";
        switch (toSet)
        {
            case "rollLeft": s = keys.rollLeft.ToString(); break;
            case "rollRight": s = keys.rollRight.ToString(); break;
            case "shoot": s = keys.shoot.ToString(); break;
            case "aim": s = keys.aim.ToString(); break;
            case "switchCam": s = keys.switchCam.ToString(); break;
            case "switchWeapon": s = keys.switchWeapon.ToString(); break;
            case "pause": s = keys.pause.ToString(); break;
        }
        me.text = s;
    }
}

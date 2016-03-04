using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

    public keyBindings keys;

    /*public void writeOptions(Toggle aim)
    {
        keys.toggleAim = aim.isOn;
        //setMovementKeys(myOptionDropdown[0].value);
        //setLookKeys(myOptionDropdown[1].value);
    }*/
    public void writeAim(bool isOn)
    {
        keys.toggleAim = isOn;
    }
    public void setMovementKeys(int which)
    {
        int i = -1;
        switch (keys.up)
        {
            case KeyCode.W: i = 0; break;
            case KeyCode.I: i = 1; break;
            case KeyCode.UpArrow: i = 2; break;
        }
        if(i!= which)
        {
            switch (which)
            {
                case 0:
                    keys.accelForward = KeyCode.W;
                    keys.accelBackward = KeyCode.S;
                    keys.accelLeft = KeyCode.A;
                    keys.accelRight = KeyCode.D;
                    break;
                case 1:
                    keys.accelForward = KeyCode.I;
                    keys.accelBackward = KeyCode.K;
                    keys.accelLeft = KeyCode.J;
                    keys.accelRight = KeyCode.L;
                    break;
                case 2:
                    keys.accelForward = KeyCode.UpArrow;
                    keys.accelBackward = KeyCode.DownArrow;
                    keys.accelLeft = KeyCode.LeftArrow;
                    keys.accelRight = KeyCode.RightArrow;
                    break;
            }
        }
        
    }
    public void readMovementKeys(Dropdown d)
    {
        if (keys.accelForward == KeyCode.W) { d.value = 0; }
        if (keys.accelForward == KeyCode.I) { d.value = 1; }
        if (keys.accelForward == KeyCode.UpArrow) { d.value = 2; }
    }
    public void setLookKeys(int which)
    {
        int i = -1;
        switch (keys.accelForward)
        {
            case KeyCode.W: i = 0; break;
            case KeyCode.I: i = 1; break;
            case KeyCode.UpArrow: i = 2; break;
        }
        if (i != which)
        {
            switch (which)
            {
                case 0:
                    keys.up = KeyCode.W;
                    keys.down = KeyCode.S;
                    keys.left = KeyCode.A;
                    keys.right = KeyCode.D;
                    break;
                case 1:
                    keys.up = KeyCode.I;
                    keys.down = KeyCode.K;
                    keys.left = KeyCode.J;
                    keys.right = KeyCode.L;
                    break;
                case 2:
                    keys.up = KeyCode.UpArrow;
                    keys.down = KeyCode.DownArrow;
                    keys.left = KeyCode.LeftArrow;
                    keys.right = KeyCode.RightArrow;
                    break;
            }
        }
    }
    public void readLookKeys(Dropdown d)
    {
        if (keys.up == KeyCode.W) { d.value = 0; }
        if (keys.up == KeyCode.I) { d.value = 1; }
        if (keys.up == KeyCode.UpArrow) { d.value = 2; }
    }
}

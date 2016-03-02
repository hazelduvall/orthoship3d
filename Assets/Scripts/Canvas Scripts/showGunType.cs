using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class showGunType : MonoBehaviour {

    public Image me;
    public keyShoot shooter;
    public Sprite[] gunTypes;

    // Update is called once per frame
    void Update () {
        me.sprite = gunTypes[shooter.gunType];
	}
}

using UnityEngine;
using System.Collections;

public class keyRotation : MonoBehaviour {

    private float xRot;
    private float yRot;
    private float zRot;
    private float zSpeed;
    private float xSpeed;

    public Transform me;

    public keyBindings keys;
    public GameStatusController game;

    // Use this for initialization
	void Start () {
        xRot = 0;
        yRot = 0;
        zRot = 0;
        zSpeed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(!game.paused&&game.gameOngoing)
        {
            if (Input.GetKey(keys.up) && xRot > -15)
            {
                xRot += -2f;
            }
            if (Input.GetKey(keys.down) && xRot < 15)
            {
                xRot += 2f;
            }
            if (Input.GetKey(keys.left) && yRot > -15)
            {
                yRot += -2f;
            }
            if (Input.GetKey(keys.right) && yRot < 15)
            {
                yRot += 2f;
            }
            if (Input.GetKey(keys.rollRight) && zRot > -15)
            {
                zRot += -2f;
            }
            if (Input.GetKey(keys.rollLeft) && zRot < 15)
            {
                zRot += 2f;
            }
            if (Input.GetKey(keys.accelForward))
            {
                zSpeed += 0.1f;
            }
            if(Input.GetKey(keys.accelBackward))
            {
                zSpeed -= 0.1f;
            }
            if (Input.GetKey(keys.accelRight))
            {
                xSpeed += 0.1f;
            }
            if (Input.GetKey(keys.accelLeft))
            {
                xSpeed -= 0.1f;
            }
        }

        me.localRotation = Quaternion.Euler(xRot, yRot, zRot);
        me.localPosition = new Vector3(xSpeed, 0, zSpeed);

        xRot *= 0.8f;
        yRot *= 0.8f;
        zRot *= 0.8f;
        zSpeed *= 0.8f;
        xSpeed *= 0.8f;

    }
}

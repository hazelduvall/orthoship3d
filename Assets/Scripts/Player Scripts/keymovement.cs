using UnityEngine;
using System.Collections;

public class keymovement : MonoBehaviour {

    public Vector3 lookDir;
    public Vector3 speedVector;
    public Transform trans;
    public float zSpeed;
    public float xSpeed;

    public GameObject blast;

    public keyBindings keys;
    public GameStatusController game;

    private Vector3 deg;
    
    // Use this for initialization
	void Start () {
        zSpeed = 0f;
        xSpeed = 0f;
        speedVector = Vector3.zero;
        lookDir = Vector3.zero;
        deg = new Vector3(0, 90, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if(!game.paused&&game.gameOngoing)
        {
            lookDir.x *= 0.8f;
            lookDir.y *= 0.8f;
            lookDir.z *= 0.8f;
            float movement;
            if (game.aiming)
            {
                movement = 0.1f;
            }
            else
            {
                movement = 0.4f;
            }
            if (Input.GetKey(keys.up))
            {
                lookDir.x -= movement;
            }
            else if (Input.GetKey(keys.down))
            {
                lookDir.x += movement;
            }
            if (Input.GetKey(keys.left))
            {
                lookDir.y -= movement;
            }
            else if (Input.GetKey(keys.right))
            {
                lookDir.y += movement;
            }
            if (Input.GetKey(keys.rollRight))
            {
                lookDir.z -= movement;
            }
            else if (Input.GetKey(keys.rollLeft))
            {
                lookDir.z += movement;
            }
            if (Input.GetKey(keys.accelForward))
            {
                zSpeed += 0.1f;
            }
            if (Input.GetKey(keys.accelBackward))
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
            xSpeed *= 0.8f;
            zSpeed *= 0.8f;
        }
        else
        {
            lookDir = Vector3.zero;
            zSpeed = 0;
            xSpeed = 0;
        }
    }

    void FixedUpdate()
    {
        trans.Rotate(lookDir);
        float elevation = Mathf.Deg2Rad * trans.rotation.eulerAngles.x;
        float heading = Mathf.Deg2Rad * trans.rotation.eulerAngles.y;
        speedVector = new Vector3(Mathf.Cos(elevation) * Mathf.Sin(heading), -1 * Mathf.Sin(elevation), Mathf.Cos(elevation) * Mathf.Cos(heading));
        trans.position += speedVector * zSpeed;
        trans.Rotate(deg);                      //rotating twice in 1 physics step will not cause any collisions, so this method is okay
        elevation = Mathf.Deg2Rad * trans.rotation.eulerAngles.x;
        heading = Mathf.Deg2Rad * trans.rotation.eulerAngles.y;
        speedVector = new Vector3(Mathf.Cos(elevation) * Mathf.Sin(heading), -1 * Mathf.Sin(elevation), Mathf.Cos(elevation) * Mathf.Cos(heading));
        trans.position += speedVector * xSpeed;
        trans.Rotate(-deg);
    }

    public void gotoPos()
    {
        trans.position = Vector3.zero;
    }
}

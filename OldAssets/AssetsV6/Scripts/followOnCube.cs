using UnityEngine;
using System.Collections;

public class followOnCube : MonoBehaviour {

    public GameStatusController game;
    public Transform follow;
    public Transform me;

    public float threshold;

    private float x;
    private float y;
    private float z;
	
	// Update is called once per frame
	void Update () {
        if(follow != null)
        {
            Vector3 pos = follow.position;
            if (Mathf.Abs(pos.x) >= Mathf.Abs(pos.z) && Mathf.Abs(pos.x) >= Mathf.Abs(pos.y))
            {
                x = game.cubeSize * Mathf.Sign(pos.x);
                y = pos.y;
                z = pos.z;
                me.rotation = Quaternion.Euler(0, -90 * Mathf.Sign(pos.x), 0);
            }
            else if (Mathf.Abs(pos.y) >= Mathf.Abs(pos.z))
            {
                x = pos.x;
                y = game.cubeSize * Mathf.Sign(pos.y);
                z = pos.z;
                me.rotation = Quaternion.Euler(90, 0, 0);
            }
            else
            {
                x = pos.x;
                y = pos.y;
                z = game.cubeSize * Mathf.Sign(pos.z);
                if (Mathf.Sign(z) < 0) { me.rotation = Quaternion.Euler(0, 0, 0); }
                else { me.rotation = Quaternion.Euler(0, 180, 0); }
                
            }
            gameObject.GetComponent<MeshRenderer>().enabled = Vector3.Distance(me.position, follow.position) <= threshold;
            me.position = new Vector3(x, y, z);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class cubeWrap : MonoBehaviour {

    public bool wrap;
    public GameStatusController game;
    public Transform me;
	
	void Update () {
        Vector3 pos = me.position;
        if(wrap)
        {
            float x = pos.x;
            float y = pos.y;
            float z = pos.z;
            if (Mathf.Abs(x)>game.cubeSize)
            {
                x = -x + Mathf.Sign(x);
            }
            if (Mathf.Abs(y) > game.cubeSize)
            {
                y = -y + Mathf.Sign(y);
            }
            if (Mathf.Abs(z) > game.cubeSize)
            {
                z = -z + Mathf.Sign(z);
            }
            me.position = new Vector3(x, y, z);
        }
        else
        {
            if(Mathf.Abs(pos.x)>game.cubeSize || Mathf.Abs(pos.y) > game.cubeSize || Mathf.Abs(pos.z) > game.cubeSize)
            {
                Destroy(gameObject);
            }
        }
    }
}

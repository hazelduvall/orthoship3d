using UnityEngine;
using System.Collections;

public class selfDestruct : MonoBehaviour {

    public float secondsLeft;
	
	// This is put on particle effects that don't loop
	void Update () {
        secondsLeft -= Time.deltaTime;
        if(secondsLeft<0)
        {
            Destroy(gameObject);
        }
	}
}

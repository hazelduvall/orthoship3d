using UnityEngine;
using System.Collections;

public class followObject : MonoBehaviour {

    public GameObject toFollow;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public Transform trans;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 euler = trans.rotation.eulerAngles;
        float elevation = Mathf.Deg2Rad * euler.x;
        float heading = Mathf.Deg2Rad * euler.y;
        trans.position = new Vector3(
            toFollow.transform.position.x + offsetX * Mathf.Cos(elevation) * Mathf.Sin(heading) * -1,
            toFollow.transform.position.y + offsetY * Mathf.Sin(elevation),
            toFollow.transform.position.z + offsetZ * Mathf.Cos(elevation) * Mathf.Cos(heading) * -1
            );
	}
}

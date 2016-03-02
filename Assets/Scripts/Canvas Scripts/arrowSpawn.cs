using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class arrowSpawn : MonoBehaviour {

    public GameObject[] arrow;
    public GameObject canvas;
    
    public void addArrow(GameObject toFollow, int type)
    {
        GameObject ar = (GameObject)Instantiate(arrow[type]);
        ar.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>()); //unity complains about the regular .parent
        ar.GetComponent<pointat>().other = toFollow.transform;
        ar.GetComponent<pointat>().clone = true;                //so it will actually show itself
        ar.GetComponent<pointat>().firstTime = true;
        ar.GetComponent<pointat>().stayOnScreen = type == 1;
        ar.GetComponent<Image>().enabled = false||type==1;    //it will enable itself
        ar.transform.SetAsFirstSibling();           //so it will be behind the blur when paused
    }
}

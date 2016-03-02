using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pointat : MonoBehaviour {

    public Transform parent;
    public Transform other;
    public RectTransform canvasRect;

    public float screenXMax;
    public float screenYMax;
    public float screenXMin;
    public float screenYMin;

    private float dist;
    private float lastWidth;
    public bool firstTime;

    public bool clone=false;
    public bool stayOnScreen;

    public Sprite reg;
    public Sprite warn;

    void Start()
    {
        dist = Vector2.Distance(Vector2.zero, new Vector2(screenXMax, screenYMax));
        screenXMax = transform.parent.gameObject.GetComponent<RectTransform>().rect.width / 2f - 24; //these will be the boundaries that the arrows go along
        screenYMax = transform.parent.gameObject.GetComponent<RectTransform>().rect.height / 2f - 24;
        screenXMin = -screenXMax;
        screenYMin = -screenYMax;
        lastWidth = transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.parent.gameObject.GetComponent<RectTransform>().rect.width!=lastWidth)
        {
            dist = Vector2.Distance(Vector2.zero, new Vector2(screenXMax, screenYMax));
            screenXMax = transform.parent.gameObject.GetComponent<RectTransform>().rect.width / 2f - 24; //these will be the boundaries that the arrows go along
            screenYMax = transform.parent.gameObject.GetComponent<RectTransform>().rect.height / 2f - 24;
            screenXMin = -screenXMax;
            screenYMin = -screenYMax;
        }
        lastWidth = transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
        if (other == null)
        {
            Destroy(gameObject);
        }
        else if(transform != null)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);

            //Why this code even works, I don't know.
            //I did make it myself, tho

            //Getting position relative to player
            Vector3 otherPos = parent.InverseTransformPoint(other.position);
            

            //If we can see object, there is no need for an arrow
            if (clone && (!stayOnScreen || !other.gameObject.GetComponentInChildren<Renderer>().isVisible)) {
                gameObject.GetComponent<Image>().enabled = clone && !other.gameObject.GetComponent<Renderer>().isVisible && !firstTime;

                //Ignore z, get angle (although backwards and with negative x)
                float ang = Mathf.Atan2(otherPos.x * -1, otherPos.y);
                //rotate to that angle
                transform.localRotation = Quaternion.Euler(0, 0, ang * Mathf.Rad2Deg);

                //go to same position on screen border as angle
                float x = Mathf.Cos(ang + Mathf.PI / 2) * dist;
                float y = Mathf.Sin(ang + Mathf.PI / 2) * dist;


                if (x > screenXMax)
                { x = screenXMax; }
                else if (x < screenXMin)
                { x = screenXMin; }

                if (y > screenYMax)
                { y = screenYMax; }
                else if (y < screenYMin)
                { y = screenYMin; }

                transform.localPosition = new Vector3(x, y, 0);
            }
            else if(stayOnScreen)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 180);
                Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(other.position);
                Vector2 WorldObject_ScreenPosition = new Vector2(
                ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f))+30);
                transform.localPosition = WorldObject_ScreenPosition;
            }
            else
            {
                gameObject.GetComponent<Image>().enabled = false;
            }

            //Change to warning if object is too close (and you can't see it)
            gameObject.GetComponent<Image>().sprite = (Vector3.Distance(other.position, parent.position) > 20) ? reg : warn;

            if(firstTime)
            {
                firstTime = false;
            }
        }
        
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class showCharge : MonoBehaviour {

    public RawImage me;
    public keyShoot shooter;
    private float x;
    private float startX;
    private float startY;
    private float totalWidth;
    private float lastWidth;
	
	void Start()
    {
        totalWidth = me.rectTransform.rect.width;
        startX = me.rectTransform.localPosition.x - totalWidth / 2;
        startY = me.rectTransform.localPosition.y;
        lastWidth = totalWidth;
    }
    
    // Update is called once per frame
	void Update () {
        totalWidth = me.rectTransform.rect.width;
        if(lastWidth!=totalWidth)
        {
            startX = me.rectTransform.localPosition.x - totalWidth / 2;
        }
        lastWidth = totalWidth;
        me.rectTransform.localScale = new Vector3(shooter.chargeLevel, 0.1f, 1);
        me.rectTransform.localPosition = new Vector3(startX + shooter.chargeLevel * totalWidth / 2, startY, 0);
	}
}

using UnityEngine;
using System.Collections;

public class keyShoot : MonoBehaviour {

    public GameObject blast;
    public keyBindings keys;
    public Transform me;
    public Camera[] cams;

    public pauseRigidBody pause;
    public GameStatusController game;

    public int gunType;
    public float chargeLevel;
    private bool canShoot=true;
    /*
     * 0=regular, medium bullets
     * 1=rapid fire, smaller bullets
     * 2=charged shot, larger bullets
    */
	
	// Update is called once per frame
	void FixedUpdate () {
        if(!game.paused)
        {
            //Debug.Log(Input.GetKeyDown(keys.aim) && keys.toggleAim);
            if (Input.GetKeyDown(keys.aim) && keys.toggleAim)
            {
                game.aiming = !game.aiming;
            }
            if(Input.GetKey(keys.aim) && !keys.toggleAim)
            {
                game.aiming = true;
            }
            else if(!keys.toggleAim)
            {
                game.aiming = false;
            }

            if (Input.GetKeyDown(keys.switchWeapon))
            {
                gunType++;
                if(gunType>2)
                {
                    gunType = 0;
                }
            }

            if (gunType==0&&Input.GetKeyDown(keys.shoot))
            {
                chargeLevel = 1;
                spawnBullet(30, 1);
            }

            if(gunType==1&&Input.GetKey(keys.shoot))
            {
                chargeLevel += Time.deltaTime * 8;

                if(chargeLevel>=1)
                {
                    chargeLevel = 0;
                    spawnBullet(7.5f, 2);
                }
            }

            if(gunType==2&&canShoot&&Input.GetKey(keys.shoot))
            {
                chargeLevel += Time.deltaTime;
                if(chargeLevel>=1)
                {
                    chargeLevel = 0;
                    spawnBullet(90, 0.5f);
                    canShoot = false;
                }
            }

            if(!Input.GetKey(keys.shoot))
            {
                chargeLevel *= 0.75f;
                canShoot = true;
            }

            foreach(Camera cam in cams)         //zooming animation, using the same formula as enlarge and shrink
            {
                if (game.aiming && cam.fieldOfView >= 50)
                {
                    cam.fieldOfView -= (cam.fieldOfView - 50) / 5f;
                }
                else if (!game.aiming && cam.fieldOfView <= 80)
                {
                    cam.fieldOfView += (80 - cam.fieldOfView) / 5f;
                }
            }
            
        }
        else
        {
            foreach(Camera cam in cams)
            {
                cam.fieldOfView = 80f;
            }
            
        }
    }

    void spawnBullet(float size, float accuracy)
    {
        GameObject blastClone = (GameObject)Instantiate(blast);

        blastClone.transform.position = me.TransformPoint(me.localPosition);

        blastClone.transform.rotation = me.rotation;
        blastClone.transform.Rotate(new Vector3(90, 0, 0));

        blastClone.GetComponent<Rigidbody>().velocity = me.TransformVector(new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), 50));
        
        blastClone.AddComponent<cubeWrap>();
        blastClone.GetComponent<cubeWrap>().wrap = false;
        blastClone.GetComponent<cubeWrap>().game = game;
        blastClone.GetComponent<cubeWrap>().me = blastClone.transform;

        blastClone.GetComponentInChildren<Light>().color = Color.green;

        blastClone.transform.localScale = new Vector3(size, size, size);

        pause.add(blastClone.GetComponent<Rigidbody>());
    }
}

using UnityEngine;
using System.Collections;

public class AIbehaviourDEF : MonoBehaviour {

    public AIbehaviour beh;
    public GameObject bullet;

    public float dist;

    private float counter;

    private GameObject toAvoid;

    void FixedUpdate()
    {
        if (!beh.game.paused)
        {
            if ((beh.me.position - beh.player.transform.position).magnitude < dist)
            {
                beh.goToPoint(beh.player.transform.position);
            }
            else if (toAvoid != null)
            {
                beh.goToPoint(-(toAvoid.transform.position - beh.me.position) + beh.me.position);
            }
            else
            {
                Quaternion rotation = Quaternion.LookRotation(beh.player.transform.position - beh.me.position);
                beh.me.rotation = Quaternion.Slerp(beh.me.rotation, rotation, Time.fixedDeltaTime * beh.multiplier);

                if (counter > 2)
                {
                    shoot();
                    counter = 0;
                }
                else if (!beh.game.paused)
                {
                    counter += Time.fixedDeltaTime;
                }
            }
        }
    }

    void shoot()
    {
        GameObject blastClone = (GameObject)Instantiate(bullet);

        blastClone.transform.position = transform.position;

        blastClone.transform.rotation = beh.me.rotation;
        blastClone.transform.Rotate(new Vector3(90, 0, 0));

        blastClone.GetComponent<Rigidbody>().velocity = beh.me.TransformVector(new Vector3(Random.Range(-beh.maxError, beh.maxError), Random.Range(-beh.maxError, beh.maxError), 50));

        blastClone.AddComponent<cubeWrap>();
        blastClone.GetComponent<cubeWrap>().wrap = false;
        blastClone.GetComponent<cubeWrap>().game = beh.game;
        blastClone.GetComponent<cubeWrap>().me = blastClone.transform;

        blastClone.GetComponentInChildren<Light>().color = Color.red;

        blastClone.transform.localScale = new Vector3(30, 30, 30);

        beh.pause.add(blastClone.GetComponent<Rigidbody>());
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.StartsWith("AI"))
        { toAvoid = other.gameObject; }
    }
    void OnTriggerExit(Collider other)
    {
        if(toAvoid==null||toAvoid.GetComponent<Collider>()==other)
        {
            toAvoid = null;
        }
    }
}

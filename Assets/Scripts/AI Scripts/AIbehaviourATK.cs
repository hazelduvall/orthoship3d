using UnityEngine;
using System.Collections;

public class AIbehaviourATK : MonoBehaviour {

    public AIbehaviour beh;
    public GameObject bullet;

    private float counter;

    void FixedUpdate()
    {
        if (!beh.game.paused)
        {

            beh.goToPoint(beh.player.transform.position);

            RaycastHit hit;
            if (!beh.game.paused && Physics.Raycast(beh.me.position, beh.me.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    if (counter > 1)
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
}

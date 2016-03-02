using UnityEngine;
using System.Collections;

public class AIbehaviour : MonoBehaviour {

    public GameObject player;
    public GameStatusController game;
    public Transform me;
    public arrowSpawn ars;
    public pauseRigidBody pause;
    public GameObject particles;

    public float maxError;
    public float multiplier;
    private float curError;
    public float speed;
    public float rotspeed;

    private RaycastHit[] hits;

    void Start()
    {
        ars.addArrow(gameObject, 1);
        pause.add(gameObject.GetComponentInChildren<Rigidbody>());
    }

    public void goToPoint(Vector3 point)
    {
        if(!game.paused&&game.gameOngoing)
        {
            if ((me.position.x > game.cubeSize / 2 && point.x < -game.cubeSize / 2) || (point.x > game.cubeSize / 2 && me.position.x < -game.cubeSize / 2))
            {
                point.x += Mathf.Sign(me.position.x) * game.cubeSize * 2;
            }
            if ((me.position.y > game.cubeSize / 2 && point.y < -game.cubeSize / 2) || (point.y > game.cubeSize / 2 && me.position.y < -game.cubeSize / 2))
            {
                point.y += Mathf.Sign(me.position.y) * game.cubeSize * 2;
            }
            if ((me.position.z > game.cubeSize / 2 && point.z < -game.cubeSize / 2) || (point.z > game.cubeSize / 2 && me.position.z < -game.cubeSize / 2))
            {
                point.z += Mathf.Sign(me.position.z) * game.cubeSize * 2;
            }

            Quaternion rotation = Quaternion.LookRotation(point - me.position);
            hits = Physics.RaycastAll(me.position, me.forward, 20);
            foreach (RaycastHit h in hits)
            {
                if (h.collider.gameObject.CompareTag("Obstacle"))
                {
                    Vector3 target1 = (h.point - h.collider.gameObject.transform.position);
                    Vector3 target2 = target1.normalized * rotspeed;

                    rotation = Quaternion.LookRotation(target2 + h.point - me.position);
                }
            }



            float elevation = Mathf.Deg2Rad * me.rotation.eulerAngles.x;
            float heading = Mathf.Deg2Rad * me.rotation.eulerAngles.y;


            me.position += new Vector3(Mathf.Cos(elevation) * Mathf.Sin(heading), -1 * Mathf.Sin(elevation), Mathf.Cos(elevation) * Mathf.Cos(heading)) * speed;
            me.rotation = Quaternion.Slerp(me.rotation, rotation, Time.fixedDeltaTime * multiplier);
        }


        
    }

    /*void FixedUpdate()
    {
 
       goToPoint(player.transform.position);
        
    }*/

    void OnCollisionEnter(Collision col)
    {
        string tag = col.gameObject.tag;
        if(tag.Equals("Player"))
        {
            game.lives -= 1;
            game.causeOfDeath = 2;
        }
        if(tag.Equals("Blast")||tag.Equals("Asteroid")||tag.Equals("AI"))
        {
            Instantiate(particles, me.position, me.rotation);
            if (tag.EndsWith("Blast"))
            {
                Destroy(col.gameObject);
            }
            game.score += 5;
            Destroy(me.gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class AsteroidExplode : MonoBehaviour {

    public GameObject smallerAsteroid;
    public GameObject particles;
    public GameStatusController game;
    public arrowSpawn ars;
    public Transform me;
    public pauseRigidBody pause;
    public bool notSmall;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            game.lives -= 1;
            game.causeOfDeath = 1;
        }
        else if (col.gameObject.tag.Equals("Blast"))
        {
            if (notSmall)                               //smallest asteroids should not spawn others
            {
                GameObject smaller1 = (GameObject)Instantiate(smallerAsteroid, me.position + Vector3.up*10, Random.rotationUniform);        //TODO: make it so that they don't seem to "teleport"
                GameObject smaller2 = (GameObject)Instantiate(smallerAsteroid, me.position + Vector3.down*10, Random.rotationUniform);
                smaller1.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere;
                smaller2.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere;
                smaller1.GetComponent<Rigidbody>().velocity = Random.insideUnitSphere;
                smaller2.GetComponent<Rigidbody>().velocity = Random.insideUnitSphere;
                smaller1.GetComponent<AsteroidExplode>().game = game;               //transfering all game objects
                smaller2.GetComponent<AsteroidExplode>().game = game;
                pause.add(smaller1.GetComponent<Rigidbody>());
                pause.add(smaller2.GetComponent<Rigidbody>());
                smaller1.GetComponent<AsteroidExplode>().pause = pause;
                smaller2.GetComponent<AsteroidExplode>().pause = pause;
                smaller1.GetComponent<AsteroidExplode>().ars = ars;
                smaller2.GetComponent<AsteroidExplode>().ars = ars;
                smaller1.GetComponent<cubeWrap>().game = game;
                smaller2.GetComponent<cubeWrap>().game = game;
                ars.addArrow(smaller1,1);                                           //adding arrows to new asteroids
                ars.addArrow(smaller2,1);
            }
            game.score = game.score + 1;
            Instantiate(particles, me.position, me.rotation);
            Destroy(col.gameObject);                    //For some reason the blast collision enter gets called first, so I have to delete the object here
            Destroy(gameObject);
        }
    }
}

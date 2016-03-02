using UnityEngine;
using System.Collections;

public class AsteroidExplode : MonoBehaviour {

    public GameObject smallerAsteroid;
    public GameObject particles;
    public GameStatusController game;
    public arrowSpawn ars;
    public Transform me;
    public pauseRigidBody pause;
    public randomlyPlaceAsteroids spawn;
    public bool notSmall;

    void OnCollisionEnter(Collision col)
    {
        /*if (col.gameObject.tag.Equals("Player"))
        {
            game.lives -= 1;
            game.causeOfDeath = 1;
        }*/
        if (col.gameObject.tag.EndsWith("Blast"))
        {
            if (notSmall)                               //smallest asteroids should not spawn others
            {
                Vector3 random = Random.onUnitSphere * 3;
                GameObject smaller1 = (GameObject)Instantiate(smallerAsteroid, me.position + random, Random.rotationUniform);
                GameObject smaller2 = (GameObject)Instantiate(smallerAsteroid, me.position - random, Random.rotationUniform);
                smaller1.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere;
                smaller2.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere;
                smaller1.GetComponent<Rigidbody>().velocity = random * Random.Range(0.2f, 0.7f);
                smaller2.GetComponent<Rigidbody>().velocity = random * Random.Range(-0.7f, 0.2f);
                smaller1.GetComponent<AsteroidExplode>().game = game;               //transfering all game objects
                smaller2.GetComponent<AsteroidExplode>().game = game;
                smaller1.GetComponent<deadlyToPlayer>().game = game;
                smaller2.GetComponent<deadlyToPlayer>().game = game;
                pause.add(smaller1.GetComponent<Rigidbody>());
                pause.add(smaller2.GetComponent<Rigidbody>());
                smaller1.GetComponent<AsteroidExplode>().pause = pause;
                smaller2.GetComponent<AsteroidExplode>().pause = pause;
                smaller1.GetComponent<AsteroidExplode>().ars = ars;
                smaller2.GetComponent<AsteroidExplode>().ars = ars;
                smaller1.GetComponent<cubeWrap>().game = game;
                smaller2.GetComponent<cubeWrap>().game = game;
                smaller1.GetComponent<AsteroidExplode>().spawn = spawn;
                smaller2.GetComponent<AsteroidExplode>().spawn = spawn;
                ars.addArrow(smaller1, 0);                                           //adding arrows to new asteroids
                ars.addArrow(smaller2, 0);
                spawn.all.Add(smaller1);
                spawn.all.Add(smaller2);
            }
            game.score = game.score + 1;
            Instantiate(particles, me.position, me.rotation);
            Destroy(col.gameObject);                    //For some reason the blast collision enter gets called first, so I have to delete the object here
            Destroy(gameObject);
        }
        //Debug.Log(col.gameObject.tag);
    }
}

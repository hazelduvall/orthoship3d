using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class randomlyPlaceAsteroids : MonoBehaviour {

    public int[] smallAsteroidRange;
    public int[] mediumAsteroidRange;
    public int[] largeAsteroidRange;
    public int AIrate;
    public GameObject small;
    public GameObject medium;
    public GameObject large;
    public GameObject aiTemplate;
    private int smallAsteroidAmount;
    private int mediumAsteroidAmount;
    private int largeAsteroidAmount;

    public List<GameObject> all = new List<GameObject>();

    public GameStatusController game;
    public pauseRigidBody pause;
    public arrowSpawn ars;

    public float speed=2;
    private float counter=0;
	
	public void reload()
    {
         foreach( GameObject g in all)  //if there is a new game, destroy all current asteroids
        {
            Destroy(g);
        }

         //TODO: make sure an asteroid doesn't spawn on/very close to the player
        for (int i = 0; i < smallAsteroidAmount; i++)
        {
            spawn(small);
        }
        for (int i = 0; i < mediumAsteroidAmount; i++)
        {
            spawn(medium);
        }
        for (int i = 0; i < largeAsteroidAmount; i++)
        {
            spawn(large);
        }
        spawnAI(aiTemplate, Random.Range(0, 2));
        counter = 0;
    }

    public void randomNumber()
    {
        smallAsteroidAmount = Random.Range(smallAsteroidRange[0], smallAsteroidRange[1]);
        mediumAsteroidAmount = Random.Range(mediumAsteroidRange[0], mediumAsteroidRange[1]);
        largeAsteroidAmount = Random.Range(largeAsteroidRange[0], largeAsteroidRange[1]);
    }

    void spawn(GameObject asteroidModel)
    {
        Vector3 random;
        do
        {
            random = Random.insideUnitSphere * (game.cubeSize - 5);
        } while (Vector3.Distance(random, Vector3.zero) < 15);
        GameObject asteroid = (GameObject)Instantiate(asteroidModel, random, Random.rotationUniform);
        asteroid.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere; //get random position, rotation, and velocity
        asteroid.GetComponent<Rigidbody>().velocity = Random.insideUnitSphere * speed;
        asteroid.GetComponent<AsteroidExplode>().game = game;                   //copy game data
        asteroid.GetComponent<deadlyToPlayer>().game = game;
        pause.add(asteroid.GetComponent<Rigidbody>());
        asteroid.GetComponent<AsteroidExplode>().pause = pause;
        asteroid.GetComponent<AsteroidExplode>().ars = ars;
        asteroid.GetComponent<AsteroidExplode>().spawn = this;
        asteroid.GetComponent<cubeWrap>().game = game;
        all.Add(asteroid);
        ars.addArrow(asteroid, 0);                  //add arrow for asteroid
    }

    void spawnAI(GameObject ai, int type)
    {
        Vector3 random;
        do
        {
            random = Random.insideUnitSphere * (game.cubeSize - 5);
        } while (Vector3.Distance(random, game.player.transform.position) < 15);

        GameObject clone = (GameObject)Instantiate(ai, random, Random.rotationUniform);

        clone.GetComponent<cubeWrap>().game = game;

        clone.GetComponentInChildren<AIbehaviour>().player = game.player;
        clone.GetComponentInChildren<AIbehaviour>().game = game;
        clone.GetComponentInChildren<AIbehaviour>().ars = ars;
        clone.GetComponentInChildren<AIbehaviour>().pause = pause;

        GameObject shooter = clone.transform.FindChild("Shooter").gameObject;

        clone.GetComponentInChildren<deadlyToPlayer>().game = game;

        switch(type)
        {
            case 0:
                shooter.AddComponent<AIbehaviourATK>();
                shooter.GetComponent<AIbehaviourATK>().beh = clone.GetComponentInChildren<AIbehaviour>();
                shooter.GetComponent<AIbehaviourATK>().bullet = clone.transform.FindChild("blast").gameObject;
                break;
            case 1:
                shooter.AddComponent<AIbehaviourDEF>();
                shooter.GetComponent<AIbehaviourDEF>().beh = clone.GetComponentInChildren<AIbehaviour>();
                shooter.GetComponent<AIbehaviourDEF>().bullet = clone.transform.FindChild("blast").gameObject;
                shooter.GetComponent<AIbehaviourDEF>().dist = 15;
                break;
        }
        all.Add(clone);
    }

    void Update()
    {
        all.RemoveAll(pause.gNil);          //get rid of any asteroid that have been destroyed
        if(counter>AIrate)
        {
            spawnAI(aiTemplate, Random.Range(0, 2));
            counter = 0;
        }
        else if (!game.paused && game.gameOngoing)
        {
            counter += Time.deltaTime;
        }
    }
}

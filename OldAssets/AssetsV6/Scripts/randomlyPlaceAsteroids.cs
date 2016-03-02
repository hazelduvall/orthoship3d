using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class randomlyPlaceAsteroids : MonoBehaviour {

    public int smallAsteroids;
    public int mediumAsteroids;
    public int largeAsteroids;
    public GameObject small;
    public GameObject medium;
    public GameObject large;

    public List<GameObject> all = new List<GameObject>();

    public GameStatusController game;
    public pauseRigidBody pause;
    public arrowSpawn ars;
	
	public void reload()
    {
         foreach( GameObject g in all)  //if there is a new game, destroy all current asteroids
        {
            Destroy(g);
        }

         //TODO: make sure an asteroid doesn't spawn on/very close to the player
        for (int i = 0; i < smallAsteroids; i++)
        {
            spawn(small);
        }
        for (int i = 0; i < mediumAsteroids; i++)
        {
            spawn(medium);
        }
        for (int i = 0; i < largeAsteroids; i++)
        {
            spawn(large);
        }
    }

    public void randomNumber()
    {
        smallAsteroids = Random.Range(2, 6);
        mediumAsteroids = Random.Range(1, 4);
        largeAsteroids = Random.Range(1, 4);
    }

    void spawn(GameObject asteroidModel)
    {
        GameObject asteroid = (GameObject)Instantiate(asteroidModel, Random.insideUnitSphere * 70, Random.rotationUniform);
        asteroid.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere; //get random position, rotation, and velocity
        asteroid.GetComponent<Rigidbody>().velocity = Random.insideUnitSphere;
        asteroid.GetComponent<AsteroidExplode>().game = game;                   //copy game data
        pause.add(asteroid.GetComponent<Rigidbody>());
        asteroid.GetComponent<AsteroidExplode>().pause = pause;
        asteroid.GetComponent<AsteroidExplode>().ars = ars;
        asteroid.GetComponent<cubeWrap>().game = game;
        all.Add(asteroid);
        ars.addArrow(asteroid, 1);                  //add arrow for asteroid
    }

    void Update()
    {
        all.RemoveAll(pause.gNil);          //get rid of any asteroid that have been destroyed
    }
}

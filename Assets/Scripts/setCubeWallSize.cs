using UnityEngine;
using System.Collections;

public class setCubeWallSize : MonoBehaviour {

    public Transform[] walls;
    public GameStatusController game;
    
    // Use this for initialization
	void Start () {
        float ds = 2 * game.cubeSize;
        walls[0].localScale = new Vector3(0.01f, ds, ds);
        walls[0].position = new Vector3(game.cubeSize, 0, 0);
        walls[1].localScale = new Vector3(0.01f, -ds, -ds);
        walls[1].position = new Vector3(-game.cubeSize, 0, 0);
        walls[2].localScale = new Vector3(ds, 0.01f, ds);
        walls[2].position = new Vector3(0, game.cubeSize, 0);
        walls[3].localScale = new Vector3(-ds, 0.01f, -ds);
        walls[3].position = new Vector3(0, -game.cubeSize, 0);
        walls[4].localScale = new Vector3(ds, ds, 0.01f);
        walls[4].position = new Vector3(0, 0, game.cubeSize);
        walls[5].localScale = new Vector3(-ds, -ds, 0.01f);
        walls[5].position = new Vector3(0, 0, -game.cubeSize);
    }
	
}

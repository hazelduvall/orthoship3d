using UnityEngine;
using System.Collections;

public class GameStatusController : MonoBehaviour {


    //This class stores all the in-game data. Is refrenced by almost all other classes.
    public int lives;
    public bool paused;
    public bool aiming;
    public bool gameOngoing;
    public int causeOfDeath;
    /*
     * -1=player won
     * 0=player restarted
     * 1=player died from obstacle
     * 2=player killed by enemy
     * 3=player killed by other player
     * 4=unexpected death
     */
    public int score;
    public randomlyPlaceAsteroids asteroids;
    public pauseRigidBody pauseing;
    private bool last;

    public int cubeSize;
    public GameObject player;

    public void startGame()
    {
        lives = 1;
        paused = false;
        gameOngoing = true;
        score = 0;
        asteroids.randomNumber();
        asteroids.reload();
        last = false;
    }

    public void stopGame()
    {
        paused = false;
        gameOngoing = false;
    }

    void Update()
    {
        if(lives<=0)
        {
            stopGame();
        }
        if(pauseing.pauseing.Count < 3 && last)
        {
            causeOfDeath = -1;
            stopGame();
        }
        last = pauseing.pauseing.Count < 3;
        Cursor.visible = paused||!gameOngoing;
        
    }

}

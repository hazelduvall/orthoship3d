using UnityEngine;
using System.Collections;

public class deadlyToPlayer : MonoBehaviour {

    public int deathCode;
    public int damage;

    public bool destroyOnContact;

    public GameStatusController game;

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            game.lives -= damage;
            game.causeOfDeath = deathCode;
            if (destroyOnContact)
            {
                Destroy(gameObject);
            }
        }
    }
}

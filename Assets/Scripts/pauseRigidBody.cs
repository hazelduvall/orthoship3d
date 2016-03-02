using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pauseRigidBody : MonoBehaviour {

    public List<Rigidbody> pauseing;
    public List<Vector3> velocity;
    public List<Vector3> angular;
    public GameStatusController game;
    public keyBindings keys;

    private bool needsATrim;
    public System.Predicate<Rigidbody> nil;      //why does c# have to be so wierd?
    public System.Predicate<GameObject> gNil;

    void Start()
    {
        velocity = new List<Vector3>();
        angular = new List<Vector3>();
        foreach(Rigidbody r in pauseing)
        {
            velocity.Add(r.velocity);
            angular.Add(r.angularVelocity);
        }
        needsATrim = false;
        nil = isNull;
        gNil = isGNull;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keys.pause)&&game.gameOngoing)
        {
            switchPaused();
        }
    }

    public void switchPaused()
    {
        game.paused = !game.paused;
        if (!game.paused)
        {
            int i = 0;
            foreach (Rigidbody r in pauseing)
            {
                if (r != null)
                {
                    r.velocity = velocity[i];
                    r.angularVelocity = angular[i];
                    i++;
                }
            }
        }
    }
    void FixedUpdate()
    {
        needsATrim = false;
        if (game.paused)
        {
            foreach (Rigidbody r in pauseing)
            {
                if(r!=null)
                {
                    r.velocity = Vector3.zero;
                    r.angularVelocity = Vector3.zero;
                }
                else
                {
                    needsATrim = true;
                }
            }
        }
        else
        {
            int i = 0;
            foreach (Rigidbody r in pauseing)
            {
                if(r!=null)
                {
                    velocity[i] = r.velocity;
                    angular[i] = r.angularVelocity;
                    i++;
                }
                else
                {
                    needsATrim = true;
                }
            }
        }
        if(needsATrim)
        {
            pauseing.RemoveAll(nil);
        }
    }

    public void add(Rigidbody r)
    {
        pauseing.Add(r);
        velocity.Add(r.velocity);
        angular.Add(r.angularVelocity);
    }
    public bool isNull(Rigidbody r)
    {
        return r == null;
    }
    public bool isGNull(GameObject r)
    {
        return r == null;
    }
}

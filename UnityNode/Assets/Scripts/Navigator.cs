using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour {
    
    NavMeshAgent agent;
    Follower follower;

    // Start is called before the first frame update
    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        follower = GetComponent<Follower>();
    }

    // Update is called once per frame
    public void navigateTo(Vector3 position) {
        agent.SetDestination(position);
        follower.target = null;
    }

    private void Update() {
        GetComponent<Animator>().SetFloat("Distance", agent.remainingDistance);   
    }
}

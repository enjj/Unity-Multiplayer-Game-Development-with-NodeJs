using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour {
    
    NavMeshAgent agent;
    Targeter targeter;

    // Start is called before the first frame update
    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        targeter = GetComponent<Targeter>();
    }

    // Update is called once per frame
    public void navigateTo(Vector3 position) {
        agent.SetDestination(position);
        targeter.target = null;
    }

    private void Update() {
        GetComponent<Animator>().SetFloat("Distance", agent.remainingDistance);   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigatePosition : MonoBehaviour {

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void navigateTo(Vector3 position) {
        agent.SetDestination(position);
    }
}

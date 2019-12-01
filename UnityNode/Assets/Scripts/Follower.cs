using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour {
    public Transform target;

    public float scanFrequency = 0.5f;
    float lastScanTime = 0;
    public float stopFollowDistance = 2f;

    NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
   
    private void Update() {
        if (isReadyToScan() && isInRange()) {
            agent.SetDestination(target.position);
        }
    }

    private bool isInRange() {
        var distance = Vector3.Distance(target.position , transform.position);
        return distance > stopFollowDistance;
    }

    private bool isReadyToScan() {
        return Time.time - lastScanTime > scanFrequency && target;
    }
}

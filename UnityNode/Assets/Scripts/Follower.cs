using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Targeter))]
public class Follower : MonoBehaviour {
    public Targeter targeter;

    public float scanFrequency = 0.5f;
    float lastScanTime = 0;
    public float stopFollowDistance = 2f;

    NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        targeter = GetComponent<Targeter>();
    }
   
    private void Update() {
        if (isReadyToScan() && targeter.IsInRangeToFollow(stopFollowDistance)) {
            agent.SetDestination(targeter.target.position);
        }
    }


    private bool isReadyToScan() {
        return Time.time - lastScanTime > scanFrequency && targeter.target;
    }
}

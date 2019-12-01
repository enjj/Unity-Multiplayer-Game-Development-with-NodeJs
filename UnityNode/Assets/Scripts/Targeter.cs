using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public Transform target;

    public bool IsInRangeToFollow(float stopFollowDistance) {
        var distance = Vector3.Distance(transform.position, target.position);
        return distance > stopFollowDistance;
    }
    public bool IsInRangeToAttack(float attackRange) {
        var distance = Vector3.Distance(transform.position, target.position);
        return distance < attackRange;
    }
}

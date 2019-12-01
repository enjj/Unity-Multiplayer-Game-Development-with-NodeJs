using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public Transform target;
    public bool IsInRange(float stopFollowDistance) {
        var distance = Vector3.Distance(transform.position, target.position);
        return distance > stopFollowDistance;
    }
}

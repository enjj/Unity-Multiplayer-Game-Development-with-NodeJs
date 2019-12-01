using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

    public float attackDistance;
    public float attackRate;

    float lastAttackTime = 0;
    Targeter targeter;
    // Start is called before the first frame update
    void Start() {
        targeter = GetComponent<Targeter>();
    }

    // Update is called once per frame
    void Update() {
        if (isReadyToAttack() && targeter.IsInRangeToAttack(attackDistance)) {
            Debug.Log("attacking" + targeter.target.name);
            lastAttackTime = Time.time;
        }
    }

    bool isReadyToAttack() {
        return Time.time - lastAttackTime > attackRate && targeter.target;
    }
}

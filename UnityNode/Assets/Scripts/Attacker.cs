using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

    public float attackDistance;
    public float attackRate;
    float lastAttackTime = 0;
    

    Targeter targeter;
    Animator animator;
    // Start is called before the first frame update
    void Start() {
        targeter = GetComponent<Targeter>();
        
    }

    // Update is called once per frame
    void Update() {
        if (!isReadyToAttack())
            return;

        if (targeter.target.GetComponent<Hittable>().IsDead) {
            targeter.target = null;
            return;
        }

        if ( targeter.IsInRangeToAttack(attackDistance)) {
            Debug.Log("attacking" + targeter.target.name);
            var targetId = targeter.target.gameObject.GetComponent<NetworkEntity>().id;
            Network.Attack(targetId);
            lastAttackTime = Time.time;
        }
    }


    bool isReadyToAttack() {
        return Time.time - lastAttackTime > attackRate && targeter.target;
    }
}

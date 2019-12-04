using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{

    public float health = 100f;
    Animator animator;
    private bool isDead;
    private float respawnTime = 5f;

    public bool IsDead {
        get { return health<=0; }
    }


    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void OnHit() {
        health -= 10;
        if (IsDead) {
            animator.SetTrigger("Dead");
            Invoke("Spawn", respawnTime);
        }
        
    }

    public void Spawn() {
        Debug.Log("spawning");
        transform.position = Vector3.zero;
        health = 100;
        animator.SetTrigger("Spawn");

    }
}


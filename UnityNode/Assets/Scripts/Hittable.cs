using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{

    public float health = 100f;
    Animator animator;
    private bool isDead;


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
        }
        
    }
     
}

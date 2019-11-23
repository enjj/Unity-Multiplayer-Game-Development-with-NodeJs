using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour {

    public GameObject player;


    // Update is called once per frame
    public void OnClick(Vector3 pos) {
        NavigatePosition navPos = player.GetComponent<NavigatePosition>();
        navPos.navigateTo(pos);
    }
}

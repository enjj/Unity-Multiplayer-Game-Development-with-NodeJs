using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour {

    public GameObject player;
    NavigatePosition navPos;
    NetworkMove netMove;

    private void Start() {
       
    }

    // Update is called once per frame
    public void OnClick(Vector3 pos) {
        navPos = player.GetComponent<NavigatePosition>();
        netMove = player.GetComponent<NetworkMove>();
        navPos.navigateTo(pos);
        netMove.OnMove(pos); 
    }
}

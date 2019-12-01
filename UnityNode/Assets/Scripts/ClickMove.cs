using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour, IClickable {

    public GameObject player;
    Navigator navigator;

    private void Start() {
       
    }

    // Update is called once per frame
    public void OnClick(RaycastHit hit) {
        navigator = player.GetComponent<Navigator>();
      
        navigator.navigateTo(hit.point);

        Network.Move(hit.point); 
    }
}

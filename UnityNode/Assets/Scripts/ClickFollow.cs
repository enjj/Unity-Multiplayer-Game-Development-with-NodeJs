using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFollow : MonoBehaviour ,IClickable
{

    public GameObject myPlayer;
    public NetworkEntity networkEntity;

    private Targeter myPlayerTargeter;

    private void Start() {
        networkEntity = GetComponent<NetworkEntity>();
        myPlayerTargeter= myPlayer.GetComponent<Targeter>();
    }

    public void OnClick(RaycastHit hit) {

        Debug.Log("following" + hit.point);

        Network.Follow(networkEntity.id);

        myPlayerTargeter.target = transform; 

    }

   
}

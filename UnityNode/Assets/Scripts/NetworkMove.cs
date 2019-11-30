using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkMove : MonoBehaviour
{

    public SocketIOComponent socket;
  
    public void OnMove(Vector3 pos) {
        socket.Emit("move", new JSONObject(Network.VectorToJson(pos)));
        //send position to node
        Debug.Log("sending pos to node"+ Network.VectorToJson(pos));
    }

   
}

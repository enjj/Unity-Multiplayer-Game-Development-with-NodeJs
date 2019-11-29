using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkMove : MonoBehaviour
{

    public SocketIOComponent socket;
  
    public void OnMove(Vector3 pos) {
        socket.Emit("move", new JSONObject(VectorToJson(pos)));
        //send position to node
        Debug.Log("sending pos to node"+ VectorToJson(pos));
    }

    string VectorToJson(Vector3 vec) {
        return string.Format(@"{{""x"":""{0}"",""y"":""{1}""}}", vec.x , vec.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour
{
    static SocketIOComponent socket;

    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);  //neden parantez yok ?
    }

    void OnConnected(SocketIOEvent e) {
        Debug.Log("Connected");
        socket.Emit("test");
    }
}

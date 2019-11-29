using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class Network : MonoBehaviour {
    static SocketIOComponent socket;

    public GameObject playerPrefab;

    void Start() {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);  //neden parantez yok ? 
        socket.On("spawn", OnSpawned);
        socket.On("move", OnMove);

    }

    private void OnMove(SocketIOEvent e) {
        Debug.Log("player is moving" + e.data);
    }

    private void OnSpawned(SocketIOEvent e) {
        Instantiate(playerPrefab);
    }

    void OnConnected(SocketIOEvent e) {
        Debug.Log("Connected");
    }

}

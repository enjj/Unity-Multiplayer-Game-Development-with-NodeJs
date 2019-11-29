using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class Network : MonoBehaviour {
    static SocketIOComponent socket;

    public GameObject playerPrefab;

    Dictionary<string, GameObject> players;
    

    void Start() {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);  //neden parantez yok ? 
        socket.On("spawn", OnSpawned);
        socket.On("move", OnMove);
        players = new Dictionary<string, GameObject>();
        

    }

    private void OnMove(SocketIOEvent e) {

        Debug.Log("player is moving" + e.data);
        Vector3 pos = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));

        GameObject player = players[e.data["id"].ToString()];
        NavigatePosition navigatePos = player.GetComponent<NavigatePosition>();

        navigatePos.navigateTo(pos);
    }

    private void OnSpawned(SocketIOEvent e) {
        Debug.Log("spawned" + e.data);
        GameObject player = Instantiate(playerPrefab);

        players.Add(e.data["id"].ToString(), player);
        Debug.Log("count : " + players.Count);

    }

    void OnConnected(SocketIOEvent e) {
        Debug.Log("Connected");
    }

    float GetFloatFromJson(JSONObject data, string key) {

        return float.Parse(data[key].ToString().Replace("\"", ""));

    }
}

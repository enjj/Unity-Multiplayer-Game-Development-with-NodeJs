using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class Network : MonoBehaviour {

    static SocketIOComponent socket;

    public GameObject playerPrefab;

    Dictionary<string, GameObject> players;

    public GameObject myPlayer;


    void Start() {
        socket = GetComponent<SocketIOComponent>();
        players = new Dictionary<string, GameObject>();

        socket.On("open", OnConnected);  //neden parantez yok ? 
        socket.On("spawn", OnSpawned);
        socket.On("move", OnMove);
        socket.On("disconnected", OnDisconnected);
        socket.On("requestPosition", OnRequestPosition);
        socket.On("updatePosition", OnUpdatePosition);
    }

    private void OnUpdatePosition(SocketIOEvent e) {
        Debug.Log("updating position" + e.data);

        Vector3 position = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));

        GameObject player = players[e.data["id"].ToString()];

        player.transform.position = position;

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
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

       if(e.data["x"]) {
            Vector3 movePos = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));
            NavigatePosition navigatePos = player.GetComponent<NavigatePosition>();
            navigatePos.navigateTo(movePos);
        }

        players.Add(e.data["id"].ToString(), player);
        Debug.Log("count : " + players.Count);

    }

    void OnConnected(SocketIOEvent e) {
        Debug.Log("Connected");
    }

    private void OnRequestPosition(SocketIOEvent e) {
        Debug.Log("server is requesting position");

        socket.Emit("updatePosition", new JSONObject(VectorToJson(myPlayer.transform.position)));
    }


    private void OnDisconnected(SocketIOEvent e) {
        var player = players[e.data["id"].ToString()];

        Destroy(player);

        players.Remove(e.data["id"].ToString());
    }


    float GetFloatFromJson(JSONObject data, string key) {

        return float.Parse(data[key].ToString().Replace("\"", ""));

    }

    public static string VectorToJson(Vector3 vec) {
        return string.Format(@"{{""x"":""{0}"",""y"":""{1}""}}", vec.x, vec.z);
    }

}

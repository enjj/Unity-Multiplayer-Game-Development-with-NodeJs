using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class Spawner : MonoBehaviour {
    public GameObject playerPrefab;

    public GameObject myPlayer;
    public SocketIOComponent socket;

    Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();



    public GameObject SpawnPlayer(string id) {
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        player.GetComponent<ClickFollow>().myPlayer = myPlayer;
        player.GetComponent<NetworkEntity>().id = id;
        AddPlayer(id, player);
        return player;
    }

    public GameObject FindPlayer(string id) {
        return players[id];
    }

    public void AddPlayer(string id, GameObject player) {
        players.Add(id, player);

    }

    public void Remove(string id) {
        var player = players[id];
        Destroy(player);
        players.Remove(id);
    }
}

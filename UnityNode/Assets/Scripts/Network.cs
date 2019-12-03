using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class Network : MonoBehaviour {

    static SocketIOComponent socket;

    public Spawner spawner;

    public GameObject myPlayer;


    void Start() {
        socket = GetComponent<SocketIOComponent>();

        socket.On("open", OnConnected);
        socket.On("register", OnRegister);
        socket.On("spawn", OnSpawned);
        socket.On("move", OnMove);
        socket.On("follow", OnFollow);
        socket.On("attack", OnAttack);
        socket.On("disconnected", OnDisconnected);
        socket.On("requestPosition", OnRequestPosition);
        socket.On("updatePosition", OnUpdatePosition);
    }

   


    private void OnRegister(SocketIOEvent e) {
        Debug.Log("succesfully registered with id: " + e.data);
        spawner.AddPlayer(e.data["id"].str, myPlayer);
    }

    private void OnFollow(SocketIOEvent e) {
        Debug.Log("follow request" + e.data);

        GameObject player = spawner.FindPlayer(e.data["id"].str);
        Transform targetTransform = spawner.FindPlayer(e.data["targetId"].str).transform;


        Targeter follower = player.GetComponent<Targeter>();
        follower.target = targetTransform;
    }

    private void OnAttack(SocketIOEvent e) {
        Debug.Log("reciveced attack " + e.data);
        GameObject targetPlayer = spawner.FindPlayer(e.data["targetId"].str);
        GameObject attackingPlayer = spawner.FindPlayer(e.data["id"].str);
        attackingPlayer.GetComponent<Animator>().SetTrigger("Attack");

        targetPlayer.GetComponent<Hittable>().OnHit();

    }

    private void OnUpdatePosition(SocketIOEvent e) {
        Debug.Log("updating position" + e.data);

        Vector3 position = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));

        GameObject player = spawner.FindPlayer(e.data["id"].str);

        player.transform.position = position;

    }

    public static void Move(Vector3 pos) {
        socket.Emit("move", new JSONObject(Network.VectorToJson(pos)));
        //send position to node
        Debug.Log("sending pos to node" + Network.VectorToJson(pos));
    }

    public static void Follow(string id) {
        socket.Emit("follow", new JSONObject(Network.idToJson(id)));
        //send position to node
        Debug.Log("sending pos to node" + Network.idToJson(id));
    }

    public static void Attack(string targetId) {
        Debug.Log("attacking player : " + Network.idToJson(targetId));
        socket.Emit("attack", new JSONObject(Network.idToJson(targetId)));
    }


    private void OnMove(SocketIOEvent e) {
        Debug.Log("player is moving" + e.data);

        Vector3 pos = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));

        GameObject player = spawner.FindPlayer(e.data["id"].str);

        Navigator navigatePos = player.GetComponent<Navigator>();

        navigatePos.navigateTo(pos);
    }

    private void OnSpawned(SocketIOEvent e) {
        Debug.Log("spawned" + e.data);
        GameObject player = spawner.SpawnPlayer(e.data["id"].str);

        if (e.data["x"]) {
            Vector3 movePos = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));
            Navigator navigatePos = player.GetComponent<Navigator>();
            navigatePos.navigateTo(movePos);
        }

    }

    void OnConnected(SocketIOEvent e) {
        Debug.Log("Connected");
    }

    private void OnRequestPosition(SocketIOEvent e) {
        Debug.Log("server is requesting position");

        socket.Emit("updatePosition", new JSONObject(VectorToJson(myPlayer.transform.position)));
    }


    private void OnDisconnected(SocketIOEvent e) {
        spawner.Remove(e.data["id"].str);
    }


    float GetFloatFromJson(JSONObject data, string key) {

        return float.Parse(data[key].str);

    }

    public static string VectorToJson(Vector3 vec) {
        return string.Format(@"{{""x"":""{0}"",""y"":""{1}""}}", vec.x, vec.z);
    }

    public static string idToJson(string id) {
        return string.Format(@"{{""targetId"":""{0}""}}", id);

    }

}

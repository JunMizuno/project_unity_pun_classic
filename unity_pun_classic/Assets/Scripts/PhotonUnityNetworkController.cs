using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonUnityNetworkController : MonoBehaviour {
    private string ROOM_NAME = "Room";

    /// <summary>
    /// 開始時
    /// </summary>
	void Start () {
        // Photonに接続する
        // @memo. 引数でゲームバージョンを指定できる
        PhotonNetwork.ConnectUsingSettings(null);	
	}
	
    /// <summary>
    /// ロビーに入った時
    /// </summary>
    void OnJoinedLobby() {
        Debug.Log("ロビーに入りました");
        PhotonNetwork.JoinRandomRoom();
    }

    /// <summary>
    /// ルームに入った時
    /// </summary>
    void OnJoinedRoom() {
        Debug.Log("ルームに入りました");
    }

    /// <summary>
    /// ルーム入室失敗時
    /// </summary>
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("ルームの入室に失敗しました。");

        // ルームがないと入室に失敗するため、その時は自分で作る
        // 引数でルーム名を指定できる
        PhotonNetwork.CreateRoom(ROOM_NAME);
    }
}

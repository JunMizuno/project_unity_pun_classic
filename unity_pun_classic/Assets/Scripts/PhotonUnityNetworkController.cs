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
        CreateToJoinRoom(ROOM_NAME);
    }

    /// <summary>
    /// ルームに入った時
    /// </summary>
    void OnJoinedRoom() {
        Debug.Log("ルームに入りました");

        // ルーム名
        Debug.Log(PhotonNetwork.room.Name);
        // 現在人数
        Debug.Log(PhotonNetwork.room.PlayerCount);
        // 最大人数
        Debug.Log(PhotonNetwork.room.MaxPlayers);
        // 開放フラグ
        Debug.Log(PhotonNetwork.room.IsOpen);
        // 可視フラグ
        Debug.Log(PhotonNetwork.room.IsVisible);
        // カスタムプロパティ
        Debug.Log(PhotonNetwork.room.CustomProperties);
    }

    /// <summary>
    /// ルーム入室失敗時
    /// </summary>
    void OnPhotonRandomJoinFailed() {
        Debug.Log("ルームの入室に失敗しました。");

        // ルームがないと入室に失敗するため、その時は自分で作る
        // 引数でルーム名を指定できる
        CreateToJoinRoom(ROOM_NAME);
    }

    /// <summary>
    /// ルームを作成
    /// </summary>
    /// <param name="_roomName">Room name.</param>
    private void CreateToJoinRoom(string _roomName) {
        RoomOptions roomOptions = new RoomOptions();
        // オプション設定
        {
            // 最大参加人数を設定
            roomOptions.MaxPlayers = 4;

            // 入室を許可するかどうか
            roomOptions.IsOpen = true;

            // ロビーのルーム一覧リストにこのルームを表示させるかどうか
            roomOptions.IsVisible = true;
        }

        // カスタムプロパティ設定
        {
            // 名前空間(ExitGames.Client.Photon)内のHashtableクラスのインスタンスを生成して
            ExitGames.Client.Photon.Hashtable roomHash = new ExitGames.Client.Photon.Hashtable();

            // 設定したいカスタムプロパティを、キーと値のセットでAdd
            roomHash.Add("Time", 0);
            roomHash.Add("MapCode", 1);
            roomHash.Add("Mode", "Easy");

            // ルームオプションにハッシュをセット
            roomOptions.CustomRoomProperties = roomHash;
        }

        // 各種値の上書き＆追加
        {
            /*
            if (PhotonNetwork.inRoom)
            {
                PhotonNetwork.room.Name = "newRoomName";        // ルーム名変更
                PhotonNetwork.room.MaxPlayers = 10;             // 最大人数を変更
                PhotonNetwork.room.IsOpen = false;              // 部屋を閉じる
                PhotonNetwork.room.IsVisible = false;           // ロビーから見えなくする

                // カスタムプロパティの追加
                ExitGames.Client.Photon.Hashtable roomHash = new ExitGames.Client.Photon.Hashtable();
                roomHash.Add("hoge", "ほげ");
                PhotonNetwork.room.SetCustomProperties(roomHash);
            }
            */
        }

        // ルーム名、オプション、ロビーを指定
        PhotonNetwork.CreateRoom(_roomName, roomOptions, null);

        // ランダム作成の場合は以下
        //PhotonNetwork.JoinRandomRoom();
    }

    /// <summary>
    /// 以下、PunRPCの実装例
    /// </summary>
    /// <param name="_message">Message.</param>
    [PunRPC]
    public void TestRPC(string _message) {
        Debug.Log("PunRPCのテスト:" + _message);
    }

    public void TestFunc(PhotonView _photonView) {
        if (!_photonView) {
            return;
        }

        // PhotonTargets.All 全員
        // PhotonTargets.Other 自分以外に対して
        // PhotonTargets.MasterClient ルーム管理者に対して
        if (_photonView.isMine) {
            _photonView.RPC("TestRPC", PhotonTargets.All, "テストメッセージ");
        }
    }
}

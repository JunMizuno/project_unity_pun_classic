using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSyncObject : MonoBehaviour {
    private List<GameObject> _cubeList = new List<GameObject>();

    /// <summary>
    /// 更新時
    /// </summary>
	void Update () {
        float x = Random.Range(-10.0f, 10.0f);
        float y = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);
        Vector3 newPos = new Vector3(x, y, z);

        // 左ボタンクリック時
        // プレハブ、座標、回転、ビューインデックス
        if (Input.GetMouseButtonDown(0)) {
            var cube = PhotonNetwork.Instantiate("Cube", newPos, Quaternion.identity, 0);
            // 生成したオブジェクトに力を加える
            Rigidbody objRB = cube.GetComponent<Rigidbody>();
            objRB.AddForce(Vector3.forward * 20f, ForceMode.Impulse);
            _cubeList.Add(cube);
        }

        // 一定距離を離れたら解放する
        foreach (var cube in _cubeList) {
            var position = cube.GetComponent<Transform>().position;

            // ループ中にリストからオブジェクトを削除する場合はリストの長さが変わるためループから抜ける
            if (position.z > 70.0f) {
                _cubeList.Remove(cube);
                Destroy(cube);
                break;
            }
        }
    }
}

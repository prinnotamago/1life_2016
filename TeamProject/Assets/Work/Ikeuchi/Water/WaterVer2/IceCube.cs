using UnityEngine;
using System.Collections;

public class IceCube : MonoBehaviour {

    [SerializeField]
    Player _player;

    BoxCollider _boxCollider;

    // Use this for initialization
    void Start () {
	    if(_player == null) { Debug.Log("プレイヤーがInspectorから設定されていません"); }

        _boxCollider = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        // プレイヤーがアイスモードになったら見えないBoxを出して
        // 氷が壁に練り込んだりしないようにする(とりあえず適当につけてみた、不具合があれば直す)
        if (_player._playerMode == _player._PLAYER_MODE_ICE)
        {          
            if (_boxCollider.enabled == false)
            {
                _boxCollider.enabled = true;
            }
        }
        else
        {
            if (_boxCollider.enabled == true)
            {
                _boxCollider.enabled = false;
            }
        }
	}
}

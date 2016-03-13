using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    [SerializeField]
    GameObject _resultObj;

    [SerializeField]
    GameObject _player;

    [SerializeField]
    CameraControl _cameraControl;

    [SerializeField]
    Vector3 _goalMoveValue;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (_player != null && _cameraControl != null)
            {
                _player.GetComponent<Player>().enabled = false;
                _player.GetComponent<PlayerManagement>().enabled = false;
                _cameraControl.enabled = false;
                _player.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                Debug.Log("プレイヤーかカメラがInspectorからはいってません");
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        _player.GetComponent<Rigidbody>().velocity = _goalMoveValue;
    }

    void OnTriggerExit(Collider collider)
    {
        _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (_resultObj != null)
        {
            GameObject jointObj = GameObject.Instantiate(_resultObj);
            jointObj.transform.position = Vector3.zero;
        }
        else
        {
            Debug.Log("リザルトがInspectorからはいってません");
        }
    }
}

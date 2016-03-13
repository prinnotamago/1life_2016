using UnityEngine;
using System.Collections;

public class PlayerPosMatch : MonoBehaviour {

    [SerializeField]
    GameObject _playerObj;

    [SerializeField]
    ParticleSystem _particle;

    int _count = 0;

	// Use this for initialization
	void Start () {
        _playerObj = GameObject.Find(_playerObj.name);
	}
	
	// Update is called once per frame
	void Update () {
        var playerMode = _playerObj.GetComponent<Player>();
        if(_playerObj == null) { return; }
        if (playerMode._playerMode == playerMode._PLAYER_MODE_WATER)
        {

        }
        else if (playerMode._playerMode == playerMode._PLAYER_MODE_ICE)
        {
            transform.position = _playerObj.transform.position;

            //_playerObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (playerMode._playerMode == playerMode._PLAYER_MODE_AIR)
        {
            _count++;
            if (_count % 10 == 0)
            {
                var particle = GameObject.Instantiate(_particle);
                particle.transform.position = _playerObj.transform.position;
                //particle.transform.parent = _playerObj.transform;
            }
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

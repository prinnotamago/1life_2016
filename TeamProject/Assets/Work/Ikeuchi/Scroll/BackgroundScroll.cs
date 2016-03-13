using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

    [SerializeField]
    GameObject _backgroundObj = null;

    [SerializeField]
    GameObject _playerObj = null;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        _backgroundObj.transform.position =
            new Vector3(_playerObj.transform.position.x / -10.0f,
            _playerObj.transform.position.y / -10.0f + 3.5f,
            _backgroundObj.transform.position.z);
	}
}

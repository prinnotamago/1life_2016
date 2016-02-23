using UnityEngine;
using System.Collections;

public class ObjPosMatch : MonoBehaviour {

    [SerializeField]
    GameObject _obj = null;

	// Use this for initialization
	void Start () {
        _obj = GameObject.Find(_obj.name);
	}
	
	// Update is called once per frame
	void Update () {
        if(_obj == null) { return; }
        transform.position = _obj.transform.position;
    }
}

using UnityEngine;
using System.Collections;

public class PlayerJointMover : MonoBehaviour {

    GameObject _playerObj = null;

    public Vector3 _OFFSET_POS { get; set; }
    public float _ANGLE { get; set; }

	// Use this for initialization
	void Start () {
        _playerObj = transform.parent.gameObject;
        //Debug.Log(_playerObj.name);
	}
	
	// Update is called once per frame
	void Update () {
        //Move();
    }

    void Move()
    {
        var rigidBodyVector = _playerObj.GetComponent<Rigidbody>().velocity.normalized;
        var thisObjAndPlayerVector = _OFFSET_POS.normalized;
        var angle = Mathf.Atan2(rigidBodyVector.y - 1,
                                rigidBodyVector.x);
        var cross = Vector3.Cross(rigidBodyVector, thisObjAndPlayerVector);
        var crossAbs = Mathf.Abs(cross.z);
        var dot = Vector3.Dot(rigidBodyVector, thisObjAndPlayerVector);
        //Debug.Log(crossAbs);
        var a = new Vector3(_OFFSET_POS.x * rigidBodyVector.x,
            _OFFSET_POS.y * rigidBodyVector.y,
            _OFFSET_POS.z * rigidBodyVector.z);
        transform.localPosition = _OFFSET_POS + a;
        // new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f) * 0.5f;
    }
}

using UnityEngine;
using System.Collections;

public class FallPart : MonoBehaviour {

    private bool _fallStart;
    private Rigidbody _rigid;

    void Start () {
        _fallStart = false;
        _rigid = GetComponent<Rigidbody>();
        _rigid.useGravity = false;
	}
	
	void Update () {
        if (_fallStart)
        {
            _rigid.useGravity = true;
        }
	}
}

using UnityEngine;
using System.Collections;

public class FallObj : MonoBehaviour {

    private Rigidbody _rigid;
    
	void Start () {
        _rigid = GetComponent<Rigidbody>();
        _rigid.useGravity = false;
        _rigid.mass = 100;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (_rigid.useGravity == true) return;
        if(collision.gameObject.tag == "Player")_rigid.useGravity = true;
    }
}

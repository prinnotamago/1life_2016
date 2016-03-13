using UnityEngine;
using System.Collections;

public class FallFoothold : MonoBehaviour {

    private Rigidbody _rigidbody;
    [SerializeField]
    Player _player;

	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"&&
            _player._playerMode == Player.PlayerMode.WATER)
        {
            _rigidbody.useGravity = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }
}

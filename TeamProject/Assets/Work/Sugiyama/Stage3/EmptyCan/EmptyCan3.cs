using UnityEngine;
using System.Collections;

public class EmptyCan3 : MonoBehaviour {

    [SerializeField]
    private Player _player;
    private Rigidbody _rigidBody;

	void Start () {
        _rigidBody = GetComponent<Rigidbody>();
	}

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (_player.transform.position.x >= this.transform.position.x)
            {
                _rigidBody.velocity = new Vector3(1.0f, 0.0f, 0.0f);
            }
            else
            {
                _rigidBody.velocity = new Vector3(-1.0f, 0.0f, 0.0f);
            }
        }
    }
}

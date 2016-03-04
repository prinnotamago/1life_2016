using UnityEngine;
using System.Collections;

public class EmptyCan : MonoBehaviour {

    [SerializeField]
    Player _player;

    [SerializeField]
    GameObject _parent;

	void Update () {
        transform.localEulerAngles+=new Vector3(0,-1,0);
	}

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //_player.transform.parent = this.transform;
            _player.transform.localEulerAngles += new Vector3(0, 1, 0);
            _player.transform.position += new Vector3(0, 0, -0.01f);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player.transform.parent = _parent.transform;
            _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, 0.0f);
            _player.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Kan_Ue : MonoBehaviour
{
    [SerializeField]
    Player _player;

    [SerializeField]
    GameObject _parent;

    bool floor = false;

    void Start () {
	
	}
	

	void Update ()
    {
        if(floor == false)
        {
            transform.Rotate(-1, 0, 0);
        }
    }


    //OnCollisionStay       OnCollisionEnter
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            floor = true;

        }
    }

    //void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        _player.transform.parent = this.transform;
    //        _player.transform.localEulerAngles += new Vector3(0, 1, 0);
    //    }
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        _player.transform.parent = _parent.transform;
    //        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, 0.0f);
    //        _player.transform.localEulerAngles = new Vector3(0, 0, 0);
    //    }
    //}
}

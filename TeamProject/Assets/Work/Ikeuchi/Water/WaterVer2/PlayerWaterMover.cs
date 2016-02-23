using UnityEngine;
using System.Collections;

public class PlayerWaterMover : MonoBehaviour {

    public GameObject _PLAYER_OBJ { get; set; }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        VelocityUpdate();
    }

    void VelocityUpdate()
    {
        if(_PLAYER_OBJ == null) { return; }
        Vector3 length = transform.position - _PLAYER_OBJ.transform.position;
        if (length.sqrMagnitude > 1.0f)
        {
            transform.position = _PLAYER_OBJ.transform.position; ;
        }
        else
        {
            GetComponent<Rigidbody>().velocity *= 0.9f;
        }
    }
}

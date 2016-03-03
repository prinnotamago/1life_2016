using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public GameObject Light;
    public GameObject Water;

    bool _Switch = false;
    int count = 15 * 60;


    void Start()
    {

    }


    void Update()
    {
        if (_Switch)
        {
            count -= 1;

            if (count >= 0)
            {
                Light.transform.Rotate(0, 2, 0);

                Water.transform.Translate(0, -0.01f, 0);
            }
        }
    }


    //OnCollisionStay       OnCollisionEnter
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _Switch = true;

        }
    }

}

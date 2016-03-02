using UnityEngine;
using System.Collections;
using System;

public class Gear : MonoBehaviour
{
    [SerializeField]
    GameObject MoveObject;
    [SerializeField]
    private Vector3 moveValue = new Vector3(0, 0.01f, 0);

    private Vector3 _nowRotate;
    private Quaternion _oldRotate;

    void Start()
    {
        _oldRotate = transform.rotation;
    }

    void Update()
    {
        HasChangeRotate();
    }

    private void HasChangeRotate()
    {
        if (transform.hasChanged)
        {
            TransformObject();
        }
    }

    private void TransformObject()
    {
        if (MoveObject.transform.position.y + MoveObject.GetComponent<MeshFilter>().mesh.bounds.size.y * 1.5f >= transform.position.y)
        {
            MoveObject.transform.Translate(new Vector3(0, (transform.rotation.x - _oldRotate.x) * moveValue.y, 0));
            _oldRotate = transform.rotation;
            transform.hasChanged = false;
        }
    }
}

using UnityEngine;
using System.Collections;

public class FloatFoothold : MonoBehaviour
{
    [SerializeField]
    private bool _startFloat;
    public bool SetStartFloat(bool value)
    {
        _startFloat = value;
        return _startFloat;
    }
    [SerializeField]
    private float _floatSpeed = 0.01f;
    private float _upLimit = 1.0f;
    private int _floatTime;

    private int _count;

    void Start()
    {
        _startFloat = false;
        _floatTime = (int)(_upLimit / _floatSpeed);
        _count = 0;
    }

    void Update()
    {
        if (_startFloat && _count <= _floatTime)
        {
            transform.Translate(new Vector3(0, _floatSpeed, 0));
            _count++;
        }
    }
}

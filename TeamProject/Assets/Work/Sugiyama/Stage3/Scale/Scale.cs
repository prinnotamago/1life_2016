using UnityEngine;
using System.Collections;

//Rigidbodyの追加
[RequireComponent(typeof(Rigidbody))]

public class Scale : MonoBehaviour
{

    //[SerializeField]
    //private float _moveSpeed = 0.1f;
    private bool _move = false;

    [SerializeField]
    GameObject _OppositeScale;

    [SerializeField]
    private float _oppositeMoveValue = 2;

    Rigidbody _rigid;

    private Vector3 _defaultPos;
    private Vector3 _defaultOppositePos;

    private bool _flowWater;
    public bool GetFlowWater()
    {
        return _flowWater;
    }

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _defaultPos = this.transform.position;
        _defaultOppositePos = _OppositeScale.transform.position;

        //XY移動以外を固定化
        _rigid.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ|
                         RigidbodyConstraints.FreezePositionX|
        RigidbodyConstraints.FreezePositionZ;

        _rigid.useGravity = false;
        _flowWater = false;
    }

    void Update()
    {
        //天秤の上にいる場合天秤を動かす
        if (_move)
        {
            //this.transform.Translate(new Vector3(0, -1 * _moveSpeed, 0));
            //_Player.transform.Translate(new Vector3(0, -1 * _moveSpeed, 0));
            //_OppositeScale.transform.Translate(new Vector3(0, _moveSpeed, 0));
            _rigid.useGravity = true;
            Vector3 OppositePos =
                new Vector3(_OppositeScale.transform.position.x, ((_defaultPos.y-transform.position.y) * _oppositeMoveValue + _defaultOppositePos.y), _OppositeScale.transform.position.z);
            _OppositeScale.transform.position = OppositePos;
        }
        else { _rigid.useGravity = false; }
    }

    void OnCollisionEnter(Collision collision)
    {
        //プレイヤーが触ったら
        if (collision.gameObject.tag == "Player")
        {
            _move = true;
            _flowWater = true;
        }

        //ステージに触れたら
        if (collision.gameObject.tag == "Floor")
        {
            _move = false;
        }

    }

    void OnCollisionExit(Collision collision)
    {
        //プレイヤーが離れたら
        if (collision.gameObject.tag == "Player")
        {
            _move = false;
        }
    }

}

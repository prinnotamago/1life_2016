using UnityEngine;
using System.Collections;

public class WaterMove : MonoBehaviour {

    [SerializeField]
    GameObject _playerObj;

    [SerializeField]
    float _velocityPower = 3.0f;

    [SerializeField]
    bool _playerHoming = false;

    //bool _isRendered = false;
    bool _isRendered = true;

    bool isIce = true;

    float _sizeOffset;

    Vector3 _iceOffset;

    public bool _IS_RENDERED { get { return _isRendered; } }

    public Vector3 _GET_POSITION { get { return transform.position - new Vector3(0.0f, transform.localScale.y / 2, 0.0f); } }

    // Use this for initialization
    void Start () {
        _playerObj = GameObject.Find(_playerObj.name);
        _sizeOffset = _playerObj.GetComponent<SphereCollider>().radius;
    }
	
	// Update is called once per frame
	void Update () {
        if (_playerHoming)
        {
            Move();          
        }
        else
        {
            PlayerOnHit();
        }

        //OnCameraWhether();
        //Debug.Log(_isRendered);
        //_isRendered = false;
        //Debug.Log(_isRendered);
    }

    void Move()
    {
        if (_playerObj == null)
        {
            Debug.Log("_playerObj が　NULL です");
            return;
        }

        //MoveWater();

        GameObject player = GameObject.Find("Player");
        var playerMode = _playerObj.GetComponent<Player>();
        if (playerMode._playerMode == playerMode._PLAYER_MODE_WATER)
        {
            MoveWater();
            isIce = true;
        }
        else if (playerMode._playerMode == playerMode._PLAYER_MODE_ICE)
        {
            //WaterAndAirMove();
            //GetComponent<Player>().enabled = false;
            //GetComponent<Rigidbody>().enabled = true;
            if (isIce)
            {
                MoveIce();
                isIce = false;
            }
        }
        else if (playerMode._playerMode == playerMode._PLAYER_MODE_AIR)
        {
            MoveAir();
            isIce = true;
        }
        else
        {
            MoveWater();
        }

    }

    // その水(中心)がカメラに映ってるかどうか調べる
    void OnCameraWhether()
    {
        if (_playerObj == null)
        {
            Debug.Log("_playerObj が　NULL です");
            return;
        }
        Camera cameraObj = _playerObj.GetComponent<CameraReference>()._CAMERA_OBJ;
        Vector3 screenPoint = cameraObj.WorldToScreenPoint(transform.position);

        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.Log(hit.transform.gameObject.name);

            _isRendered = hit.transform == transform;
       
            //Debug.Log(_isRendered);
        }
    }

    public void PlayerHomingOn()
    {
        _playerHoming = true;
    }

    void PlayerOnHit()
    {
        if (_playerObj == null)
        {
            Debug.Log("_playerObj が　NULL です");
            return;
        }

        var offset =
            new Vector3(
                0.0f,
                -_sizeOffset,
                0.0f);
        Vector3 length = transform.position + offset - _playerObj.transform.position;
        float x = length.x * length.x;
        float y = length.y * length.y;
        //float z = length.z * length.z;

        float r = (_sizeOffset + _sizeOffset) * 2.0f;
        //if(x + y + z < r * r * r)
        if (x + y < r * r)
        {
            GameObject parentObj = GameObject.Find("PlayerWaters");
            if (parentObj != null)
            {
                transform.parent = parentObj.transform;
                PlayerHomingOn();
                //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            else
            {
                Debug.Log("PlayerWaters が見つかりません");
            }
        }
    }

    void MoveWater()
    {
        var offset =
            new Vector3(
                0.0f,
                -_playerObj.GetComponent<SphereCollider>().radius,
                0.0f);
        Vector3 targetPos = _playerObj.transform.position + offset;
        Vector3 myPos = transform.position;
        Vector3 targetDirection = targetPos - myPos;
        //transform.position += targetDirection / 50;
        //GetComponent<Rigidbody>().AddForce(targetDirection * 5);
        float value =
            targetDirection.x * targetDirection.x +
            targetDirection.y * targetDirection.y +
            targetDirection.z * targetDirection.z;
        //if(value < 0.3) { power = 3.0f; }

        // 1.0f * 1.0f = 1.0f
        if(value > 2.0f)
        {
            transform.position = _playerObj.transform.position;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (value > 1.0f)
        {
            //power = 5.0f;
            GetComponent<Rigidbody>().velocity = targetDirection * value * _velocityPower;
            //if()
        }
        else
        {
            GetComponent<Rigidbody>().velocity = targetDirection.normalized * _velocityPower;
        }

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_playerObj.transform.position - transform.position), 0.07f);
        ////transform.position += new Vector3(transform.forward.x, transform.forward.y, 0.0f) * 0.05f;
        //GetComponent<Rigidbody>().velocity = new Vector3(transform.forward.x, transform.forward.y, 0.0f) * 5.0f;

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;

        _iceOffset = transform.position - _playerObj.transform.position;
    }

    void MoveIce()
    {      
        var parent = GetComponentInParent<PlayerPosMatch>();
        transform.position = parent.transform.position + _iceOffset;

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    void MoveAir()
    {
        var offset =
            new Vector3(
                0.0f,
                -_playerObj.GetComponent<SphereCollider>().radius,
                0.0f);
        Vector3 targetPos = _playerObj.transform.position + offset;
        Vector3 myPos = transform.position;
        Vector3 targetDirection = targetPos - myPos;
        //transform.position += targetDirection / 50;
        //GetComponent<Rigidbody>().AddForce(targetDirection * 5);
        float value =
            targetDirection.x * targetDirection.x +
            targetDirection.y * targetDirection.y +
            targetDirection.z * targetDirection.z;
        //if(value < 0.3) { power = 3.0f; }

        // 1.0f * 1.0f = 1.0f
        if (value > 2.0f)
        {
            transform.position = _playerObj.transform.position;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (value > 1.0f)
        {
            //power = 5.0f;
            GetComponent<Rigidbody>().velocity = targetDirection * value * _velocityPower;
            //if()
        }
        else
        {
            GetComponent<Rigidbody>().velocity = targetDirection.normalized * _velocityPower;
        }

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_playerObj.transform.position - transform.position), 0.07f);
        ////transform.position += new Vector3(transform.forward.x, transform.forward.y, 0.0f) * 0.05f;
        //GetComponent<Rigidbody>().velocity = new Vector3(transform.forward.x, transform.forward.y, 0.0f) * 5.0f;

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;

        _iceOffset = transform.position - _playerObj.transform.position;
    }

    //void OnWillRenderObject()
    //{
    //_isRendered = true;
    //Debug.Log(_isRendered);
    //}

    //private void OnBecameVisible() { _isRendered = true; }
    //private void OnBecameInvisible() { _isRendered = false; }
}

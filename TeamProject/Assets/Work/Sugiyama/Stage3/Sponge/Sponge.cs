using UnityEngine;
using System.Collections;
public class Sponge : MonoBehaviour
{

    [SerializeField]
    private int _maxCount = 30;
    private int _damageCount;
    [SerializeField]
    private float _damageValue = 1;

    //仮置き
    [SerializeField]
    private float _hp = 100;

    private float _defaultspongeSize;
    private float _spongeSize;
    [SerializeField]
    private float _smallerValue;
    [SerializeField]
    Player _player;

    MeshCollider _meshCollider;
    

    void Start()
    {
        _damageCount = _maxCount;
        _defaultspongeSize = transform.localScale.x;
        _spongeSize = _defaultspongeSize;

        _meshCollider = GetComponent<MeshCollider>();
    }

    void Update()
    {
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && _player._playerMode == Player.PlayerMode.WATER)
        {
            _damageCount--;
            if (_damageCount <= 0)
            {
                _hp -= _damageValue;
                _damageCount = _maxCount;
            }
        }

        if (collision.gameObject.tag == "Player" && _player._playerMode == Player.PlayerMode.ICE)
        {
            if (_spongeSize >= _defaultspongeSize * 1 / 4)
            {
                _spongeSize = _spongeSize - _smallerValue;
                transform.localScale = new Vector3(_spongeSize, transform.localScale.y, transform.localScale.z);
                transform.position += new Vector3(0,- _smallerValue * 3 , 0);
                _player.transform.position += new Vector3(0,  - _smallerValue * 3, 0);
            }
        }

        if (collision.gameObject.tag == "Player" && _player._playerMode == Player.PlayerMode.AIR)
        {
            _meshCollider.convex = true;
            _meshCollider.isTrigger = true;
        }
    }


}

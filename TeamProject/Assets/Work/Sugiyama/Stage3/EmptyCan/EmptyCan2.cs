using UnityEngine;
using System.Collections;

public class EmptyCan2 : MonoBehaviour
{

    [SerializeField]
    private Player _player;

    private MeshRenderer _meshRenderer;
    private MeshCollider _meshCollider;

    private GameObject _childCan;
    private MeshRenderer _childMeshRenderer;
    private MeshCollider _childMeshCollider;

    private GameObject _childCan2;
    private MeshRenderer _child2MeshRenderer;
    private MeshCollider _child2MeshCollider;

    private const int _animeCount = 0;
    private const int _animeCount2 = 30;

    private int _count;
    private bool _countStart;

    private bool _animeEnd;

    void Start()
    {
        _childCan = transform.FindChild("gola_02").gameObject;
        _childCan2 = transform.FindChild("gola_03").gameObject;

        _meshRenderer = GetComponent<MeshRenderer>();
        _meshCollider = GetComponent<MeshCollider>();

        _childMeshRenderer = _childCan.GetComponent<MeshRenderer>();
        _childMeshCollider = _childCan.GetComponent<MeshCollider>();

        _child2MeshRenderer = _childCan2.GetComponent<MeshRenderer>();
        _child2MeshCollider = _childCan2.GetComponent<MeshCollider>();

        _count = 0;
        _countStart = false;
        _animeEnd = false;
    }

    void Update()
    {
        if (!_animeEnd)
        {
            if (_countStart && _count <= _animeCount2) _count++;

            if (_count > _animeCount)
            {
                _meshRenderer.enabled = false;
                _meshCollider.enabled = false;

                _childMeshRenderer.enabled = true;
                _childMeshCollider.enabled = true;
            }

            if (_count > _animeCount2)
            {
                _childMeshRenderer.enabled = false;
                _childMeshCollider.enabled = false;

                _child2MeshRenderer.enabled = true;
                _child2MeshCollider.enabled = true;

                if (_player._playerMode == Player.PlayerMode.ICE) _player._playerMode = Player.PlayerMode.WATER;
                _animeEnd = true;
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && _player._playerMode == Player.PlayerMode.ICE)
        {
            _countStart = true;
        }
    }
}

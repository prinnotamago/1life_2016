using UnityEngine;
using System.Collections;

//レイヤーに折れる枝の先端と折れる枝の根元を追加する

public class BrokenFoothold : MonoBehaviour
{

    private Rigidbody _rigidBody;

    private enum Break { NotBreak, Breaking, Breaked }

    private Break _break = Break.NotBreak;

    [SerializeField]
    private GameObject _parent;

    private const int _brokenBranchRoot = 11;

    [SerializeField]
    private Water _water;
    private int _countMax = (int)4.0f * 60;
    private int _count = 0;

    [SerializeField]
    private GameObject[] _brokeObjects;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        //回転と位置を固定
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;

        foreach (var brokeObj in _brokeObjects)
        {
            Rigidbody rigidbody = brokeObj.GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void Update()
    {
        //枝を回転
        if (_break == Break.Breaking)
        {
            _parent.transform.localEulerAngles += new Vector3(0.0f, 0.0f, 0.5f);

            foreach (var brokeObj in _brokeObjects)
            {
                Rigidbody rigidbody = brokeObj.GetComponent<Rigidbody>();
                rigidbody.constraints = RigidbodyConstraints.None;
                rigidbody.useGravity = true;
            }
        }
        else if (_break == Break.Breaked) _rigidBody.isKinematic = true;

        if (_water.scale.GetFlowWater()) _count++;
        if (_break == Break.NotBreak && _count >= _countMax) _break = Break.Breaking;
    }

    private void OnCollisionStay(Collision collision)
    {

        //ステージにぶつかったら
        if (collision.gameObject.tag == "Floor")
            if (collision.gameObject.layer != _brokenBranchRoot)
            {
                _break = Break.Breaked;
                _rigidBody.isKinematic = true;
            }

    }
}

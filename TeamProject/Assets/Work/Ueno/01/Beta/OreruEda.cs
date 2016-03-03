using UnityEngine;
using System.Collections;

public class OreruEda : MonoBehaviour {
    private Rigidbody _rigidBody;

    private enum Break { NotBreak, Breaking, Breaked }

    private Break _break = Break.NotBreak;

    private GameObject _parent;

    private const int _brokenBranchRoot = 11;

    [SerializeField]
    float _MAX_COUNT = 30;
    float _count = 0;

    void Start()
    {

        _parent = transform.root.gameObject;
        //_rigidBody = GetComponent<Rigidbody>();

        //回転と位置を固定
        //_rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {
        //枝を回転
        if (_break == Break.Breaking)
        {
            _parent.transform.localEulerAngles += new Vector3(0.0f, 0.0f, -1.0f);
            _count++;
            if(_count > _MAX_COUNT)
            {
                _break = Break.Breaked;
            }
        }
        //Debug.Log(_break);

    }

    private void OnCollisionStay(Collision collision)
    {
        //Playerタグのものにぶつかっているとき
        if (collision.gameObject.tag == "Player")
            //プレイヤーが一定以上進んだら折れる
            if (collision.transform.position.y >= this.transform.position.y)
                if (_break == Break.NotBreak) _break = Break.Breaking;

        //ステージにぶつかったら
        if (collision.gameObject.tag == "Floor")
            if (collision.gameObject.layer != _brokenBranchRoot)
            {
                _break = Break.Breaked;
                //_rigidBody.isKinematic = true;
            }

    }
}

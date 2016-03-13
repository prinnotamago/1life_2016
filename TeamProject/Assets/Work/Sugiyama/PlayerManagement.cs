using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//READ ME//
//Tagを使用しているので、床となるオブジェクトに"Floor"Tagを追加してください

//Rigidbodyの追加
[RequireComponent(typeof(Rigidbody))]

public class PlayerManagement : MonoBehaviour
{

    //速度
    [SerializeField]
    private float _GROUND_SPEED = 0.2f;
    [SerializeField]
    private float _SKY_SPEED = 0.1f;

    //空中にいるかどうかのフラグ
    [SerializeField]
    private bool _jump = true;

    //プレイヤーの状態
    [SerializeField]
    private Player _fujiwaraPlayer;

    //角度の限界値
    private const float _MAX_ANGLE = 0.5f;
    private const float _MIN_ANGLE = 0.2f;

    private Rigidbody _rigidBody;

    //プレイヤーを操作可能かどうか
    private bool _movingPlayer = true; 
    public void StopPlayer()
    {
        _movingPlayer = false;
    }

    public void MovingPlayer()
    {
        _movingPlayer = true;
    }



    private void Awake()
    {
        //ジャイロセンサーを有効化
        Input.gyro.enabled = true;
    }

    private void Start()
    {
        //コンポーネントを代入
        _rigidBody = GetComponent<Rigidbody>();

        //藤原プレイヤーを代入
        _fujiwaraPlayer = GetComponent<Player>();

        //XY移動以外を固定化
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezePositionZ;

        //プレイヤータグの追加
        this.tag = "Player";
    }

    private void Update()
    {
        //プレイヤーを動かせる状態の時
        if(_movingPlayer)_PlayerUpdate();
    }

    private void _PlayerUpdate()
    {
        //フレームレートの取得
        float fps = Time.deltaTime / (1f / 60f);

        _fujiwaraPlayer.Jump();

        //スマホの角度
        float angle = Input.gyro.gravity.x * 2;
        //最大値最低値の設定
        if (Input.gyro.gravity.x >= _MAX_ANGLE) angle = _MAX_ANGLE;
        if (Input.gyro.gravity.x <= -_MAX_ANGLE) angle = -_MAX_ANGLE;

        //氷状態の時はジャンプモードをオフに
        if (_fujiwaraPlayer._playerMode == Player.PlayerMode.ICE) _jump = false;

        //速度の代入
        float speed;
        if (!_jump) speed = _GROUND_SPEED * angle;
        else speed = _SKY_SPEED * angle;

        //水蒸気状態の時は操作が反転する
        if (_fujiwaraPlayer._playerMode == Player.PlayerMode.AIR) speed = speed * -1.0f;

        //プレイヤーの移動
        float AddVectorX = (-_rigidBody.velocity.x);
        _rigidBody.AddForce(new Vector3(AddVectorX, 0, 0));
        if (angle >= _MIN_ANGLE || angle <= -1 * _MIN_ANGLE) _rigidBody.velocity = new Vector3(speed * fps, _rigidBody.velocity.y, _rigidBody.velocity.z);

        //Debug.Log(_rigidBody.velocity);

        //PCでの操作確認用デバックキー
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!_jump)
            {
                //transform.Translate(new Vector3(_GROUND_SPEED * -1, 0.0f, 0.0f));
                _rigidBody.velocity = new Vector3(_GROUND_SPEED * -1, _rigidBody.velocity.y, _rigidBody.velocity.z);
            }
            else
            {
                //transform.Translate(new Vector3(_SKY_SPEED * -1, 0.0f, 0.0f));
                _rigidBody.velocity = new Vector3(_SKY_SPEED * -1, _rigidBody.velocity.y, _rigidBody.velocity.z);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!_jump)
            {
                //transform.Translate(new Vector3(_GROUND_SPEED, 0.0f, 0.0f));
                _rigidBody.velocity = new Vector3(_GROUND_SPEED, _rigidBody.velocity.y, _rigidBody.velocity.z);
            }
            else
            {
                //transform.Translate(new Vector3(_SKY_SPEED, 0.0f, 0.0f));
                _rigidBody.velocity = new Vector3(_SKY_SPEED, _rigidBody.velocity.y, _rigidBody.velocity.z);
            }
        }

        //地面との当たり判定
        //_jump = jumpDecision();

    }

    //public bool jumpDecision()
    //{
    //    Vector3 rayForward = new Vector3(0, -1, 0);
    //    Ray ray = new Ray(transform.position, rayForward,);
    //    RaycastHit hit;
    //    Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);
    //    if (Physics.Raycast(ray,out hit))
    //    {
    //        float dis = hit.distance;
    //        Debug.Log(dis);
    //        if (dis <= 0.06f) return false;
    //        else return true;
    //    }
    //    return true;
    //}

    private void OnCollisionStay(Collision collision)
    {
        //Floorタグのものにぶつかっているとき
        if (collision.gameObject.tag == "Floor")
        {
            //地面にいる判定
            _jump = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Floorタグのものから離れたら
        if (collision.gameObject.tag == "Floor")
        {
            //ジャンプしてる判定
            _jump = true;
        }
    }
}

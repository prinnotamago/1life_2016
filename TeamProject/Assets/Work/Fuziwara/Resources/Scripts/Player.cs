using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Fire _fire;
    private ParticleSystem _snowEffect;

    //重力を反転させるためのbool
    private bool _gravityMode = false;
    //重力
    [SerializeField]
    private Vector3 Down_Gravity = new Vector3(0, 1, 0);
    [SerializeField]
    private Vector3 Fly_Gravity = new Vector3(0, 1, 0);


    //プレイヤーの各状態を表す変数
    public enum PlayerMode
    {
        WATER = 0,
        AIR = 1,
        ICE = 2,
        CHANGE = 3,
    }
    public PlayerMode _playerMode = 0;
    public PlayerMode _PLAYER_MODE_WATER { get { return PlayerMode.WATER; } }
    public PlayerMode _PLAYER_MODE_ICE { get { return PlayerMode.ICE; } }
    public PlayerMode _PLAYER_MODE_AIR { get { return PlayerMode.AIR; } }
    public PlayerMode _PLAYER_MODE_CHANGE { get { return PlayerMode.CHANGE; } }

    //状態変化前の変数を格納する変数
    private PlayerMode _changeOldStay = 0;
    //状態変化先の変数を格納する変数
    private PlayerMode _changeNewStay = 0;
    //状態変化中にpositionを固定する変数
    private Vector3 _playerFreezePos = new Vector3(0, 0, 0);


    //ジャンプ関係の変数
    [SerializeField]
    private float Jump_Power = 50.0f;
    private bool _jump = false;


    //状態変化時に使う変数
    [SerializeField]
    private float _changeTime = 10;
    private float _changeCount = 0;

    //強制的に水蒸気状態から水に戻す
    [SerializeField]
    private float _autoChangeWater = 30;
    private float _changeWaterCount = 0;
    public float ChangeWaterCount { get { return _changeWaterCount; } }
    private bool _changeAir = false;

    public bool ChangeAir { get { return _changeAir; } }

    [SerializeField]
    private bool _debug;

    //ジャンプ
    private void JumpSystem(ref bool _jump)
    {
        if (!_jump && _playerMode == PlayerMode.WATER)
        {
            float _jumpPower = Jump_Power;
            _jump = true;

            _rigidbody.AddForce(new Vector3(0, 1, 0) * _jumpPower, ForceMode.Impulse);
        }
    }
    public void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            JumpSystem(ref _jump);
            if (_playerMode == PlayerMode.WATER)
            {
                SoundManager._instance.SePlay(1);
            }
        }
    }

    //重力
    private void GravitySystem()
    {
        if (_gravityMode)
        {
            _rigidbody.AddForce(Fly_Gravity, ForceMode.Acceleration);
        }
        else
        {
            _rigidbody.AddForce(-1 * Down_Gravity, ForceMode.Acceleration);
        }

        _gravityMode = (_playerMode == PlayerMode.AIR) ? true : false;
    }

    //状態変化
    private void ChangeState(Collision collision, ref PlayerMode _changeOldStay)
    {
        //オブジェクトの判別はtagで行っているので、tag追加お願いします
        //
        //プレイヤーが火に触れた時の処理
        if (collision.gameObject.tag == "Fire" && _changeCount == 0)
        {
            var changeStateF = (_playerMode == PlayerMode.WATER) ? PlayerMode.AIR
                : PlayerMode.WATER;

            ChangePlayerState(_playerMode, changeStateF);
        }

        //プレイヤーが氷に触れた時の処理
        if (collision.gameObject.tag == "Ice" && _changeCount == 0)
        {
            var changeStateI = (_gravityMode) ? PlayerMode.WATER
                : PlayerMode.ICE;

            ChangePlayerState(_playerMode, changeStateI);
        }

        //プレイヤーが着地している時
        if (collision.gameObject.tag == "Floor")
        {
            _jump = false;
            if (_playerMode == PlayerMode.WATER)
            {
                SoundManager._instance.SePlay(12);
            }
        }

        //プレイヤーが壊せる壁に触れた時の処理
        if (collision.gameObject.tag == "BreakWall")
        {
            if (_playerMode == PlayerMode.ICE) { _playerMode = PlayerMode.WATER; }
        }
    }

    private void ChangePlayerState(PlayerMode playerMode, PlayerMode changeState)
    {
        _changeOldStay = playerMode;
        _playerMode = PlayerMode.CHANGE;
        _changeNewStay = changeState;

        _playerFreezePos = gameObject.transform.position;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _fire = FindObjectOfType<Fire>();
        _snowEffect = FindObjectOfType<ParticleSystem>();
    }

    private void Update()
    {
        DebugKey(_debug);

        //一定時間たつとプレイヤーの状態が変化する処理
        if (_playerMode == PlayerMode.CHANGE)
        {
            TimeChangePlayerState(_changeNewStay);
        }
        else
        {
            //各状態の重力処理
            GravitySystem();
        }

        //一定時間で水蒸気から水にする処理
        if (_playerMode == PlayerMode.AIR)
        {
            AutoChangeWater();
        }
    }

    private void DebugKey(bool _debug)
    {
        if (_debug)
        {
            //デバック用のキー操作
            if (!((_playerMode == PlayerMode.ICE) && _jump))
            {
                var r = Input.GetAxis("Horizontal");
                transform.Translate(r / 5, 0, 0);
            }
            Jump();
        }
    }

    private void TimeChangePlayerState(PlayerMode _changeNewStay)
    {
        _changeCount += Time.deltaTime;
        gameObject.transform.position = _playerFreezePos;
        if (_changeCount > _changeTime)
        {
            _playerMode = _changeNewStay;
            if (!(_changeOldStay == PlayerMode.AIR))
            {
                _fire.Destroy();
            }
            _changeCount = 0;
        }
    }

    private void AutoChangeWater()
    {
        _changeWaterCount++;

        if (_changeWaterCount > _autoChangeWater * 60)
        {
            _playerMode = PlayerMode.WATER;
            _changeWaterCount = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ChangeState(collision, ref _changeOldStay);
    }
}
using UnityEngine;
using System.Collections;

//READ ME//
//Tagを使用しているので、プレイヤーオブジェクトに"Player"Tagを追加してください

//ボックスコライダーの追加
[RequireComponent(typeof(BoxCollider))]

public class DeathZone : MonoBehaviour
{

    private BoxCollider _boxCollider;

    private Player _player;
    private PlayerManagement _playerManagement;

    [SerializeField]
    private GameObject _redFadeOutParticle;

    private GameObject _gameOverParticle;

    private bool _gameOver = false;
    [SerializeField]
    private int _effectLimit = 2;
    private int _effectCount = 0;

    [SerializeField]
    Option_Button _optionButton = null;

    //ゲームオーバー
    private float _gameOverTextPosX;
    private float _gameOverTextPosY;

    private float _gameOverTextSizeX;
    private float _gameOverTextSizeY;

    //リスタート
    private float _restartButtonPosX;
    private float _restartButtonPosY;

    private float _restartButtonSizeX;
    private float _restartButtonSizeY;

    //ステージセレクト
    private float _returnButtonPosX;
    private float _returnButtonPosY;

    private float _returnButtonSizeX;
    private float _returnButtonSizeY;

    private bool _gameOverScreen = false;

    private bool _deathFlag = false;
    private int _deathCount = 0;

    private void Start()
    {
        //IsTriggerをTrueにする
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;

        _player = GameObject.FindObjectOfType<Player>();
        _playerManagement = _player.GetComponent<PlayerManagement>();

        _gameOverTextSizeX = 110;
        _gameOverTextSizeY = 25;

        _gameOverTextPosX = (Screen.width - _gameOverTextSizeX) / 2;
        _gameOverTextPosY = Screen.height / 4;

        int OffSet = 50;

        _restartButtonSizeX = 150;
        _restartButtonSizeY = 50;

        _restartButtonPosX = (Screen.width) / 2 + OffSet;
        _restartButtonPosY = Screen.height / 4 + _gameOverTextSizeY + OffSet;

        _returnButtonSizeX = 150;
        _returnButtonSizeY = 50;

        _returnButtonPosX = (Screen.width) / 2 - _returnButtonSizeX - OffSet;
        _returnButtonPosY = Screen.height / 4 + _gameOverTextSizeY + OffSet;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Playerタグのついたオブジェクトと接触したら
        if (other.tag == "Player")
        {
            //_gameOver = true;

            _deathFlag = true;

            //プレイヤーの操作受付を解除
            _playerManagement.StopPlayer();
        }
    }

    private void Update()
    {
        //ゲームオーバーなら
        if (_deathFlag)
        {
            ++_deathCount;
            if(_deathCount > 30)
            {
                _deathFlag = false;
                _gameOver = true;
                _optionButton.DeathPauseOn();
            }
        }
        if (_gameOver)
        {
            if (_GameOverEffect())
            {
                _gameOverScreen = true;
            }
        }
    }

    void OnGUI()
    {
        if (!_gameOverScreen) return;

        _player.GetComponent<Option_Button>().enabled = false;

        GUI.TextField(new Rect(_gameOverTextPosX, _gameOverTextPosY, _gameOverTextSizeX, _gameOverTextSizeY), "ゲームオーバー！");

        //リスタートボタン
        if (GUI.Button(new Rect(_restartButtonPosX, _restartButtonPosY, _restartButtonSizeX, _restartButtonSizeY), "Restart"))
        {
            Application.LoadLevel("StageTest");
        }

        //ステージセレクトへのボタン
        if (GUI.Button(new Rect(_returnButtonPosX, _returnButtonPosY, _returnButtonSizeX, _returnButtonSizeY), "Return"))
        {
            Application.LoadLevel("StageSelect");
        }
    }

    private bool _GameOverEffect()
    {
        //ゲームオーバーエフェクトが終了していたら削除＆カウント
        if (_gameOverParticle != null)
        {
            //if (!_gameOverParticle.GetComponent<ParticleSystem>().IsAlive())
            //{
            //    Destroy(_gameOverParticle);
            //    _effectCount++;
            //    Debug.Log("エフェクト終了");
            //}
        }
        //エフェクトの表示が２回以上行い終わっていたら
        if (_effectCount >= 2)
        {
            return true;
        }

        //ゲームオーバーエフェクトを生成
        if (_gameOverParticle == null)
        {
            _gameOverParticle = Instantiate(_redFadeOutParticle);
            CameraReference cameraRef = _player.GetComponent<CameraReference>();
            Camera cameraObj = cameraRef._CAMERA_OBJ;
            _gameOverParticle.transform.parent = cameraObj.transform;
            _gameOverParticle.transform.localPosition = new Vector3(0.0f, 0.0f, 0.5f);
        }
        return false;
    }
}

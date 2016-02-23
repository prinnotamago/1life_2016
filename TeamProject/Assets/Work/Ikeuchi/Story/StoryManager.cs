using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {

    [SerializeField]
    Text _text;

    [SerializeField]
    GameObject _imageObj;

    [SerializeField]
    GameObject _nextTextObj;

    [SerializeField]
    string _stageName;

    [SerializeField]
    int _bgnNum;

    //struct Story
    //{
    //    Vector3 cameraPos;
    //    string text;
    //}
    [SerializeField]
    Vector3[] _cameraPostions;
    [SerializeField, TextArea(2, 5)]
    string[] _messages;

    const int _MESSAGE_SPEED = 3;
    int _messageTimer = 0;

    int _messageCount = 0;
    int _messageStringCount = 0;
    bool _isTextUpdate = true;

    bool _isGetUpdate = true;

    // Use this for initialization
    void Start () {
        SoundManager._instance.BgmPlay(_bgnNum);
    }
	
	// Update is called once per frame
	void Update () {
        if (_isGetUpdate)
        {
            var objs = GetComponentsInChildren<ParticleSystem>();
            if (objs.Length != 0) { return; }
            else
            {
                _imageObj.SetActive(true);
                _isGetUpdate = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!_isTextUpdate)
            {
                if (_messageCount + 1 != _messages.Length)
                {
                    _text.text = "";
                    _messageTimer = 0;
                    ++_messageCount;
                    _messageStringCount = 0;
                    _isTextUpdate = true;
                    _nextTextObj.SetActive(false);
                }
                else
                {
                    _imageObj.SetActive(false);
                    if (GameManager._isStoryMode)
                    {
                        GameManager._isStoryMode = false;
                        //Debug.Log("ストーリーにいく");
                        GetComponent<FadeCreater>().CreateFadeOut(_stageName);
                        SoundManager._instance.BgmStop();
                    }
                    else
                    {
                        //Debug.Log("セレクト画面に戻る");
                        GetComponent<FadeCreater>().CreateFadeOut("Gallery");
                        SoundManager._instance.BgmStop();
                    }
                }
            }
            else
            {
                _text.text = _messages[_messageCount];
                _isTextUpdate = false;
                _nextTextObj.SetActive(true);
            }
        }

        ++_messageTimer;
        if (_messageTimer % _MESSAGE_SPEED == 0)
        {
            if (_isTextUpdate)
            {
                _text.text += _messages[_messageCount][_messageStringCount];
            }
            if (_messageStringCount + 1 != _messages[_messageCount].Length)
            {
                ++_messageStringCount;
            }
            else
            {
                _isTextUpdate = false;
                _nextTextObj.SetActive(true);
            }
        }

        Vector3 moveValue = (transform.position - _cameraPostions[_messageCount]) / 10;
        transform.position -= moveValue;
    }
}

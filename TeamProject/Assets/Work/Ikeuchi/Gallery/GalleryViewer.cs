using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GalleryViewer : MonoBehaviour
{

    [SerializeField]
    string _stageSelectSceneName;

    [SerializeField]
    string[] _stageSceneNames;

    [SerializeField]
    Texture[] _stageImages;

    [SerializeField]
    Texture _leftImages;
    [SerializeField]
    Texture _rightImages;

    [SerializeField]
    Texture _backImages;

    static int _selectNum = 0;

    float _scrollX = 0;

    const float _FADE_COUNT_MAX = 60.0f;
    float _fadeCount = 0.0f;

    bool _isSelectOn = false;
    bool _isSelectEnter = false;

    // Use this for initialization
    void Start()
    {
        SoundManager._instance.BgmPlay(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsScrollStop()) { _scrollX -= (_scrollX - Screen.width * _selectNum) / 5; }
        //else { Debug.Log("一致"); }
    }

    bool IsScrollStop()
    {
        return _scrollX < Screen.width * _selectNum - 1 ||
            _scrollX > Screen.width * _selectNum + 1;
    }

    void OnGUI()
    {
        //GUI.backgroundColor = new Color(0, 0, 0, _fadeCount / _FADE_COUNT_MAX);
        GUI.color = new Color(1, 1, 1, _fadeCount / _FADE_COUNT_MAX);
        if (!_isSelectOn)
        {
            if (_fadeCount != _FADE_COUNT_MAX)
            {
                _fadeCount++;
            }
            else
            {
                _isSelectOn = true;
            }
        }

        if (_isSelectEnter)
        {
            if (_fadeCount != 0) { _fadeCount--; }
        }

        // ステージボタン
        Vector2 stageOffsetPos = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 stageButtonSize = new Vector2(Screen.width / 5 * 3, Screen.height / 9 * 5);
        for (int i = 0; i < _stageImages.Length; ++i)
        {
            Rect stagePos = new Rect(stageOffsetPos.x - stageButtonSize.x / 2 + Screen.width * i - _scrollX,
                                     stageOffsetPos.y - stageButtonSize.y / 2,
                                     stageButtonSize.x, stageButtonSize.y);
            if (GUI.Button(stagePos, _stageImages[i]) && !IsScrollStop() && _isSelectOn)
            {
                //Debug.Log("Stage");
                SelectEnterOn();
                GetComponent<FadeCreater>().CreateFadeOut(_stageSceneNames[_selectNum]);
                SoundManager._instance.SePlay(2);
                SoundManager._instance.BgmStop();
            }
        }

        // 右左ボタン
        Vector2 ArrowOffsetPos = new Vector2(Screen.width / 10, Screen.height / 2);
        Vector2 ArrowButtonSize = new Vector2(Screen.width / 10 * 1.5f, Screen.height / 10 * 2.5f);
        Rect ArrowPosLeft = new Rect(ArrowOffsetPos.x - ArrowButtonSize.x / 2,
                                     ArrowOffsetPos.y - ArrowButtonSize.y / 2,
                                     ArrowButtonSize.x, ArrowButtonSize.y);
        Rect ArrowPosRIGHT = new Rect(Screen.width - ArrowOffsetPos.x - ArrowButtonSize.x / 2,
                                      ArrowOffsetPos.y - ArrowButtonSize.y / 2,
                                      ArrowButtonSize.x, ArrowButtonSize.y);
        if (GUI.Button(ArrowPosLeft, _leftImages) && _isSelectOn)
        {
            //Debug.Log("ひだり");
            if (0 < _selectNum) { _selectNum--; }
            SoundManager._instance.SePlay(3);
        }

        if (GUI.Button(ArrowPosRIGHT, _rightImages) && _isSelectOn)
        {
            //Debug.Log("みぎ");
            if (_selectNum < _stageImages.Length - 1) { _selectNum++; }
            SoundManager._instance.SePlay(3);
        }

        // 戻るボタン
        Vector2 OptionOffsetPos = new Vector2(Screen.width - Screen.width / 15, Screen.height / 10);
        Vector2 OptionButtonSize = new Vector2(Screen.width / 10 * 1.0f, Screen.height / 10 * 1.5f);
        Rect OptionPosLeft = new Rect(OptionOffsetPos.x - OptionButtonSize.x / 2,
                                      OptionOffsetPos.y - OptionButtonSize.y / 2,
                                      OptionButtonSize.x, OptionButtonSize.y);
        if (GUI.Button(OptionPosLeft, _backImages) && _isSelectOn)
        {
            //Debug.Log("戻る");
            _isSelectEnter = true;
            GetComponent<FadeCreater>().CreateFadeOut(_stageSelectSceneName);
            SoundManager._instance.SePlay(3);
            SoundManager._instance.BgmStop();
        }
    }

    void SelectEnterOn()
    {
        _isSelectEnter = true;
        _fadeCount = _FADE_COUNT_MAX;
    }
}

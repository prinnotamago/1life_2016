using UnityEngine;
using System.Collections;

public class StageScroll : MonoBehaviour
{

    [SerializeField]
    string _gallerySceneNames;

    //[SerializeField]
    //string _optionSceneNames;

    [SerializeField]
    string[] _stageSceneNames;

    [SerializeField]
    Texture[] _stageImage;

    [SerializeField]
    Texture _leftImage;
    [SerializeField]
    Texture _rightImage;

    [SerializeField]
    Texture _galleryImage;
    [SerializeField]
    Texture _optionImage;

    static int _selectNum = 0;

    float _scrollX = 0;

    //[SerializeField]
    float _FADE_OUT_COUNT_MAX = 60.0f;
    //[SerializeField]
    float _FADE_IN_COUNT_MAX = 60.0f;

    float _fadeCount = 0.0f;
    //float _fadeOutCount = -30.0f;

    bool _isSelectOn = false;
    bool _isSelectEnter = false;

    // Use this for initialization
    void Start()
    {

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
        if (!_isSelectOn)
        {
            if (_fadeCount != _FADE_IN_COUNT_MAX)
            {
                _fadeCount++;
            }
            else
            {
                _isSelectOn = true;
            }
            GUI.color = new Color(1, 1, 1, _fadeCount / _FADE_IN_COUNT_MAX);
        }

        if (_isSelectEnter)
        {
            if (_fadeCount != 0) { _fadeCount--; }
            GUI.color = new Color(1, 1, 1, _fadeCount / _FADE_OUT_COUNT_MAX);
        }

        // ステージボタン
        Vector2 stageOffsetPos = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 stageButtonSize = new Vector2(Screen.width / 5 * 3, Screen.height / 9 * 5);
        for (int i = 0; i < _stageSceneNames.Length; ++i)
        {
            Rect stagePos = new Rect(stageOffsetPos.x - stageButtonSize.x / 2 + Screen.width * i - _scrollX,
                                     stageOffsetPos.y - stageButtonSize.y / 2,
                                     stageButtonSize.x, stageButtonSize.y);
            if (GUI.Button(stagePos, _stageImage[i]) && !IsScrollStop() && _isSelectOn)
            {
                //Debug.Log("Stage");
                SelectEnterOn();
                GetComponent<FadeCreater>().CreateFadeOut(_stageSceneNames[_selectNum]);
                SoundManager._instance.SePlay(2);
                GameManager._isStoryMode = true;
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
        if (GUI.Button(ArrowPosLeft, _leftImage) && _isSelectOn)
        {
            //Debug.Log("ひだり");
            if (0 < _selectNum) { _selectNum--; }
            SoundManager._instance.SePlay(3);
        }

        if (GUI.Button(ArrowPosRIGHT, _rightImage) && _isSelectOn)
        {
            //Debug.Log("みぎ");
            if (_selectNum < _stageSceneNames.Length - 1) { _selectNum++; }
            SoundManager._instance.SePlay(3);
        }

        // オプションボタン
        Vector2 OptionOffsetPos = new Vector2(Screen.width - Screen.width / 15, Screen.height / 10);
        Vector2 OptionButtonSize = new Vector2(Screen.width / 10 * 1.0f, Screen.height / 10 * 1.5f);
        Rect OptionPosLeft = new Rect(OptionOffsetPos.x - OptionButtonSize.x / 2,
                                      OptionOffsetPos.y - OptionButtonSize.y / 2,
                                      OptionButtonSize.x, OptionButtonSize.y);
        if (GUI.Button(OptionPosLeft, _optionImage) && _isSelectOn)
        {
            //Debug.Log("オプション");
            //SelectEnterOn();
            //GetComponent<FadeCreater>().CreateFadeOut(_optionSceneNames);
            GameObject.Find("PauseManager").GetComponent<Pausable>().pausing = true;
            SoundManager._instance.SePlay(3);
        }

        // ギャラリーボタン
        Vector2 GalleryOffsetPos = new Vector2(Screen.width - Screen.width / 5.5f, Screen.height / 10);
        Vector2 GalleryButtonSize = new Vector2(Screen.width / 10 * 1.0f, Screen.height / 10 * 1.5f);
        Rect GalleryPosLeft = new Rect(GalleryOffsetPos.x - GalleryButtonSize.x / 2,
                                       GalleryOffsetPos.y - GalleryButtonSize.y / 2,
                                        GalleryButtonSize.x, GalleryButtonSize.y);
        if (GUI.Button(GalleryPosLeft, _galleryImage) && _isSelectOn)
        {
            //Debug.Log("ギャラリー");
            SelectEnterOn();
            GetComponent<FadeCreater>().CreateFadeOut(_gallerySceneNames);
            SoundManager._instance.SePlay(3);
        }
    }

    void SelectEnterOn()
    {
        _isSelectEnter = true;
        _fadeCount = _FADE_OUT_COUNT_MAX;
    }
}

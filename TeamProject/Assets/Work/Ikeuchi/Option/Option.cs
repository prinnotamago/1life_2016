using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour
{

    [SerializeField]
    GameObject _bamButton;
    [SerializeField]
    GameObject _seButton;
    [SerializeField]
    GameObject _backGround;

    [SerializeField]
    Texture _backImage;

    Pausable _pousable;

    // Use this for initialization
    void Start()
    {
        _pousable = GameObject.Find("PauseManager").GetComponent<Pausable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pousable.pausing)
        {
            _bamButton.SetActive(true);
            _seButton.SetActive(true);
            _backGround.SetActive(true);
        }
        else
        {
            _bamButton.SetActive(false);
            _seButton.SetActive(false);
            _backGround.SetActive(false);
        }
    }

    void OnGUI()
    {
        if (_pousable.pausing)
        {
            // 戻るボタン
            Vector2 OptionOffsetPos = new Vector2(Screen.width / 15, Screen.height / 10);
            Vector2 OptionButtonSize = new Vector2(Screen.width / 10 * 1.0f, Screen.height / 10 * 1.5f);
            Rect OptionPosLeft = new Rect(OptionOffsetPos.x - OptionButtonSize.x / 2,
                                          OptionOffsetPos.y - OptionButtonSize.y / 2,
                                          OptionButtonSize.x, OptionButtonSize.y);
            if (GUI.Button(OptionPosLeft, _backImage))
            {
                //Debug.Log("戻る");
                _pousable.pausing = false;
                SoundManager._instance.SePlay(3);
            }
        }
    }
}

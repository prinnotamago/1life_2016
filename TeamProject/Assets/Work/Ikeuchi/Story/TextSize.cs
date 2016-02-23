using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextSize : MonoBehaviour {

    [SerializeField]
    Text _text;

    [SerializeField]
    bool _isTextTriangle = false;

    float count;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_text != null)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10 * 4.0f, Screen.height / 6);
            if (_isTextTriangle)
            {
                count += 0.1f;
                GetComponent<RectTransform>().position = new Vector3(Screen.width / 10 * 5.0f, Screen.height / 6.0f + Mathf.Sin(count) * Screen.height / 100.0f, 0.0f);
            }
            else
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10 * 3.0f, Screen.height / 4.5f);
            }
            _text.fontSize = Screen.width / 30;
        }
        else
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10 * 3.0f, Screen.height / 4);
            GetComponent<RectTransform>().position = new Vector3(Screen.width / 4, Screen.height / 4, 0.0f);
        }
    }
}

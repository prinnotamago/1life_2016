using UnityEngine;
using System.Collections;

public class ButtonSizeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
	}

    public void BgmStop()
    {
        SoundManager._instance.BgmStop();
    }
}

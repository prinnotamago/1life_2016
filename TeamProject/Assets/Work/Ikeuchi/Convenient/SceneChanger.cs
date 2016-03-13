using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {

    [SerializeField]
    private string _nextSceneName;

    [SerializeField]
    private Button _button;

	// Use this for initialization
	void Start () {
        SoundManager._instance.BgmPlay(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SceneChange()
    {
        //Application.LoadLevel(_nextSceneName);
        _button.enabled = false;

        //CreateFadeOut();
        SoundManager._instance.BgmStop();
        SoundManager._instance.SePlay(2);
        GetComponent<FadeCreater>().CreateFadeOut(_nextSceneName);
    }

    //void CreateFadeOut()
    //{
    //    var fade = GameObject.Instantiate(_fadeOutParticle);
    //    fade.transform.parent = _cameraObj.transform;
    //    fade.transform.localPosition = new Vector3(0.0f, 0.0f, 0.5f);
    //    fade.GetComponent<FadeOutParticleDestroyAndChangeScene>()._NEXT_SCENE_NAME = _nextSceneName;
    //}
}

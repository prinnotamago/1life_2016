using UnityEngine;
using System.Collections;

public class FadeCreater : MonoBehaviour {

    [SerializeField]
    GameObject _fadeOutParticle;

    [SerializeField]
    GameObject _cameraObj;

    void Strat()
    {
        _cameraObj = GameObject.Find(_cameraObj.name);
    }

    public void CreateFadeOut(string nextSceneName)
    {
        var fade = GameObject.Instantiate(_fadeOutParticle);
        _cameraObj = _cameraObj == null ? _cameraObj : GameObject.Find(_cameraObj.name);
        fade.transform.parent = _cameraObj.transform;
        fade.transform.localPosition = new Vector3(0.0f, 0.0f, 0.5f);
        fade.GetComponent<FadeOutParticleDestroyAndChangeScene>()._NEXT_SCENE_NAME = nextSceneName;
    }
}

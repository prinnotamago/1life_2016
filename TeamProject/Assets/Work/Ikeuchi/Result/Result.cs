using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Result : MonoBehaviour {

    [SerializeField]
    string _stageSelectName = "StageSelect";

    [SerializeField]
    Button _btn;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SceneChangeGoStageSelect()
    {
        GetComponent<FadeCreater>().CreateFadeOut(_stageSelectName);
        _btn.interactable = false;       
    }
}

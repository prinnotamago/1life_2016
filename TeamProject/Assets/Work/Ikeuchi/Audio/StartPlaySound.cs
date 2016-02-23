using UnityEngine;
using System.Collections;

public class StartPlaySound : MonoBehaviour {

    [SerializeField]
    int _soundNum;

	// Use this for initialization
	void Start () {
        SoundManager._instance.BgmPlay(_soundNum);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundManager._instance.BgmStop();
        }
	}
}

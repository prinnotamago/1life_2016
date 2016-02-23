using UnityEngine;
using System.Collections;

public class SoundTest : MonoBehaviour {

    SoundManager _soundTest;

	// Use this for initialization
	void Start () {
        _soundTest = SoundManager._instance;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _soundTest.SePlay(2);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            _soundTest.SePlay(3);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _soundTest.BgmPlay(0);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            _soundTest.BgmPlay(1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _soundTest.ChangeSePitch(1.0f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _soundTest.ChangeSePitch(0.5f);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _soundTest.ChangeSePitch(1.0f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _soundTest.ChangeSePitch(0.5f);
        }
    }
}

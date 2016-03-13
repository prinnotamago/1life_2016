﻿using UnityEngine;
using System.Collections;

public class CameraReference : MonoBehaviour {

    [SerializeField]
    Camera _cameraObj;
    public Camera _CAMERA_OBJ { get { return _cameraObj; } }

	// Use this for initialization
	void Start () {
        _cameraObj = GameObject.Find(_cameraObj.name).GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

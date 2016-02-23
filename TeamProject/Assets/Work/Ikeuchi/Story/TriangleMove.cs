using UnityEngine;
using System.Collections;

public class TriangleMove : MonoBehaviour {

    [SerializeField]
    RectTransform _moveText;

    float count;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _moveText.position += new Vector3(0.0f, Mathf.Sin(count), 0.0f) / 5.0f;
        count += 0.1f;
    }
}

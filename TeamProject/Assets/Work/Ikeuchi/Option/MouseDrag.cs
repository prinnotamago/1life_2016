using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour {

    [SerializeField]
    float _maxPosX = 6.0f;
    [SerializeField]
    float _minPosX = -2.2f;

    public float _Value { get; set; }

    enum SelectSoundValume
    {
        BGM,
        SE,
    }
    [SerializeField]
    SelectSoundValume _selectSoundValume;

    bool _isSePlay = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_selectSoundValume == SelectSoundValume.BGM) { return; }

        if (_isSePlay)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SoundManager._instance.SePlay(0);
                _isSePlay = false;
            }
        }
    }

    void OnMouseDrag()
    {
        Vector3 objScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x,
                                             Input.mousePosition.y,
                                             objScreenPos.z);

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        if (mouseWorldPos.x > _maxPosX) { mouseWorldPos.x = _maxPosX; }
        if (mouseWorldPos.x < _minPosX) { mouseWorldPos.x = _minPosX; }
        mouseWorldPos.y = transform.position.y;
        mouseWorldPos.z = transform.position.z;
        transform.position = mouseWorldPos;

        float value = mouseWorldPos.x - _minPosX;
        float maxValue = _maxPosX - _minPosX;
        _Value = value / maxValue;
        //Debug.Log(_Value);

        if (SoundManager._instance != null)
        {
            if (_selectSoundValume == SelectSoundValume.BGM)
            {
                SoundManager._instance.ChangeBgmPitch(_Value);
            }
            else
            {
                SoundManager._instance.ChangeSePitch(_Value);
                _isSePlay = true;
            }
        }
    }
}

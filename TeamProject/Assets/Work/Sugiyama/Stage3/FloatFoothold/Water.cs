using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
    [SerializeField]
    public Scale scale;

    private Vector3 _decreasePower = new Vector3(0, 0.002f, 0);
    [SerializeField]
    private float _decreaseLimit = 0;
    [SerializeField]
    private GameObject _flowWaterPr;
    private GameObject _flowWater;
    private bool _createdFlowWater;
    private GameObject _waterDestination;
    private float _gainDelay = 2 * 60;
    private float _count = 0;

    [SerializeField]
    private FloatFoothold _floatFoothold;

    void Start () {
        _createdFlowWater = false;
        _waterDestination = GameObject.Find("WaterDestination");
    }
	
	void Update () {
        if (scale.GetFlowWater()&&transform.localScale.y >= _decreaseLimit){
            transform.localScale -= _decreasePower;
            transform.position -= _decreasePower / 2;

            if (_count >= _gainDelay)
            {
                Vector3 decreasePower = _decreasePower * 2;
                _waterDestination.transform.localScale += decreasePower;
                _waterDestination.transform.position += decreasePower / 2;
                if (_floatFoothold != null) _floatFoothold.SetStartFloat(true);
            }
            else _count++;

            if (!_createdFlowWater)
            {
                GameObject waterChild = transform.FindChild("WaterChild").gameObject;
                Destroy(waterChild);

                _flowWater = Instantiate(_flowWaterPr);
                _createdFlowWater = true;
            }
        }
	}
}

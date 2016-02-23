using UnityEngine;
using System.Collections;

public class PlayerWaterCreater : MonoBehaviour {

    [SerializeField]
    GameObject _jointPointObj = null;

    [SerializeField]
    GameObject _createWaterObj = null;

    [SerializeField]
    GameObject _parentObj = null;

    [SerializeField]
    int _waterNum = 10;

    [SerializeField]
    float _waterPosValX = 1.0f;
    [SerializeField]
    float _waterPosValY = 1.0f;

    // Use this for initialization
    void Start () {
        CreateWater();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateWater()
    {
        if (_jointPointObj == null ||
            _createWaterObj == null ||
            _parentObj == null)
        {
            Debug.Log("ジョイントオブジェクト、水オブジェクト 又は 親オブジェクトが設定されていません。");
            return;
        }
        float pi_2 = Mathf.PI * 2;
        float r_val = pi_2 / _waterNum;
        for (float r = 0; r < pi_2; r += r_val)
        {
            GameObject jointObj = GameObject.Instantiate(_jointPointObj); 
            Vector3 offset = new Vector3(Mathf.Cos(r) * _waterPosValX, Mathf.Sin(r) * _waterPosValY, 0.0f);
            jointObj.transform.position = transform.position + offset;
            jointObj.transform.parent = transform;
            jointObj.GetComponent<PlayerJointMover>()._OFFSET_POS = offset;
            jointObj.GetComponent<PlayerJointMover>()._ANGLE = r;
            //jointObj.GetComponent<PlayerWaterMover>()._PARENT_OBJ = gameObject;
            //jointObj.GetComponent<PlayerWaterMover>()._MOVE_POS_OFFSET = offset;

            GameObject water = GameObject.Instantiate(_createWaterObj);
            water.transform.position += transform.position + offset;
            water.transform.parent = _parentObj.transform;
            water.GetComponent<PlayerWaterMover>()._PLAYER_OBJ = gameObject;
            water.GetComponent<SpringJoint>().connectedBody = jointObj.GetComponent<Rigidbody>();
        }
    }
}

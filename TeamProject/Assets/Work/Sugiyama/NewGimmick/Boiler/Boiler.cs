using UnityEngine;
using System.Collections;

public class Boiler : MonoBehaviour {

    [SerializeField]
    private ParticleSystem _boilerEffect;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Vector3 _effectOffSet;

    [SerializeField]
    private Vector3 _power;

    // Use this for initialization
    void Start () {
        _boilerEffect = Instantiate(_boilerEffect);
        _boilerEffect.transform.position = this.transform.position + _effectOffSet;
        _boilerEffect.Play();

        if (_player == null) Debug.Log("Playerを代入してください");
        if (_boilerEffect == null) Debug.Log("Resorceの中のBoilerを代入してください");
    }
	
	// Update is called once per frame
	void Update () {
        if (_player == null) { return; }
        if (_player.transform.position.x >= (this.transform.position.x - 0.5f) + _effectOffSet.x &&
            _player.transform.position.x <= (this.transform.position.x + 0.5f) + _effectOffSet.x)
        {
            _player.transform.position += _power;
        }
    }
}

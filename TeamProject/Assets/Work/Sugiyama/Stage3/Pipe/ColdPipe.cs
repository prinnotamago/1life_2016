using UnityEngine;
using System.Collections;

public class ColdPipe : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem _coldGas;

    [SerializeField]
    private Player _player;

    private Vector3 _gasOffSet;


    void Start()
    {
        _gasOffSet = new Vector3(-2.2f, -1.2f, 0);

        _coldGas = Instantiate(_coldGas);
        _coldGas.transform.position = this.transform.position + _gasOffSet;
        _coldGas.Play();
    }

    void Update()
    {
        if(_player == null) { return; }
        if(_player.transform.position.x >= (this.transform.position.x - 0.5f) + _gasOffSet.x&&
            _player.transform.position.x <= (this.transform.position.x + 0.5f) + _gasOffSet.x)
        {
            _player._playerMode = Player.PlayerMode.ICE;
            SoundManager._instance.SePlay(8);
        }
    }
}

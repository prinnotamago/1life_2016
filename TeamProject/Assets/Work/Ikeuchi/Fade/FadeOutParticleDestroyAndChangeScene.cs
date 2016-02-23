using UnityEngine;
using System.Collections;

public class FadeOutParticleDestroyAndChangeScene : MonoBehaviour
{
    ParticleSystem _particle;

    private string _nextSceneName = null;
    public string _NEXT_SCENE_NAME { set { _nextSceneName = value; } }

    private const float _TIME_LUG = 0.5f;

    void Start()
    {
        Init();
    }

    void Update()
    {
        Destroy();
    }

    void Init()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    void Destroy()
    {
        if (_particle)
        {
            if (!_particle.IsAlive())
            {
                Destroy(gameObject);  
            }

            if (_nextSceneName != null)
            {
                if (_particle.duration - _TIME_LUG < _particle.time)
                {
                    //Debug.Log(_nextSceneName);
                    Application.LoadLevel(_nextSceneName);
                }
            }
        }
        else
        {
            Debug.Log("これは「ParticleSystem」じゃないです");
        }
    }
}

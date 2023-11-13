using UnityEngine;
using com.super2games.idle.factory;
using System.Collections;

public class BuildingRemovalFX : MonoBehaviour {

    private readonly string PREFAB_PATH = "Prefabs/fx/fx_building_removal_01";
    private ParticleSystem _particleSystem;
    private bool _setToPlay = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_setToPlay && !_particleSystem.IsAlive())
        {
            _particleSystem.Stop();
            gameObject.SetActive(false);
            _setToPlay = false;
            BuildingFactory.prefabManager.returnPrefab(PREFAB_PATH, gameObject);
        }
    }

    public void playParticles()
    {
        gameObject.SetActive(true);
        _setToPlay = true;
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Play();
    }
}

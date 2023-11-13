using UnityEngine;
using System.Collections;
using com.super2games.idle.manager;

public class PlunkoBumper : MonoBehaviour {

    private readonly int LIT_UP_TIME_LIMIT = 15;

    public ParticleSystem particleSparks;

    private int _litUpTicker = 0;

    void Awake()
    {
        particleSparks.Stop();
    }

    void Start () {
	}
	
	void Update () {
        _litUpTicker--;

        if (_litUpTicker == 0)
        {
            SoundManager.instance.playSound(SoundManager.SOUND_PLUNKO_PEG_HIT);
            GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_MATERIAL) as Material;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        particleSparks.Play();
        GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_LIT_UP_MATERIAL) as Material;

        _litUpTicker = LIT_UP_TIME_LIMIT;
    }

    void OnCollisionExit(Collision collision)
    {
        particleSparks.Play();
    }
}

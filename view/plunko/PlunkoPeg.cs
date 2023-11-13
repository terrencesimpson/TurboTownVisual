using UnityEngine;
using System.Collections;
using com.super2games.idle.manager;
using com.super2games.idle.enums;

public class PlunkoPeg : MonoBehaviour {

    private readonly int LIT_UP_TIME_LIMIT = 15;
    private readonly int SOUND_PLAY_LIMIT = 15;

    public ParticleSystem particleSparks;

    private int _litUpTicker = 0;
    private int _soundPlayTicker = 0;

    void Awake()
    {
        particleSparks.Stop();
    }

    void Start () {
	}
	
	void Update () {
        _litUpTicker--;
        _soundPlayTicker--;

        if (_litUpTicker == 0)
        {
            GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_MATERIAL) as Material;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        particleSparks.Play();
        GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_LIT_UP_MATERIAL) as Material;

        _litUpTicker = LIT_UP_TIME_LIMIT;

        //if (collision.collider.tag != TagEnum.CUBIE && _soundPlayTicker < 0)
        //{
        //    SoundManager.instance.playSound(SoundManager.SOUND_PLUNKO_PEG_HIT);
        //    _soundPlayTicker = SOUND_PLAY_LIMIT;
        //}
    }

    void OnCollisionExit(Collision collision)
    {
        particleSparks.Play();
    }
}

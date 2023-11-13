using UnityEngine;
using com.super2games.idle.enums;
using com.super2games.idle.manager;
using System.Collections;

public class PlunkoBucket : MonoBehaviour {

    private readonly string SEQUENCE_NAME = "PrestigeSequence";
    private readonly int LIT_UP_TIME_LIMIT = 25;

    public ParticleSystem particleStars;
    public bool lightUpBucket = false;

    private PrestigeSequence _prestigeSequence;
    private int _litUpTicker = 0;

    void Awake()
    {
        particleStars.Stop();
    }

    void Start () {
        _prestigeSequence = GameObject.Find(SEQUENCE_NAME).GetComponent<PrestigeSequence>();
	}
	
	void Update () {
        _litUpTicker--;

        if (_litUpTicker == 0)
        {
            //GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_MATERIAL) as Material;
            lightUpBucket = false;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != TagEnum.CUBIE)
        {
            particleStars.Play();
            lightUpBucket = true;
        }

        _litUpTicker = LIT_UP_TIME_LIMIT;

        _prestigeSequence.removeObjectFromBucket(collision.gameObject, getMultiplier());
    }

    private void OnCollisionExit(Collision collision)
    {
        particleStars.Play();
    }

    private int getMultiplier()
    {
        if (this.name == "plunko_board_bucket_01" || this.name == "plunko_board_bucket_05")
        {
            return 10;
        }
        else if (this.name == "plunko_board_bucket_02" || this.name == "plunko_board_bucket_04")
        {
            return 5;
        }
        else if (this.name == "plunko_board_bucket_03")
        {
            return 1;
        }

        return 0;
    }

    //private void addToScore()
    //{
    //    if (this.name == "plunko_board_bucket_01" || this.name == "plunko_board_bucket_05")
    //    {
    //        PlunkoBoard.score += 10000;
    //    }
    //    else if (this.name == "plunko_board_bucket_02" || this.name == "plunko_board_bucket_04")
    //    {
    //        PlunkoBoard.score += 5000;
    //    }
    //    else if (this.name == "plunko_board_bucket_03")
    //    {
    //        PlunkoBoard.score += 1000;
    //    }
    //}
}

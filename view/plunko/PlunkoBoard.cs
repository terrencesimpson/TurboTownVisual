using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.manager;
using System.Collections;

public class PlunkoBoard : MonoBehaviour {

    public static readonly string PLUNKO_BOARD_MATERIAL = "Materials/plunko_board";
    public static readonly string PLUNKO_BOARD_LIT_UP_MATERIAL = "Materials/plunko_board_litUp";

    public PlunkoBucket bucket_01;
    public PlunkoBucket bucket_02;
    public PlunkoBucket bucket_03;
    public PlunkoBucket bucket_04;
    public PlunkoBucket bucket_05;

    public GameObject sign_01;
    public GameObject sign_02;
    public GameObject sign_03;
    public GameObject sign_04;
    public GameObject sign_05;

    public TextMesh scoreText;
    public Button tiltBtn;

    public static double score = 0;

    // Use this for initialization
    void Start () {
        tiltBtn.onClick.AddListener(onTiltBtnClick);
    }

    private void onTiltBtnClick()
    {
        if (iTween.Count(gameObject) < 1)
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            iTween.ShakePosition(gameObject, iTween.Hash("x", 20, "y", 20, "z", 20, "time", .3f));
        }
    }
	
	// Update is called once per frame
	void Update () {

        scoreText.text = score.ToString();

	    if (bucket_01.lightUpBucket)
        {
            sign_01.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_LIT_UP_MATERIAL) as Material;
        }
        else
        {
            sign_01.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_MATERIAL) as Material;
        }

        if (bucket_02.lightUpBucket)
        {
            sign_02.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_LIT_UP_MATERIAL) as Material;
        }
        else
        {
            sign_02.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_MATERIAL) as Material;
        }

        if (bucket_03.lightUpBucket)
        {
            sign_03.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_LIT_UP_MATERIAL) as Material;
        }
        else
        {
            sign_03.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_MATERIAL) as Material;
        }

        if (bucket_04.lightUpBucket)
        {
            sign_04.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_LIT_UP_MATERIAL) as Material;
        }
        else
        {
            sign_04.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_MATERIAL) as Material;
        }

        if (bucket_05.lightUpBucket)
        {
            sign_05.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_LIT_UP_MATERIAL) as Material;
        }
        else
        {
            sign_05.GetComponent<Renderer>().material = Resources.Load(PlunkoBoard.PLUNKO_BOARD_MATERIAL) as Material;
        }
    }
}

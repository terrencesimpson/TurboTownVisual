using UnityEngine;
using System.Collections;

public class MrBusinessStatueFX : MonoBehaviour {

    private readonly static string MR_BUSINESS = "mrBusinessScaler/mr_business";

    private readonly static string POSES_01 = "Poses_01";
    private readonly static string POSES_02 = "Poses_02";
    private readonly static string POSES_03 = "Poses_03";
    private readonly static string POSES_04 = "Poses_04";
    private readonly static string POSES_05 = "Poses_05";
    private readonly static string POSES_06 = "Poses_06";
    private readonly static string POSES_07 = "Poses_07";
    private readonly static string POSES_08 = "Poses_08";
    private readonly static string POSES_09 = "Poses_09";
    private readonly static string POSES_10 = "Poses_10";

    private readonly string[] POSES = new string[10] { POSES_01, POSES_02, POSES_03, POSES_04, POSES_05, POSES_06, POSES_07, POSES_08, POSES_09, POSES_10 };

    private GameObject _mrBusiness;
    private Animator _anim;
    private string _currentAnim = POSES_01;

	// Use this for initialization
	void Start () {
        _mrBusiness = transform.Find(MR_BUSINESS).gameObject;
        _anim = _mrBusiness.GetComponent<Animator>();
        _anim.speed = 0f;
    }
	
	// Update is called once per frame
	//void Update () {
	
	//}

    public void changePose()
    {
        //Animator anim = GetComponent
        int randIndex = 0;
        string randAnim = _currentAnim;

        while (randAnim == _currentAnim)
        {
            randIndex = Random.Range(0, POSES.Length);
            randAnim = POSES[randIndex];
        }

        _anim = _mrBusiness.GetComponent<Animator>();
        _anim.Play(randAnim);
        _currentAnim = randAnim;
        //_anim.speed = 0f;
    }
}

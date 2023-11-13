using UnityEngine;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.factory;
using System.Collections;
using System;

public class CameraGizmo : MonoBehaviour 
{

    public GameObject arrowLeft;
    public GameObject arrowRight;
    public GameObject gizmoCube;
    public GameObject topLetter;
    public GameObject cameraTarget;
    public GameObject cameraHolder;
    public GameObject camera;
    public Vector3 previousCameraPosition;
    public Vector3 previousCameraRotation;
    public bool topDownEnabled = false;

    private float _origPositionX = 194f;

    public static CameraGizmo instance;

    void Awake()
    {
        instance = this;
        hideGizmo();

        _origPositionX = this.transform.localPosition.x;
    }

    void Start()
    {
        adjustGizmoToFitDevice();
    }

    void Update()
    {
        //correctAllCubiePopulationText();
    }

    private void adjustGizmoToFitDevice()
    {
        float ratioDif = ScreenUtility.getAspectRatio() / UIManager.ORIG_SCREEN_ASPECT_RATIO;
        float posAdjustment = 12f * (ratioDif-1);

        float newPosX = _origPositionX * ratioDif + posAdjustment;//_origPositionX * ratioDif - 3;
        this.transform.localPosition = new Vector3(newPosX, this.transform.localPosition.y, this.transform.localPosition.z);
    }

    public void leftArrowClicked()
    {
        if (iTween.Count(cameraTarget) > 0 || iTween.Count(cameraHolder) > 0)
        {
            return;
        }

        if (topDownEnabled)
        {
            return;
        }

        SoundManager.instance.playSound(SoundManager.SOUND_CAMERA_TURN);

        iTween.RotateBy(gizmoCube, iTween.Hash("y", .25, "easeType", "easeOutElastic", "time", 1f));
        iTween.RotateBy(cameraTarget, iTween.Hash("y", .25, "easeType", "easeInOutCubic", "time", 1, "oncomplete", "correctCubiePopulationText", "oncompletetarget", gameObject));

        if (iTween.Count(arrowLeft) < 1)
        {
            iTween.ScaleBy(arrowLeft, iTween.Hash("x", 1.5, "y", 1.5, "z", 1.5, "easeType", "easeInOutCubic", "oncomplete", "_returnToOriginalScale", "oncompleteparams", arrowLeft, "oncompletetarget", gameObject, "time", 0.125));
        }
    }

    public void rightArrowClicked()
    {
        if (iTween.Count(cameraTarget) > 0 || iTween.Count(cameraHolder) > 0)
        {
            return;
        }

        if (topDownEnabled)
        {
            return;
        }

        SoundManager.instance.playSound(SoundManager.SOUND_CAMERA_TURN);

        iTween.RotateBy(gizmoCube, iTween.Hash("y", -.25, "easeType", "easeOutElastic", "time", 1f));
        iTween.RotateBy(cameraTarget, iTween.Hash("y", -.25, "easeType", "easeInOutCubic", "time", 1, "oncomplete", "correctAllCubiePopulationText", "oncompletetarget", gameObject));

        if (iTween.Count(arrowRight) < 1)
        {
            iTween.ScaleBy(arrowRight, iTween.Hash("x", 1.5, "y", 1.5, "z", 1.5, "easeType", "easeInOutCubic", "oncomplete", "_returnToOriginalScale", "oncompleteparams", arrowRight, "oncompletetarget", gameObject, "time", 0.125));
        }
    }

    public void topClicked()
    {
        if (iTween.Count(cameraTarget) > 0 || iTween.Count(cameraHolder) > 0)
        {
            return;
        }

        SoundManager.instance.playSound(SoundManager.SOUND_CAMERA_TURN);

        if (topDownEnabled)
        {
            iTween.RotateTo(this.gameObject, iTween.Hash("x", 45, "easeType", "easeInOutCubic", "time", 1));

            //iTween.RotateTo(cameraHolder, iTween.Hash("x", 45, "y", 0, "z", 0, "easeType", "easeInOutCubic", "time", 1, "isLocal", true));
            iTween.MoveTo(cameraHolder, iTween.Hash("x", previousCameraPosition.x, "y", previousCameraPosition.y, "z", previousCameraPosition.z, "easeType", "easeInOutCubic", "time", 1, "isLocal", true));

            iTween.RotateTo(cameraTarget, iTween.Hash("y", 45, "easeType", "easeInOutCubic", "time", 1));
            iTween.RotateTo(cameraHolder, iTween.Hash("x", 45, "y", 0, "z", 0, "easeType", "easeInOutCubic", "time", 1, "isLocal", true));
            topDownEnabled = false;
        }
        else
        {
            iTween.RotateTo(this.gameObject, iTween.Hash("x", 90, "easeType", "easeInOutCubic", "time", 1));

            //the mouse control script is moving the camera and not the holder
            previousCameraRotation = new Vector3(cameraHolder.transform.localRotation.x, cameraHolder.transform.localRotation.y, cameraHolder.transform.localRotation.z);
            previousCameraPosition = new Vector3(cameraHolder.transform.localPosition.x, cameraHolder.transform.localPosition.y, cameraHolder.transform.localPosition.z);
            iTween.MoveTo(cameraHolder, iTween.Hash("x", -.01, "y", cameraHolder.transform.localPosition.y - 20f, "z", -.01, "easeType", "easeInOutCubic", "time", 1, "isLocal", true));
            //iTween.RotateTo(cameraHolder, iTween.Hash("x", 90, "z", 45, "easeType", "easeInOutCubic", "time", 1));

            iTween.RotateTo(cameraTarget, iTween.Hash("x", 0, "y", 45, "z", 0, "easeType", "easeInOutCubic", "time", 1, "isLocal", true));
            iTween.RotateTo(cameraHolder, iTween.Hash("x", 90, "y", 315, "z", 0, "easeType", "easeInOutCubic", "time", 1, "isLocal", true));

            topDownEnabled = true;
        }

        iTween.ScaleBy(topLetter, iTween.Hash("x", 1.5, "y", 1.5, "z", 1.5, "easeType", "easeInOutCubic", "oncomplete", "_returnToOriginalScale", "oncompleteparams", topLetter, "oncompletetarget", gameObject, "time", 0.125));
    }

    public void moveCamTargetToPosition(Vector3 pos)
    {
        if (iTween.Count(cameraTarget) > 0 || iTween.Count(cameraHolder) > 0)
        {
            return;
        }

        iTween.MoveTo(cameraTarget, iTween.Hash("x", pos.x, "y", cameraTarget.transform.position.y, "z", pos.z, "easeType", "easeInOutCubic", "time", 1, "isLocal", false));
    }

    public void hideGizmo()
    {
        gameObject.SetActive(false);
    }

    public void showGizmo()
    {
        gameObject.SetActive(true);
    }

    public void correctAllCubiePopulationText()
    {
        //bool isFacingBack = isCameraFacingBack();
        //if (isFacingBack)
        //{
        //    JobFactory.cubieManager.flipAllCubiePopulationText();
        //}

        //if (camera.activeSelf && camera.GetComponent<Camera>().isActiveAndEnabled)
        //{
        //    JobFactory.cubieManager.makeAllCubiePopTextFaceCamera();
        //}
    }

    //public bool isCameraFacingBack()
    //{
    //    bool facingBack = false;
    //    double targetYRot = Math.Floor(cameraTarget.transform.localEulerAngles.y);
    //    Debug.Log("targetYRot: "+ targetYRot);

    //    if (targetYRot >= 135 && targetYRot <= 225)
    //    {
    //        Debug.Log("IS FACING BACK");
    //        facingBack = true;
    //    }

    //    return facingBack;
    //}

    private void _returnToOriginalScale(GameObject go)
    {
        iTween.ScaleTo(go, iTween.Hash("x", 1, "y", 1, "z", 1, "easeType", "easeInOutCubic", "time", 1));
        //Debug.Log("rebound");
    }
}

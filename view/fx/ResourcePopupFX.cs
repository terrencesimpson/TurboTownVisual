using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.super2games.idle.ui;
using com.super2games.idle.manager;

public class ResourcePopupFX : MonoBehaviour {

    private readonly string FX_RESOURCE_POPUP = "Prefabs/fx/fx_resource_popup";

    private readonly string RESOURCE_ICON = "canvas/ResourceIcon";
    private readonly string RESOURCE_TEXT = "canvas/text";
    private readonly float TWEEN_TIME = 1f;

    private Camera _mainCamera;
    private ResourceIconUI _icon;
    private Text _text;
    private PrefabManager _prefabManager;

    //private bool _hasStarted = false;

    // Use this for initialization
    void Awake () {
        _mainCamera = Camera.main;
        _icon = transform.Find(RESOURCE_ICON).GetComponent<ResourceIconUI>();
        _text = transform.Find(RESOURCE_TEXT).GetComponent<Text>();
	}

    public void init(string id, string value, Vector3 pos, PrefabManager prefabManager, float scaleFactor=1.5f)
    {
        gameObject.SetActive(true);
        _icon.setIcon(id);
        _text.text = value;
        _prefabManager = prefabManager;
        //_hasStarted = true;
        transform.position = new Vector3(pos.x, pos.y, pos.z);

        //iTween.MoveBy(this, iTween.Hash("y", 100, "duration", 3f))
        iTween.MoveBy(gameObject, iTween.Hash("y", 40f, "islocal", true, "time", TWEEN_TIME, "easetype", iTween.EaseType.linear, "onComplete", "onTweenComplete", "oncompletetarget", gameObject));
        iTween.ScaleTo(gameObject, iTween.Hash("x", scaleFactor, "y", scaleFactor, "islocal", true, "time", .15f, "easetype", iTween.EaseType.linear, "onComplete", "onScaleTweenComplete", "oncompletetarget", gameObject));

        //iTween.ValueTo(gameObject, iTween.Hash(
        //            "delay", .5f,
        //            "from", 1f,
        //            "to", 0f,
        //            "time", TWEEN_TIME-.5f,
        //            "onupdatetarget", gameObject,
        //            "onupdate", "popupAlphaCallback",
        //            //"oncomplete", "animateTitle",
        //            "easetype", iTween.EaseType.linear
        //            ));
    }

    // Update is called once per frame
    void Update()
    {
        //if (_hasStarted)
        //{
        //    transform.position
        //}

        transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
        _mainCamera.transform.rotation * Vector3.up);
    }

    private void popupAlphaCallback(float newValue)
    {
        GetComponentInChildren<CanvasGroup>().alpha = newValue;
    }

    private void onScaleTweenComplete()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1, "islocal", true, "time", .15f, "easetype", iTween.EaseType.linear));
    }

    private void onTweenComplete()
    {
        iTween.Stop(gameObject);
        gameObject.SetActive(false);
        _prefabManager.returnPrefab(FX_RESOURCE_POPUP, this.gameObject);
    }
}

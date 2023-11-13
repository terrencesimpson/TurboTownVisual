using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.ui;
using com.super2games.idle.manager;
using com.super2games.idle.component.possessor;
using com.super2games.idle.config;
using com.super2games.idle.component.view.building;
using com.super2games.idle.component.boosts.building;
using com.super2games.idle.enums;
using com.super2games.idle.component.task;
using System.Collections;

public class BuildingEnhancementOverlayFX : MonoBehaviour {

    public static readonly string FX_ENHANCEMENT_OVERLAY = "Prefabs/fx/fx_building_enhancementOverlay";
    public static readonly string FX_ENHANCEMENT_OVERLAY_NAME = "fx_building_enhancementOverlay(Clone)";

    private readonly string ENHANCEMENT_PERCENTAGE_TEXT = "canvas/enhancementPercentage/text";

    private Camera _mainCamera;
    private Building _building;
    private BuildingViewComponent _buildingView;
    private BuildingBoosterComponent _buildingBoosterComp;

    private Text _enhancementText;

    // Use this for initialization
    void Awake () {
        _mainCamera = Camera.main;
        _enhancementText = transform.Find(ENHANCEMENT_PERCENTAGE_TEXT).GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void init(PrefabManager prefabManager, Building building, BuildingViewComponent buildingView)
    {
        _building = building;
        _buildingView = buildingView;

        Mesh mesh = _buildingView.view.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;

        transform.SetParent(_buildingView.view.transform, false);
        transform.localPosition = new Vector3(0, (_buildingView.view.transform.position.y + bounds.size.y + 3), 0);
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
        }
    }

    public void show()
    {
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void updateText(string newText)
    {
        _enhancementText.text = newText;
    }
}

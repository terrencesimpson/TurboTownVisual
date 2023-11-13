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

public class BuildingOverlayFX : MonoBehaviour {

    public static readonly string FX_BUILDING_OVERLAY = "Prefabs/fx/fx_building_overlay";

    private readonly string POP_COUNT_TEXT = "canvas/populationCount/text";
    private readonly string LEVEL_COUNT_TEXT = "canvas/levelCount/text";
    private readonly string ITEM_COUNT_TEXT = "canvas/itemCount/text";
    private readonly string JOBS_COUNT_TEXT = "canvas/jobsCount/text";
    private readonly string ENHANCEMENT_TEXT = "canvas/enhancementText/text";

    private Camera _mainCamera;
    private Building _building;
    private BuildingViewComponent _buildingView;
    private BuildingSlotsComponent _buildingSlots;
    private JobAccessorComponent _buildingJobs;
    private BuildingBoosterComponent _buildingBoosterComp;
    //private BuildingsConfig _buildingConfig;

    private Text _populationText;
    private Text _levelText;
    private Text _itemText;
    private Text _jobsText;
    private Text _enhancementText;

    // Use this for initialization
    void Awake () {
        _mainCamera = Camera.main;

        _populationText = transform.Find(POP_COUNT_TEXT).GetComponent<Text>();
        _levelText = transform.Find(LEVEL_COUNT_TEXT).GetComponent<Text>();
        _itemText = transform.Find(ITEM_COUNT_TEXT).GetComponent<Text>();
        _jobsText = transform.Find(JOBS_COUNT_TEXT).GetComponent<Text>();
        _enhancementText = transform.Find(ENHANCEMENT_TEXT).GetComponent<Text>();
        _enhancementText.gameObject.SetActive(false);
    }

    public void init(PrefabManager prefabManager, Building building, BuildingViewComponent buildingView)
    {
        _building = building;
        _buildingView = buildingView;
        _buildingSlots = _building.findComponent(ComponentIDEnum.BUILDING_SLOTS) as BuildingSlotsComponent;
        _buildingJobs = _building.findComponent(ComponentIDEnum.JOB_ACCESSOR) as JobAccessorComponent;

        Mesh mesh = _buildingView.view.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;

        transform.SetParent(_buildingView.view.transform, false);
        transform.localPosition = new Vector3(bounds.size.x/2, (_buildingView.view.transform.position.y + bounds.size.y), bounds.size.z/2);

        if (_building.findComponent(ComponentIDEnum.BUILDING_BOOSTER) != null)
        {
            _buildingBoosterComp = _building.findComponent(ComponentIDEnum.BUILDING_BOOSTER) as BuildingBoosterComponent;
            _enhancementText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
        _mainCamera.transform.rotation * Vector3.up);

        updateText();
    }

    public void updateText()
    {
        //_populationText.text = _building.population + "/" + _building.populationCap;
        //_levelText.text = "LEVEL: " + _building.level;
        //_itemText.text = "ITEM: " + _buildingSlots.numOfSlots + "/" + _buildingSlots.maxSlots;
        //_jobsText.text = "JOBS: " + _buildingJobs.getNumJobsRunning() + "/" + _buildingJobs.getNumJobs();

        //if (_buildingBoosterComp != null)
        //{
        //    _enhancementText.text = "ENHC: " + _buildingBoosterComp.getBoostAmount() + "/" + _buildingBoosterComp.boostCap;
        //}
    }
}

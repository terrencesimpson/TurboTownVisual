using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using com.super2games.idle.manager;
using com.super2games.idle.component.possessor;
using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.component.view.building;
using com.super2games.idle.utilities;
using com.super2games.idle.delegates.building;

public class PrestigeSequence : MonoBehaviour
{
    public readonly static string TYPE_PLUNKO = "plunko";
    public readonly static int BUILDING_Y_THRESHOLD = -3000;
    public readonly static float TIME_BEFORE_FORCE_END = 180f;

    private readonly static float SECOND_CAM_MOVEMENT_TIME = 7f;
    private readonly static string PLUNKO_FLASH_MOTION_NAME = "plunkoFlash";
    private readonly static bool USE_SKYBOX_DARKEN = false;

    private readonly static string MR_BUSINESS_TIP_MAP = "TipMapOver";
    private readonly static string MR_BUSINESS_IDLE = "Idle";

    [HideInInspector]
    public bool running = false;

    public Camera prestigeCamera;
    public GameObject cameraTarget;
    public GameObject cameraHolder;
    public GameObject cameraFlash;

    public GameObject plunkoBoard;
    public GameObject prestigeCamNode01;
    public GameObject mrBusiness;

    private Camera _mainCamera;
    private string _prestigeType = "";
    private Vector3 _originalTargetPos;
    private Vector3 _originalHolderPos;
    private Vector3 _origGrassPosition;
    private GameObject _prestigeBuilding;
    private PrestigeManager _prestigeManager;
    private BuildingManager _buildingManager;
    private GridManager _gridManager;
    private UIManager _uiManager;
    private List<Building> _buildingList;
    private float _timeTillForceEnd = 3000f;
    private float _camTrackNodeMovementTime = 0f;
    private bool _moveHolderTowardsNode01 = false;
    private Color _skyboxGroundColorOrig;
    private Color _skyboxGroundColor2ndPhase = new Color(.117f, .003f, .0015f, 1f);

    void Awake()
    {
        _timeTillForceEnd = TIME_BEFORE_FORCE_END;
    }

    void Start()
    {
        _originalTargetPos = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, cameraTarget.transform.position.z);
        _originalHolderPos = new Vector3(cameraHolder.transform.position.x, cameraHolder.transform.position.y, cameraHolder.transform.position.z);
        _mainCamera = Camera.main;

        if (USE_SKYBOX_DARKEN)
        {
            _skyboxGroundColorOrig = RenderSettings.skybox.GetColor("_GroundColor");
        }
    }

    void OnApplicationQuit()
    {
        if (USE_SKYBOX_DARKEN)
        {
            RenderSettings.skybox.SetFloat("_AtmosphereThickness", 1f);
            RenderSettings.skybox.SetColor("_GroundColor", _skyboxGroundColorOrig);
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            AmbientMapFXManager.instance.forceSkyboxToDefault();
        }
    }

    void Update()
    {
        if (!running)
        {
            return;
        }

        prestigeCamera.transform.LookAt(cameraTarget.transform);

        if (_moveHolderTowardsNode01)
        {
            iTween.MoveUpdate(cameraHolder, prestigeCamNode01.transform.position, _camTrackNodeMovementTime);
            _camTrackNodeMovementTime -= Time.deltaTime;
        }

        if (_prestigeType == TYPE_PLUNKO)
        {
            destroyIfBelowThreshold(BUILDING_Y_THRESHOLD);

            if (_buildingList.Count < 1)
            {
                endSequence();
                return;
            }
        }

        _timeTillForceEnd -= Time.deltaTime;
        if (_timeTillForceEnd < 0)
        {
            endSequence();
            return;
        }
    }

    public void pause()
    {
        running = false;
    }

    public void unpause()
    {
        running = true;
    }

    public void removeObjectFromBucket(GameObject go, int multiplier)
    {
        Building building = BuildingFunctionalityDelegate.getBuildingByGameObject(go);

        if (building != null)
        {
            SoundManager.instance.playSound(SoundManager.SOUND_PLUNKO_BUCKET_HIT);

            //Calculate score
            double score = Math.Floor(BuildingUtilityDelegate.findPrestigeValueOfBuilding(building) * multiplier);
            PlunkoBoard.score += (int)score;

            //Destroy
            _buildingList.Remove(building);
            _buildingManager.remove(building);
            return;
        }
    }

    public void destroyIfBelowThreshold(int yThreshold)
    {
        for (int i = 0; i < _buildingList.Count; ++i)
        {
            Building building = _buildingList[i] as Building;
            BuildingViewComponent buildingView = (building.findComponent(ComponentIDEnum.BUILDING_VIEW) as BuildingViewComponent);
            if (buildingView != null && buildingView.view != null)
            {
                if (buildingView.view.transform.position.y < yThreshold)
                {
                    _buildingList.Remove(building);
                    _buildingManager.remove(building);
                }
            }
        }
    }

    public void beginSequence(string type, PrestigeManager prestigeManager, BuildingManager buildingManager, GridManager gridManager, UIManager uiManager, Building prestigeBuilding)
    {
        _prestigeType = type;
        _prestigeManager = prestigeManager;
        _buildingManager = buildingManager;
        _gridManager = gridManager;
        _uiManager = uiManager;

        _timeTillForceEnd = TIME_BEFORE_FORCE_END;

        BuildingViewComponent buildingView = prestigeBuilding.findComponent(ComponentIDEnum.BUILDING_VIEW) as BuildingViewComponent;
        _prestigeBuilding = buildingView.view;

        cameraTarget.transform.position = new Vector3(_prestigeBuilding.transform.position.x, _prestigeBuilding.transform.position.y, _prestigeBuilding.transform.position.z+25);
        _origGrassPosition = new Vector3(_gridManager.ground.transform.position.x, _gridManager.ground.transform.position.y, _gridManager.ground.transform.position.z);

        _gridManager.setPrestigeBoxCollider();

        _mainCamera.enabled = false;
        prestigeCamera.enabled = true;

        _buildingList = BuildingRidgidBodiesDelegate.getBuildingsWithRigidBodyList(_buildingManager);
        adjustBuildingColliders();

        BuildingRidgidBodiesDelegate.parentAllNonRigidBodiesToGround(_buildingManager, _gridManager);

        plunkoBoard.SetActive(true);

        //Rotate ground
        iTween.RotateTo(_gridManager.ground, iTween.Hash("x", -90, "delay", 5, "time", 17, "easetype", "linear", "oncompletetarget", gameObject));//time 18
        //iTween.MoveBy(_gridManager.ground, iTween.Hash("y", -4000, "delay", 14, "time", 8, "easetype", "linear", "islocal", false));
        iTween.MoveBy(_gridManager.ground, iTween.Hash("y", -3000, "z", -4000, "delay", 14, "time", 8, "easetype", "linear", "islocal", true));

        //Start cam movement
        cameraFlashTransition();
        camMovement1();

        Animator anim = buildingView.view.GetComponent<Animator>();
        anim.enabled = true;
        anim.Play(PLUNKO_FLASH_MOTION_NAME);

        mrBusiness.SetActive(true);
        mrBusiness.GetComponent<Animator>().Play(MR_BUSINESS_TIP_MAP);

        SoundManager.instance.playSpecialMusicClip(SoundManager.MUSIC_PLUNKO);
    }

    private void cameraFlashTransition()
    {
        cameraFlash.SetActive(true);
        cameraFlash.GetComponent<CanvasRenderer>().SetAlpha(0);

        iTween.ValueTo(gameObject, iTween.Hash(
                "from", 0f,
                "to", 1f,
                "time", .1f,
                "onupdatetarget", gameObject,
                "onupdate", "camFlashTweenOnUpdateCallBack",
                "oncomplete", "camFlashTweenFadeOut",
                "easetype", iTween.EaseType.linear
                ));
    }

    private void camFlashTweenFadeOut()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
                "from", 1f,
                "to", 0f,
                "time", .1f,
                "onupdatetarget", gameObject,
                "onupdate", "camFlashTweenOnUpdateCallBack",
                "oncomplete", "hideCamFlash",
                "easetype", iTween.EaseType.linear
                ));
    }

    private void camFlashTweenOnUpdateCallBack(float newValue)
    {
        cameraFlash.GetComponent<CanvasRenderer>().SetAlpha(newValue);
    }

    private void skyboxTweenOnUpdateCallBack(float newValue)
    {
        RenderSettings.skybox.SetFloat("_AtmosphereThickness", newValue);
        DynamicGI.UpdateEnvironment();
    }

    private void hideCamFlash()
    {
        cameraFlash.SetActive(false);
    }

    private void adjustBuildingColliders()
    {
        for (int i = 0; i < _buildingList.Count; ++i)
        {
            Building building = _buildingList[i] as Building;
            BuildingViewComponent buildingView = (building.findComponent(ComponentIDEnum.BUILDING_VIEW) as BuildingViewComponent);
            if (buildingView != null && buildingView.view != null)
            {
                BoxCollider collider = buildingView.view.GetComponent<BoxCollider>();
                Mesh mesh = buildingView.view.GetComponent<MeshFilter>().mesh;
                Bounds bounds = mesh.bounds;

                float boundsX = collider.size.x * .8f;
                float boundsY = mesh.bounds.size.y * .8f;
                float boundsZ = collider.size.z * .8f;

                int maxDimensionTotal = 105;
                float currentDimensionTotal = boundsX + boundsY + boundsZ;

                if (currentDimensionTotal > maxDimensionTotal)
                {
                    float reductionPercentage = maxDimensionTotal / currentDimensionTotal;

                    if (boundsX > boundsY && boundsX > boundsZ)
                    {
                        boundsY = boundsY * reductionPercentage;
                        boundsZ = boundsZ * reductionPercentage;
                    }
                    else if (boundsY > boundsX && boundsX > boundsZ)
                    {
                        boundsX = boundsX * reductionPercentage;
                        boundsZ = boundsZ * reductionPercentage;
                    }
                    else if (boundsZ > boundsX && boundsX > boundsY)
                    {
                        boundsX = boundsX * reductionPercentage;
                        boundsY = boundsY * reductionPercentage;
                    }
                    else
                    {
                        boundsX = boundsX * reductionPercentage;
                        boundsY = boundsY * reductionPercentage;
                        boundsZ = boundsZ * reductionPercentage;
                    }
                }

                collider.size = new Vector3(boundsX, boundsY, boundsZ);
                collider.center = new Vector3(collider.center.x, boundsY / 2, collider.center.z);
            }
        }
    }

    private void camMovement1()
    {
        ConsoleUtility.Log("[Prestige Sequence] Camera Movement 1");
        //iTween.MoveBy(cameraHolder, iTween.Hash("z", -120, "islocal", true, "delay", 2, "time", 2, "easetype", "linear", "oncomplete", "camMovement2", "oncompletetarget", gameObject));
        iTween.MoveBy(cameraHolder, iTween.Hash("y", -120, "delay", 2, "time", 2, "easetype", "linear", "oncomplete", "camMovement2", "oncompletetarget", gameObject));

        if (USE_SKYBOX_DARKEN)
        {
            RenderSettings.skybox.SetColor("_GroundColor", _skyboxGroundColor2ndPhase);
            iTween.ValueTo(gameObject, iTween.Hash(
                    "from", 1f,
                    "to", 4f,
                    "time", 5f,
                    "onupdatetarget", gameObject,
                    "onupdate", "skyboxTweenOnUpdateCallBack",
                    "easetype", iTween.EaseType.linear
                    ));
        }
    }

    private void camMovement2()
    {
        ConsoleUtility.Log("[Prestige Sequence] Camera Movement 2");

        BuildingRidgidBodiesDelegate.enableRigidBodies(_buildingManager);

        //Find the difference in the plunko building's position and the size of the grass plane.
        //This is needed because the camera needs to drop more the closer to the board the plunko building is
        float camDropY = -250;
        float difZ = Math.Abs(_prestigeBuilding.transform.position.z / GridManager.GRASS_SIZE);
        float camDropPercent = 1 - difZ;
        camDropY = camDropY * camDropPercent;

        //ConsoleUtility.Log("[Prestige Sequence] Camera Drop Y"+ camDropY);

        iTween.MoveTo(cameraTarget, iTween.Hash("x", plunkoBoard.transform.position.x, "y", camDropY, "time", SECOND_CAM_MOVEMENT_TIME, "easetype", "linear", "oncomplete", "camMovement3", "oncompletetarget", gameObject));
        _moveHolderTowardsNode01 = true;
        _camTrackNodeMovementTime = SECOND_CAM_MOVEMENT_TIME;

        //cameraHolder.transform.position = prestigeCamNode01.transform.position;

        //iTween.MoveTo(cameraHolder, iTween.Hash("x", 0, "y", 0, "z", -16, "islocal", true, "time", 8));
    }

    private void camMovement3()
    {
        _moveHolderTowardsNode01 = false;
        ConsoleUtility.Log("[Prestige Sequence] Camera Movement 3");
        iTween.MoveTo(cameraTarget, iTween.Hash("x", plunkoBoard.transform.position.x, "z", plunkoBoard.transform.position.z - 250, "y", -1045, "time", 5, "easetype", "linear"));
        iTween.MoveTo(cameraHolder, iTween.Hash("x", 0, "y", 0, "z", -10, "islocal", true, "time", 8));

        _uiManager.showPrestigeControlsPanel();
    }

    public void revertCamerasAndGround()
    {
        _gridManager.ground.transform.rotation = Quaternion.identity;
        _gridManager.ground.transform.position = new Vector3(_origGrassPosition.x, _origGrassPosition.y, _origGrassPosition.z);
        iTween.Stop(_gridManager.ground);

        _mainCamera.enabled = true;
        prestigeCamera.enabled = false;

        resetCameraPosition();

        mrBusiness.SetActive(false);
        mrBusiness.GetComponent<Animator>().Play(MR_BUSINESS_IDLE);

        if (USE_SKYBOX_DARKEN)
        {
            RenderSettings.skybox.SetFloat("_AtmosphereThickness", 1f);
            RenderSettings.skybox.SetColor("_GroundColor", _skyboxGroundColorOrig);
            DynamicGI.UpdateEnvironment();
        }
    }

    public void resetCameraPosition()
    {
        cameraTarget.transform.position = new Vector3(_originalTargetPos.x, _originalTargetPos.y, _originalTargetPos.z);
        cameraHolder.transform.position = new Vector3(_originalHolderPos.x, _originalHolderPos.y, _originalHolderPos.z);
    }

    private void endSequence()
    {
        _prestigeManager.showPrestigePanel();
        pause();
    }
}

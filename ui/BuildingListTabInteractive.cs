using UnityEngine;
using com.super2games.idle.ui;
using com.super2games.idle.manager;
using UnityEngine.UI;
using System.Collections;
using com.super2games.idle.enums;

public class BuildingListTabInteractive : MonoBehaviour {

    private readonly string SCROLL_VIEW = "Scroll View";

    [HideInInspector]
    public GameObject parentPanel;
    public GameObject listScrollView;

    private BuildingListUI _buildingListUI;
    public UIManager uiManager;

    public string tabName = "";

    public bool enabled = true;

    void Start()
    {
        findScrollViewIfNull();
    }

    public void findScrollViewIfNull()
    {
        if (listScrollView == null)
        {
            listScrollView = parentPanel.transform.Find(SCROLL_VIEW).gameObject;
        }
    }

    public void selectTab()
    {
        if (!enabled) return;

        if (_buildingListUI == null) 
        {
            _buildingListUI = uiManager.GetComponent<UIManager>().buildingList;
        }

        _buildingListUI.clickOnTab(this);
        SoundManager.instance.playSound(SoundManager.SOUND_TAB_SWITCH);
    }
}

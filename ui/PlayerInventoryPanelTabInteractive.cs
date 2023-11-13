using UnityEngine;
using com.super2games.idle.ui;
using com.super2games.idle.manager;
using UnityEngine.UI;
using System.Collections;

public class PlayerInventoryPanelTabInteractive : MonoBehaviour 
{

    public GameObject parentPanel;

    private PlayerInventoryPanelsUI _playerInventoryPanelsUI;
    public UIManager uiManager;

    public string tabName;

    public void selectTab()
    {
        if (_playerInventoryPanelsUI == null)
        {
            _playerInventoryPanelsUI = uiManager.playerInventoryPanelsUI;
        }
        
        _playerInventoryPanelsUI.clickOnTab(this);
        SoundManager.instance.playSound(SoundManager.SOUND_TAB_SWITCH);
    }
}

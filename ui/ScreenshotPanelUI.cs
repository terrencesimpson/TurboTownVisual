using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.super2games.idle.manager;

namespace com.super2games.idle.ui
{
    public class ScreenshotPanelUI
    {
        private readonly string SCREENSHOT_BTN = "screenBtn";

        public GameObject screenshotPanel;
        private GameObject _screenshotButton;
        private UIManager _uiManager;

        public ScreenshotPanelUI(GameObject panel, UIManager uiManager)
        {
            screenshotPanel = panel;
            _screenshotButton = screenshotPanel.transform.Find(SCREENSHOT_BTN).gameObject;
            _screenshotButton.GetComponent<Button>().onClick.AddListener(screenshotButtonOnClick);

            _uiManager = uiManager;
        }

        private void screenshotButtonOnClick()
        {
            //Application.CaptureScreenshot("Screenshot.png");
            //gameObject.GetComponent<ScreenCapture>().SaveScreenshot(CaptureMethod.ReadPixels_Synch, "TurboTownScreenshot");
            screenshotPanel.GetComponent<ScreenshotUtility>().SaveScreen();
            Debug.Log("screen click");
        }
    }
}

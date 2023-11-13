using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class MusicWarningPanelUI
    {
        private readonly string OK_BUTTON = "panel/okButton";

        private Button _okButton;

        private UIManager _uiManager;

        public MusicWarningPanelUI(GameObject panel, UIManager uiManager)
        {
            _uiManager = uiManager;

            _okButton = panel.transform.Find(OK_BUTTON).gameObject.GetComponent<Button>();

            _okButton.onClick.AddListener(onOkClick);
        }

        private void onOkClick()
        {
            _uiManager.hideMusicWarningPanel();
        }

    }
}

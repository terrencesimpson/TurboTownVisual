using com.super2games.idle.goals;
using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class TipBoxUI
    {
        private readonly string DESCRIPTION_TEXT = "descriptionText";
        private readonly string IMAGE_1 = "imageContainer/image1";
        private readonly string IMAGE_2 = "imageContainer/image2";
        private readonly string NEXT_BUTTON = "pageBtnsContainer/nextButton";
        private readonly string PREV_BUTTON = "pageBtnsContainer/prevButton";
        private readonly string CLOSE_BUTTON = "exitBtnHotspot";
        private readonly string PAGE_TEXT = "pageBtnsContainer/pageText";

        private Text _descriptionText;
        private Text _pageText;
        private TipBoxIcon _icon1;
        private TipBoxIcon _icon2;
        private Button _nextButton;
        private Button _prevButton;
        private Button _closeButton;

        private GameObject _tipBoxPanel;
        private TipBoxManager _tipBoxManager;
        private UIManager _uiManager;
        private SettingsManager _settingsManager;

        //private List<Goal> _completedGoals;
        private List<Goal> _readyToShowGoals;

        private int _index = 0;

        public TipBoxUI(GameObject panel, TipBoxManager tipBoxManager, SettingsManager settingsManager, UIManager uiManager)
        {
            _tipBoxPanel = panel;
            _tipBoxManager = tipBoxManager;
            _uiManager = uiManager;
            _settingsManager = settingsManager;

            _descriptionText = _tipBoxPanel.transform.Find(DESCRIPTION_TEXT).gameObject.GetComponent<Text>();
            _pageText = _tipBoxPanel.transform.Find(PAGE_TEXT).gameObject.GetComponent<Text>();
            _icon1 = _tipBoxPanel.transform.Find(IMAGE_1).gameObject.GetComponent<TipBoxIcon>();
            _icon2 = _tipBoxPanel.transform.Find(IMAGE_2).gameObject.GetComponent<TipBoxIcon>();
            _nextButton = _tipBoxPanel.transform.Find(NEXT_BUTTON).gameObject.GetComponent<Button>();
            _prevButton = _tipBoxPanel.transform.Find(PREV_BUTTON).gameObject.GetComponent<Button>();
            _closeButton = _tipBoxPanel.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();

            _tipBoxManager.onShowTipEvent += onShowTip;

            _nextButton.onClick.AddListener(onNextClick);
            _prevButton.onClick.AddListener(onPrevClick);
            _closeButton.onClick.AddListener(onCloseClick);
        }

        public void postInitialize()
        {
            _readyToShowGoals = _tipBoxManager.readyToShowGoals;
        }

        public void showLastReadyTip(bool forceShow = false)
        {
            //force show ignores the settings. This is needed for when you click the tips button in settings but you have the box set to hidden
            if (_settingsManager.tipBoxEnabled || forceShow)
            {
                if (_readyToShowGoals.Count > 0)
                {
                    _uiManager.showTipBoxPanel();
                    _index = (_readyToShowGoals.Count - 1);
                    showTip(_readyToShowGoals[_index]);
                }
            }
        }

        private void onShowTip(Goal goal)
        {
            _index = (_readyToShowGoals.Count - 1); //Setting to the end of the list, where this goal is.
            if (_settingsManager.tipBoxEnabled && !_uiManager.isAnyPanelOpen())
            {
                showTip(goal);
            }
        }

        private void showTip(Goal goal)
        {
            _uiManager.showTipBoxPanel();
            _descriptionText.text = goal.config.description;

            _icon1.setIcon("", goal.config.imgPath1, 0, false, true);
            _icon2.setIcon("", goal.config.imgPath2, 0, false, true);

            if (goal.config.imgPath1 == "")
            {
                _icon1.gameObject.SetActive(false);
            }
            else
            {
                _icon1.gameObject.SetActive(true);
            }

            if (goal.config.imgPath2 == "")
            {
                _icon2.gameObject.SetActive(false);
            }
            else
            {
                _icon2.gameObject.SetActive(true);
            }

            updatePageText();
        }

        private void updatePageText()
        {
            string currentPageText = (_index + 1).ToString();
            string lastPageText = _readyToShowGoals.Count.ToString();
            _pageText.text = _index+1 + " / " + _readyToShowGoals.Count;
        }

        private void onNextClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            modifyIndex(1);
            showTip(_readyToShowGoals[_index]);
        }

        private void onPrevClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            modifyIndex(-1);
            showTip(_readyToShowGoals[_index]);
        }

        private void onCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideTipBoxPanel();
        }

        private void modifyIndex(int modifier)
        {
            _index += modifier;

            if (_index < 0)
            {
                _index = (_readyToShowGoals.Count - 1);
            }
            else if (_index >= _readyToShowGoals.Count)
            {
                _index = 0;
            }

            updatePageText();
        }
    }
}

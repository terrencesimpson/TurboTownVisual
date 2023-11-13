using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using com.super2games.idle.manager;
using UnityEngine.UI;
using com.super2games.idle.utilities;

namespace com.super2games.idle.ui
{
    public class SavePanelUI
    {
        private readonly string SAVE_TEXT_BASE = "Saving";
        private readonly Color ENABLED_COLOR = new Color(1, 1, 1);
        private readonly Color DISABLED_COLOR = new Color(.5f, .5f, .5f);
        private readonly string SAVE_BTN = "saveBtn";
        private readonly string SAVE_PROGRESS_TEXT = "savingProgressText";
        private readonly float SAVING_PROGRESS_TEXT_BUFFER = 10f;
        private readonly float SAVE_BUTTON_DISABLED_BUFFER = 20f;
        private readonly float SAVE_TEXT_DOT_TICK_MAX = .5f;

        private bool _isEnabled = true;
        private bool _isSaving = false;

        public float savingProgressTextBufferTime = 0;
        public float saveButtonBufferTime = 0;
        public float saveTextDotTime = 0;

        public GameObject savePanel;
        private GameObject _saveButton;
        private GameObject _saveProgressText;
        private UIManager _uiManager;
        private FileManager _fileManager;

        public SavePanelUI(GameObject panel, UIManager uiManager, FileManager fileManager)
        {
            savePanel = panel;
            _saveButton = savePanel.transform.Find(SAVE_BTN).gameObject;
            _saveProgressText = savePanel.transform.Find(SAVE_PROGRESS_TEXT).gameObject;

            _saveProgressText.SetActive(false);

            _saveButton.GetComponent<Button>().onClick.AddListener(saveButtonOnClick);

            hideSaveProgressText();

            _uiManager = uiManager;
            _fileManager = fileManager;

            _saveProgressText.GetComponent<Text>().text = SAVE_TEXT_BASE;
        }

        public void Update()
        {
            saveButtonBufferTime += Time.deltaTime;

            if (_isSaving)
            {
                savingProgressTextBufferTime += Time.deltaTime;
            }

            saveTextDotTime += Time.deltaTime;
            if (saveTextDotTime > SAVE_TEXT_DOT_TICK_MAX)
            {
                updateSavingText();
                saveTextDotTime = 0;
            }

            if (saveButtonBufferTime >= SAVE_BUTTON_DISABLED_BUFFER)
            {
                enableSaveButton();
                saveButtonBufferTime = 0;
            }

            if (savingProgressTextBufferTime >= SAVING_PROGRESS_TEXT_BUFFER)
            {
                hideSaveProgressText();
                savingProgressTextBufferTime = 0;
                _isSaving = false;
                ConsoleUtility.Log("[SavePanelUI] Hiding saving progress text");
            }
        }

        public void showSaveProgressText()
        {
            _saveProgressText.SetActive(true);
            disableSaveButton();
            saveButtonBufferTime = 0;
            savingProgressTextBufferTime = 0;
            _isSaving = true;
            ConsoleUtility.Log("[SavePanelUI] Showing saving progress text");
        }
        
        public void restart()
        {
            disableSaveButton();
            saveButtonBufferTime = 0;
        }

        public void hideSaveProgressText()
        { 
            _saveProgressText.SetActive(false);
        }

        public void enableSaveButton()
        {
            _saveButton.GetComponent<Button>().enabled = true;
            _saveButton.GetComponent<Image>().color = ENABLED_COLOR;
        }

        public void disableSaveButton()
        {
            _saveButton.GetComponent<Button>().enabled = false;
            _saveButton.GetComponent<Image>().color = DISABLED_COLOR;
        }

        public void showSaveButton()
        {
            _saveButton.SetActive(true);
        }

        public void hideSaveButton()
        {
            _saveButton.SetActive(false);
        }

        private void updateSavingText()
        {
            String saveString = _saveProgressText.GetComponent<Text>().text += ".";

            if (saveString == SAVE_TEXT_BASE + ".....")
            {
                _saveProgressText.GetComponent<Text>().text = SAVE_TEXT_BASE;
            }
            else
            {
                _saveProgressText.GetComponent<Text>().text = saveString;
            }
        }

        private void saveButtonOnClick()
        {
            _fileManager.saveAll();
            disableSaveButton();
        }
    }
}

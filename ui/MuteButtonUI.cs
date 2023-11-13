using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using com.super2games.idle.manager;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class MuteButtonUI
    {
        private readonly string MUTE_ON = "muteOn";
        private readonly string MUTE_OFF = "muteOff";

        public bool isEnabled = false;

        private Button _muteBtn;
        private GameObject _offGraphic;
        private GameObject _onGraphic;
        private UIManager _uiManager;

        public MuteButtonUI(Button muteBtn, UIManager uiManager)
        {
            _muteBtn = muteBtn;
            _muteBtn.onClick.AddListener(muteButtonOnClick);

            _uiManager = uiManager;

            _offGraphic = muteBtn.gameObject.transform.Find(MUTE_OFF).gameObject;
            _onGraphic = muteBtn.gameObject.transform.Find(MUTE_ON).gameObject;
        }

        public void showMuteBtn()
        {
            _muteBtn.gameObject.SetActive(true);
        }

        public void hideMuteBtn()
        {
            _muteBtn.gameObject.SetActive(false);
        }

        public void initialize()
        {
            if (!SoundManager.instance.musicEnabled && !SoundManager.instance.soundFXEnabled)
            {
                isEnabled = false;
                _offGraphic.SetActive(true);
                _onGraphic.SetActive(false);
            }
        }

        public void setGraphicState(bool enabled)
        {
            isEnabled = enabled;

            if (isEnabled)
            {
                _onGraphic.SetActive(true);
                _offGraphic.SetActive(false);
            }
            else
            {
                _offGraphic.SetActive(true);
                _onGraphic.SetActive(false);
            }
        }

        private void muteButtonOnClick()
        {
            //_uiManager.showMusicWarningPanel();

            isEnabled = !isEnabled;

            if (isEnabled)
            {
                SoundManager.instance.muteAllSounds();
            }
            else
            {
                SoundManager.instance.unmuteAllSounds();
            }

            setGraphicState(isEnabled);
        }
    }
}

using com.super2games.idle.factory;
using com.super2games.idle.utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    
    public class MobileAdPopupUI
    {

        private readonly string CONFIRM_BUTTON = "mobileInfoPopup/ConfirmBtns/ConfirmBtn";
        private readonly string CANCEL_BUTTON = "mobileInfoPopup/ConfirmBtns/CancelBtn";

        private readonly string ITUNES_LINK = "https://itunes.apple.com/us/app/turbo-town/id1188524025?ls=1&mt=8";

        private GameObject _panel;

        private Button _confirmButton;
        private Button _cancelButton;

        public MobileAdPopupUI(GameObject panel, Button webMobileButton)
        {
            _panel = panel;

            _confirmButton = _panel.transform.Find(CONFIRM_BUTTON).GetComponent<Button>();
            _cancelButton = _panel.transform.Find(CANCEL_BUTTON).GetComponent<Button>();
            
            _cancelButton.onClick.AddListener(cancelClick);
            _confirmButton.onClick.AddListener(confirmClick);

            webMobileButton.onClick.AddListener(onWebMobileButtonClick);
        }

        private void cancelClick()
        {
            JobFactory.uiManager.hideMobileAdPanel();
        }

        private void confirmClick()
        {
            JobFactory.uiManager.hideMobileAdPanel();
            URLUtility.openURL(ITUNES_LINK);
        }

        private void onWebMobileButtonClick()
        {
            JobFactory.uiManager.showMobileAdPanel();
        }


    }
}

using com.super2games.idle.enums;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class HelpPanelUI
    {
        private readonly string CLOSE_BUTTON = "exitBtn";
        private readonly string HELP_CONTENT = "Scroll View/Viewport/Content";
        private readonly string HELP_PARAGRAPH_TITLE = "Title";

        private GameObject _helpPanel;
        private UIManager _uiManager;
        private Button _closeButton;
        private GameObject _helpContent;

        public HelpPanelUI(GameObject helpPanel, UIManager uiManager)
        {
            _helpPanel = helpPanel;
            _uiManager = uiManager;
            _closeButton = helpPanel.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();
            _closeButton.onClick.AddListener(onCloseClick);
            _helpContent = _helpPanel.transform.Find(HELP_CONTENT).gameObject;
            setup();
        }

        public void addParagraph(string titleText, List<string> descriptions)
        {
            GameObject paragraph = GameObjectUtility.instantiateGameObject(PrefabsEnum.HELP_PARAGRAPH_PREFAB);
            Text pTitle = paragraph.transform.Find(HELP_PARAGRAPH_TITLE).gameObject.GetComponent<Text>();

            pTitle.text = titleText;
            paragraph.transform.SetParent(_helpContent.transform, false);

            for (int i = 0; i < descriptions.Count; i++)
            {
                //this is problematic in that things that exceed a single line need their preferred line set, multipled by every extra line they take up
                GameObject helpText = GameObjectUtility.instantiateGameObject(PrefabsEnum.HELP_TEXT_ENTRY_PREFAB);
                helpText.GetComponent<Text>().text = descriptions[i];
                helpText.transform.SetParent(paragraph.transform, false);
            }
        }

        private void setup()
        {
            List<String> helpList_01 = new List<string>();
            helpList_01.Add("- Tap on buildings to access their information.");
            helpList_01.Add("- Tap and hold on buildings to reposition them.");
            helpList_01.Add("- Tap the arrows on the bottom center Camera Gizmo to turn the camera. Tap the T for a top down view.");
            helpList_01.Add("- Pinch and stretch to zoom the camera in and out.");
            helpList_01.Add("- Tap down and move to pan the camera.");
            helpList_01.Add("- Tap on resources to gather them.");
            helpList_01.Add("- Apply more population to resources to get bigger bonuses.");
            addParagraph("CONTROLS", helpList_01);


            List<String> helpList_02 = new List<string>();
            helpList_02.Add("- Assigning population to resources will allow them to clear out land and collect raw resources which buildings use for jobs.");
            helpList_02.Add("- Jobs are unlocked on buildings by assigning population to them and Upgrading them.");
            helpList_02.Add("- Upgrading buildings also improves their production and reduces their time to complete.");
            helpList_02.Add("- You can assign boosts you acquire to buildings by tapping on the items tab while viewing a building's information.");
            helpList_02.Add("- Some buildings enhance other buildings by being near them. Affected buildings are highlighted when the building is selected.");
            helpList_02.Add("- Many buildings require advanced materials for them to be built. Try unlocking jobs on resource collectors and factories to produce these.");
            helpList_02.Add("- Different buildings cost different amounts to upgrade. Usually you just want to upgrade a building until you hit your next job. However, buildings like Resource Collectors (Lumberyards, Quarries etc.) are cheaper to upgrade and can produce high demand building materials.");
            addParagraph("GAMEPLAY", helpList_02);
        }

        private void onCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideHelpPanel();
        }
    }
}

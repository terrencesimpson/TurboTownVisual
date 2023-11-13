using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class TutorialQuestPanelUI
    {
        private readonly string TUTORIAL_QUEST_ICON = "LayoutPanel/TutorialQuestIcon";
        private readonly string SKIP_TUTORIAL_BUTTON = "SkipTutorialButton";
        private readonly string YES_BUTTON = "panel/yesButton";
        private readonly string NO_BUTTON = "panel/noButton";

        private TutorialQuestIcon _tutorialQuestIcon1;
        private TutorialQuestIcon _tutorialQuestIcon2;
        private TutorialQuestIcon _tutorialQuestIcon3;

        private Button _skipTutorialButton;

        private List<TutorialQuestIcon> _tutorialQuestIcons = new List<TutorialQuestIcon>();

        public bool isHighlighted = false;

        public GameObject _skipTutorialConfirmPanel;
        public Button _yesButton;
        public Button _noButton;

        public TutorialQuestPanelUI(GameObject panel, GameObject skipTutorialConfirm)
        {
            _skipTutorialConfirmPanel = skipTutorialConfirm;

            _tutorialQuestIcon1 = panel.transform.Find(TUTORIAL_QUEST_ICON + "1").gameObject.GetComponent<TutorialQuestIcon>();
            _tutorialQuestIcon2 = panel.transform.Find(TUTORIAL_QUEST_ICON + "2").gameObject.GetComponent<TutorialQuestIcon>();
            _tutorialQuestIcon3 = panel.transform.Find(TUTORIAL_QUEST_ICON + "3").gameObject.GetComponent<TutorialQuestIcon>();

            _yesButton = skipTutorialConfirm.transform.Find(YES_BUTTON).gameObject.GetComponent<Button>();
            _noButton = skipTutorialConfirm.transform.Find(NO_BUTTON).gameObject.GetComponent<Button>();

            _skipTutorialButton = panel.transform.Find(SKIP_TUTORIAL_BUTTON).gameObject.GetComponent<Button>();

            _tutorialQuestIcons.Add(_tutorialQuestIcon1);
            _tutorialQuestIcons.Add(_tutorialQuestIcon2);
            _tutorialQuestIcons.Add(_tutorialQuestIcon3);

            _skipTutorialButton.onClick.AddListener(onSkipTutorialClick);

            _yesButton.onClick.AddListener(yesButtonClick);
            _noButton.onClick.AddListener(noButtonClick);
        }

        private void onSkipTutorialClick()
        {
            JobFactory.uiManager.showSkipTutorialConfirmPanel();
        }

        private void yesButtonClick()
        {
            JobFactory.uiManager.hideSkipTutorialConfirmPanel();
            JobFactory.tutorialManager.skipTutorial();
        }

        private void noButtonClick()
        {
            JobFactory.uiManager.hideSkipTutorialConfirmPanel();
        }

        public void highlightFirstQuestIcon()  
        {
            if (isHighlighted) return;

            for (int i = 0; i < _tutorialQuestIcons.Count; ++i)
            {
                TutorialQuestIcon icon = _tutorialQuestIcons[i];
                if (icon.tutorialQuestConfig != null)
                {
                    JobFactory.uiManager.createHighlight(icon.gameObject, DirectionEnum.EAST);
                    isHighlighted = true;
                    break;
                }
            }
        }

        public void addTutorialQuest(TutorialQuestConfig config)
        {
            for (int i = 0; i < _tutorialQuestIcons.Count; ++i)
            {
                TutorialQuestIcon icon = _tutorialQuestIcons[i];
                if (icon.tutorialQuestConfig == null)
                {
                    icon.tutorialQuestConfig = config;
                    icon.setIcon(config.icon);
                    icon.show();
                    break;
                }
            }
        }

        public void completeTutorialQuest(TutorialQuestConfig config)
        {
            for (int i = 0; i <= _tutorialQuestIcons.Count; ++i)
            {
                TutorialQuestIcon icon = _tutorialQuestIcons[i];
                if (icon.tutorialQuestConfig == config)
                {
                    icon.markComplete();
                    icon.showCheckmark();
                    break; //Not really needed since there is one match, but you should break out of the loop anyways.
                }
            }
        }


    }
}

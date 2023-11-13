using com.super2games.idle.config;
using com.super2games.idle.factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.super2games.idle.ui
{
    public class TutorialQuestIcon : AbstractIcon
    {
        private readonly string BASE_PATH = "Textures/ui/tutorialQuest/";

        public TutorialQuestConfig tutorialQuestConfig;

        public GameObject checkmark;

        private bool _isComplete = false;

        public TutorialQuestIcon()
        {
            basePath = BASE_PATH;
        }

        public void onClick()
        {
            JobFactory.tutorialManager.resetByIconClick();
            if (_isComplete)
            {
                TutorialQuestConfig configRef = tutorialQuestConfig; //We rely on the tutorialQuestConfig var to be null when we call collectRewardByIconClick.
                reset();
                hide();
                JobFactory.tutorialManager.collectRewardByIconClick(configRef);
            }
            else
            {
                JobFactory.tutorialManager.runTutorialQuest(tutorialQuestConfig);
            }
        }

        public void show()
        {
            gameObject.SetActive(true);
        }

        public void hide()
        {
            gameObject.SetActive(false);
        }

        public void showCheckmark()
        {
            checkmark.SetActive(true);
        }

        public void hideCheckmark()
        {
            checkmark.SetActive(false);
        }

        public void markComplete()
        {
            _isComplete = true;
        }

        public void reset()
        {
            hideCheckmark();
            tutorialQuestConfig = null;
            _isComplete = false;
        }

    }
}

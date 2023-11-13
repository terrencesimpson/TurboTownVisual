using com.super2games.idle.goals;
using com.super2games.idle.utilities;
using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class QuestListEntry
    {
        private readonly string QUEST_ENTRY = "Prefabs/ui/QuestEntry";
        private readonly string QUEST_TEXT = "questText";
        private readonly string QUEST_PROGRESS_TEXT = "questProgressText";
        private readonly string QUEST_PROGRESS_BAR = "questProgressBar";
        private readonly string GET_REWARD_BUTTON = "getRewardBtn";
        private readonly string STAR = "star";
        private readonly string STARS = "stars";

        private readonly int NUM_OF_STARS = 3;

        public delegate void OnGetRewardClickDelegate(Goal goal);
        public event OnGetRewardClickDelegate onGetRewardClickEvent;

        private Text _questText;
        private Text _questProgressText;
        private Image _questProgressBar;

        private Button _getRewardButton;
        private GameObject _getRewardGO;

        private Goal _goal;

        private GameObject _entry;

        public QuestListEntry(GameObject content, Goal goal)
        {
            _goal = goal;

            _entry = GameObjectUtility.instantiateGameObject(QUEST_ENTRY);

            _entry.transform.SetParent(content.transform, false);

            _questText = _entry.transform.Find(QUEST_TEXT).gameObject.GetComponent<Text>();
            _questProgressText = _entry.transform.Find(QUEST_PROGRESS_TEXT).gameObject.GetComponent<Text>();
            _questProgressBar = _entry.transform.Find(QUEST_PROGRESS_BAR).gameObject.GetComponent<Image>();
            _getRewardGO = _entry.transform.Find(GET_REWARD_BUTTON).gameObject;
            _getRewardButton = _getRewardGO.GetComponent<Button>();

            _getRewardButton.onClick.AddListener(onGetRewardClick);
            _getRewardGO.SetActive(false);

            GameObject starContainer = _entry.transform.Find(STARS).gameObject;
            for (int i = 1; i <= NUM_OF_STARS; ++i)
            {
                if (i <= goal.config.rank)
                {
                    starContainer.transform.Find(STAR + i).gameObject.SetActive(true);
                }
                else
                {
                    starContainer.transform.Find(STAR + i).gameObject.SetActive(false);
                }
            }

            _questText.text = _goal.config.description;

            update();
        }

        private void onGetRewardClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            if (onGetRewardClickEvent != null)
            {
                onGetRewardClickEvent(_goal);
            }
            destroy();
        }

        public void update()
        {
            float percent = (float)(_goal.amount / _goal.config.amount);
            if (percent > 1)
            {
                percent = 1;
            }
            _questProgressText.text = StringUtility.percentToString(percent);
            _questProgressBar.transform.localScale = new Vector3(percent, _questProgressBar.transform.localScale.y, _questProgressBar.transform.localScale.z);
        }

        public void showGetRewardButton()
        {
            _getRewardGO.SetActive(true);
        }

        public void destroy()
        {
            GameObject.Destroy(_questText);
            GameObject.Destroy(_questProgressText);
            GameObject.Destroy(_questProgressBar);
            GameObject.Destroy(_entry);
            _entry = null;
            _questProgressText = null;
            _questProgressBar = null;
            _questText = null;
        }


    }
}

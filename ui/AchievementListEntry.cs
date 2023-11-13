using com.super2games.idle.goals;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.manager;

namespace com.super2games.idle.ui
{
    public class AchievementListEntry
    {

        private readonly string ACHIEVEMENT_ENTRY = "Prefabs/ui/AchievementEntry"; //Remember these entries are bigger than Quest ones.
        private readonly string ACHIEVEMENT_TEXT = "achievementText";
        private readonly string ACHIEVEMENT_PROGRESS_TEXT = "achievementProgressText";
        private readonly string ACHIEVEMENT_PROGRESS_BAR = "achievementProgressBar";
        private readonly string GET_REWARD_BUTTON = "getRewardBtn";
        private readonly string GOAL_TEXT = "goalText";
        private readonly string STARS = "stars";
        private readonly string STAR_TOGGLE = "starToggle";
        private readonly string STAR_FILLED = "starFilled";
        private readonly string STAR_EMPTY = "starEmpty";

        private readonly int NUM_OF_STARS = 10;

        public delegate void OnGetRewardClickDelegate(string goalID);
        public event OnGetRewardClickDelegate onGetRewardClickEvent;

        private Text _achievementText;
        private Text _achievementProgressText;
        private Image _achievementProgressBar;
        private Text _goalText;

        public GameObject getRewardGO;
        private Button _getRewardButton;

        public GameObject view;

        private List<GameObject> starsFilled = new List<GameObject>();
        private List<GameObject> starsEmpty = new List<GameObject>();

        private string _goalID;

        private int _currentRank = -1; //So it updates the first time.

        public AchievementListEntry(string goalID)
        {
            _goalID = goalID;

            view = GameObjectUtility.instantiateGameObject(ACHIEVEMENT_ENTRY);

            //_entry.transform.SetParent(content.transform);

            _achievementText = view.transform.Find(ACHIEVEMENT_TEXT).gameObject.GetComponent<Text>();
            _achievementProgressText = view.transform.Find(ACHIEVEMENT_PROGRESS_TEXT).gameObject.GetComponent<Text>();
            _achievementProgressBar = view.transform.Find(ACHIEVEMENT_PROGRESS_BAR).gameObject.GetComponent<Image>();
            _goalText = view.transform.Find(GOAL_TEXT).gameObject.GetComponent<Text>();
            getRewardGO = view.transform.Find(GET_REWARD_BUTTON).gameObject;
            _getRewardButton = getRewardGO.GetComponent<Button>();

            GameObject starContainer = view.transform.Find(STARS).gameObject;
            for (int i = 1; i <= NUM_OF_STARS; ++i)
            {
                GameObject starToggle = starContainer.transform.Find(STAR_TOGGLE + i).gameObject;
                starsFilled.Add(starToggle.transform.Find(STAR_FILLED).gameObject);
                starsEmpty.Add(starToggle.transform.Find(STAR_EMPTY).gameObject);
            }

            _getRewardButton.onClick.AddListener(onGetRewardClick);

            hideGetRewardButton();
        }

        private void onGetRewardClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            onGetRewardClickEvent(_goalID);
        }

        public void update(Goal goal)
        {
            float percent = (float)(goal.amount / goal.config.amount);
            percent = (percent > 1) ? 1 : percent;
            _achievementText.text = goal.config.description;
            _goalText.text = StringUtility.toNumString(goal.config.amount);
            _achievementProgressText.text = StringUtility.percentToString(percent);
            _achievementProgressBar.transform.localScale = new Vector3(percent, _achievementProgressBar.transform.localScale.y, _achievementProgressBar.transform.localScale.z);
            updateStars((int)(goal.config.rank - 1));
        }

        public void updateToEnd(Goal goal)
        {
            _achievementText.text = goal.config.description;
            _goalText.text = StringUtility.toNumString(goal.config.amount);
            _achievementProgressText.text = StringUtility.percentToString(1);
            _achievementProgressBar.transform.localScale = new Vector3(1, _achievementProgressBar.transform.localScale.y, _achievementProgressBar.transform.localScale.z);
            fillAllStars();
        }

        public void updateStars(int rank)
        {
            if (_currentRank == rank)
            {
                return; //No need to loop and update.
            }

            for (int i = 0; i <= rank; ++i)
            {
                starsFilled[i].SetActive(true);
                starsEmpty[i].SetActive(false);
            }
            for (int j = rank; j < NUM_OF_STARS; ++j)
            {
                starsFilled[j].SetActive(false);
                starsEmpty[j].SetActive(true);
            }
            _currentRank = rank;
        }

        public void fillAllStars()
        {
            for (int j = 0; j < NUM_OF_STARS; ++j)
            {
                starsFilled[j].SetActive(true);
                starsEmpty[j].SetActive(false);
            }
        }

        public void showGetRewardButton()
        {
            getRewardGO.SetActive(true);
        }

        public void hideGetRewardButton()
        {
            getRewardGO.SetActive(false);
        }

    }
}

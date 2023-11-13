using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class PrestigePanelUI
    {
        private readonly string COLLECT_BUTTON = "collectButton";
        private readonly string SCORE_TEXT = "pointValueText";
        private readonly string BRONZE_COUNT_TEXT = "rewardTallyBriefcases/briefcasePrestigePreview/countText";
        private readonly string SILVER_COUNT_TEXT = "rewardTallyBriefcases/briefcasePrestigePreview (1)/countText";
        private readonly string GOLD_COUNT_TEXT = "rewardTallyBriefcases/briefcasePrestigePreview (2)/countText";
        private readonly string ANGEL_WINGS_COUNT_TEXT = "rewardTallyCurrency/countText";

        private Button _collectButton;
        private PrestigeManager _prestigeManager;

        private GameObject _panel;
        private Text _scoreText;

        private Text _bronzeCaseCountText;
        private Text _silverCaseCountText;
        private Text _goldCaseCountText;

        private Text _angelWingsCountText;

        public PrestigePanelUI(GameObject panel, PrestigeManager prestigeManager)
        {
            _panel = panel;
            _prestigeManager = prestigeManager;
            _collectButton = _panel.transform.Find(COLLECT_BUTTON).gameObject.GetComponent<Button>();
            _collectButton.onClick.AddListener(onCollectClick);
            
            _bronzeCaseCountText = _panel.transform.Find(BRONZE_COUNT_TEXT).gameObject.GetComponent<Text>();
            _silverCaseCountText = _panel.transform.Find(SILVER_COUNT_TEXT).gameObject.GetComponent<Text>();
            _goldCaseCountText = _panel.transform.Find(GOLD_COUNT_TEXT).gameObject.GetComponent<Text>();

            _angelWingsCountText = _panel.transform.Find(ANGEL_WINGS_COUNT_TEXT).gameObject.GetComponent<Text>();

            _scoreText = _panel.transform.Find(SCORE_TEXT).gameObject.GetComponent<Text>();
        }

        public void setScore(double score)
        {
			_scoreText.text = score.ToString("N0");
        }

        public void setCaseValues(double bronze, double silver, double gold)
        {
            _bronzeCaseCountText.text = bronze.ToString();
            _silverCaseCountText.text = silver.ToString();
            _goldCaseCountText.text = gold.ToString();
        }

        public void setAngelWingsValue(double angelWings)
        {
            _angelWingsCountText.text = angelWings.ToString();
        }

        private void onCollectClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _prestigeManager.restart();
        }

    }
}

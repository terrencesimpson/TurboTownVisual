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
    public class LeaderboardEntry
    {
        private readonly string LEADERBOARD_ENTRY = "Prefabs/ui/LeaderboardEntry";
        private readonly string PLAYER_NAME = "playerNameText";
        private readonly string SCORE = "scoreText";
        private readonly string RANK = "rankText";

        public GameObject view;

        private Text _playerNameText;
        private Text _scoreText;
        private Text _rankText;

        public LeaderboardEntry()
        {
            view = GameObjectUtility.instantiateGameObject(LEADERBOARD_ENTRY);

            _playerNameText = view.transform.Find(PLAYER_NAME).gameObject.GetComponent<Text>();
            _scoreText = view.transform.Find(SCORE).gameObject.GetComponent<Text>();
            _rankText = view.transform.Find(RANK).gameObject.GetComponent<Text>();
        }

        public void update(string playerName, string score, string rank)
        {
            _playerNameText.text = playerName;
            _scoreText.text = score;
            _rankText.text = rank;
        }

        public void colorEntryMe()
        {
            Color color = new Color() { r = 105, g = 255, b = 0, a = 255 }; //Yellow color
            _playerNameText.color = color;
            _scoreText.color = color;
            _rankText.color = color;
        }

    }
}

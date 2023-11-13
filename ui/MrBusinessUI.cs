using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.config;
using com.super2games.idle.enums;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.goals;
using com.super2games.idle.factory;
using com.super2games.idle.utilties;
using UnityEngine.Advertisements;

namespace com.super2games.idle.ui
{
	internal class AdLoadListeners : IUnityAdsLoadListener
	{
		public void OnUnityAdsAdLoaded(string placementId)
		{
			ConsoleUtility.Log("[AdLoadListeners].OnUnityAdsAdLoaded -- placementId: " + placementId);
		}

		public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
		{
			ConsoleUtility.Log("[AdLoadListeners].OnUnityAdsFailedToLoad -- placementId: " + placementId + " message: " + message);
		}
	}

	internal class InitializationListeners : IUnityAdsInitializationListener
	{
		public void OnInitializationComplete()
		{
			ConsoleUtility.Log("[InitializationListeners].OnInitializationComplete");
			Advertisement.Load("rewardedVideo", new AdLoadListeners());
		}

		public void OnInitializationFailed(UnityAdsInitializationError error, string message)
		{
			ConsoleUtility.Log($"[InitializationListeners].OnInitializationFailed -- {error.ToString()} - {message}");
		}
	}

	internal class AdShowListeners : IUnityAdsShowListener
	{
		private AdManager _adManager;
		private UIManager _uiManager;

		public AdShowListeners(AdManager adManager, UIManager uiManager)
		{
			_adManager = adManager;
			_uiManager = uiManager;
		}

		public void OnUnityAdsShowClick(string placementId)
		{
			ConsoleUtility.Log("[AdShowListeners].OnUnityAdsShowClick -- placementId: " + placementId);
		}

		public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
		{
			ConsoleUtility.Log("[AdShowListeners].OnUnityAdsShowComplete -- placementId: " + placementId);
			if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
			{
				_adManager.awardAdPrize();
				_uiManager.alertPopupUI.showAlert("Thanks for your support! Your Reward has been applied! :-)", "Reward!");

				JobFactory.playFabManager.analytics(AnalyticsEnum.AD_COMPLETED);
				JobFactory.itemsManager.adWatches++; //Tracking ad watches in the players data, because we should.

				_adManager.resetAdTime();
				_uiManager.bottomLeftMenuUI.disableAdButton();
			}
			else if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
			{
				_uiManager.alertPopupUI.showAlert("Ad skipped! Hope you'll try again later!", "Ad skipped!");
			}
		}

		public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
		{
			ConsoleUtility.Log("[AdLoadListeners].OnUnityAdsShowFailure -- placementId: " + placementId + " message: " + message);
			_uiManager.alertPopupUI.showAlert("No Ads Available! Sorry try again later!", "Error!");
		}

		public void OnUnityAdsShowStart(string placementId)
		{
			ConsoleUtility.Log("[AdShowListeners].OnUnityAdsShowStart -- placementId: " + placementId);
		}
	}

	public class MrBusinessUI
    {
        public static readonly string BUBBLE_LARGE_RIGHT = "BUBBLE_LARGE_RIGHT";
        public static readonly string BUBBLE_LARGE_RIGHT_2 = "BUBBLE_LARGE_RIGHT_2";
        public static readonly string BUBBLE_SMALL_LEFT = "BUBBLE_SMALL_LEFT";
        public static readonly string BUBBLE_SMALL_RIGHT = "BUBBLE_SMALL_RIGHT";

        private readonly string SPEECH_UI_PANEL = "SpeechUI";
        private readonly string SPEECH_BUBBLE = "SpeechUI/SpeechBubble";
        private readonly string HIT_AREA = "HitArea";
        private readonly string BUBBLE_POINT = "BubblePoint";
        private readonly string BUBBLE_TEXT_LARGE = "ContentText";
        private readonly string BUBBLE_TEXT_SMALL = "ContentTextSmall";
        private readonly string CONTINUE_TEXT = "ContinueText";
        private readonly string AD_BTNS = "SpeechUI/SpeechBubble/ConfirmBtns";
        private readonly string CONFIRM_BTN = "ConfirmBtn";
        private readonly string CANCEL_BTN = "CancelBtn";

        private readonly string TITLE_TEXT = "SpeechUI/TitleBubble/TitleText";
        private readonly string TITLE_BUBBLE = "SpeechUI/TitleBubble";

        private readonly string REWARD_ICON_1 = "SpeechUI/RewardBubble/RewardsLayout/Reward1/RewardIcon";
        private readonly string REWARD_ICON_2 = "SpeechUI/RewardBubble/RewardsLayout/Reward2/RewardIcon";
        private readonly string REWARD_ICON_3 = "SpeechUI/RewardBubble/RewardsLayout/Reward3/RewardIcon";

        private readonly string REWARD_TITLE_1 = "SpeechUI/RewardBubble/RewardsLayout/Reward1/RewardTitle";
        private readonly string REWARD_TITLE_2 = "SpeechUI/RewardBubble/RewardsLayout/Reward2/RewardTitle";
        private readonly string REWARD_TITLE_3 = "SpeechUI/RewardBubble/RewardsLayout/Reward3/RewardTitle";

        private readonly string REWARD_PANEL_1 = "SpeechUI/RewardBubble/RewardsLayout/Reward1";
        private readonly string REWARD_PANEL_2 = "SpeechUI/RewardBubble/RewardsLayout/Reward2";
        private readonly string REWARD_PANEL_3 = "SpeechUI/RewardBubble/RewardsLayout/Reward3";

        private readonly string REWARD_PANEL = "SpeechUI/RewardBubble";

        private readonly int BUBBLE_LARGE_NUM_LINES = 8;
        private readonly int BUBBLE_LARGE_NUM_LINES_2 = 10;
        private readonly int BUBBLE_SMALL_NUM_LINES = 8;

        private readonly Vector2 BUBBLE_LARGE_RIGHT_SIDE_POS = new Vector2(-48, 234);
        private readonly Vector2 BUBBLE_LARGE_RIGHT_SIDE_SIZE = new Vector2(490, 310);//234, 310  

        private readonly Vector2 BUBBLE_LARGE_RIGHT_SIDE_POS_2 = new Vector2(-48, 220);
        private readonly Vector2 BUBBLE_LARGE_RIGHT_SIDE_SIZE_2 = new Vector2(490, 408);

        private readonly Vector2 BUBBLE_SMALL_RIGHT_SIDE_POS = new Vector2(-170, 66);
        private readonly Vector2 BUBBLE_SMALL_RIGHT_SIDE_SIZE = new Vector2(348, 246);

        private readonly Vector2 BUBBLE_SMALL_LEFT_SIDE_POS = new Vector2(-431, 95);
        private readonly Vector2 BUBBLE_SMALL_LEFT_SIDE_SIZE = new Vector2(348, 246);

        private readonly Vector2 BUBBLE_POINT_LEFT_SIDE_POS = new Vector2(-20, -60);//(-20, 50);

        private readonly Vector2 BUBBLE_POINT_LEFT_SIDE_SMALL_POS = new Vector2(-20, -50);//(-20, 50);

        private readonly Vector2 BUBBLE_POINT_RIGHT_SIDE_SMALL_POS = new Vector2(366, -20);//(366, 50);

        //private readonly float TIME_BETWEEN_TEXT_PAUSES = 3f;
        private readonly float TEXT_PAUSE_TIME = .25f;

        private GameObject _tutorialPanel;
        private GameObject _speechUIPanel;
        private MrBusinessManager _mrBusinessManager;
        private UIManager _uiManager;
        private PrefabManager _prefabManager;
        private KongregateWebAdManager _kongWebAdManager;
        private ModelManager _modelManager;
        private AdManager _adManager;

        private GameObject _bubbleArea;
        private GameObject _bubblePoint;
        private GameObject _bubbleTextLarge;
        private GameObject _bubbleTextSmall;
        private GameObject _continueText;
        private GameObject _adBtns;
        private GameObject _adConfirmBtn;
        private GameObject _adCancelBtn;
        private Button _hitArea;

        private Text _adDescriptionText;
        private string _adDescriptionString = "";
        private string _fullAdSpeechString = "";

        private string _fullSpeechBubbleText = "";
        private int _speechBubbleTextCurrentIndex = 0;
        private bool _speechIsBeingTypedOut = false;
        private GameObject _currentSpeechBubble;

        private List<string> _speechBubbleSegments = new List<string>();
        private string _currentSpeechSegmentText = "";
        private int _currentSpeechSegmentIndex = 0;

        //private float _timeBetweenTextPauseTimer = 0f;
        private float _textPauseTimer = 0f;
        private bool _pauseTextOnFrame = false;

        private Text _titleText;

        private ResourceIconUI _rewardIcon1;
        private ResourceIconUI _rewardIcon2;
        private ResourceIconUI _rewardIcon3;

        private Text _rewardTitle1;
        private Text _rewardTitle2;
        private Text _rewardTitle3;

        private GameObject _rewardPanel1;
        private GameObject _rewardPanel2;
        private GameObject _rewardPanel3;

        private List<ResourceIconUI> _rewardIcons;
        private List<Text> _rewardTitles;
        private List<GameObject> _rewardPanels;

        private GameObject _rewardsPanel;

        private GameObject _titleBubble;

        private bool _soundEnabled;
        private bool _musicEnabled;

        public MrBusinessUI(GameObject panel, MrBusinessManager mrBusinessManager, UIManager uiManager, PrefabManager prefabManager, ModelManager modelManager, AdManager adManager, KongregateWebAdManager kongWebAdManager)
        {
            _tutorialPanel = panel;
            _mrBusinessManager = mrBusinessManager;
            _uiManager = uiManager;
            _prefabManager = prefabManager;
            _modelManager = modelManager;
            _adManager = adManager;
            _kongWebAdManager = kongWebAdManager;

            _speechUIPanel = _tutorialPanel.transform.Find(SPEECH_UI_PANEL).gameObject;
            _bubbleArea = _tutorialPanel.transform.Find(SPEECH_BUBBLE).gameObject;
            _bubblePoint = _bubbleArea.transform.Find(BUBBLE_POINT).gameObject;
            _bubbleTextLarge = _bubbleArea.transform.Find(BUBBLE_TEXT_LARGE).gameObject;
            _bubbleTextSmall = _bubbleArea.transform.Find(BUBBLE_TEXT_SMALL).gameObject;
            _continueText = _bubbleArea.transform.Find(CONTINUE_TEXT).gameObject;

            _titleText = _tutorialPanel.transform.Find(TITLE_TEXT).gameObject.GetComponent<Text>();
            _titleBubble = _tutorialPanel.transform.Find(TITLE_BUBBLE).gameObject;

            _rewardIcon1 = _tutorialPanel.transform.Find(REWARD_ICON_1).gameObject.GetComponent<ResourceIconUI>();
            _rewardIcon2 = _tutorialPanel.transform.Find(REWARD_ICON_2).gameObject.GetComponent<ResourceIconUI>();
            _rewardIcon3 = _tutorialPanel.transform.Find(REWARD_ICON_3).gameObject.GetComponent<ResourceIconUI>();

            _rewardTitle1 = _tutorialPanel.transform.Find(REWARD_TITLE_1).gameObject.GetComponent<Text>();
            _rewardTitle2 = _tutorialPanel.transform.Find(REWARD_TITLE_2).gameObject.GetComponent<Text>();
            _rewardTitle3 = _tutorialPanel.transform.Find(REWARD_TITLE_3).gameObject.GetComponent<Text>();

            _rewardPanel1 = _tutorialPanel.transform.Find(REWARD_PANEL_1).gameObject;
            _rewardPanel2 = _tutorialPanel.transform.Find(REWARD_PANEL_2).gameObject;
            _rewardPanel3 = _tutorialPanel.transform.Find(REWARD_PANEL_3).gameObject;

            _rewardsPanel = _tutorialPanel.transform.Find(REWARD_PANEL).gameObject;

            _rewardIcons = new List<ResourceIconUI>();
            _rewardTitles = new List<Text>();
            _rewardPanels = new List<GameObject>();

            _rewardIcons.Add(_rewardIcon1);
            _rewardIcons.Add(_rewardIcon2);
            _rewardIcons.Add(_rewardIcon3);

            _rewardTitles.Add(_rewardTitle1);
            _rewardTitles.Add(_rewardTitle2);
            _rewardTitles.Add(_rewardTitle3);

            _rewardPanels.Add(_rewardPanel1);
            _rewardPanels.Add(_rewardPanel2);
            _rewardPanels.Add(_rewardPanel3);

            _hitArea = _tutorialPanel.transform.Find(HIT_AREA).gameObject.GetComponent<Button>();
            _hitArea.onClick.AddListener(onHitAreaClick);

            _adBtns = _tutorialPanel.transform.Find(AD_BTNS).gameObject;
            _adConfirmBtn = _adBtns.transform.Find(CONFIRM_BTN).gameObject;
            _adCancelBtn = _adBtns.transform.Find(CANCEL_BTN).gameObject;

            _adConfirmBtn.GetComponent<Button>().onClick.AddListener(onAdConfirmClick);
            _adCancelBtn.GetComponent<Button>().onClick.AddListener(onAdCancelClick);

            _adDescriptionString = (_modelManager.globalVarsModel.getConfig(GlobalVarEnum.AD_CONFIRM_PANEL_DESCRIPTION_TEXT) as GlobalVarConfig).value;

            _adBtns.SetActive(false);

#if UNITY_IOS
			Advertisement.Initialize("1417153", false, new InitializationListeners());
#endif
		}

        public void update()
        {
            if (_speechIsBeingTypedOut)
            {
                if (_pauseTextOnFrame)
                {
                    _textPauseTimer += Time.deltaTime;
                    if (_textPauseTimer >= TEXT_PAUSE_TIME)
                    {
                        _pauseTextOnFrame = false;
                        _textPauseTimer = 0f;
                    }
                    return;
                }

                if (_currentSpeechBubble.GetComponent<Text>().text == _currentSpeechSegmentText)
                {
                    _speechIsBeingTypedOut = false;
                }
                else
                {
                    string currentLetter = _currentSpeechSegmentText[_speechBubbleTextCurrentIndex].ToString();

                    if (currentLetter == ".")
                    {
                        _pauseTextOnFrame = true;
                    }

                    if (_speechBubbleTextCurrentIndex < _currentSpeechSegmentText.Length)
                    {
                        _currentSpeechBubble.GetComponent<Text>().text += currentLetter;
                    }

                    _speechBubbleTextCurrentIndex++;
                }
            }
        }

        public void setRewards(List<ItemConfig> rewards)
        {
            enableRewardPanels(false);
            enableRewardIcons(false);
            enableRewardTitles(false);

            for (int i = 0; i < rewards.Count; ++i)
            {
                ItemConfig itemConfig = rewards[i];
                _rewardPanels[i].gameObject.SetActive(true);
                _rewardIcons[i].gameObject.SetActive(true);
                _rewardTitles[i].gameObject.SetActive(true);
                _rewardIcons[i].setIcon(itemConfig.itemID);
                _rewardTitles[i].text = StringUtility.toNumString(itemConfig.amount);
            }
        }

        private void enableRewardIcons(bool enabled)
        {
            for (int i = 0; i < _rewardIcons.Count; ++i) _rewardIcons[i].gameObject.SetActive(enabled);
        }

        private void enableRewardTitles(bool enabled)
        {
            for (int i = 0; i < _rewardTitles.Count; ++i) _rewardTitles[i].gameObject.SetActive(enabled);
        }

        private void enableRewardPanels(bool enabled)
        {
            for (int i = 0; i < _rewardPanels.Count; ++i) _rewardPanels[i].gameObject.SetActive(enabled);
        }

        //Use this to position the bubble and set the speech text that will appear.
        //Uses the same states that the manager does for mr business' animation states
        public void setBubbleTransformAndText(string speechText, string state, string titleText, bool showConfirmBtns = false)
        {
            _speechUIPanel.SetActive(true);
            _bubbleArea.SetActive(true);
            _hitArea.gameObject.SetActive(true);

            _titleBubble.SetActive(false); //Hide by default, only show if there is text
            _rewardsPanel.SetActive(false);

            if (titleText != "")
            {
                _rewardsPanel.SetActive(true); //If there is a title, it's likely there are rewards. Or should be.
                _titleBubble.SetActive(true);
                _titleText.gameObject.SetActive(true);
                _titleText.text = titleText;
            }

            if (showConfirmBtns)
            {
                _rewardsPanel.SetActive(false);
                _titleBubble.SetActive(false);
                _continueText.SetActive(false);
                _adBtns.SetActive(true);
            }
            else
            {
                _continueText.SetActive(true);
                _adBtns.SetActive(false);
            }

            if (state == MrBusinessManager.LARGE_TALK_LEFT)
            {
                setSpeechBubbleTransform(BUBBLE_LARGE_RIGHT);
                setSpeechText(speechText, BUBBLE_TEXT_LARGE, BUBBLE_LARGE_NUM_LINES);
            }
            else if (state == MrBusinessManager.LARGE_TALK_LEFT_2)
            {
                setSpeechBubbleTransform(BUBBLE_LARGE_RIGHT_2);
                setSpeechText(speechText, BUBBLE_TEXT_LARGE, BUBBLE_LARGE_NUM_LINES_2);
            }
            else if (state == MrBusinessManager.SMALL_TALK_LEFT)
            {
                setSpeechBubbleTransform(BUBBLE_SMALL_LEFT);
                setSpeechText(speechText, BUBBLE_TEXT_SMALL, BUBBLE_SMALL_NUM_LINES);
            }
            else if (state == MrBusinessManager.SMALL_TALK_RIGHT)
            {
                setSpeechBubbleTransform(BUBBLE_SMALL_RIGHT);
                setSpeechText(speechText, BUBBLE_TEXT_SMALL, BUBBLE_SMALL_NUM_LINES);
            }
        }

        public void hideBubble()
        {
            _bubbleArea.SetActive(false);
            _hitArea.gameObject.SetActive(true);
        }

        private void setSpeechText(string speechText, string whichTextField, int lines)
        {
            _fullSpeechBubbleText = speechText;
            _speechBubbleSegments.Clear();
            _speechIsBeingTypedOut = true;
            _speechBubbleTextCurrentIndex = 0;

            if (whichTextField == BUBBLE_TEXT_LARGE)
            {
                _bubbleTextLarge.SetActive(true);
                _bubbleTextSmall.SetActive(false);

                _currentSpeechBubble = _bubbleTextLarge;
                _speechBubbleSegments = determineSpeechBubbleSegments(speechText, _bubbleTextLarge, lines);
                _bubbleTextLarge.GetComponent<Text>().text = "";
            }
            else if(whichTextField == BUBBLE_TEXT_SMALL)
            {
                _bubbleTextLarge.SetActive(false);
                _bubbleTextSmall.SetActive(true);

                _currentSpeechBubble = _bubbleTextSmall;

                int lineNumModifier = 0;
                if (!_continueText.activeSelf)
                {
                    lineNumModifier = 1;
                }
                _speechBubbleSegments = determineSpeechBubbleSegments(speechText, _bubbleTextSmall, lines + lineNumModifier);
                _bubbleTextSmall.GetComponent<Text>().text = "";
            }

            _currentSpeechSegmentText = _speechBubbleSegments[0];
            Canvas.ForceUpdateCanvases();
        }

        private List<string> determineSpeechBubbleSegments(string fullText, GameObject textBubble, int numLinesInBubble)
        {
            //Break this up into "segments"
            //So cut up the text into chunks that will all fit into the speech bubble
            List<string> segments = new List<string>();

            textBubble.GetComponent<Text>().text = fullText;
            Canvas.ForceUpdateCanvases();

            TextGenerator textGen = textBubble.GetComponent<Text>().cachedTextGenerator;
            //TextGenerator textGenForLayout = textBubble.GetComponent<Text>().cachedTextGeneratorForLayout;

            int numLines = textGen.lineCount;
            int numSegments = Mathf.CeilToInt((float)numLines / (float)numLinesInBubble);

            IList<UILineInfo> lines = textGen.lines;
            int segmentStartCharIndex = 0;
            int segmentEndCharIndex = 0;
            string segmentText = "";
            int currentLineIndex = 0;

            for (int i = 0; i < numSegments; i++)
            {
                if (currentLineIndex > lines.Count - 1)
                {
                    break;
                }

                int nextSegmentLineIndex = currentLineIndex + numLinesInBubble;

                if (nextSegmentLineIndex >= lines.Count)
                {
                    segmentText = fullText.Substring(segmentStartCharIndex);
                }
                else
                {
                    segmentEndCharIndex = lines[nextSegmentLineIndex].startCharIdx - 1;
                    segmentText = fullText.Substring(segmentStartCharIndex, segmentEndCharIndex - segmentStartCharIndex);
                }

                currentLineIndex += numLinesInBubble;

                segmentStartCharIndex = segmentEndCharIndex + 1;
                segments.Add(segmentText);
            }

            return segments;
        }

        private void setSpeechBubbleTransform(string transformState)
        {
            if (transformState == BUBBLE_LARGE_RIGHT)
            {
                _bubbleArea.GetComponent<RectTransform>().sizeDelta = new Vector2(BUBBLE_LARGE_RIGHT_SIDE_SIZE.x, BUBBLE_LARGE_RIGHT_SIDE_SIZE.y);
                _bubbleArea.GetComponent<RectTransform>().anchoredPosition = new Vector2(BUBBLE_LARGE_RIGHT_SIDE_POS.x, BUBBLE_LARGE_RIGHT_SIDE_POS.y);

                _bubblePoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(BUBBLE_POINT_LEFT_SIDE_POS.x, BUBBLE_POINT_LEFT_SIDE_POS.y);
                _bubblePoint.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            else if (transformState == BUBBLE_LARGE_RIGHT_2)
            {
                _bubbleArea.GetComponent<RectTransform>().sizeDelta = new Vector2(BUBBLE_LARGE_RIGHT_SIDE_SIZE_2.x, BUBBLE_LARGE_RIGHT_SIDE_SIZE_2.y);
                _bubbleArea.GetComponent<RectTransform>().anchoredPosition = new Vector2(BUBBLE_LARGE_RIGHT_SIDE_POS_2.x, BUBBLE_LARGE_RIGHT_SIDE_POS_2.y);

                _bubblePoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(BUBBLE_POINT_LEFT_SIDE_SMALL_POS.x, BUBBLE_POINT_LEFT_SIDE_SMALL_POS.y);
                _bubblePoint.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            else if (transformState == BUBBLE_SMALL_LEFT)
            {
                _bubbleArea.GetComponent<RectTransform>().sizeDelta = new Vector2(BUBBLE_SMALL_LEFT_SIDE_SIZE.x, BUBBLE_SMALL_LEFT_SIDE_SIZE.y);
                _bubbleArea.GetComponent<RectTransform>().anchoredPosition = new Vector2(BUBBLE_SMALL_LEFT_SIDE_POS.x, BUBBLE_SMALL_LEFT_SIDE_POS.y);

                _bubblePoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(BUBBLE_POINT_LEFT_SIDE_SMALL_POS.x, BUBBLE_POINT_LEFT_SIDE_SMALL_POS.y);
                _bubblePoint.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            else if (transformState == BUBBLE_SMALL_RIGHT)
            {
                _bubbleArea.GetComponent<RectTransform>().sizeDelta = new Vector2(BUBBLE_SMALL_RIGHT_SIDE_SIZE.x, BUBBLE_SMALL_RIGHT_SIDE_SIZE.y);
                _bubbleArea.GetComponent<RectTransform>().anchoredPosition = new Vector2(BUBBLE_SMALL_RIGHT_SIDE_POS.x, BUBBLE_SMALL_RIGHT_SIDE_POS.y);

                _bubblePoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(BUBBLE_POINT_RIGHT_SIDE_SMALL_POS.x, BUBBLE_POINT_RIGHT_SIDE_SMALL_POS.y);
                _bubblePoint.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            }
        }

        public void onHitAreaClick()
        {
            if (_speechIsBeingTypedOut)
            {
                _currentSpeechBubble.GetComponent<Text>().text = _currentSpeechSegmentText;
                _speechIsBeingTypedOut = false;
            }
            else
            {
                _currentSpeechSegmentIndex++;

                if (_currentSpeechSegmentIndex > _speechBubbleSegments.Count-1)
                {
                    _currentSpeechBubble.GetComponent<Text>().text = "";
                    _currentSpeechSegmentIndex = 0;
                    _speechIsBeingTypedOut = false;
                    _currentSpeechSegmentText = "";

                    _mrBusinessManager.tweenMrBusinessOut();
                    _speechUIPanel.SetActive(false);
                    _hitArea.gameObject.SetActive(false);
                }
                else
                {
                    _currentSpeechBubble.GetComponent<Text>().text = "";
                    _speechIsBeingTypedOut = true;
                    _speechBubbleTextCurrentIndex = 0;
                    _currentSpeechSegmentText = _speechBubbleSegments[_currentSpeechSegmentIndex];
                }
            }
        }

        public string getCurrentAdSpeechText()
        {
            ItemConfig adItem = JobFactory.itemsManager.getAdItem();
            _fullAdSpeechString = _adDescriptionString + " " + adItem.description;
            return _fullAdSpeechString;
        }

        private void onAdConfirmClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);

            _uiManager.hideAdConfirmPanel();
            _adBtns.SetActive(false);
            _speechUIPanel.SetActive(false);
            _hitArea.gameObject.SetActive(false);

            if (PlatformUtility.isIOS())
            {
                checkIOSAd();
            }

            if (PlatformUtility.isWebGL())
            {
                //_kongWebAdManager.showVideo();
            }

            JobFactory.playFabManager.analytics(AnalyticsEnum.AD_CONFIRM_BUTTON_CLICK);
        }

        private void checkIOSAd()
        {
#if UNITY_IOS
			StartUpManager.showingVideoAd = true;
            soundOff();

			Advertisement.Show("rewardedVideo", new AdShowListeners(_adManager, _uiManager));
#endif
        }

        private void onAdCancelClick() 
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _adBtns.SetActive(false);
            _speechUIPanel.SetActive(false);
            _hitArea.gameObject.SetActive(false);
            _uiManager.hideAdConfirmPanel();
        }

		
#if UNITY_IOS
		private void HandleShowResult(ShowResult result)
        {
            soundOn();

            switch (result)
            {
                case ShowResult.Finished:

                    _adManager.awardAdPrize();
                    _uiManager.alertPopupUI.showAlert("Thanks for your support! Your Reward has been applied! :-)", "Reward!");

                    JobFactory.playFabManager.analytics(AnalyticsEnum.AD_COMPLETED);
                    JobFactory.itemsManager.adWatches++; //Tracking ad watches in the players data, because we should.

                    _adManager.resetAdTime();
                    _uiManager.bottomLeftMenuUI.disableAdButton();

                    break;
                case ShowResult.Skipped:
                    _uiManager.alertPopupUI.showAlert("Ad skipped! Hope you'll try again later!", "Ad skipped!");
                    break;
                case ShowResult.Failed:
                    _uiManager.alertPopupUI.showAlert("No Ads Available! Sorry try again later!", "Error!");
                    break;
            }
            StartUpManager.showingVideoAd = false; 
        }
#endif
        private void soundOn()
        {
            JobFactory.settingsManager.setSoundEnabled(_soundEnabled);
            JobFactory.settingsManager.setMusicEnabled(_musicEnabled);
        }

        public void soundOff()
        {
            _soundEnabled = JobFactory.settingsManager.soundEnabled;
            _musicEnabled = JobFactory.settingsManager.musicEnabled;

            JobFactory.settingsManager.setSoundEnabled(false);
            JobFactory.settingsManager.setMusicEnabled(false);
        }

    }
}

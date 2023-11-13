using com.super2games.idle.component.boosts;
using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class BottomLeftMenuUI : MonoBehaviour
    {
        private readonly Color NORMAL_COLOR = new Color(1, 1, 1);
        private readonly Color DISABLED_COLOR = new Color(.5f, .5f, .5f);
        private readonly Color ENABLED_COLOR = new Color(0, 1, 0);
        private readonly float KEY_PRESS_BUFFER = .2f;

        private readonly string ACHIEVEMENTS_BUTTON = "achievementBtn";
        private readonly string SHOP_BUTTON = "shopBtn";
        private readonly string CHESTS_BUTTON = "briefcaseBtn";
        private readonly string QUESTS_BUTTON = "questBtn";
        private readonly string COLLAPSE_BUTTON = "collapseBtn";
        private readonly string BOOST_BUTTON = "boostBtn";
        private readonly string AD_BUTTON = "adBtn";

        private readonly string ACHIEVEMENTS_NOTIFICATION_ICON = "achievementBtn/notificationIcon";
        private readonly string SHOP_NOTIFICATION_ICON = "shopBtn/notificationIcon";
        private readonly string CHESTS_NOTIFICATION_ICON = "briefcaseBtn/notificationIcon";
        private readonly string QUESTS_NOTIFICATION_ICON = "questBtn/notificationIcon";

        private readonly string BOOST_AMOUNT = "amountEntry/boostAmount";
        private readonly string BOOST_DURATION = "durationEntry/boostDuration";
        private readonly string BUCKS_AMOUNT_TEXT = "costEntry/bucksAmountText";

        private readonly string FX_POWER_UP_BTN_CLICK_01 = "fx_powerup_btn_click_01";
        private readonly string FX_POWER_UP_BTN_CLICK_02 = "fx_powerup_btn_click_02";
        private readonly string FX_POWER_UP_BTN_CLICK_03 = "fx_powerup_btn_click_03";

        private readonly float AD_BUTTON_FLASH_TIME = 29;
        private readonly float BOOST_BUTTON_FLASH_TIME = 37;
 
        private GameObject _boostPopupPanel;
        private Text _boostAmount;
        private Text _boostDuration;
        private Text _bucksAmountText;

        private UIManager _uiManager;
        private ItemsManager _itemsManager;

        private Button _achievementsButton;
        private Button _questsButton;
        private Button _shopButton;
        private Button _chestsButton;
        public Button adButton;
        private Button _boostButton;
        private Button _collapseButton;

        private GameObject _achievementsNotificationDot;
        private GameObject _shopNotificationDot;
        private GameObject _chestsNotificationDot;
        private GameObject _questsNotificationDot;

        private GameObject _panel;

        private float _keyPressBuffer = 0f;
        private bool _hasStarted = false;

        private GameObject _fxBoostClickEffect1;
        private GameObject _fxBoostClickEffect2;
        private GameObject _fxBoostClickEffect3;

        private Animator _fxBoostEffectAnimator1;
        private Animator _fxBoostEffectAnimator2;
        private Animator _fxBoostEffectAnimator3;

        private List<GameObject> _effectsGameObjects = new List<GameObject>();
        private List<Animator> _effectsAnimators = new List<Animator>();
        private List<Animator> _playingAnimators = new List<Animator>();
        private List<GameObject> _playerEffectsGameObjects = new List<GameObject>();

        private int effectIndex = 0;

        private double _boostBucksAmount = 0;

        public bool isCollapsed = true;

        private Animator _adButtonFlashAnimator;
        private Animator _boostButtonFlashAnimator;

        private float _adButtonFlashCount = 0;
        private float _boostButtonFlashCount = 0;

        private bool _adFlashIsPlaying = false;
        private bool _boostFlashIsPlaying = false;

        private List<Button> _buttons = new List<Button>();

        public void initialize(GameObject panel, UIManager uiManager, ItemsManager itemsManager)
        {
            _panel = panel;
            _uiManager = uiManager;
            _itemsManager = itemsManager;

            _achievementsButton = panel.transform.Find(ACHIEVEMENTS_BUTTON).gameObject.GetComponent<Button>();
            _shopButton = panel.transform.Find(SHOP_BUTTON).gameObject.GetComponent<Button>();
            _chestsButton = panel.transform.Find(CHESTS_BUTTON).gameObject.GetComponent<Button>();
            _questsButton = panel.transform.Find(QUESTS_BUTTON).gameObject.GetComponent<Button>();
            _collapseButton = panel.transform.Find(COLLAPSE_BUTTON).gameObject.GetComponent<Button>();
            adButton = panel.transform.Find(AD_BUTTON).gameObject.GetComponent<Button>();
            _boostButton = panel.transform.Find(BOOST_BUTTON).gameObject.GetComponent<Button>();

            _achievementsNotificationDot = panel.transform.Find(ACHIEVEMENTS_NOTIFICATION_ICON).gameObject;
            _shopNotificationDot = panel.transform.Find(SHOP_NOTIFICATION_ICON).gameObject;
            _chestsNotificationDot = panel.transform.Find(CHESTS_NOTIFICATION_ICON).gameObject;
            _questsNotificationDot = panel.transform.Find(QUESTS_NOTIFICATION_ICON).gameObject;

            _boostPopupPanel = _uiManager.temporaryBoostPanel;
            _boostAmount = _boostPopupPanel.transform.Find(BOOST_AMOUNT).gameObject.GetComponent<Text>();
            _boostDuration = _boostPopupPanel.transform.Find(BOOST_DURATION).gameObject.GetComponent<Text>();
            _bucksAmountText = _boostPopupPanel.transform.Find(BUCKS_AMOUNT_TEXT).gameObject.GetComponent<Text>();

            _adButtonFlashAnimator = adButton.gameObject.GetComponent<Animator>();
            _boostButtonFlashAnimator = _boostButton.gameObject.GetComponent<Animator>();

            _achievementsButton.onClick.AddListener(onAchievementsClick);
            _shopButton.onClick.AddListener(onShopClick);
            _chestsButton.onClick.AddListener(onChestsClick);
            _questsButton.onClick.AddListener(onQuestsButtonClick);
            adButton.onClick.AddListener(onAdButtonClick);
            _boostButton.onClick.AddListener(onBoostButtonClick);
            _collapseButton.onClick.AddListener(onCollapseButtonClick);

            _buttons.Add(_achievementsButton);
            _buttons.Add(_shopButton);
            _buttons.Add(_chestsButton);
            _buttons.Add(_questsButton);
            _buttons.Add(adButton);
            _buttons.Add(_boostButton);
            _buttons.Add(_collapseButton);

            _fxBoostClickEffect1 = _boostButton.transform.Find(FX_POWER_UP_BTN_CLICK_01).gameObject;
            _fxBoostClickEffect2 = _boostButton.transform.Find(FX_POWER_UP_BTN_CLICK_02).gameObject;
            _fxBoostClickEffect3 = _boostButton.transform.Find(FX_POWER_UP_BTN_CLICK_03).gameObject;

            _fxBoostEffectAnimator1 = _fxBoostClickEffect1.GetComponent<Animator>();
            _fxBoostEffectAnimator2 = _fxBoostClickEffect2.GetComponent<Animator>();
            _fxBoostEffectAnimator3 = _fxBoostClickEffect3.GetComponent<Animator>();

            _effectsGameObjects.Add(_fxBoostClickEffect1);
            _effectsGameObjects.Add(_fxBoostClickEffect2);
            _effectsGameObjects.Add(_fxBoostClickEffect3);

            _effectsAnimators.Add(_fxBoostEffectAnimator1);
            _effectsAnimators.Add(_fxBoostEffectAnimator2);
            _effectsAnimators.Add(_fxBoostEffectAnimator3);

            _hasStarted = true;

            _boostBucksAmount = double.Parse((JobFactory.modelManager.globalVarsModel.getConfig(GlobalVarEnum.TEMP_BOOST_BUCKS_AMOUNT) as GlobalVarConfig).value);
        }

        void Update()
        {
            if (!_hasStarted)
            {
                return;
            }

            updateButtonFlashesStop();
            updateButtonFlashes();
            updatePayBoostPanel();
            updateKeys();
            updateStopEffectAnimation();
        }

        public void setButtonEnablementByID(string uiID, bool enabled)
        {
            if (uiID == UIEnum.ALL_LEFT) setAllButtonsEnablement(enabled);
            else if (uiID == UIEnum.ACHIEVEMENTS) setButtonEnablement(_achievementsButton, enabled);
            else if (uiID == UIEnum.SHOP) setButtonEnablement(_shopButton, enabled);
            else if (uiID == UIEnum.BRIEFCASES) setButtonEnablement(_chestsButton, enabled);
            else if (uiID == UIEnum.QUESTS) setButtonEnablement(_questsButton, enabled);
			else if (uiID == UIEnum.AD) setButtonEnablement(adButton, enabled);
			else if (uiID == UIEnum.BOOST || uiID == UIEnum.OPEN_BOOST_PANEL) setButtonEnablement(_boostButton, enabled);
            else if (uiID == UIEnum.COLLAPSE_LEFT) setButtonEnablement(_collapseButton, enabled);
        }

        public bool isHiddenByCollapse(string uiID)
        {
            if (uiID == UIEnum.ACHIEVEMENTS || uiID == UIEnum.SHOP || uiID == UIEnum.BRIEFCASES || uiID == UIEnum.QUESTS) return true;
            return false;
        }

        public void checkTutorialUseCase(string uiID)
        {
            if (!JobFactory.tutorialManager.isUIFunctional(uiID)) return;
            if (isHiddenByCollapse(uiID) && isCollapsed) transitionCollapse();
        }

        private void setButtonEnablement(Button button, bool enabled)
        {
            button.enabled = enabled;
            if (enabled) lighten(button.gameObject);
            else darken(button.gameObject);
        }

        public void setAllButtonsEnablement(bool enabled)
        {
            for (int i = 0; i < _buttons.Count; ++i)
            {
                setButtonEnablement(_buttons[i], enabled);
            }
        }

        private void lighten(GameObject go)
        {
            go.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }

        private void darken(GameObject go)
        {
            go.GetComponent<Image>().color = new Color(.4f, .4f, .4f);
        }

        private void updateButtonFlashes()
        {
            if (!adButton.gameObject.activeSelf || !_boostButton.gameObject.activeSelf) return; 
            _adButtonFlashCount += Time.deltaTime;
            _boostButtonFlashCount += Time.deltaTime;
            if (_adButtonFlashCount >= AD_BUTTON_FLASH_TIME)
            {
                if (!adButton.GetComponent<Button>().enabled) return; //No need to play it if it's not enabled.
                _adButtonFlashAnimator.playbackTime = 0;
				_adButtonFlashAnimator.Play("ad_btn_static");
                _adButtonFlashCount = 0;
                _adFlashIsPlaying = true;
            }
            if (_boostButtonFlashCount >= BOOST_BUTTON_FLASH_TIME)
            {
                _boostButtonFlashAnimator.playbackTime = 0;
				_boostButtonFlashAnimator.Play("boost_btn_flash");
                _boostButtonFlashCount = 0;
                _boostFlashIsPlaying = true;
            }
        }

        private void updateButtonFlashesStop()
        {
            if (!_adFlashIsPlaying && !_boostFlashIsPlaying) return; //If nothing is playing don't go any further or you will get warnings for GetCurrentAnimatorStateInfo
            AnimatorStateInfo asiAd = _adButtonFlashAnimator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo asiBoost = _boostButtonFlashAnimator.GetCurrentAnimatorStateInfo(0);
            if (_adFlashIsPlaying && asiAd.normalizedTime >= .95)
            {
                _adButtonFlashAnimator.playbackTime = 0;
				_adButtonFlashAnimator.Play("ad_btn_idle");
                _adFlashIsPlaying = false;
            }
            if (_boostFlashIsPlaying && asiBoost.normalizedTime >= .95)
            {
                _boostButtonFlashAnimator.playbackTime = 0;
				_boostButtonFlashAnimator.Play("boost_btn_idle");
                _boostFlashIsPlaying = false;
            }
        }

        private void updatePayBoostPanel()
        {
            BoostComponent boostComponent = _itemsManager.getPayBoostComponent();
            if (boostComponent != null)
            {
                _boostDuration.text = StringUtility.formatSecondsNoLabels(boostComponent.timeOutCount);
                _boostAmount.text = StringUtility.percentToString(boostComponent.amount);
            }
            else
            {
                _boostAmount.text = "0%";
            }

            if (_itemsManager.isTimedBoostFree())
            {
                _bucksAmountText.text = "FREE";
            }
            else
            {
                _bucksAmountText.text = "-" + _boostBucksAmount;
            }
        }

        private void updateKeys()
        {
            _keyPressBuffer += Time.deltaTime;

            if (_keyPressBuffer < KEY_PRESS_BUFFER || !_panel.activeInHierarchy)
            {
                return;
            }

            else if (InputManager.key_down_3)
            {
                onQuestsButtonClick();
                _keyPressBuffer = 0;
            }
            else if (InputManager.key_down_4)
            {
                onChestsClick();
                _keyPressBuffer = 0;
            }
            else if (InputManager.key_down_5)
            {
                onShopClick();
                _keyPressBuffer = 0;
            }
            else if (InputManager.key_down_6)
            {
                onAchievementsClick();
                _keyPressBuffer = 0;
            }
        }

        public void highlightUI(string uiID, string fingerRotationState)
        {
            if (uiID == UIEnum.ACHIEVEMENTS) _uiManager.createHighlight(_achievementsButton.gameObject, fingerRotationState);
            else if (uiID == UIEnum.BRIEFCASES) _uiManager.createHighlight(_chestsButton.gameObject, fingerRotationState);
            else if (uiID == UIEnum.QUESTS) _uiManager.createHighlight(_questsButton.gameObject, fingerRotationState);
            else if (uiID == UIEnum.SHOP) _uiManager.createHighlight(_shopButton.gameObject, fingerRotationState);
            else if (uiID == UIEnum.AD) _uiManager.createHighlight(adButton.gameObject, fingerRotationState);
            else if (uiID == UIEnum.BOOST || uiID == UIEnum.OPEN_BOOST_PANEL) _uiManager.createHighlight(_boostButton.gameObject, fingerRotationState);
            else if (uiID == UIEnum.COLLAPSE_LEFT) _uiManager.createHighlight(_collapseButton.gameObject, fingerRotationState);
        }

        private void updateStopEffectAnimation()
        {
            for (int i = 0; i < _playingAnimators.Count; ++i)
            {
                GameObject effect = _playerEffectsGameObjects[i];
                Animator animator = _playingAnimators[i];
                AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
                if (asi.normalizedTime >= .95)
                {
					animator.enabled = false;
                    animator.playbackTime = 0;
					effect.SetActive(false);
                    _playingAnimators.Remove(animator);
                    _playerEffectsGameObjects.Remove(effect);
                }
            }
        }

        private bool isAnyEffectPlaying()
        {
            return (_fxBoostEffectAnimator1.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 || _fxBoostEffectAnimator2.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 || _fxBoostEffectAnimator3.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        }

        public void playBoostEffect()
        {
            Animator animator = _effectsAnimators[effectIndex];
            GameObject effect = _effectsGameObjects[effectIndex];
            effect.SetActive(true);
            animator.playbackTime = 0;
			animator.Play("boost_btn_click_0" + (effectIndex + 1));
            if (!_playingAnimators.Contains(animator)) _playingAnimators.Add(animator);
            if (!_playerEffectsGameObjects.Contains(effect)) _playerEffectsGameObjects.Add(effect);
            incrementBoostEffectIndex();
        }

        private void incrementBoostEffectIndex()
        {
            effectIndex++;
            if (effectIndex >= _effectsGameObjects.Count) effectIndex = 0;
        }

        public void enabledAdButton()
        {   //TODO: Uncomment when you need to enable Ads
			if (!JobFactory.tutorialManager.isComplete) return;
			//ConsoleUtility.Log("[BottomLeftMenuUI].enabledAdButton -- ENABLE AD BUTTON");
			adButton.GetComponent<Button>().enabled = true;
			lighten(adButton.gameObject);
		}

        public void disableAdButton()
		{   //TODO: Uncomment when you need to enable Ads
			//ConsoleUtility.Log("[BottomLeftMenuUI].disableAdButton -- DISABLE AD BUTTON");
			adButton.GetComponent<Button>().enabled = false;
			darken(adButton.gameObject);
		}

        private void onAdButtonClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.AD)) return;
			//ConsoleUtility.Log("[BottomLeftMenuUI].onAdButtonClick -- AD BUTTON CLICK");
			_uiManager.hideAllPanels();
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.showAdConfirmPanel();
            JobFactory.playFabManager.analytics(AnalyticsEnum.AD_BUTTON_CLICK);
        }

        private void onBoostButtonClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.BOOST) && !JobFactory.tutorialManager.isUIFunctional(UIEnum.OPEN_BOOST_PANEL)) return;

            if (!_uiManager.temporaryBoostPanel.activeSelf)
            {
                _uiManager.hideAllPanels();
                _uiManager.showTemporaryBoostPanel();
                if (isCollapsed)_boostPopupPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 60); //For Boost panel positioning
                else _boostPopupPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(300, 60);
                JobFactory.recordsManager.uiClick(UIEnum.OPEN_BOOST_PANEL);
                return;
            }

            JobFactory.playFabManager.analytics(AnalyticsEnum.TIMED_BOOST_BUTTON_CLICKED);

            if (!_itemsManager.isTimedBoostFree() && JobFactory.player.bucksAmount < _boostBucksAmount)
            {
                _uiManager.showBucksPurchasePanel();
                JobFactory.playFabManager.analytics(AnalyticsEnum.PURCHASE_BUCKS_POPUP_SHOWN_BY_TIMED_BOOST_BUTTON);
                return;
            }

            if (_itemsManager.isTimedBoostFree())
            {
                JobFactory.playFabManager.analytics(AnalyticsEnum.FREE_TIMED_BOOST_APPLIED);
            }
            else
            {
                JobFactory.playFabManager.analytics(AnalyticsEnum.PAID_TIMED_BOOST_APPLIED);
            }

            JobFactory.recordsManager.uiClick(UIEnum.BOOST);
            _itemsManager.awardPayBoost();
        }

        private void onChestsClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.BRIEFCASES)) return;

            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            hideChestsDot();
            if (_uiManager.chestsPanel.activeSelf)
            {
                _uiManager.hideChestsPanel();
            }
            else
            {
                _uiManager.hideAllPanels();
                _uiManager.showChestsPanel();
            }
            JobFactory.recordsManager.uiClick(UIEnum.BRIEFCASES);
            JobFactory.playFabManager.analytics(AnalyticsEnum.CASE_BUTTON_CLICKED);
        }

        private void onShopClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.SHOP)) return;

            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            hideShopDot();
            if (_uiManager.shopPanel.activeSelf)
            {
                _uiManager.hideShopPanel();
            }
            else
            {
                _uiManager.hideAllPanels();
                _uiManager.showShopPanel();
            }
            JobFactory.recordsManager.uiClick(UIEnum.SHOP);
            JobFactory.playFabManager.analytics(AnalyticsEnum.STORE_BUTTON_CLICKED);
        }

        private void onAchievementsClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.ACHIEVEMENTS)) return;

            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            hideAchievementsDot();
            if (_uiManager.achievementsPanel.activeSelf)
            {
                _uiManager.hideAchievementsPanel();
            }
            else
            {
                _uiManager.hideAllPanels();
                _uiManager.showAchievementPanel();
            }
            JobFactory.recordsManager.uiClick(UIEnum.ACHIEVEMENTS);
        }

        private void onQuestsButtonClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.QUESTS)) return;

            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            hideQuestsDot();
            if (_uiManager.questPanel.activeSelf)
            {
                _uiManager.hideQuestPanel();
            }
            else
            {
                _uiManager.hideAllPanels();
                _uiManager.showQuestPanel();
            }
            JobFactory.recordsManager.uiClick(UIEnum.QUESTS);
        }

        private void onCollapseButtonClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.COLLAPSE_LEFT)) return;

            if (iTween.Count(_uiManager.bottomLeftMenuPanel) > 0 || iTween.Count(_collapseButton.gameObject) > 0)
            {
                return;
            }

            _uiManager.hideTemporaryBoostPanel(); //Hide this panel if showing

            if (!JobFactory.tutorialManager.isComplete) _uiManager.removeHighlights();

            transitionCollapse();
        }

        public void transitionCollapse()
        {
            int XPosExpanded = 34;
            int XPosClosed = -205;

            RectTransform panelRectTransform = _panel.GetComponent<RectTransform>();

            if (isCollapsed)
            {
                //iTween.MoveTo(_panel, iTween.Hash("x", -690, "easeType", "easeInOutCubic", "time", .75f, "isLocal", true, "onComplete", "onCollapseTransitionComplete"));

                iTween.ValueTo(_panel, iTween.Hash(
                "from", panelRectTransform.anchoredPosition,
                "to", new Vector2(XPosExpanded, panelRectTransform.anchoredPosition.y),
                "easetype", "easeInOutCubic",
                "time", .75f,
                "onupdatetarget", this.gameObject,
                "onupdate", "movePanel",
				"onComplete", "onCollapseTransitionComplete"));

                iTween.RotateTo(_collapseButton.gameObject, iTween.Hash("z", 180, "easeType", "easeInOutCubic", "time", .75f, "isLocal", true));
                isCollapsed = false;
            }
            else
            {
                //iTween.MoveTo(_panel, iTween.Hash("x", -464, "easeType", "easeInOutCubic", "time", .75f, "isLocal", true, "onComplete", "onCollapseTransitionComplete"));

                iTween.ValueTo(_panel, iTween.Hash(
                "from", panelRectTransform.anchoredPosition,
                "to", new Vector2(XPosClosed, panelRectTransform.anchoredPosition.y),
                "easetype", "easeInOutCubic",
                "time", .75f,
                "onupdatetarget", this.gameObject,
                "onupdate", "movePanel",
				"onComplete", "onCollapseTransitionComplete"));

                iTween.RotateTo(_collapseButton.gameObject, iTween.Hash("z", 0, "easeType", "easeInOutCubic", "time", .75f, "isLocal", true));
                isCollapsed = true;
            }
        }

        private void movePanel(Vector2 position)
        {
            RectTransform panelRectTransform = _panel.GetComponent<RectTransform>();
            panelRectTransform.anchoredPosition = position;
        }

        public void collapse()
        {
            if (!isCollapsed) transitionCollapse();
        }

        private void onCollapseTransitionComplete()
        {   //We want the click to record when it's done transitioning. Mostly for the tutorial.
            JobFactory.recordsManager.uiClick(UIEnum.COLLAPSE_LEFT);
        }

        public void onQuestsButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Quests" : "Quests (3)";
            setTooltip(text, _questsButton);
        }

        public void onChestsButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Briefcases" : "Briefcases (4)";
            setTooltip(text, _chestsButton);
        }

        public void onShopButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Shop" : "Shop (5)";
            setTooltip(text, _shopButton);
        }

        public void onAchievementsButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Achievements" : "Achievements (6)";
            setTooltip(text, _achievementsButton);
        }

        public void onAdButtonMouseEnter()
        {
            string text = "Ad";
            setTooltip(text, adButton);
        }

        public void onBoostButtonMouseEnter()
        {
            string text = "Boost";
            setTooltip(text, _boostButton);
        }

        private void setTooltip(string text, Button button)
        {
            TooltipUI.instance.displaySimpleTooltip(text, button.gameObject.transform.position, TooltipUI.TOOLTIP_TYPE_GENERIC, "", 50, 0);
        }

        public void onButtonMouseOut()
        {
            TooltipUI.instance.hideTooltip();
        }

        public void showAchievementsDot()
        {
            _achievementsNotificationDot.SetActive(true);
        }

        public void showShopDot()
        {
            _shopNotificationDot.SetActive(true);
        }

        public void showChestsDot()
        {
            _chestsNotificationDot.SetActive(true);
        }

        public void showQuestsDot()
        {
            _questsNotificationDot.SetActive(true);
        }

        public void hideAchievementsDot()
        {
            _achievementsNotificationDot.SetActive(false);
        }

        public void hideShopDot()
        {
            _shopNotificationDot.SetActive(false);
        }

        public void hideChestsDot()
        {
            _chestsNotificationDot.SetActive(false);
        }

        public void hideQuestsDot()
        {
            _questsNotificationDot.SetActive(false);
        }
    }
}

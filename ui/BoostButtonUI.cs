using com.super2games.idle.component.boosts;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
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
    public class BoostButtonUI
    {
        private readonly string HOTSPOT = "hotspot";
        private readonly string POWER_UP_DURATION = "powerupDuration";
        private readonly string BUCKS_AMOUNT_TEXT = "bucksAmountText";
        private readonly string POWER_UP_AMOUNT = "powerupAmount";
        private readonly string FX_POWER_UP_BTN_CLICK = "fx_powerup_btn_click";

        private readonly string FX_POWER_UP_BTN_CLICK_01 = "fx_powerup_btn_click_01";
        private readonly string FX_POWER_UP_BTN_CLICK_02 = "fx_powerup_btn_click_02";
        private readonly string FX_POWER_UP_BTN_CLICK_03 = "fx_powerup_btn_click_03";

        private readonly int BUCKS_AMOUNT = 25;

        private Button _boostButton;
        private Text _boostDuration;
        private Text _bucksAmount;
        private Text _boostAmount;

        private GameObject _fxBoostClickEffect1;
        private GameObject _fxBoostClickEffect2;
        private GameObject _fxBoostClickEffect3;

        private Animator _fxBoostEffectAnimator1;
        private Animator _fxBoostEffectAnimator2;
        private Animator _fxBoostEffectAnimator3;

        private List<GameObject> _effectsGameObjects = new List<GameObject>();
        private List<Animator> _effectsAnimators = new List<Animator>();

        private int effectIndex = 0;

        private ItemsManager _itemsManager;
        private UIManager _uiManager;

        public BoostButtonUI(GameObject panel, ItemsManager itemsManager, UIManager uiManager)
        {
            _itemsManager = itemsManager;
            _uiManager = uiManager;

            _boostButton = panel.GetComponent<Button>();
            _fxBoostClickEffect1 = panel.transform.Find(FX_POWER_UP_BTN_CLICK_01).gameObject;
            _fxBoostClickEffect2 = panel.transform.Find(FX_POWER_UP_BTN_CLICK_02).gameObject;
            _fxBoostClickEffect3 = panel.transform.Find(FX_POWER_UP_BTN_CLICK_03).gameObject;

            _fxBoostEffectAnimator1 = _fxBoostClickEffect1.GetComponent<Animator>();
            _fxBoostEffectAnimator2 = _fxBoostClickEffect2.GetComponent<Animator>();
            _fxBoostEffectAnimator3 = _fxBoostClickEffect3.GetComponent<Animator>();

            _effectsGameObjects.Add(_fxBoostClickEffect1);
            _effectsGameObjects.Add(_fxBoostClickEffect2);
            _effectsGameObjects.Add(_fxBoostClickEffect3);

            _effectsAnimators.Add(_fxBoostEffectAnimator1);
            _effectsAnimators.Add(_fxBoostEffectAnimator2);
            _effectsAnimators.Add(_fxBoostEffectAnimator3);

            _boostButton.onClick.AddListener(onBoostButtonClick);
        }

        public void update()
        {
            if (isAnyEffectPlaying())
            {   //Stop them if the timing is right.
                stopEffectAnimation(_fxBoostClickEffect1, _fxBoostEffectAnimator1);
                stopEffectAnimation(_fxBoostClickEffect2, _fxBoostEffectAnimator2);
                stopEffectAnimation(_fxBoostClickEffect3, _fxBoostEffectAnimator3);
            }
        }

        private void stopEffectAnimation(GameObject effect, Animator animator)
        {
            AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
            if (asi.normalizedTime >= .95)
            {
				animator.enabled = false;
				animator.playbackTime = 0;
                effect.SetActive(false);
            }
        }

        private bool isAnyEffectPlaying()
        {
            return (_fxBoostEffectAnimator1.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 || _fxBoostEffectAnimator2.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 || _fxBoostEffectAnimator3.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        }

        public void playEffect()
        {
            _effectsGameObjects[effectIndex].SetActive(true);
            _effectsAnimators[effectIndex].playbackTime = 0;
			_effectsAnimators[effectIndex].Play("boost_btn_click_0" + (effectIndex + 1));
            incrementIndex();
        }

        private void incrementIndex()
        {
            effectIndex++;
            if (effectIndex >= _effectsGameObjects.Count) effectIndex = 0;
        }

        public void highlightUI(string uiID)
        {
            //if (uiID == UIEnum.BOOST) _uiManager.createHighlight(_boostButton.gameObject);
        }

        private void onBoostButtonClick()
        {
            JobFactory.playFabManager.analytics(AnalyticsEnum.TIMED_BOOST_BUTTON_CLICKED);

            if (!_itemsManager.isTimedBoostFree() && JobFactory.player.bucksAmount < BUCKS_AMOUNT)
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

    }
}

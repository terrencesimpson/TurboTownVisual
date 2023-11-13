using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.manager;

namespace com.super2games.idle.ui
{
    public class AdButtonEffect : MonoBehaviour
    {
        private readonly string LIT_UP_GRAPHIC = "adLit";
        private readonly string AD_BTN = "adButton";
        private readonly int FLICKER_TIME_LOW_END = 120;
        private readonly int FLICKER_TIME_HIGH_END = 300;

        private float _flickerTime = 600;
        private Image _litUpGraphic;
        private Button _adBtn;

        public bool isOn = false;

        void Start()
        {
            _adBtn = transform.Find(AD_BTN).GetComponent<Button>();
            _litUpGraphic = _adBtn.transform.Find(LIT_UP_GRAPHIC).gameObject.GetComponent<Image>();
        }

        void Update()
        {
            _flickerTime -= Time.deltaTime;

            if (_flickerTime < 0)
            {
                _flickerTime = UnityEngine.Random.Range(FLICKER_TIME_LOW_END, FLICKER_TIME_HIGH_END);
                flicker();
            }
        }

        public void flicker()
        {
            if (iTween.Count(gameObject) > 1 || !isOn)
            {
                return;
            }

            iTween.ValueTo(gameObject, iTween.Hash(
                "from", 0f,
                "to", 255f,
                "time", .5f,
                "onupdatetarget", gameObject,
                "onupdate", "tweenOnUpdateCallBack",
                "easetype", iTween.EaseType.easeInOutElastic
                ));
        }

        public void turnLightsOn()
        {
            if (iTween.Count(gameObject) > 1 || isOn)
            {
                return;
            }

            isOn = true;

            iTween.ValueTo(gameObject, iTween.Hash(
                "from", 0f,
                "to", 255f,
                "time", .5f,
                "onupdatetarget", gameObject,
                "onupdate", "tweenOnUpdateCallBack",
                "easetype", iTween.EaseType.easeInOutElastic
                ));
        }

        public void tweenOnUpdateCallBack(float newValue)
        {
            if (_litUpGraphic == null)
            {
                return;
            }

            Color c = _litUpGraphic.color;
            c.a = newValue;
            _litUpGraphic.color = c;
        }

        public void turnLightsOff()
        {
            if (iTween.Count(gameObject) > 1 || !isOn)
            {
                return;
            }

            isOn = false;

            iTween.ValueTo(gameObject, iTween.Hash(
                "from", 255f,
                "to", 0f,
                "time", .5f,
                "onupdatetarget", gameObject,
                "onupdate", "tweenOnUpdateCallBack",
                "easetype", iTween.EaseType.easeInOutElastic
                ));
        }

        public void turnButtonOn()
        {
            if (isOn)
            {
                turnLightsOff();
            }
            else
            {
                turnLightsOn();
            }
        }

    }
}

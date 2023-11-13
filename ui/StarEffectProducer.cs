using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.super2games.idle.ui
{
    public class StarEffectProducer : MonoBehaviour
    {
        private readonly string STAR_PATH = "Prefabs/ui/FX_Star";

        public float minRange = 1;
        public float maxRange = 3;

        public float produceAtTime = 0;
        public float currentTime = 0;

        void Update()
        {
            if (!gameObject.activeSelf || !gameObject.transform.parent.gameObject.activeSelf) return; //No need to update if this or the parent is not active.

            if (produceAtTime == 0) findRange();

            currentTime += Time.deltaTime;

            if (currentTime >= produceAtTime)
            {
                reset();
                produceStar();
            }
        }

        private void findRange()
        {
            produceAtTime = UnityEngine.Random.Range(minRange, maxRange);
        }

        private void reset()
        {
            currentTime = 0;
            findRange();
        }

        private void produceStar()
        {
            GameObject starGO = GameObjectUtility.instantiateGameObject(STAR_PATH);
            //iTween.MoveTo(starGO, iTween.Hash("x", GAME_CAMERA_GAME_POSITION.x, "y", GAME_CAMERA_GAME_POSITION.y, "z", GAME_CAMERA_GAME_POSITION.z, "easetype", "easeInOutExpo", "islocal", true, "oncomplete", "onStarTweenComplete", "time", 4f));
        }

        private void onStarTweenComplete()
        {

        }

    }
}

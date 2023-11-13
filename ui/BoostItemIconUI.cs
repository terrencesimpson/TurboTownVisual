using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using com.super2games.idle.factory;

namespace com.super2games.idle.ui
{
    public class BoostItemIconUI : AbstractIcon, IPointerEnterHandler, IPointerExitHandler
    {
        private readonly string BASE_PATH = "Textures/ui/boosts/";
        private readonly string STARS_CONTAINER = "stars";
        private readonly string STAR_ICON= "Prefabs/ui/StarIcon";
        private GameObject starContainer;

        public BoostItemIconUI()
        {
            this.basePath = BASE_PATH;
        }

        public override void setIcon(string itemID, string imgPath = "", double rankNum=0, bool isPlayerBoostIcon = false, bool resizeImageToFitSprite = false, bool isSpacer = false)
        {
            base.setIcon(itemID, imgPath, rankNum, isPlayerBoostIcon);
            setRank(rankNum);
        }

        public void setRank(double rankNum)
        {
            starContainer = transform.Find(STARS_CONTAINER).transform.gameObject;

            foreach (Transform child in starContainer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            for (int i = 0; i < rankNum; i++)
            {
                GameObject star = JobFactory.prefabManager.getPrefab(STAR_ICON);
                star.transform.SetParent(starContainer.transform, false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipUI.instance.displayItemIDBasedTooltip(iconID, transform.position, TooltipUI.TOOLTIP_TYPE_TITLE_AND_DESCRIPTION, TooltipUI.ASSET_TYPE_BOOST_ICON);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipUI.instance.hideTooltip();
        }
    }
}

using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class PlayerSlotIconUI : AbstractIcon, IPointerEnterHandler, IPointerExitHandler
    {
        private static readonly string BASE_PATH = "Textures/ui/boosts/";

        public override void setIcon(string itemID, string imgPath = "", double rankNum = 0, bool isPlayerBoostIcon = false, bool resizeImageToFitSprite = false, bool isSpacer = false)
        {
            this.basePath = BASE_PATH;
            base.setIcon(itemID, imgPath, rankNum, isPlayerBoostIcon);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipUI.instance.displayItemIDBasedTooltip(iconID, transform.position, TooltipUI.TOOLTIP_TYPE_TITLE_AND_DESCRIPTION, TooltipUI.ASSET_TYPE_PLAYER_SLOT_BOOST_ICON);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipUI.instance.hideTooltip();
        }
    }
}

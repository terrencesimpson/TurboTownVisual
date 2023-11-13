using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace com.super2games.idle.ui
{
    public class ResourceIconUI : AbstractIcon, IPointerEnterHandler, IPointerExitHandler
    {
        public static readonly string BASE_PATH = "Textures/ui/icons/";

        public ResourceIconUI()
        {
            basePath = BASE_PATH;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipUI.instance.displayItemIDBasedTooltip(iconID, transform.position, TooltipUI.TOOLTIP_TYPE_GENERIC, TooltipUI.ASSET_TYPE_RESOURCE_ICON);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipUI.instance.hideTooltip();
        }
    }
}

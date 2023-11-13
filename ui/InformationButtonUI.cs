using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using com.super2games.idle.factory;
using com.super2games.idle.ui;

public class InformationButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipUI.instance.displayItemIDBasedTooltip(JobFactory.buildingManager.currentSelectedBuilding.id, transform.position, TooltipUI.TOOLTIP_TYPE_GENERIC, TooltipUI.ASSET_TYPE_BUILDING_ICON);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.instance.hideTooltip();
    }
}

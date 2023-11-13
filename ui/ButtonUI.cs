using com.super2games.idle.component.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.super2games.idle.ui
{
    public class ButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        public IButtonViewComponent buttonComponent;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (buttonComponent != null)
            {
                buttonComponent.onButtonDown(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (buttonComponent != null)
            {
                buttonComponent.onButtonUp(eventData);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buttonComponent != null)
            {
                buttonComponent.onButtonExit(eventData);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (buttonComponent != null)
            {
                buttonComponent.onButtonClick(eventData);
            }
        }
    }
}

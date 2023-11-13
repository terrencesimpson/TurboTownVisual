using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public abstract class AbstractIcon : MonoBehaviour
    {
        protected string basePath;
        public string iconID = "";
        public string iconPath = "";
        public Image iconImage;

        //isSpacer is used for dumb scroll lists because they don't "see" the last entry and you just need something to space out the end so the last entry can be seen.
        public virtual void setIcon(string itemID, string imgPath="", double rank=0, bool isPlayerBoostIcon = false, bool resizeImageToFitSprite = false, bool isSpacer = false)
        {
            if (isSpacer)
            {
                GetComponent<Image>().gameObject.SetActive(false);
                return;
            }

            if (imgPath == "")
            {
                iconPath = itemID;
            }
            else
            {
                iconPath = imgPath;
            }

            Sprite sprite = Resources.Load(basePath + iconPath, typeof(Sprite)) as Sprite;
            if (sprite != null)
            {

                if (GetComponent<Image>() != null)
                {
                    iconImage = GetComponent<Image>();
                }
                else
                {
                    Transform child = transform.Find("image");
                    iconImage = child.GetComponent<Image>();
                }

                iconImage.sprite = sprite;

                if (resizeImageToFitSprite)
                {
                    iconImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);

                    LayoutElement layoutElem = GetComponent<LayoutElement>();

                    if (layoutElem != null)
                    {
                        layoutElem.preferredWidth = sprite.rect.width;
                        layoutElem.preferredHeight = sprite.rect.height;
                    }
                }
            }

            iconID = itemID;
        }

        public virtual void adjustForPlayerBoost()
        {
            iconImage.transform.localScale = new Vector3(.5f, .5f, .5f);
        }
    }
}

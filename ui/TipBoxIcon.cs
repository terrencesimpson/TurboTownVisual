using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace com.super2games.idle.ui
{
    public class TipBoxIcon : AbstractIcon
    {
        private readonly string BASE_PATH = "Textures/ui/icons/tipBox/";

        public TipBoxIcon()
        {
            basePath = BASE_PATH;
        }
    }
}

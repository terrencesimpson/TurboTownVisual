using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class BoostPlayerIcon : AbstractIcon
    {
        private readonly string BASE_PATH = "Textures/ui/playerBoost/";

        public BoostPlayerIcon()
        {
            basePath = BASE_PATH;
        }

    }
}

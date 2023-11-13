using com.super2games.idle.factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.super2games.idle.ui.debug
{
    public class DebugUI : MonoBehaviour
    {

        public void unlinkPlayer()
        {
            JobFactory.playFabManager.unlinkAndroidDevice();
        }

        public void linkPlayer()
        {
            JobFactory.playFabManager.linkAndroidDevice();
        }

    }
}

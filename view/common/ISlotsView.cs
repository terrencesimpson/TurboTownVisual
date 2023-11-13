using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.super2games.idle.view.common
{
    public interface ISlotsView : IView
    {
        List<GameObject> slots { get; }
        void changeSlotMaterial(GameObject slot, string materialPath);
    }
}

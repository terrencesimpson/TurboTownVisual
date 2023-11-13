using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.super2games.idle.view
{
    public interface IView
    {
        GameObject view { get; }
        void release();
        void refresh();
    }
}

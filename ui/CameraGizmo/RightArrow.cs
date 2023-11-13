using UnityEngine;
using System;
using System.Collections;

public class RightArrow : MonoBehaviour {

    void OnMouseDown()
    {
        CameraGizmo.instance.rightArrowClicked();
    }
}

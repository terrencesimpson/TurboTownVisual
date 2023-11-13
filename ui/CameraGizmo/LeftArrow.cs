using UnityEngine;
using System.Collections;

public class LeftArrow : MonoBehaviour {

    void OnMouseDown()
    {
        CameraGizmo.instance.leftArrowClicked();
    }
}

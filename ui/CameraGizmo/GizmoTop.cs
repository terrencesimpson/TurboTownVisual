using UnityEngine;
using System.Collections;

public class GizmoTop : MonoBehaviour {

    void OnMouseDown()
    {
        CameraGizmo.instance.topClicked();
    }
}

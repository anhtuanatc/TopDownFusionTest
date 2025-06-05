using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BillBoard : MonoBehaviour
{
    //For text rotate to camera

    private Transform camTransform;

    void Start()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            camTransform = cam.transform;
        }
    }

    void LateUpdate()
    {
        if (camTransform != null)
        {
            transform.forward = camTransform.forward;
        }
    }
}

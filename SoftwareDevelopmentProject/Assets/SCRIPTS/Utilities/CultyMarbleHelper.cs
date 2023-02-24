using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultyMarbleHelper : MonoBehaviour
{
    private static Camera mainCamera;

    //===========================================================================

    public static Vector3 GetMouseToWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;

        return mousePosition;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }
}

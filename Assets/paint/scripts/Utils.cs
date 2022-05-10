using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    private static Plane _XYPlane = new Plane(Vector3.forward, new Vector3(0, 0, 0));
    
    public static Vector3 ScreenToWorld3D(Camera cam, Vector3 position, float zOffset = 0)
    {
        Ray ray = cam.ScreenPointToRay(position);
        //Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
        if (zOffset != 0) _XYPlane = new Plane(Vector3.forward, new Vector3(0, 0, zOffset));
        float distance;
        _XYPlane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}

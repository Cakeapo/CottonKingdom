using UnityEngine;

public static class Helper
{
    public struct ClipPlanePoints
    {
        public Vector3 upperLeft;
        public Vector3 upperRight;
        public Vector3 lowerLeft;
        public Vector3 lowerRight;
    }

    public static ClipPlanePoints ClipPlaneAtNear(Vector3 pos)
    {
        var clipPlanePoints = new ClipPlanePoints();

        if (Camera.main == null)
            return clipPlanePoints;

        var transform = Camera.main.transform;
        var halfFOV = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;
        var aspect = Camera.main.aspect;
        var distance = Camera.main.nearClipPlane;
        var height = distance * Mathf.Tan(halfFOV);
        var width = height * aspect;

        clipPlanePoints.lowerRight = pos + transform.right * width;
        clipPlanePoints.lowerRight -= transform.up * height;
        clipPlanePoints.lowerRight += transform.forward * distance;

        clipPlanePoints.lowerLeft = pos - transform.right * width;
        clipPlanePoints.lowerLeft -= transform.up * height;
        clipPlanePoints.lowerLeft += transform.forward * distance;

        clipPlanePoints.upperRight = pos + transform.right * width;
        clipPlanePoints.upperRight += transform.up * height;
        clipPlanePoints.upperRight += transform.forward * distance;

        clipPlanePoints.upperLeft = pos - transform.right * width;
        clipPlanePoints.upperLeft += transform.up * height;
        clipPlanePoints.upperLeft += transform.forward * distance;

        return clipPlanePoints;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        do
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
        } while (angle < -360 || angle > 360);

        return Mathf.Clamp(angle, min, max);
    }
}

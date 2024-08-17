using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

static class Utils
{
    public interface iTile
    {
        Vector2Int gridPosition {get;}
        IEnumerable<Vector2Int> usedConnections {get;}
        IEnumerable<Vector2Int> openConnections {get;}
    }

    public static Vector2 GetMousePosition()
    {
        return Camera.main.GetComponent<FollowCam>().mousePos;
    }

    public static float GetMouseAngleFrom(Vector2 relativePoint)
    {
        Vector2 mousePos = GetMousePosition();
        
        Vector2 relativeVector = mousePos - relativePoint; //get the vector representing the mouse's position relative to the point
        return VectorToAngle(relativeVector);
    }

    public static float VectorToAngle(Vector2 vector)
    {
        var angleRadians = Mathf.Atan2(vector.y, vector.x); //use atan2 to get the angle; Atan2 returns radians
        return angleRadians * Mathf.Rad2Deg;   //convert to degrees
    }

    public static void RotateTowards(Transform transform, float targetRotation, float speed, float maxSpeed = float.PositiveInfinity)
    {
        float toRotate = targetRotation - transform.rotation.eulerAngles.z;
        if (toRotate > 180) {toRotate -= 360;}
        else if (toRotate < -180) {toRotate += 360;}
        toRotate *= speed;
        if (toRotate > maxSpeed)
        {
            toRotate = maxSpeed;
        }
        else if (toRotate < -maxSpeed)
        {
            toRotate = -maxSpeed;
        }
        transform.Rotate(Vector3.forward, toRotate);
    }

    public static void RotateTowardsLocal(Transform transform, float targetRotation, float speed, float maxSpeed = float.PositiveInfinity)
    {
        float toRotate = targetRotation - transform.localRotation.eulerAngles.z;
        if (toRotate > 180) {toRotate -= 360;}
        else if (toRotate < -180) {toRotate += 360;}
        toRotate *= speed;
        if (toRotate > maxSpeed)
        {
            toRotate = maxSpeed;
        }
        else if (toRotate < -maxSpeed)
        {
            toRotate = -maxSpeed;
        }
        transform.Rotate(Vector3.forward, toRotate);
    }
}

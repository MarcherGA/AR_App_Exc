using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushDistance = 1f;
    [SerializeField] float pushSpeed = 1f;

    [SerializeField] private Transform objectToPush;
    [SerializeField] private Transform Camera;

    public void pushObject()
    {
        Vector3 pushDir = (objectToPush.position - Camera.position).normalized;
        pushDir.y = 0f;
        Vector3 ZeroYObjForward = new Vector3(objectToPush.forward.x, 0f, objectToPush.forward.z);
        float angleToRotate = Vector3.SignedAngle(ZeroYObjForward, pushDir, Vector3.up);
        pushDir = Quaternion.AngleAxis(angleToRotate, objectToPush.up) * objectToPush.forward;
        Vector3 targetPos = objectToPush.position + (pushDir * pushDistance);
        StartCoroutine(moveObjectSmoothly(objectToPush, targetPos, pushSpeed));
    }

    IEnumerator moveObjectSmoothly(Transform obj, Vector3 targetPos, float speed)
    {
        while (Vector3.Distance(obj.position, targetPos) > 0.001f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.position, targetPos, speed * Time.deltaTime);
            yield return null;
        } 
    }
}

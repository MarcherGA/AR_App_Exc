using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;

public class PlaceInfrontCam : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private UnityEvent ObjPlaced;
    void Start()
    {
        StartCoroutine(PlaceObjInfrontCam());
    }

    IEnumerator PlaceObjInfrontCam()
    {
        while (true)
        {
            Vector2 screenPos = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.4f));
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(screenPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                //transform.parent = hits[0].trackable.transform;

                ObjPlaced?.Invoke();
                break;
            }
            yield return null;
        }
    }
}

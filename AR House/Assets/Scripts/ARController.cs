using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
using System;
using UnityEngine.UI;



public class ARController : MonoBehaviour
{
    public GameObject portal;
    public GameObject arCam;
    
    private ARPlaneManager arPlaneMan;
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRay;
    private bool pValid;
    private List<ARRaycastHit> hits;
    
    // Start is called before the first frame update
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRay = FindObjectOfType<ARRaycastManager>();
        hits = new List<ARRaycastHit>();

        arPlaneMan = FindObjectOfType<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlacePortal();
    }

    private void PlacePortal()
    {
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount > 0 && touch.phase == TouchPhase.Began)
        {
            var tray = Camera.main.ScreenPointToRay(touch.position);
            arRay.Raycast(tray, hits, TrackableType.PlaneWithinPolygon);

            pValid = hits.Count > 0;

            if (pValid)
            {
                portal.SetActive(true);

                portal.transform.position = hits[0].pose.position;
                portal.transform.rotation = hits[0].pose.rotation;

                Vector3 campos = arCam.transform.position;
                campos.y = portal.transform.position.y;

                portal.transform.LookAt(campos, portal.transform.up);
            }
        }
    }
}

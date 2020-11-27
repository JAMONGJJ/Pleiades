using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProfileCamRotationScript : MonoBehaviour//, IDragHandler, IPointerEnterHandler
{
    public GameObject ProfileCam;
    Vector3 currentPos, startPos, startRot;
    float deltaX;

    private void OnEnable()
    {
        Quaternion tmp = ProfileCam.transform.rotation;
        tmp.y = 0.0f;
        ProfileCam.transform.localRotation = tmp;
    }

    //Update is called once per frame
    private void FixedUpdate()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject())
        {
            currentPos = Input.mousePosition;
            deltaX = currentPos.x - startPos.x;
            ProfileCam.transform.rotation = Quaternion.Euler(0.0f, deltaX, 0.0f);
        }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject())
            {
                startPos = Input.GetTouch(0).position;
            }
            if (touch.phase == TouchPhase.Moved && EventSystem.current.IsPointerOverGameObject())
            {
                currentPos = Input.GetTouch(0).position;
                deltaX = currentPos.x - startPos.x;
                ProfileCam.transform.rotation = Quaternion.Euler(0.0f, deltaX, 0.0f);
            }
        }
#endif
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (eventData.pointerCurrentRaycast.gameObject.name == "ProfileImage")
    //    {
    //        startPos = Input.mousePosition;
    //        startRot = ProfileCam.transform.rotation.eulerAngles;
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (eventData.pointerCurrentRaycast.gameObject.name == "ProfileImage")
    //    {
    //        currentPos = Input.mousePosition;
    //        deltaX = currentPos.x - startPos.x;
    //        ProfileCam.transform.rotation = Quaternion.Euler(0.0f, startRot.y + deltaX, 0.0f);
    //    }
    //}
}

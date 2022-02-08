/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;


public class VRInputModule : BaseInputModule
{
    public Camera m_Camera;
    public SteamVR_Input_Sources m_TargetSource
    public SteamVR_Action_Boolean m_ClickAction

    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;

    protected override void Awake()
    {
        base.Awake();

        m_Data = new PointerEventData(eventSystem);

    }

    public override void Proces()
    {
        //Reset data, set camera
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelWidth / 2);

        //Raycast
        eventSystem.RaycastAll(m_Data, m_RaycastResultCahce);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCahce);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject


        //Clear
        m_RaycastResultCahce.Clear();

        //Hover
        HandPointerExitandEnter(m_Data, m_CurrentObject);

        //Press
        if (m_ClickAction.GetStateDown(m_TargetSource))
            ProcessPress(m_Data);
        
        //Release
        if(m_ClickAction.GetStateUp(m_TargetSource))
            ProcessReleases(m_Data)

    }

    public PointerEventData GetData()
    {
        return m_Data;
    }

    private void ProcessPress(PointerEventData data)
    {
        //Set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        //Check for object hit, get the down handler, call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

        //if no down handler, try and get click handler
        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandlers<IPointerClickHanders>(m_CurentObject);

        //Set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;
    }

    private void ProcessReleases(PointerEventData data)
    {

        //use default or distance
        float tagetLength = m_DefaultLength;

        //Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        //Default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        //or based on hit
        if (hit.collider != null)
            endPosition = hit.point;
            
        // Set position of the dot
        m_Dot.transform.position = endPosition;

        //Set linerenderer
        m_LineRenderer.SetPosition(0,  transform.position);
        m_LineRenderer.SetPosition(0,  endPosition);


    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
        
    }
}

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

[RequireComponent(typeof(OVRInteractionEvent))]
public class CupInteraction : OVRInteractionEvent {
    
    OVRInteractionEvent m_InteractiveEvent;
    Renderer m_Renderer;

    Vector3 defaultScale;
    [SerializeField] Vector3 hoverScale;
    [SerializeField] Vector3 clickScale;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] GameObject item;

	private void OnEnable()
    {
        m_InteractiveEvent = GetComponent<OVRInteractionEvent>();
        m_InteractiveEvent.OnEnter += HandleEnter;
        m_InteractiveEvent.OnExit += HandleExit;
        m_InteractiveEvent.OnDown += HandleDown;
        m_InteractiveEvent.OnUp += HandleUp;
    }
    
    IEnumerator AnimateScale(Vector3 origin, Vector3 target, float duration){
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            float curvePercent = animationCurve.Evaluate(percent);
            
            // item.transform.localScale = Vector3.Lerp(origin, target, percent);
            item.transform.localScale = Vector3.LerpUnclamped(origin, target, curvePercent);
            
            yield return null;
        }
    }

    void Start()
    {
        if(item == null){
            item = gameObject;
        }
        defaultScale = item.transform.localScale;
    }

    private void OnDisable()
    {
        m_InteractiveEvent.OnEnter -= HandleEnter;
        m_InteractiveEvent.OnExit -= HandleExit;
        m_InteractiveEvent.OnDown -= HandleDown;
        m_InteractiveEvent.OnUp -= HandleUp;
    }

    private void HandleEnter(OVRInteractionEvent evt)
    {
        // transform.localScale = hoverScale;
        StartCoroutine(AnimateScale(item.transform.localScale, hoverScale, .25f));
    }

    private void HandleExit(OVRInteractionEvent evt)
    {
        // transform.localScale = defaultScale;
        StartCoroutine(AnimateScale(item.transform.localScale, defaultScale, .25f));
    }

    private void HandleDown(OVRInteractionEvent evt)
    {
        // transform.localScale = clickScale;
        StartCoroutine(AnimateScale(item.transform.localScale, clickScale, .25f));
    }

    private void HandleUp(OVRInteractionEvent evt)
    {
        // transform.localScale = hoverScale;
        StartCoroutine(AnimateScale(item.transform.localScale, hoverScale, .25f));
    }
}

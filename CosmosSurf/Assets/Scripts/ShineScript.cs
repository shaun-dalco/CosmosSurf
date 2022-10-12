using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShineScript : MonoBehaviour
{
    private Material imageMaterial = null;
    private float shinePositon = 0;
    private Coroutine shineRoutine = null;
    private int shineLocationParameterID;
    private Image imageRenderer;
    public float shineSpeed = 0.7f;
    private int shineRepeats;
    public float shineWaitTime;
    private bool loop = true;

 
    void Start()
    {
        shineLocationParameterID = Shader.PropertyToID("_ShineLocation");
        imageRenderer = GetComponent<Image>();
        imageMaterial = imageRenderer.material;

        StartShine(0);
    }
 
    public void StartShine(float delay)
    {
        if (shineRoutine != null)
            StopCoroutine(shineRoutine);
        shineRoutine = StartCoroutine(StartShineCoroutine(delay));
    }
 
    public void StopShine()
    {
        if (shineRoutine != null)
            StopCoroutine(shineRoutine);
        shineRoutine = null;
    }
 
    private float ShineCurve(float lerpProgress)
    {
        float newValue = lerpProgress * lerpProgress * lerpProgress * (lerpProgress * (6f * lerpProgress - 15f) + 10f);
        return newValue;
    }
 
    private IEnumerator StartShineCoroutine(float dealay)
    {
        yield return new WaitForSeconds(dealay);
    
        if (shineSpeed <= 0f)
            yield break;
 
        int count = loop ? 1 : shineRepeats;
        while (count > 0)
        {
            yield return new WaitForSeconds(shineWaitTime);
 
            count = loop ? 1 : count - 1;
 
            float startTime = Time.time;
 
            while (Time.time < startTime + 1f / shineSpeed)
            {
                shinePositon = ShineCurve((Time.time - startTime) * shineSpeed);
                imageMaterial.SetFloat(shineLocationParameterID, shinePositon);
 
                yield return new WaitForEndOfFrame();
            }
        }
 
        yield break;
    }
}
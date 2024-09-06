using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseTrek : MonoBehaviour
{
    public float trailTime = 1.0f;
    public Color trailColor = Color.white;
    public float minDistance = 0.1f;

    private TrailRenderer trailRenderer;
    private float timer;

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        //trailRenderer.material.SetColor("_TintColor", trailColor);
        timer = 0f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
            trailRenderer.emitting = true;
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= trailTime)
            {
                trailRenderer.emitting = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonPause : MonoBehaviour
{
    public GameObject objectToActivate; // объект, который вы хотите включить/выключить
    public float holdTime = 1.0f; // время удержания кнопки мыши (в секундах), чтобы включить объект

    private bool holdingMouse = false;
    private float mouseHeldTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            holdingMouse = true;
            mouseHeldTime = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            if (holdingMouse)
            {
                mouseHeldTime += Time.deltaTime;
                if (mouseHeldTime >= holdTime)
                {
                    objectToActivate.SetActive(true);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            holdingMouse = false;
            if (objectToActivate.activeSelf)
            {
                objectToActivate.SetActive(false);
            }
            mouseHeldTime = 0f;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Скорость движения героя
    public float leftBoundary = -9f; // Левая граница экрана
    public float rightBoundary = 9f; // Правая граница экрана

    private bool movingRight = true; // Флаг направления движения
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void EatFalse()
    {
        animator.SetBool("eat",false);
    }
    public void AngryFalse()
    {
        animator.SetBool("angry", false);
    }
}

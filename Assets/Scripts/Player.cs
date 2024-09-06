using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // �������� �������� �����
    public float leftBoundary = -9f; // ����� ������� ������
    public float rightBoundary = 9f; // ������ ������� ������

    private bool movingRight = true; // ���� ����������� ��������
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

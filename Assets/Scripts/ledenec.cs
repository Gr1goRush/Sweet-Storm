using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledenec : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
     
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StopAnimationLedenec()
    {
        Destroy(gameObject);
    }
}

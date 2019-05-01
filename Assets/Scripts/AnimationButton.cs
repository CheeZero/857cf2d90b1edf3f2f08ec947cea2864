using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationButton : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        anim.SetBool("active", true);
    }

    private void OnMouseExit()
    {
        anim.SetBool("active", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runner : MonoBehaviour
{
    [SerializeField] Animator anim;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            anim.SetBool("isPause", true);
        }       
    }
}

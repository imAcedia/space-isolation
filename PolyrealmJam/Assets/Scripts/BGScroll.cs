using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGScroll : MonoBehaviour
{
    public Transform[] bgs;

    public float bgWidth;
    public float scrollSpeed = 5f;

    private void Update()
    {
        for (int i = 0; i < bgs.Length; i++)
        {
            Transform bg = bgs[i];

            bg.position += Vector3.right * scrollSpeed * Time.deltaTime;

            if (bg.position.x >= bgWidth)
            {
                bg.position += Vector3.left * bgWidth * bgs.Length;
            }
        }
    }
}

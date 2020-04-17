using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] Transform target;

    private void LateUpdate()
    {
        Vector3 tgtPos = transform.position;
        tgtPos.x = target.position.x;
        tgtPos.y = target.position.y;

        transform.position = tgtPos;
    }
}

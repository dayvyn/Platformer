using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRotation : MonoBehaviour
{
    [SerializeField] Transform playerPos;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = playerPos.rotation;
    }
}

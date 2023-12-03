using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCarSelection : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(0, 0.5f, 0);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Update()
    {
        try
        {
            throw new Exception();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            throw e;
        }
    }
}

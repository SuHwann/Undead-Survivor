using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EventTest : MonoBehaviour
{
    [SerializeField] protected Image image;

    public static event Action Event;

    private void OnEnable()
    {
        
    }

}

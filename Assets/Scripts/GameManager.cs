using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isActivate;

    public GameObject gameoverObject;

    private void Awake()
    {
        isActivate = true;
    }

    
}

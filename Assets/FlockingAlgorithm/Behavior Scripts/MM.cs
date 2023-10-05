using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    public static MM Instance;

    private void Awake()
    {
        Instance= this;
    }
}

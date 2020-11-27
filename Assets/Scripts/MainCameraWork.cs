using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraWork : MonoBehaviour
{
    GameObject BDD;
    public Vector3 CameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        BDD = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        transform.transform.position = BDD.transform.position + CameraPosition;
    }
}

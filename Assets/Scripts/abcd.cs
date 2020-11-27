using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abcd : MonoBehaviour
{
    public GameObject pos1;
    public GameObject pos2;

    float time;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(time <= 5.0f)
            time += Time.deltaTime;

        gameObject.transform.position = pos1.transform.position + (pos2.transform.position - pos1.transform.position) * (time / 5.0f);
        gameObject.transform.eulerAngles = pos1.transform.eulerAngles + (pos2.transform.eulerAngles - pos1.transform.eulerAngles) * (time / 5.0f);
    }
}

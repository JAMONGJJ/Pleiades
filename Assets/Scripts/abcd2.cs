using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abcd2 : MonoBehaviour
{
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;

    float time;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (time <= 8.0f)
            time += Time.deltaTime;

        if(time <= 4.0f)
        {
            gameObject.transform.position = pos1.transform.position + (pos2.transform.position - pos1.transform.position) * (time / 4.0f);
            gameObject.transform.eulerAngles = pos1.transform.eulerAngles + (pos2.transform.eulerAngles - pos1.transform.eulerAngles) * (time / 4.0f);
        }
        else if (time < 8.0f)
        {
            gameObject.transform.position = pos2.transform.position + (pos3.transform.position - pos2.transform.position) * (time / 8.0f);
            gameObject.transform.eulerAngles = pos2.transform.eulerAngles + (pos3.transform.eulerAngles - pos2.transform.eulerAngles) * (time / 8.0f);
        }
    }
}

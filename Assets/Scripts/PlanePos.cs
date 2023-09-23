using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePos : MonoBehaviour
{

    public GameObject Plane_10, Plane_7, Plane_5, CBR15000_10, CBR15000_7, CBR15000_5;
    private bool Update_10 = false, Update_7 = false, Update_5 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Update_10) {
            Transform ObjectTrans = CBR15000_10.transform;
            Plane_10.transform.position = ObjectTrans.position;
            Plane_10.transform.rotation = ObjectTrans.rotation;
            Plane_10.transform.Translate(new Vector3(0.2f, 0f, 0f));
        }
        if (Update_7)
        {
            Transform ObjectTrans = CBR15000_7.transform;
            Plane_7.transform.position = ObjectTrans.position;
            Plane_7.transform.rotation = ObjectTrans.rotation;
            Plane_7.transform.Translate(new Vector3(0.2f, 0f, 0f));
        }
        if (Update_5)
        {
            Transform ObjectTrans = CBR15000_5.transform;
            Plane_5.transform.position = ObjectTrans.position;
            Plane_5.transform.rotation = ObjectTrans.rotation;
            Plane_5.transform.Translate(new Vector3(0.2f, 0f, 0f));
        }
    }
    public void Update_10_On()
    {
        Update_10 = true;
        Plane_10.SetActive(true);
    }
    public void Update_10_Off()
    {
        Update_10 = false;
    }
    public void Update_7_On()
    {
        Update_7 = true;
        Plane_7.SetActive(true);
    }
    public void Update_7_Off()
    {
        Update_7 = false;
    }
    public void Update_5_On()
    {
        Update_5 = true;
        Plane_5.SetActive(true);
    }
    public void Update_5_Off()
    {
        Update_5 = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceBetweenShower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject object1, object2;
    private TextMeshPro Display;
    private bool ChangeFirst = true;
    void Start()
    {
        Display = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Distance = object1.transform.position - object2.transform.position;
        Display.text = "X: "+Distance.x.ToString()+ "\nY: "+Distance.y.ToString() + "\nZ: "+ Distance.z.ToString()+"\n"+Vector3.Magnitude(Distance).ToString();
    }
    public void ChangeObject(GameObject Object)
    {
        if (ChangeFirst)
        {
            object1 = Object;
        }
        else
        {
            object2 = Object;
        }
        
    }
    public void Toggle()
    {
        ChangeFirst = !ChangeFirst;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyObject : MonoBehaviour
{
    public GameObject ImageTarget;

    private GameObject Copy;
    private List<GameObject> PlacedObjects;
    private GameObject Parent;
    void Start()
    {
        Copy = ImageTarget.transform.GetChild(0).gameObject;
        Parent = GameObject.Find("TargetsPlaced");
        PlacedObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CopyPlaceObject()
    {
        GameObject ObjectV2 = GameObject.Instantiate(Copy);
        ObjectV2.transform.localScale = Copy.transform.localScale;
        ObjectV2.transform.SetPositionAndRotation(Copy.transform.position, Copy.transform.rotation);
        ObjectV2.transform.SetParent(Parent.transform, true);
        PlacedObjects.Add(ObjectV2);
    }
    public void RemoveLastObject()
    {
        GameObject ObjectDestroy = PlacedObjects[PlacedObjects.Count - 1];
        PlacedObjects.Remove(ObjectDestroy);
        Destroy(ObjectDestroy);
    }
    public void RemoveFirstObject()
    {
        GameObject ObjectDestroy = PlacedObjects[0];
        PlacedObjects.Remove(ObjectDestroy);
        Destroy(ObjectDestroy);
    }
}

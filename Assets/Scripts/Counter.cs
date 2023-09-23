using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{

    public int counter = 0;
    public TMP_Text counterText; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = counter.ToString();
    }

    public void increase()
    {
        counter++;
    }

    public void decrease()
    {
        counter--;
    }

    public void clear()
    {
        counter = 0;
    }
}

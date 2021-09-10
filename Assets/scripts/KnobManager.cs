using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobManager : MonoBehaviour
{
    [SerializeField]
    GameObject knob;

    List<KnobBehavior> knobBehaviorList;

    private void Awake()
    {
        knobBehaviorList = new List<KnobBehavior>();
    }


    //Debugging code
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        UpdateKnobAmount(3);
    //    }
    //    if (Input.GetKeyDown(KeyCode.J))
    //    {
    //        UpdateKnobAmount(4);
    //    }
    //}


    public void SetKnobAmount(int amount)
    {
        for(int n = 0; n < amount; n++)
        {
            GameObject nKnob = Instantiate(knob, this.gameObject.transform);
            KnobBehavior nBehavior = nKnob.GetComponent<KnobBehavior>();
            knobBehaviorList.Add(nBehavior);
        }
    }

    public void UpdateKnobAmount(int amount)
    {
        amount = knobBehaviorList.Count - amount;

        int n = 0;
        foreach(var behavior in knobBehaviorList)
        {
            if(n >= amount)
            {
                behavior.Lighten();
            }
            else
            {
                behavior.Darken();
            }
            n++;
        }
    }
}

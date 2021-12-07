using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractGoal : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    public Text objText;

    private int counter = 0;

    private float distance;

    private int sharedcount;

    private bool interacted = false;

    // Update is called once per frame
    void Update()
    {
        distance = (player.transform.position - transform.position).magnitude;
        sharedcount = int.Parse(objText.text) + counter;
        objText.text = sharedcount.ToString();
    }

    private void OnMouseDown()
    {
        if(!interacted)
        {
            if (distance < 5)
            {
                counter++;
                interacted = true;
            }
        }
    }
}

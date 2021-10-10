using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToGoal : MonoBehaviour
{
    // reference player position
    [SerializeField]
    private Transform player;

    // reference to UI text
    [SerializeField]
    public Text distanceText;

    // distance value
    private float distance;

    // Update is called once per frame
    void Update()
    {
        distance = (player.transform.position - transform.position).magnitude;
        distanceText.text = distance.ToString("F1") + " m";
        distanceText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        distanceText.enabled = distanceText.transform.position.z > 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToGoal : MonoBehaviour
{
    // reference player position
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform target;

    [SerializeField]
    public Image border;

    // reference to UI text
    [SerializeField]
    public Text distanceText;

    [SerializeField]
    public Vector3 offset;

    // distance value
    private float distance;

    // Update is called once per frame
    void Update()
    {
        float minX = border.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX + 10;

        float minY = border.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.transform.position + offset);

        if(Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if(pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
            if (pos.y < Screen.width / 2)
            {
                pos.y = maxY;
            }
            else
            {
                pos.y = minY;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        border.transform.position = pos;

        distance = (player.transform.position - target.transform.position).magnitude;
        distanceText.text = distance.ToString("F1") + " m";
    }
}

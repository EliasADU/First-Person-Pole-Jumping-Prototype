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
    private Transform target2;

    [SerializeField]
    private Transform target3;

    [SerializeField]
    public Image border;

    [SerializeField]
    public Image border2;

    [SerializeField]
    public Image border3;

    // reference to UI text
    [SerializeField]
    public Text distanceText;

    [SerializeField]
    public Text distanceText2;

    [SerializeField]
    public Text distanceText3;

    [SerializeField]
    public Vector3 offset;

    // distance value
    private float distance;

    private float distance2;

    private float distance3;

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



        float minX2 = border2.GetPixelAdjustedRect().width / 2;
        float maxX2 = Screen.width - minX2 + 10;

        float minY2 = border2.GetPixelAdjustedRect().height / 2;
        float maxY2 = Screen.height - minY2;

        Vector2 pos2 = Camera.main.WorldToScreenPoint(target2.transform.position + offset);

        if (Vector3.Dot((target2.position - transform.position), transform.forward) < 0)
        {
            if (pos2.x < Screen.width / 2)
            {
                pos2.x = maxX2;
            }
            else
            {
                pos2.x = minX2;
            }
            if (pos2.y < Screen.width / 2)
            {
                pos2.y = maxY2;
            }
            else
            {
                pos2.y = minY2;
            }
        }

        pos2.x = Mathf.Clamp(pos2.x, minX2, maxX2);
        pos2.y = Mathf.Clamp(pos2.y, minY2, maxY2);

        border2.transform.position = pos2;

        distance2 = (player.transform.position - target2.transform.position).magnitude;
        distanceText2.text = distance2.ToString("F1") + " m";



        float minX3 = border3.GetPixelAdjustedRect().width / 2;
        float maxX3 = Screen.width - minX3 + 10;

        float minY3 = border3.GetPixelAdjustedRect().height / 2;
        float maxY3 = Screen.height - minY3;

        Vector2 pos3 = Camera.main.WorldToScreenPoint(target3.transform.position + offset);

        if (Vector3.Dot((target3.position - transform.position), transform.forward) < 0)
        {
            if (pos3.x < Screen.width / 2)
            {
                pos3.x = maxX3;
            }
            else
            {
                pos3.x = minX3;
            }
            if (pos3.y < Screen.width / 2)
            {
                pos3.y = maxY3;
            }
            else
            {
                pos3.y = minY3;
            }
        }

        pos3.x = Mathf.Clamp(pos3.x, minX3, maxX3);
        pos3.y = Mathf.Clamp(pos3.y, minY3, maxY3);

        border3.transform.position = pos3;

        distance3 = (player.transform.position - target3.transform.position).magnitude;
        distanceText3.text = distance3.ToString("F1") + " m";
    }
}

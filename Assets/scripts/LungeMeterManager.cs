using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LungeMeterManager : MonoBehaviour
{
    [SerializeField]
    RectTransform meter;

    float meterFullSize;

    float currentSize;

    [SerializeField]
    float recoverySpeed; // percentage value

    [SerializeField]
    Image border1;

    [SerializeField]
    Image border2;

    [SerializeField]
    Image border3;

    [SerializeField]
    Image border4;
    // Start is called before the first frame update
    void Start()
    {
        meterFullSize = meter.sizeDelta.y;
        currentSize = meterFullSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (MeterIsFull())
        {
            border1.color = Color.white;
            border2.color = Color.white;
            border3.color = Color.white;
            border4.color = Color.white;
        }
        else
        {
            border1.color = Color.black;
            border2.color = Color.black;
            border3.color = Color.black;
            border4.color = Color.black;
        }
    }

    public void UpdateMeter(float percentage)
    {
        percentage /= 100;
        currentSize += percentage * meterFullSize;
        Mathf.Clamp(currentSize, 0, meterFullSize);

        UpdateMeterFullness();
    }

    public bool MeterIsFull()
    {
        return currentSize >= meterFullSize - 1 || currentSize >= (meterFullSize * 0.75) - 1;
    }

    void UpdateMeterFullness()
    {
        if (currentSize > meterFullSize)
        {
            currentSize = meterFullSize;
        }
        if(currentSize < 0){
            currentSize = 0;
        }
        meter.sizeDelta = new Vector2(meter.sizeDelta.x, currentSize);

    }
}

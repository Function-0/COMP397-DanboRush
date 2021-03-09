using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceBarScreenSpaceController : MonoBehaviour
{
    public Slider distanceBarSlider;

    public PlayerBehaviour playerBehaviour;
    public TruckGoal truckGoal;

    [Header("Distance to Goal")]
    public float distanceTotal;
    [Header("Distance Left")]
    public float distanceLeft;

    public float offset = 200;

    // Start is called before the first frame update
    void Start()
    {
        distanceBarSlider = GetComponent<Slider>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        truckGoal = FindObjectOfType<TruckGoal>();

        distanceTotal = Vector3.Distance(playerBehaviour.transform.position, truckGoal.transform.position);

        distanceBarSlider.minValue = 0;
        distanceBarSlider.maxValue = distanceTotal;
    }

    // Update is called once per frame
    void Update()
    {
        distanceLeft = Vector3.Distance(playerBehaviour.transform.position, truckGoal.transform.position);
        distanceBarSlider.value = (distanceTotal - distanceLeft) + offset;

        Debug.Log(distanceLeft);
    }
}

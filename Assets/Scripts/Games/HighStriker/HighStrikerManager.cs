using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighStrikerManager : MonoBehaviour
{
    [Header("Gameplay Objects")]
    [SerializeField] private GameObject hitTable;
    [SerializeField] private GameObject highStrikerHammer;
    [SerializeField] private HighStrikerWeight travelWeight;
    [SerializeField] private HighStrikerLamps lamps;

    [Header("Physics")]
    [SerializeField] private AnimationCurve hitAngleToForceMultiplier;
    [SerializeField] private AnimationCurve hitForceToLevelReach;
    [SerializeField] private float forceMultiplier;

    private float _heightLevelPercentage;
    public float HeightLevelPercentage
    {
        get { return _heightLevelPercentage; }
        set {   
            _heightLevelPercentage = value;
            AdjustLamps(value);
        }
    }




    private void Start()
    {
        RegisterHitTableEvent();

        travelWeight.RegisterManager(this);

        //StartCoroutine(Testing());
    }

    public void HandleNewHeight()
    {
        UpdateHeightLevelPercentage(travelWeight.gameObject.transform.position.y - 0.2765782f);
    }

    public void HandlePeak()
    {
        StartCoroutine(PeakHandling());
    }

    private void AdjustLamps(float newVal)
    {
        lamps.SetValueForLamps(newVal);
    }

    private void UpdateHeightLevelPercentage(float yPos)
    {
        HeightLevelPercentage = yPos / 2.39f; // Fixed Distance from starting point to stopper obj on top.
    }


    private void RegisterHitTableEvent()
    {
        CollisionProxy prox = hitTable.GetComponent<CollisionProxy>();

        if (prox != null)
        {
            prox.OnCustomTriggerEnter += HitDetect;
        }
        
    }


    private void HitDetect(Collider other)
    {
        if (other == null || !other.CompareTag("HammerStriker")) return;

        VelocityCalculator hammerVelo = other.gameObject.GetComponent<VelocityCalculator>();


        Vector3 hitVector = hammerVelo.GetBufferedVelocity(5);
        Debug.Log(hitVector.ToString());



        float hitForce = hitVector.magnitude; // Power of the movement
        float hitAngle = Vector3.Angle(hitVector, Vector3.down); // Calculate angle of incoming hit
        float hitAngleMultiplier = hitAngleToForceMultiplier.Evaluate(hitAngle); // Apply multiplier in dependence of hitAngle

        float hitImpact = hitForce * hitAngleMultiplier; // Calculate end result
            
        Debug.Log($"Force of hit: {hitForce}. Angle of hit: {hitAngle} translates to -> {hitAngleMultiplier} multiplier. End result: {hitImpact}");


        LaunchWeight(hitImpact);
    }

    private void LaunchWeight(float force)
    {
        travelWeight.ApplyForce(force * forceMultiplier);
        travelWeight.shouldObserveTravel = true;
    }



    IEnumerator Testing()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("launching");
            LaunchWeight(10);
        }
       
    }

    IEnumerator PeakHandling()
    {
        yield return new WaitForSeconds(2f);

        HandleNewHeight();
    }

}

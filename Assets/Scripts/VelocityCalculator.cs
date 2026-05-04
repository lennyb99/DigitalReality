using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VelocityCalculator : MonoBehaviour
{
    [SerializeField] private int bufferSize = 10; 

    private Queue<Vector3> velocityHistory = new Queue<Vector3>();
    private Vector3 lastPosition;

    // Called externally to start/stop recording the velocity.
    public bool IsGrabbed { get; set; } 

    void FixedUpdate()
    {
        if (IsGrabbed)
        {
            Vector3 currentVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;

            velocityHistory.Enqueue(currentVelocity);

            if (velocityHistory.Count > bufferSize)
            {
                velocityHistory.Dequeue();
            }

        }
        lastPosition = transform.position;
    }

    public Vector3 GetBufferedVelocity()
    {
        if (velocityHistory.Count == 0) return Vector3.zero;

        Vector3 sum = Vector3.zero;
        foreach (var v in velocityHistory)
        {
            sum += v;
        }

        return sum / velocityHistory.Count;
    }

    public Vector3 GetBufferedVelocity(int recentCount)
    {
        if (velocityHistory.Count == 0 || recentCount <= 0) return Vector3.zero;
        var recentElements = velocityHistory.TakeLast(recentCount);
        Vector3 sum = Vector3.zero;
        int actualCount = 0;

        foreach (var v in recentElements)
        {
            sum += v;
            actualCount++;
        }
        return sum / actualCount;
    }

    public void ClearBuffer()
    {
        velocityHistory.Clear();
    }
}
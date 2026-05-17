using UnityEngine;

public class HighscoreMarkerBlock : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("HighStrikerWeight"))
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            
        }
    }

}

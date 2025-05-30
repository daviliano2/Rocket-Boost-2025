using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("friendly tag");
                break;
            case "Finish":
                Debug.Log("finish tag");
                break;
            case "Fuel":
                Debug.Log("fuel tag");
                break;
            default:
                Debug.Log("U DED!");
                break;
                
        }
    }
}

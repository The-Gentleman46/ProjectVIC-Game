using UnityEngine;

public class foodCollection : MonoBehaviour
{
    public int collectedFoodCount = 0;
    public int totalFoodCount = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            collectedFoodCount++;
            Destroy(other.gameObject);
            Debug.Log("Collected food: " + collectedFoodCount);
            
        }
    }
}

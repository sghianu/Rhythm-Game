using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour {

    public GameObject fruitSlicedPrefab;
    public float speed = 2f;

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Blade")
        {
            Vector3 direction = (col.transform.position - transform.position).normalized;

            Quaternion rotation = Quaternion.LookRotation(direction);

            Instantiate(fruitSlicedPrefab, transform.position, transform.rotation); //instanziamo il prefab della frutta tagliata nel punto in cui tocchiamo la mela con la rotazione del taglio
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Translate(0, 0, - speed * Time.fixedDeltaTime);
    }
}

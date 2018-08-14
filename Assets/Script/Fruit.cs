using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour {

    public GameObject fruitSlicedPrefab;
    public float speed = 2f;

    public Renderer[] rend;

    bool isDissolving = false;
    float dissolvingMultiplayer = -1;

    void Start()
    {
        rend[0] = GetComponentInChildren<Renderer>();
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Blade")
        {
            /*Vector3 direction = (col.transform.position - transform.position).normalized;

            Quaternion rotation = Quaternion.LookRotation(direction);

            Instantiate(fruitSlicedPrefab, transform.position, transform.rotation); //instanziamo il prefab della frutta tagliata nel punto in cui tocchiamo la mela con la rotazione del taglio
            Destroy(gameObject);*/
            isDissolving = true;
        }

        if (col.tag == "Death")
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        transform.Translate(0, 0, - speed * Time.fixedDeltaTime);

        if (isDissolving)
        {
            Dissolve();
            dissolvingMultiplayer += 0.1f;
        }
    }

    void Dissolve()
    {
        if (dissolvingMultiplayer < 1)
        {
            foreach (var r in rend)
            {
                r.material.SetFloat("_Dissolve", dissolvingMultiplayer);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

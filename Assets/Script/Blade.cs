using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

    public GameObject bladeTrailPrefab; //prefab della scia da instanziare
    public float minCuttingVelocity = .001f;

    bool isCutting = false; //check se stiamo premendo il tasto

    Vector3 previousPosition; //ultima posizione del mouse quando è stato premuto
    Vector2 mousePos;

    GameObject currentBladeTrail; //scia attuale che viene creata e poi distrutta

    //Rigidbody2D rb; //riferimento del rigidbody
    Camera cam; //riferimento della camera per avere la posizione nello schermo
    //CircleCollider2D circleCollider; //riferimento del collider
    SphereCollider sphereCollider;

    void Start() //vengono inizializzati i riferimenti
    {
        cam = Camera.main;
        //rb = GetComponent<Rigidbody2D>();
        //circleCollider = GetComponent<CircleCollider2D>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
		
	}

    void UpdateCut() 
    {
        //metodo con perspective camera
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        Vector3 newPosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane + 1));
        gameObject.transform.position = newPosition;

        //metodo con orthographic camera
        /*Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 2;
        gameObject.transform.position = newPosition;*/ //settiamo la posizione dell'oggetto in base a dove premiamo con il mouse


        float velocity = (newPosition - previousPosition).magnitude / Time.deltaTime; //se il mouse si muove lentamente, il collider non viene attivato
        if (velocity > minCuttingVelocity)
        {
            //circleCollider.enabled = true;
            sphereCollider.enabled = true;
        }
        else
        {
            //circleCollider.enabled = false;
            sphereCollider.enabled = false;
        }

        previousPosition = newPosition; //aggiorniamo l'ultima posizione del taglio a quella di partenza del nuovo taglio
    }

    void StartCutting() 
    {
        isCutting = true; //abilitiamo il check del tasto premuto
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform); //instanziamo la scia
        previousPosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane + 1));
        //previousPosition = cam.ScreenToWorldPoint(Input.mousePosition); //settiamo la posizione del nuovo taglio senza partire dalla posizione finale dell'ultimo taglio
        //circleCollider.enabled = false; //disabilitiamo il collider
        sphereCollider.enabled = false;
    }

    void StopCutting() 
    {
        isCutting = false; //disabilitiamo il check del tasto premuto
        currentBladeTrail.transform.SetParent(null); //togliamo la scia dal padre
        Destroy(currentBladeTrail, 1f); //distruggiamo la scia
        //circleCollider.enabled = false; //disabilitiamo il collider
        sphereCollider.enabled = false;
    } 
}

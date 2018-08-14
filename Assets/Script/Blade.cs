using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

    public GameObject bladeTrailPrefab; //prefab della scia da instanziare
    public float minCuttingVelocity = 0.001f;

    bool isCutting = false; //check se stiamo premendo il tasto

    Vector3 previousPosition; //ultima posizione del mouse quando è stato premuto
    Vector2 mousePos;

    GameObject currentBladeTrail; //scia attuale che viene creata e poi distrutta

    Camera cam; //riferimento della camera per avere la posizione nello schermo
    CapsuleCollider capsuleCollider;

    void Start() //vengono inizializzati i riferimenti
    {
        cam = Camera.main;
        capsuleCollider = GetComponent<CapsuleCollider>();
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

        float velocity = (newPosition - previousPosition).magnitude / Time.deltaTime; //se il mouse si muove lentamente, il collider non viene attivato
        if (velocity > minCuttingVelocity)
        {
            capsuleCollider.enabled = true;
        }
        else
        {
            capsuleCollider.enabled = false;
        }

        previousPosition = newPosition; //aggiorniamo l'ultima posizione del taglio a quella di partenza del nuovo taglio
    }

    void StartCutting() 
    {
        isCutting = true; //abilitiamo il check del tasto premuto
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform); //instanziamo la scia
        previousPosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane + 1));
        capsuleCollider.enabled = false;
    }

    void StopCutting() 
    {
        isCutting = false; //disabilitiamo il check del tasto premuto
        currentBladeTrail.transform.SetParent(null); //togliamo la scia dal padre
        Destroy(currentBladeTrail, 1f); //distruggiamo la scia
        capsuleCollider.enabled = false;
    } 
}

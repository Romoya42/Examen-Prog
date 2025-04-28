using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform card1;
    public Transform card2;
    void Update()
    {
        
    // Si clic gauche de la souris
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Crée un rayon depuis la caméra
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Card"))
            {

                if (hit.collider.transform.childCount > 0)
                {
                    if (card1 == null) {
                        card1 = hit.collider.transform.GetChild(0);
                    }
                    else if (card1 != hit.collider.transform)
                    {
                        card2 = hit.collider.transform.GetChild(0);
                    }
                }

                else
                {
                    if (card1 == null)
                    {
                        card1 = hit.collider.transform;
                    }
                    else if (card1 != hit.collider.transform)
                    {
                        card2 = hit.collider.transform;
                    }
                }
                
                if (card1  != null && card2 != null)
                {
                    if (card1.GetComponent<Renderer>().material.mainTexture == card2.GetComponent<Renderer>().material.mainTexture )
                    {
                        GameManager.Instance.point++;
                        print("point");
                    }
                    card1 = null;
                    card2 = null;
                }

            }
            
            
            /*
                else {

                    if (hit.collider.CompareTag("Card"))
                {
                    if (hit.collider.transform.childCount > 0)
                    {

                        if (hit.collider.GetComponent<Renderer>().material.mainTexture == )

                    }
                    }
                }
            */
            
        }
    }
}

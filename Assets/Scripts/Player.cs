using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private Turn card1;
    [SerializeField] private Turn card2;
    public Transform hited;

    private bool isBusy = false;

    void Update()
    {
        if (isBusy) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Card"))
            {   
                hited = hit.collider.transform.parent;
                Turn hitCard = hited.GetComponent<Turn>();

                if (hitCard != null && !hitCard.rotated && !hitCard.isRotating)
                {
                    if (card1 == null)
                    {                        
                        card1 = hitCard;
                        card1.Rotate180Smooth();
                    }
                    else if (card2 == null && card1 != hitCard)
                    {                    
                        card2 = hitCard;
                        card2.Rotate180Smooth();
                        StartCoroutine(CheckMatchAfterRotation());
                    }
                }
            }   
        }
    }

    IEnumerator CheckMatchAfterRotation()
    {
        isBusy = true;

        while (card1.isRotating || card2.isRotating)
        {
            yield return null;
        }

        Texture tex1 = card1.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture;
        Texture tex2 = card2.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture;

        if (tex1 != null && tex2 != null && tex1.name == tex2.name)
        {
            card1.rotated = true;
            card2.rotated = true;
            GameManager.Instance.point++;
            print("point");
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            card1.Rotate180Smooth();
            card2.Rotate180Smooth();
        }

        card1 = null;
        card2 = null;
        isBusy = false;
    }
}

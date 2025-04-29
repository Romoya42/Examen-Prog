using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System.Linq;
using System.Drawing;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject card;
    [SerializeField] private Material baseMaterial;
    private Material instanceMaterial;
    [SerializeField] public List<Texture2D> images;

    [SerializeField] private int scaleY = 0;
    [SerializeField] private float deplacement = 1.5f;
    [SerializeField] private int scaleX = 0;
    private int cardnumber=0;
    public List<Texture2D> pairImages;
    public int point;



    void Awake()
    {
        point = 0;
        card.transform.GetChild(0).GetComponent<Renderer>();



        if (Instance is null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    


    void Start(){
        CreatePair();
        Shuffle();
        for (int i = 0; i < scaleY; i++)
        {
            for (int j = 0; j < scaleX; j++)
            {
                
                GenerateCard(i, j);
                cardnumber++;
            }
        }
     }


    void CreatePair()
    {
        for (int taille =0; taille < (scaleY* scaleX)/2; taille++)
        {

            Texture2D randomTexture = images[Random.Range(0, images.Count)];
            pairImages.Add(randomTexture);
            pairImages.Add(randomTexture);
        }
    }
    
    void Shuffle()
    {
        pairImages = pairImages.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    void GenerateCard(float i, float j)
    {
                
        var newcard = Instantiate(card, new Vector3( i * deplacement, 0, j * deplacement), Quaternion.Euler(0, 0, 90), transform);

        instanceMaterial = new Material(baseMaterial);
        
        instanceMaterial.mainTexture = pairImages[cardnumber];


        
        newcard.transform.GetChild(0).GetComponent<Renderer>().material = instanceMaterial;
        
    }

   
}
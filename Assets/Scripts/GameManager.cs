using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System.Linq;
using System.Drawing;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject card;
    [SerializeField] private Material baseMaterial;
    private Material instanceMaterial;
    [SerializeField] private List<Texture2D> images;

    [SerializeField] private int scaleY = 0;
    [SerializeField] private float deplacement = 1.5f;
    [SerializeField] private int scaleX = 0;
    private int errorCount=0;
    [SerializeField] private TMP_Text errorText;
    private int cardnumber=0;

    public List<Texture2D> pairImages;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    public int maxErreur;
    


    void Awake(){
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
    
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            DestroyAllSpawnedObjects();
            Restartscene();
        }
    }
 
    void Start(){
        Restartscene();
     }

    void Restartscene(){
        spawnedObjects.Clear();
        pairImages.Clear();
        cardnumber=0;
        

        errorCount = 0;
        errorText.text = "Erreurs : " + errorCount + "/" + maxErreur+1;

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

    public void DestroyAllSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }

        spawnedObjects.Clear();
    }



    public void PointManager(int point){
        errorCount+= point;
        errorText.text = "Erreurs : " + errorCount + "/" + maxErreur+1;

        if (errorCount > maxErreur)
        {
            
            DestroyAllSpawnedObjects();
            Restartscene();
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
                
        var newcard = Instantiate(card, new Vector3( i * deplacement, 0, j * deplacement), Quaternion.Euler(0, 180, 90-180), transform);
        spawnedObjects.Add(newcard);

        instanceMaterial = new Material(baseMaterial);
        
        instanceMaterial.mainTexture = pairImages[cardnumber];


        
        newcard.transform.GetChild(0).GetComponent<Renderer>().material = instanceMaterial;
        
    }

   
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{

    public static DataController Instance;
    public Dictionary<string, List<Animal>> animalListDict;

    private void Awake() {
        CreateEmptyDatas();
    }

    public void CreateEmptyDatas() {

        animalListDict = new Dictionary<string, List<Animal>>();


    }

    void Start()
    {
        
    }

 
    void Update()
    {
        
    }
}

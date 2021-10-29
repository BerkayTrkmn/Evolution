using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Clickable , IMovable {

    public float population;
    public float feeding;
    public float speed;
    Coroutine moveCoroutine;
    [Header("Feeding Data")]
    public float foodfactor;
    public int optimumTypeIndex;
    public float optimumTemperature;
    public float optimumHeight;
    public float updateInterval;
    public BonePile deadPrefab;
    public Animal animalPrefab;
    private void Start() {
       
        UpdateAnimalStats(0.25f, 0.25f);
        InvokeRepeating("StartUpdateAnimal", 1, updateInterval);
    }


    public void AddAnimalToDictionary() {
     DataController datacont = DataController.Instance;
        if (datacont.animalListDict.ContainsKey(name)) {
            datacont.animalListDict[name].Add(this);
        } else {
            datacont.animalListDict.Add(name, new List<Animal>());
            datacont.animalListDict[name].Add(this);
        }

    }

    public void SetAnimal(float _population, float _feeding) {
        population = _population;
        feeding = _feeding;
    }

    public void MoveTowardsToPoint(GameObject objectToMove, Vector3 endPoint) {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveOverSpeed(objectToMove, endPoint, speed));
    }
    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed) {
        float timer = 0;
        float roadTime = RoadTime(transform.position, end);
        Debug.Log("RoadTİme: " + roadTime);
        while (objectToMove.transform.position != end && timer < roadTime) {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            //Debug.Log("Timer: " + timer);
            timer += Time.deltaTime;
        }
    }

    public float RoadDistance(Vector3 startPoint, Vector3 endPoint) {

        return (endPoint - startPoint).magnitude;
    }
    public float RoadTime(Vector3 startPoint, Vector3 endPoint) {

        return RoadDistance(startPoint, endPoint) / speed;
    }

    public override void Selected(Target target) {

        base.Selected(target);
    }

    public override void Deselected() {
        base.Deselected();
    }

    public virtual void Move(GameObject objectToMove, Vector3 endPoint) {

        //Debug.Log("Animal");
        MoveTowardsToPoint(objectToMove, endPoint);
    }

    public void StartUpdateAnimal() {
        UpdateClickable();
    }
    public void StopUpdatingAnimal() {
        CancelInvoke();
    }
    public override void UpdateClickable() {

        Tile tile =  LookGround();
        UpdateAnimalStats(tile); 
    }

    public void UpdateAnimalStats(float _population, float _feeding) {
        //UpdatePopulation();
       // UpdateFeeding(tile);
        SetAnimal(_population, _feeding);
        if(isSelected) UIController.Instance.UpdateCLickableUI(name, population, feeding);

    }
    public void UpdateAnimalStats(Tile currentTile) {
        UpdateFeeding(currentTile);
        UpdatePopulation();
        SetAnimal(population, feeding);
        if (isSelected) UIController.Instance.UpdateCLickableUI(name, population, feeding);
        CheckAnimalPopulation();
    }
    public void UpdatePopulation() {
        population += 0.04f * feeding;
    }
    public void UpdateFeeding(Tile tile) {
        float baseFeeding;
        if (tile.type.id == optimumTypeIndex)
            baseFeeding = 1f;
        else baseFeeding = 0.5f;

        feeding = baseFeeding - (0.05f * Mathf.Abs(optimumTemperature-tile.type.temperature)) ;
    }
   public void CheckAnimalPopulation() {
        if (population <= 0)AnimalDies();
         else if (population >= 1) AnimalMultiplies();
    }
    public void AnimalDies() {
        Instantiate(deadPrefab.gameObject, transform.position,Quaternion.identity);
        UIController.Instance.CloseStatusWindow(this);
        Destroy(gameObject);
        //TODO:Die
    }
    public void AnimalMultiplies() {
        //TODO:Multiply
        Animal animalLeft = Instantiate(animalPrefab.gameObject,transform.position,Quaternion.identity).GetComponent<Animal>();
        Animal animalRight = Instantiate(animalPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<Animal>();
        animalLeft.MoveTowardsToPoint(animalLeft.gameObject, animalLeft.transform.position + Vector3.left * 2);
        animalRight.MoveTowardsToPoint(animalRight.gameObject, animalRight.transform.position + Vector3.right * 2);
        UIController.Instance.CloseStatusWindow(this);
        Destroy(gameObject);
       
    }

    public Tile LookGround() {
        int layerMask = 1 << 8;
        // Debug.DrawRay(transform.position, Vector2.zero);
        //Debug.Log(LayerMask.GetMask("Map"));
        
        RaycastHit2D hit = Physics2D.Linecast(transform.position,transform.position + Vector3.forward * 1f , layerMask);
        return hit.transform.GetComponent<Tile>();
       // Debug.Log(transform.name + " "  +hit.transform.GetComponent<Tile>().x + " " +hit.transform.GetComponent<Tile>().y);
    }
    public void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * 1f);
    }

}

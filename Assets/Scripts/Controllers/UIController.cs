using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController Instance;
    public List<GameObject> panelList;
    [Header("ClickableUI")]
    public GameObject clickableUI;
    public TextMeshProUGUI nameText;
    public Image populationFillerImage;
    public Image feedingFillerImage;
    public Image clickableImage;

    private void Awake() {
        if (Instance == null)
            Instance = this;
    }


    public void OpenStatusWindow(Clickable clickable) {
        clickable.UpdateClickable();
        clickableUI.SetActive(true);
    }
    public void CloseStatusWindow(Clickable clickable) {
        clickableUI.SetActive(false);
    }
    public void UpdateCLickableUI(string clickableName, float populationFillerAmount, float feedingFillerAmount, Sprite clickableSprite = null) {

        nameText.text = "Name : " + clickableName;
        populationFillerImage.fillAmount = populationFillerAmount;
        feedingFillerImage.fillAmount = feedingFillerAmount;
        clickableImage.sprite = clickableSprite;

    }


    public bool IsUIClicked() {
        if (EventSystem.current.currentSelectedGameObject == null) return false;
        else return true;

    }
    public bool IsPointerOverUI() {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        if (raysastResults.Count != 0) {
            return true;
        }
        //foreach (RaycastResult raysastResult in raysastResults) {
        //    if (raysastResult.gameObject.CompareTag(tag)) {
        //        return true;
        //    }
        //}
        return false;
    }

}

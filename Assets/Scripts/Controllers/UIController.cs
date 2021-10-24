using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public List<GameObject> panelList;
    [Header("ClickableUI")]
    public GameObject clickableUI;
    public TextMeshProUGUI nameText;
    public Image populationFillerImage;
    public Image feedingFillerImage;
    public Image clickableImage;
   
    private void Awake() {
        if (instance == null) 
        instance = this;
    }


    public void OpenStatusWindow(Clickable clickable) {
        clickable.UpdateClickable();
        clickableUI.SetActive(true);
    }
    public void CloseStatusWindow(Clickable clickable) {
        clickableUI.SetActive(false);
    }
    public void UpdateCLickableUI(string clickableName, float populationFillerAmount,float feedingFillerAmount, Sprite clickableSprite = null) {

        nameText.text = "Name : "  + clickableName;
        populationFillerImage.fillAmount = populationFillerAmount;
        feedingFillerImage.fillAmount = feedingFillerAmount;
        clickableImage.sprite = clickableSprite;

    }


}

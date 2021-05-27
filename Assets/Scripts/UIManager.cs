using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager Instance
        
    {
        get
        {
            return _instance;
        }
        set
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager Instance is null");
            }
            _instance = value;
        }
    }

    [SerializeField] private GameObject _shopUI;
    public Text playerGemCountText;
    public Image selectionImg;
    public Text gemCountText;
    public Image[] healthBars;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = gemCount + "G";
    }

    public void ShopSystem(bool isOpen)
    {
        _shopUI.gameObject.SetActive(isOpen);
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int gemCount)
    {
        gemCountText.text = gemCount.ToString();
    }

    public void UpdateLives(int livesRemaining)
    {
        for(int i =0; i<=livesRemaining; i++)
        {
            if(i == livesRemaining)
            {
                healthBars[i].enabled = false;
            }
        }

    }
 

}

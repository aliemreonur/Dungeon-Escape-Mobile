using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    private bool _shopOpen = false;
    public int currentItemIndex;
    public int currentItemCost;

    private Player _player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if(_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }
            UIManager.Instance.ShopSystem(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.ShopSystem(false);
        }

    }

    public void ItemSelect(int item)
    {
        Debug.Log("Selected " + item);
        switch(item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(-30);
                currentItemIndex = 0;
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-144);
                currentItemIndex = 1;
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-254);
                currentItemIndex = 2;
                currentItemCost = 100;
                break;
        }

    }

    public void Buy()
    {
        //Player player = 
        if(_player.diamonds >= currentItemCost)
        {
            if(currentItemIndex == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
                //this is the win condition - if you purchase the key to castle you win!
            }

            _player.diamonds -= currentItemCost;
            Debug.Log("Purchase Successful. Remaining Gems : " + _player.diamonds);
            UIManager.Instance.ShopSystem(false);
        }
        else
        {
            Debug.Log("You do not have enough gems. Closing the shop.");
            UIManager.Instance.ShopSystem(false);
        }

    }
}

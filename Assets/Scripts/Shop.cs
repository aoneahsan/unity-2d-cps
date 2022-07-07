using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
  [SerializeField] Button flameSwardButton;
  [SerializeField] Button KeyToCastleButton;
  [SerializeField] Button BootsOfFlightButton;
  [SerializeField] Button buyButton;

  GameManager gameManager;

  ShopItems selectedItem;
  int selectedItemPrice;

  private void Awake()
  {
    gameManager = FindObjectOfType<GameManager>();
  }

  public void SelectItem(int itemIndex)
  {
    if (gameManager != null)
    {
      DeselectAllItems();
      switch (itemIndex)
      {
        case 0:
          selectedItem = ShopItems.FlameSward;
          SetItemSelected(flameSwardButton);
          break;
        case 1:
          selectedItem = ShopItems.BootsOfFlight;
          SetItemSelected(KeyToCastleButton);
          break;
        case 2:
          selectedItem = ShopItems.KeyToCastle;
          SetItemSelected(BootsOfFlightButton);
          break;
      }
    }
  }

  void SetItemSelected(Button button)
  {
    button.GetComponent<Image>().color = Color.green;
    selectedItemPrice = button.GetComponent<PurchasableItem>().Price;

    if (gameManager.Player.Diamonds >= selectedItemPrice)
    {
      buyButton.interactable = true;
    }
    else
    {
      buyButton.interactable = false;
    }
  }

  void DeselectAllItems()
  {
    selectedItem = ShopItems.None;
    flameSwardButton.GetComponent<Image>().color = Color.white;
    KeyToCastleButton.GetComponent<Image>().color = Color.white;
    BootsOfFlightButton.GetComponent<Image>().color = Color.white;
  }

  public void BuyItem()
  {
    if (gameManager != null)
    {
      bool itemBought = gameManager.Player.BuyItem(selectedItem, selectedItemPrice);
      DeselectAllItems();
      if (itemBought)
      {
        gameManager.GameUIManager.ShowPurchaseSuccessMessage();
      }
      else
      {
        gameManager.GameUIManager.ShowPurchaseFailedMessage();
      }
    }
  }
}

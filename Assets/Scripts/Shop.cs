using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
  [SerializeField] Button flameSwardButton;
  [SerializeField] Button KeyToCastleButton;
  [SerializeField] Button BootsOfFlightButton;
  [SerializeField] Button buyButton;

  GameManager gameManager;

  int selectedItem;
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
      selectedItem = itemIndex;
      switch (selectedItem)
      {
        case 0:
          SetItemSelected(flameSwardButton);
          break;
        case 1:
          SetItemSelected(KeyToCastleButton);
          break;
        case 2:
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
    selectedItem = -1;
    flameSwardButton.GetComponent<Image>().color = Color.white;
    KeyToCastleButton.GetComponent<Image>().color = Color.white;
    BootsOfFlightButton.GetComponent<Image>().color = Color.white;
  }
}

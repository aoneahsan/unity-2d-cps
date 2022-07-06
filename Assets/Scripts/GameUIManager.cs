using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
  [SerializeField] GameObject shopUI;

  public bool IsShopOpen()
  {
    if (shopUI != null)
    {
      return shopUI.activeInHierarchy;
    }
    else
    {
      return false;
    }
  }

  public void OpenShop()
  {
    if (shopUI != null)
    {
      shopUI.SetActive(true);
    }
  }

  public void CloseShop()
  {
    if (shopUI != null)
    {
      shopUI.SetActive(false);
    }
  }
}

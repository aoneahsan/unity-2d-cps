using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickable : Pickable
{
  public override void SetPickableType(PickableType pickableType)
  {
    if (pickableType != PickableType.Diamond)
    {
      Debug.LogError("PickableType is not set to Diamond, as this is a DiamondPickable.");
    }
    else
    {
      this.pickableType = pickableType;
    }
  }
}

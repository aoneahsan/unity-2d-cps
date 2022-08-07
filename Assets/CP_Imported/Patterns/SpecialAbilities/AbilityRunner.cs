using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRunner : MonoBehaviour
{

  //   [SerializeField] IAbility currentAbility = new DelayedDecorator(new RageAbility(), 2);
  [SerializeField]
  IAbility currentAbility = new SequenceComposite(new List<IAbility>(){
    new DelayedDecorator(new RageAbility(), 2),
    new DelayedDecorator(new RageAbility(), 2),
    new RageAbility(),
    new HealAbility(),
    new FireBallAbility(),
  });

  public void UseAbility()
  {
    currentAbility.Use();
  }
}

public interface IAbility
{
  void Use();
}

public class DelayedDecorator : MonoBehaviour, IAbility
{
  IAbility wrappedAbility;
  float delay;

  public DelayedDecorator(IAbility wrappedAbility, float delay)
  {
    this.delay = delay;
    this.wrappedAbility = wrappedAbility;
  }

  public void Use()
  {
    StartCoroutine(InvokeWrappedAbilityUse());
  }

  IEnumerator InvokeWrappedAbilityUse()
  {
    yield return new WaitForSeconds(delay);
    wrappedAbility.Use();
  }
}

public class RageAbility : IAbility
{
  public void Use()
  {
    Debug.Log("I'm always angry");
  }
}

public class FireBallAbility : IAbility
{
  public void Use()
  {
    Debug.Log("Launch Fireball");
  }
}

public class HealAbility : IAbility
{
  public void Use()
  {
    Debug.Log("Here eat this!");
  }
}

public class SequenceComposite : IAbility
{
  List<IAbility> abilities;

  public SequenceComposite(List<IAbility> abilities)
  {
    this.abilities = abilities;
  }

  public void Use()
  {
    foreach (IAbility ability in abilities)
    {
      ability.Use();
    }
  }
}
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public float attackRate;

    public abstract void Attack();

    public virtual void SpecialAttack()
    {
        Debug.Log("Special attack for " + weaponName);
    }

    public virtual void Equip()
    {
        Debug.Log("Equipped " + weaponName);
    }

    public virtual void Unequip()
    {
        Debug.Log("Unequipped " + weaponName);
    }
}
using UnityEngine;

public class Sword : Weapon
{
    private void Start()
    {
        weaponName = "Sword";
        damage = 20;
        attackRate = 1.2f;
    }

    public override void Attack()
    {
        Debug.Log("Sword Attack");
    }

    public override void SpecialAttack()
    {
        Debug.Log("Sword Special Attack");
    }
}

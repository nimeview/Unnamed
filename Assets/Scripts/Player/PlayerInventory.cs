using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    private Weapon equippedWeapon;

    private void Start()
    {
        if (weapons.Count > 0)
        {
            EquipWeapon(weapons[0]);
        }
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.Unequip();
        }
        
        equippedWeapon = newWeapon;
        equippedWeapon.Equip();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.Count > 0)
        {
            EquipWeapon(weapons[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1)
        {
            EquipWeapon(weapons[1]);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && equippedWeapon != null)
        {
            equippedWeapon.Attack();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController_sc : MonoBehaviour
{
    public weapon_sc[] weapons = new weapon_sc[2]; // Array to hold two weapons
    private int currentWeaponIndex = 0; // Index of the currently equipped weapon

    public Transform weaponHolder; // Transform of the character's weapon holder (e.g., hand or weapon slot)

    private void Start()
    {
        EquipWeapon(currentWeaponIndex); // Equip the first weapon by default
    }

    private void Update()
    {
        HandleWeaponSwitching();
        HandleAttack();


    }

    // Handles weapon switching via keyboard (1, 2) or mouse scroll
    private void HandleWeaponSwitching()
    {
        // Switch weapon using number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }

        // Switch weapon using mouse scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            EquipWeapon((currentWeaponIndex + 1) % weapons.Length);
        }
        else if (scroll < 0f)
        {
            EquipWeapon((currentWeaponIndex - 1 + weapons.Length) % weapons.Length);
        }
    }

    // Handles attacking with the current weapon
    private void HandleAttack()
    {
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            if (weapons[currentWeaponIndex] != null)
            {
                weapons[currentWeaponIndex].Fire();
            }
        }
    }

    // Equips a weapon by index
    private void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Length || weapons[weaponIndex] == null)
        {
            Debug.LogWarning("Invalid weapon index or weapon not assigned.");
            return;
        }

        // Deactivate all weapons
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                weapons[i].gameObject.SetActive(false);
                
            }
        }

        // Activate the selected weapon
        currentWeaponIndex = weaponIndex;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        weapons[currentWeaponIndex].weaponHolder = weaponHolder;
        Debug.Log($"Equipped weapon: {weapons[currentWeaponIndex].weaponName}");
    }

    // Allows changing the weapon layout via UI
    public void SetWeapon(int slot, weapon_sc newWeapon)
    {
        if (slot < 0 || slot >= weapons.Length)
        {
            Debug.LogWarning("Invalid weapon slot.");
            return;
        }

        weapons[slot] = newWeapon;
        Debug.Log($"Weapon in slot {slot + 1} set to: {newWeapon.weaponName}");
    }
}
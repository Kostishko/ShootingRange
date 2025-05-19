using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController_sc : MonoBehaviour
{
    public weapon_sc[] weapons = new weapon_sc[4]; // Array to hold two weapons
    public int currentWeaponIndex = 0; // Index of the currently equipped weapon

    public Transform weaponHolder; // Transform of the character's weapon holder 

    private void Start()
    {
        EquipWeapon(currentWeaponIndex); // Equip the first weapon by default
    }

    private void Update()
    {

        if (gameManager_sc.currentGameState == gameManager_sc.GameState.Gameplay || gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting)
        {
            HandleWeaponSwitching();
            //HandleAttack();
        }


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
            EquipWeapon((currentWeaponIndex + 1) % 2);
        }
        else if (scroll < 0f)
        {
            EquipWeapon((currentWeaponIndex - 1 + 2) % 2);
        }
    }

    // Handles attacking with the current weapon
    //private void HandleAttack()
    //{
    //    if (Input.GetButtonDown("Fire1")) // Left mouse button
    //    {
    //        if (weapons[currentWeaponIndex] != null)
    //        {
    //            weapons[currentWeaponIndex].Fire();
    //        }
    //    }
    //}

    // Equips a weapon by index
    public void EquipWeapon(int weaponIndex)
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
        weapon_sc tempWeapon = weapons[slot];
        int tempIndex = 0;
        while(newWeapon!=weapons[tempIndex])
        {
            tempIndex++;         
        }
        weapons[slot] = newWeapon;
        weapons[tempIndex] = tempWeapon;
        
    }
}
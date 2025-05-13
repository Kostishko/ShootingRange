using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class weaponCardData_sc 
{

    public weapon_sc weapon;
    public Label WeaponName;
    public Label WeaponDescription;
    public VisualElement WeaponPicture;
    public VisualElement WeaponCard;

    public weaponCardData_sc(weapon_sc weapon, VisualElement weaponCard)
    {
        this.WeaponCard = weaponCard;
        this.weapon = weapon;
        WeaponName = weaponCard.Q<Label>("WeaponName");
        WeaponDescription = weaponCard.Q<Label>("WeaponDescription");
        WeaponPicture = weaponCard.Q<VisualElement>("WeaponPicture");

        WeaponName.text = weapon.weaponName;
        WeaponDescription.text = weapon.description;
        WeaponPicture.style.backgroundImage = new StyleBackground( weapon.weaponStats.weaponIcon);
    }


    public void UpdateMe(weapon_sc newWeapon)
    {
        WeaponName.text = newWeapon.weaponName;
        WeaponDescription.text = newWeapon.description;
        WeaponPicture.style.backgroundImage = new StyleBackground(newWeapon.weaponStats.weaponIcon);
    }



}

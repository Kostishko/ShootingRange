using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapons/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    public Sprite weaponIcon; // Sprite for UI
    public string weaponName; // Name of the weapon
    public string description; // Description of the weapon
    public float damage; // Damage dealt by the weapon
    public float fireRate; // Rate of fire (shots per second)
    public int magazineSize; // Magazine size
    public float reloadTime; // Reload time   
}
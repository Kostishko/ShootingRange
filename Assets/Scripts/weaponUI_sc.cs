using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class weaponUI_sc : MonoBehaviour
{
    public weaponController_sc weaponController; // Reference to the weapon controller
    public VisualElement root;
    public Label AmmoCcounter;

    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        AmmoCcounter = root.Q<Label>("WeaponAmmo");
    }

    public void Update()
    {
      AmmoCcounter.text = weaponController.weapons[weaponController.currentWeaponIndex].currentAmmo.ToString() + "/" + weaponController.weapons[weaponController.currentWeaponIndex].magazineSize.ToString();
    }


}

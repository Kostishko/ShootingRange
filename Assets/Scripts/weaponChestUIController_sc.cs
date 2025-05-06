using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class weaponChestUIController_sc : MonoBehaviour
{
  

    public VisualElement root;
    public weaponChest_sc weaponChest; // Reference to the weapon chest script
    public weaponController_sc weaponController; // Reference to the weapon controller

    public VisualElement weaponCard1;
    public VisualElement weaponCard2;
    public VisualElement weaponCard3;
    public VisualElement weaponCard4;

    public VisualElement currentWeaponCard;

    public weaponCardData_sc weaponCardData1;
    public weaponCardData_sc weaponCardData2;
    public weaponCardData_sc weaponCardData3;
    public weaponCardData_sc weaponCardData4;



    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None; // Hide the UI by default

        weaponCard1 = root.Q<VisualElement>("WeaponCard1");
        weaponCard2 = root.Q<VisualElement>("WeaponCard2");
        weaponCard3 = root.Q<VisualElement>("WeaponCard3");
        weaponCard4 = root.Q<VisualElement>("WeaponCard4");

        // Set up the weapon cards with the weapons from the weapon chest
        weaponCardData1 = new weaponCardData_sc(weaponController.weapons[0], weaponCard1);
        weaponCardData2 = new weaponCardData_sc(weaponController.weapons[1], weaponCard2);
        weaponCardData3 = new weaponCardData_sc(weaponController.weapons[2], weaponCard3);
        weaponCardData4 = new weaponCardData_sc(weaponController.weapons[3], weaponCard4);



        weaponCard1.RegisterCallback<ClickEvent>(e =>
        {
           if (e.propagationPhase == PropagationPhase.BubbleUp || e.propagationPhase == PropagationPhase.AtTarget)
            {
                if(currentWeaponCard != null)
                {
                    if(currentWeaponCard != weaponCard1)
                    {
                        

                    }
                }
                else
                {
                    currentWeaponCard = weaponCard1;
                }
            }

        });

    }




}

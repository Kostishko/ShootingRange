using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class weaponChestUIController_sc : MonoBehaviour
{
  

    public VisualElement root;
    public weaponChest_sc weaponChest; // Reference to the weapon chest script
    public weaponController_sc weaponController; // Reference to the weapon controller


    public VisualElement chestUI;
    public VisualElement playerWeaponLayout;
    public VisualElement weaponInTheChest;
    public weaponCardData_sc currentWeaponCard;
    public VisualTreeAsset weaponCardTemplate;

    public List<weaponCardData_sc> weaponData;
    public List<VisualElement> weaponsCards;





    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        chestUI = root.Q("Inventory");
        playerWeaponLayout = root.Q("PlayerLayout");
        weaponInTheChest = root.Q("InTheChest");

        weaponCardTemplate = Resources.Load<VisualTreeAsset>("UI/WeaponCard");


        weaponData = new List<weaponCardData_sc>();
        weaponsCards = new List<VisualElement>();

        //add UI information for all weapon 
        for (int i = 0; i <= weaponController.weapons.Length; i++)
        {
            VisualElement newCard = weaponCardTemplate.CloneTree();
            weaponCardData_sc weaponCard = new weaponCardData_sc(weaponController.weapons[i], newCard);

            if (i < 2)
            {
                //first two weapon are in player layout
                playerWeaponLayout.Add(newCard);
            }
            else
            {
                //rest of the weapons are in the chest
                weaponInTheChest.Add(newCard);
            }
            
            weaponData.Add(weaponCard);

            newCard.RegisterCallback<ClickEvent>(e => {
                if (e.propagationPhase == PropagationPhase.AtTarget || e.propagationPhase == PropagationPhase.BubbleUp)
                {
                    if (currentWeaponCard != null)
                    {
                        weaponController.SetWeapon(i, currentWeaponCard.weapon);
                        currentWeaponCard.WeaponCard.RemoveFromClassList("weaponCardSelected");


                    }
                    else
                    {
                        currentWeaponCard = weaponData[i];
                        currentWeaponCard.WeaponCard.AddToClassList("weaponCardSelected");
                        

                    }
                }
            });

        }





    }

    public void ShowWeaponChestUI()
    {
        chestUI.RemoveFromClassList("hiddenLeft");
        UnityEngine.Cursor.lockState = CursorLockMode.None;





    }




}

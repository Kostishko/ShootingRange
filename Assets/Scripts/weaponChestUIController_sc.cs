using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class weaponChestUIController_sc : MonoBehaviour
{

    //Connect to the game world
    public VisualElement root;
    public weaponChest_sc weaponChest; // Reference to the weapon chest script
    public weaponController_sc weaponController; // Reference to the weapon controller


    // UI elements for the weapon layout
    public VisualElement chestUI;
    public VisualElement playerWeaponLayout;
    public VisualElement weaponInTheChest;
    public weaponCardData_sc currentWeaponCard;
    public VisualTreeAsset weaponCardTemplate;

    //Button for exit from the chest (lol)
    public Button exitButton;

    public List<weaponCardData_sc> weaponData;






    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        chestUI = root.Q("Inventory");
        playerWeaponLayout = root.Q("PlayerLayout");
        weaponInTheChest = root.Q("InTheChest");
        exitButton = root.Q<Button>("InventoryExit");

        weaponCardTemplate = Resources.Load<VisualTreeAsset>("UI/WeaponCard");


        weaponData = new List<weaponCardData_sc>();


        //add UI information for all weapon 
        for (int i = 0; i < weaponController.weapons.Length; i++)
        {
            VisualElement newCard = weaponCardTemplate.CloneTree();
            weaponCardData_sc weaponCard = new weaponCardData_sc(weaponController.weapons[i], newCard);
            newCard.AddToClassList("weaponCard");

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

            // Fix closure issue by creating a local copy
            var localWeaponCard = weaponCard;
            int localIndex = i;

            localWeaponCard.WeaponCard.RegisterCallback<ClickEvent>(e =>
            {
                if (e.propagationPhase == PropagationPhase.AtTarget || e.propagationPhase == PropagationPhase.BubbleUp)
                {
                    if (currentWeaponCard != null && currentWeaponCard != localWeaponCard)
                    {
                        // Swap weapons in weaponController
                        int firstIndex = weaponData.IndexOf(currentWeaponCard);
                        int secondIndex = weaponData.IndexOf(localWeaponCard);

                        // Swap in weaponController
                        weapon_sc tempWeapon = weaponController.weapons[firstIndex];
                        weaponController.SetWeapon(firstIndex, weaponController.weapons[secondIndex]);
                        weaponController.SetWeapon(secondIndex, tempWeapon);
                        weaponController.EquipWeapon(0);

                        // Swap in weaponData
                        weaponCardData_sc tempCard = weaponData[firstIndex];
                        weaponData[firstIndex] = weaponData[secondIndex];
                        weaponData[secondIndex] = tempCard;

                        // Update UI
                        currentWeaponCard.WeaponCard.RemoveFromClassList("weaponCardSelected");
                        UpdateIventoryUI();
                        currentWeaponCard = null;
                    }
                    else if (currentWeaponCard != localWeaponCard)
                    {
                        // Select this card
                        if (currentWeaponCard != null)
                            currentWeaponCard.WeaponCard.RemoveFromClassList("weaponCardSelected");

                        currentWeaponCard = localWeaponCard;
                        currentWeaponCard.WeaponCard.AddToClassList("weaponCardSelected");
                    }
                }
            });

            exitButton.clicked += () =>
            {
                chestUI.AddToClassList("hiddenLeft");
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                gameManager_sc.currentGameState = gameManager_sc.GameState.Waiting;
            };

        }
    }

    public void ShowWeaponChestUI()
    {
        UpdateIventoryUI();
        chestUI.RemoveFromClassList("hiddenLeft");
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        gameManager_sc.currentGameState = gameManager_sc.GameState.MainMenu;

    }

    /// <summary>
    /// Update the inventory UI when a weapon is added or removed from the chest.
    /// </summary>
    public void UpdateIventoryUI()
   {
        for (int i = 0; i < weaponData.Count; i++)
        {
            weaponData[i].WeaponCard.parent.hierarchy.Remove(weaponData[i].WeaponCard);
            if(i < 2)
            {
                playerWeaponLayout.Add(weaponData[i].WeaponCard);
            }
            else
            {
                weaponInTheChest.Add(weaponData[i].WeaponCard);
            }
        }
    }
}

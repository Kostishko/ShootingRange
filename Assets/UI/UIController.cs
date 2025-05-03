using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public gameManager_sc gameManager; // Reference to the GameManager script

    //major root element
    public VisualElement root;
    

    //menu elements
    //main Menu
    public VisualElement menuRoot;
    public VisualElement MainMenuPanel;
    public Button NewGameButton;
    public Button RecordsButton;
    public Button SettingsButton;
    public Button ExitButton;

    //settings menu
    public VisualElement SettingsPanel;

    //records menu
    public VisualElement RecordsPanel;

    //overlay elements
    public VisualElement overlayRoot;

    //inventary elements
    public VisualElement inventaryRoot;

    //promted elements
    public VisualElement promptMessage;
    public Label promtLabel;

    //Character
    public characterInteraction_sc characterInteraction;


    // Start is called before the first frame update
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        //menu elements initialisation
        menuRoot = root.Q("Canvas");
        menuRoot.style.display = DisplayStyle.Flex;
                


        //main menu
        MainMenuPanel = root.Q("MainMenuPanel");
        MainMenuPanel.style.display = DisplayStyle.Flex;
        NewGameButton = root.Q<Button>("NewGameButton");
        RecordsButton = root.Q<Button>("RecordsButton");
        SettingsButton = root.Q<Button>("SettingsButton");
        ExitButton = root.Q<Button>("ExitButton");
        MainMenuPanel.RemoveFromClassList("hiddenLeft");
        //MainMenuPanel.AddToClassList("shownPanel");

        NewGameButton.clicked += () =>
        {
            gameManager.SetGameState(gameManager_sc.GameState.Gameplay);
            MainMenuPanel.AddToClassList("hiddenLeft");            
            //MainMenuPanel.RemoveFromClassList("shownPanel");
           // overlayRoot.style.display = DisplayStyle.Flex;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        };

        RecordsButton.clicked += () =>
        {
            MainMenuPanel.AddToClassList("hiddenLeft");
            //MainMenuPanel.RemoveFromClassList("shownPanel");
            RecordsPanel.RemoveFromClassList("hiddenRight");
            //RecordsPanel.AddToClassList("shownPanel");

        };

        SettingsButton.clicked += () =>
        {
            MainMenuPanel.AddToClassList("hiddenLeft");
            //MainMenuPanel.RemoveFromClassList("shownPanel");
            SettingsPanel.RemoveFromClassList("hiddenTop");
            //SettingsPanel.AddToClassList("shownPanel");

        };

        ExitButton.clickable.clicked += () =>
        {
            Application.Quit();
        };

        SettingsPanel = root.Q("Setting");        
        RecordsPanel = root.Q("Records");
        

        //overlay elements initialisation
        overlayRoot = root.Q("GameplayOverlay");
        overlayRoot.style.display = DisplayStyle.None;
        //inventaryRoot = root.Q("InventaryRoot");

        //prompt
        promptMessage = root.Q("PromtBox");
        promtLabel = promptMessage.Q<Label>("PromtLabel");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager_sc.currentGameState == gameManager_sc.GameState.Gameplay || 
                gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting ||
                gameManager_sc.currentGameState == gameManager_sc.GameState.Inventary)
            {
                gameManager.SetGameState(gameManager_sc.GameState.MainMenu);
                overlayRoot.style.display = DisplayStyle.None;
                menuRoot.style.display = DisplayStyle.Flex;
                MainMenuPanel.style.display = DisplayStyle.Flex;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
            }            
        }
        else
        {
           Application.Quit();
        }


        //promt message logic
        if(characterInteraction.isInteracting)
        {
            promptMessage.style.visibility = Visibility.Visible;
            promtLabel.text = characterInteraction.detectHit.collider.GetComponent<interactable_sc>().promtMessage;
            Vector3 screenPos = characterInteraction.cam.WorldToScreenPoint(characterInteraction.detectHit.collider.transform.position);
            promptMessage.style.left = screenPos.x + promptMessage.layout.width/2;
            promptMessage.style.top = Screen.height - screenPos.y - 100;
        }
        else
        {
            promptMessage.style.visibility = Visibility.Hidden;
        }

    }

    public void Awake()
    {
        // Set the initial game state to MainMenu
        gameManager.SetGameState(gameManager_sc.GameState.MainMenu);
    }

    public void OpenInventoryPanel()
    {

    }

    public void CloseInventoryPanel()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class tagetInfoUIController_sc : MonoBehaviour
{

    //major root element
    public VisualElement root;



    //promted elements
    public ProgressBar HealthBar;
    public Label DamageInfo;

    //Character
    public targetInfoReciever_sc characterReciever;


    public void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        //menu elements initialisation
        HealthBar = root.Q<ProgressBar>("HealthBar");
        HealthBar.style.visibility = Visibility.Hidden;
        DamageInfo = root.Q<Label>("DamageInfo");
        DamageInfo.style.visibility = Visibility.Hidden;
    }


    public void Update()
    {
        if (gameManager_sc.currentGameState == gameManager_sc.GameState.Gameplay || gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting)
        {

            if (characterReciever.isInteracting)
            {
                if (characterReciever.currentTarget != null)
                {
                    HealthBar.value = characterReciever.currentTarget.health;
                    //HealthBar.transform.position = characterReciever.currentTarget.healthBarPosition.position;
                    Vector3 screenPos = characterReciever.cam.WorldToScreenPoint(characterReciever.detectHit.collider.transform.position);
                    HealthBar.style.left = screenPos.x + HealthBar.layout.width;
                    HealthBar.style.top = screenPos.y - 100;
                    HealthBar.style.visibility = Visibility.Visible;
                    // DamageInfo.style.visibility = Visibility.Visible;
                }


            }
            else
            {
                HealthBar.style.visibility = Visibility.Hidden;
                // DamageInfo.style.visibility = Visibility.Hidden;
            }
        }

    }
}

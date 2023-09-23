using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public GameObject[] menus; // Array de men�s
    public GameObject[] topicButtons; // Array de objetos de juego de temas

    private int currentMenuIndex = 0; // �ndice del men� actual


    public void OnBackButtonClick()
    {
        currentMenuIndex--; // Retroceder al men� anterior

        if (currentMenuIndex < 0)
        {
            currentMenuIndex = 0; // Evitar �ndices negativos
        }

        UpdateMenus();
    }

    public void OnNextButtonClick()
    {
        currentMenuIndex++; // Avanzar al siguiente men�

        if (currentMenuIndex >= menus.Length)
        {
            currentMenuIndex = menus.Length - 1; // Evitar �ndices fuera de rango
        }

        UpdateMenus();
    }

    public void OnTopicButtonClick(int topicIndex)
    {
        switch (topicIndex)
        {
            case 0: // Tema 1
                currentMenuIndex = 0; 
                break;
            case 1: // Tema 2
                currentMenuIndex = 6; 
                break;
            case 2: // Tema 3
                currentMenuIndex = 8; 
                break;
            case 3: // Tema 4
                currentMenuIndex = 9; 
                break;
            case 4: // Tema 5
                currentMenuIndex = 11; 
                break;
            case 5: // Tema 6
                currentMenuIndex = 12; 
                break;
        }

        UpdateMenus();
    }

    private void UpdateMenus()
    {
        // Activar el men� actual y desactivar los dem�s
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == currentMenuIndex);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using static UnityEngine.GraphicsBuffer;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject configurationMenu;
    public GameObject robotMenu;
    public GameObject targetMenu;
    public GameObject stationMenu;
    public GameObject languageMenu;
    public GameObject guidanceMenu;
    public GameObject ToolMenu;
    public GameObject advicemenu;
    public GameObject advicerobotmenu;
    public GameObject JogRobot;
    public GameObject Intro1;
    public GameObject Intro2;
    public GameObject Intro3;
    public GameObject Intro4;
    public GameObject Intro5;
    public GameObject Intro6;
    public GameObject QRCode1;
    public GameObject QRCode2;
    public GameObject Assembly1;
    public GameObject Targets1;
    public GameObject Targets2;
    public GameObject Gripper1;
    public GameObject Robot1;
    public GameObject Robot2;
    public GameObject backplatestandard;
    public GameObject AdviceJog;
    public GameObject ErrorTarget;
    public GameObject RightHandMenu;

    private void Start()
    {
        // Mostrar solo el menú principal al inicio
        mainMenu.SetActive(true);
        configurationMenu.SetActive(false);
        robotMenu.SetActive(false);
        targetMenu.SetActive(false);
        stationMenu.SetActive(false);
        languageMenu.SetActive(false);
        guidanceMenu.SetActive(false);
        ToolMenu.SetActive(false);
        Intro1.SetActive(false);
        Intro2.SetActive(false);
        Intro3.SetActive(false);
        Intro4.SetActive(false);
        Intro5.SetActive(false);
        Intro6.SetActive(false);
        QRCode1.SetActive(false);
        QRCode2.SetActive(false);
        Assembly1.SetActive(false);
        Targets1.SetActive(false);
        Targets2.SetActive(false);
        Gripper1.SetActive(false);
        Robot2.SetActive(false);
        Robot1.SetActive(false);
        backplatestandard.SetActive(false);
        advicemenu.SetActive(false);
        advicerobotmenu.SetActive(false);
        JogRobot.SetActive(false);
        AdviceJog.SetActive(false);
        ErrorTarget.SetActive(false);   
    }

    public void ShowTutorialMenu()
    {
        // Ocultar el menú principal y mostrar el menú de tutorial
        mainMenu.SetActive(false);
    }

    public void ShowNormalMenu()
    {
        // Ocultar el menú principal y mostrar los 4 menús de opciones
        mainMenu.SetActive(false);
        advicemenu.SetActive(true);
    }

    public void ShowAdvancedMenu()
    {
        mainMenu.SetActive(false);
        RightHandMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        // Mostrar solo el menú principal
        mainMenu.SetActive(true);
        configurationMenu.SetActive(false);
        robotMenu.SetActive(false);
        targetMenu.SetActive(false);
        stationMenu.SetActive(false);
        guidanceMenu.SetActive(false);
    }

}

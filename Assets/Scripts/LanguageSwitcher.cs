using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class LanguageSwitcher : MonoBehaviour
{

    private bool inEnglish = true;
    private bool inSwedish = false;
    private bool inSpanish = false;
    // Reference to the all TextMeshPro-Text components that needs to be translated

    // Hand menu 
    [SerializeField]
    private TextMeshPro HandConfigMenu;
    [SerializeField]
    private TextMeshPro HandRobotMenu;
    [SerializeField]
    private TextMeshPro HandTargetMenu;
    [SerializeField]
    private TextMeshPro HandStationMenu;
    [SerializeField]
    private TextMeshPro HandGuidanceMenu;

    // Configuration menu 
    [SerializeField]
    private TextMeshPro ConfigTitle;
    [SerializeField]
    private TextMeshPro ConfigLanguage;
    [SerializeField]
    private TextMeshPro ConfigGoHome;
    [SerializeField]
    private TextMeshPro ConfigOnlyVirtual;
    [SerializeField]
    private TextMeshPro ConfigHelp;

    // Station menu
    [SerializeField]
    private TextMeshPro StationTitle;
    [SerializeField]
    private TextMeshPro StatQRCodeRobot;
    [SerializeField]
    private TextMeshPro StatQRCodeWorkobject;
    [SerializeField]
    private TextMeshPro StatQRCodeSearchTurnOFF;
    [SerializeField]
    private TextMeshPro StatTurnOnVuforia;
    [SerializeField]
    private TextMeshPro StatTurnOffVuforia;
    [SerializeField]
    private TextMeshPro StatHelp;


    // Robot menu 
    [SerializeField]
    private TextMeshPro RobotTitle;
    [SerializeField]
    private TextMeshPro RobotSendTargets;
    [SerializeField]
    private TextMeshPro RobotStartPath;
    [SerializeField]
    private TextMeshPro RobotStartProcess;
    [SerializeField]
    private TextMeshPro RobotStopSim;
    [SerializeField]
    private TextMeshPro RobotHomePosition;
    [SerializeField]
    private TextMeshPro RobotMoveAtTarget;
    [SerializeField]
    private TextMeshPro RobotJogRobot;
    [SerializeField]
    private TextMeshPro RobotChangeTool;
    [SerializeField]
    private TextMeshPro RobotToggleRealRobot;
    [SerializeField]
    private TextMeshPro PreviewPath;
    [SerializeField]
    private TextMeshPro ClearTrajectoryPreview;
    [SerializeField]
    private TextMeshPro RobotResume;
    [SerializeField]
    private TextMeshPro RobotPause;
    [SerializeField]
    private TextMeshPro RobotHelp;


    // Target menu 
    [SerializeField]
    private TextMeshPro TargetTitle;
    [SerializeField]
    private TextMeshPro TargetAddTarget;
    [SerializeField]
    private TextMeshPro TargetAddPath;
    [SerializeField]
    private TextMeshPro TargetAddPathEnd;
    [SerializeField]
    private TextMeshPro TargetDeleteAllTargets;
    [SerializeField]
    private TextMeshPro TargetDeleteSelectedTargets;
    [SerializeField]
    private TextMeshPro TargetDeleteSelectedPath;
    [SerializeField]
    private TextMeshPro TargetDeleteAllPaths;
    [SerializeField]
    private TextMeshPro TargetTogglePreview;
    [SerializeField]
    private TextMeshPro TargetPreviousPath;
    [SerializeField]
    private TextMeshPro TargetNextPath;
    [SerializeField]
    private TextMeshPro TargetHelp;
    [SerializeField]
    private TextMeshPro TargetAfterFinishing;


    // StartMenu
    [SerializeField]
    private TextMeshPro StartDescription;
    [SerializeField]
    private TextMeshPro StartTutorial;
    [SerializeField]
    private TextMeshPro startNormal;
    [SerializeField]
    private TextMeshPro startAdvanced;

    // LanguageMenu
    [SerializeField]
    private TextMeshPro ChooseLanguage;
    [SerializeField]
    private TextMeshPro LangEnglish;
    [SerializeField]
    private TextMeshPro LangSwedish;
    [SerializeField]
    private TextMeshPro LangSpanish;


    // BackPlate Buttons
    [SerializeField]
    private TextMeshPro IntroButton;
    [SerializeField]
    private TextMeshPro QRCodesButton;
    [SerializeField]
    private TextMeshPro AssemblyButton;
    [SerializeField]
    private TextMeshPro TargetButton;
    [SerializeField]
    private TextMeshPro GripperButton;
    [SerializeField]
    private TextMeshPro RobotButton;

    //Intro 1 Menu
    [SerializeField]
    private TextMeshPro Intro1Title;
    [SerializeField]
    private TextMeshPro Intro1Description;

    // Intro 2 Menu
    [SerializeField]
    private TextMeshPro Intro2Title;
    [SerializeField]
    private TextMeshPro Intro2Description;

    //Intro 3 Menu
    [SerializeField]
    private TextMeshPro Intro3Title;
    [SerializeField]
    private TextMeshPro Intro3Description;

    //Intro 4 Menu
    [SerializeField]
    private TextMeshPro Intro4Title;
    [SerializeField]
    private TextMeshPro Intro4Description;
    [SerializeField]
    private TextMeshPro Intro4HandMenuText;

    //Intro 5 Menu
    [SerializeField]
    private TextMeshPro Intro5Title;
    [SerializeField]
    private TextMeshPro Intro5Description;

    //Intro 6 Menu
    [SerializeField]
    private TextMeshPro Intro6Title;
    [SerializeField]
    private TextMeshPro Intro6Description;


    //QR 1 Menu
    [SerializeField]
    private TextMeshPro QR1Title;
    [SerializeField]
    private TextMeshPro QR1Description;

    //QR 2 Menu
    [SerializeField]
    private TextMeshPro QR2Title;
    [SerializeField]
    private TextMeshPro QR2Description;

    //Assembly 1 Menu
    [SerializeField]
    private TextMeshPro Assembly1Title;
    [SerializeField]
    private TextMeshPro Assembly1Description;


    //Target 1 Menu
    [SerializeField]
    private TextMeshPro Target1Title;
    [SerializeField]
    private TextMeshPro Target1Description;

    //Target 2 Menu
    [SerializeField]
    private TextMeshPro Target2Title;
    [SerializeField]
    private TextMeshPro Target2Description;

    //Gripper 1 Menu
    [SerializeField]
    private TextMeshPro Gripper1Title;
    [SerializeField]
    private TextMeshPro Gripper1Description;

    //Robot 1 Menu
    [SerializeField]
    private TextMeshPro Robot1Title;
    [SerializeField]
    private TextMeshPro Robot1Description;

    //Robot 2 Menu
    [SerializeField]
    private TextMeshPro Robot2Title;
    [SerializeField]
    private TextMeshPro Robot2Description;

    //Advice Menu
    [SerializeField]
    private TextMeshPro AdviceNormalDescription;

    //Advice Robot Menu
    
    [SerializeField]
    private TextMeshPro AdviceRobotDescription;

    //Advice Jog
    
    [SerializeField]
    private TextMeshPro AdviceJogDescription;

    //Error Display
    
    [SerializeField]
    private TextMeshPro ErrorDisplayDescription;

    //Guidance Menu
    [SerializeField]
    private TextMeshPro GuidanceChangePhoto;
    [SerializeField]
    private TextMeshPro GuidanceChangeText;
    [SerializeField]
    private TextMeshPro GuidanceTitle;



    public void changeToEnglish()
    {
        inEnglish = true;
        inSwedish = false;
        inSpanish = false;

        // Hand menu 
        HandConfigMenu.text = "Configuration Menu";
        HandRobotMenu.text = "Robot Menu";
        HandTargetMenu.text = "Target Menu";
        HandStationMenu.text = "Station Menu";
        HandGuidanceMenu.text = "Guidance Menu";

        // Configuration menu 
        ConfigTitle.text = "CONFIGURATION MENU";
        ConfigLanguage.text = "Language";
        ConfigGoHome.text = "Go Home";
        ConfigOnlyVirtual.text = "Only Virtual\r\nRobot";
        ConfigHelp.text = "Help";

        // Station menu
        StationTitle.text = "STATION MENU";
        StatQRCodeRobot.text = "QR Code\r\nRobot";
        StatQRCodeWorkobject.text = "QR Code\r\nWorkobject";
        StatQRCodeSearchTurnOFF.text = "QR Code\r\nSearchTurnOFF";
        StatTurnOnVuforia.text = "Turn On\r\nVuforia";
        StatTurnOffVuforia.text = "Turn Off\r\nVuforia";
        StatHelp.text = "Help";

        // Robot menu 
        RobotTitle.text = "ROBOT MENU";
        RobotSendTargets.text = "Send Targets";
        RobotStartPath.text = "Start Path";
        RobotStartProcess.text = "Start Process";
        RobotStopSim.text = "Stop Sim";
        RobotHomePosition.text = "Home Position";
        RobotMoveAtTarget.text = "Move\r\nAt Target";
        RobotJogRobot.text = "Jog Robot";
        RobotChangeTool.text = "Change Tool";
        RobotHelp.text = "Help";
        RobotToggleRealRobot.text = "Real Robot";
        RobotResume.text = "Resume Robot";
        RobotPause.text = "Pause Robot";
        PreviewPath.text = "Preview\r\nPath";
        ClearTrajectoryPreview.text = "Clear Trajectory\r\nPreview";


        // Target menu 
        TargetTitle.text = "TARGET MENU";
        TargetAddTarget.text = "Add Target";
        TargetAddPath.text = "Add Path\r\nFirst Position";
        TargetAddPathEnd.text = "Add Path\r\nLast Position";
        TargetDeleteAllTargets.text = "Delete\r\nAll Targets";
        TargetDeleteSelectedTargets.text = "Delete\r\nSelected Targets";
        TargetDeleteAllPaths.text = "Delete\r\nSelected Paths";
        TargetTogglePreview.text = "Preview Tool";
        TargetPreviousPath.text = "Previous Path";
        TargetNextPath.text = "Next Path";
        TargetDeleteSelectedPath.text = "Delete\r\nSelected Path";
        TargetHelp.text = "Help";
        TargetAfterFinishing.text = "Pause After\r\nFinishing";



        // StartMenu
        StartDescription.text= "<size=400><b>Welcome to Mixed Reality  for Assembly processes</b></size>\r\n\r\n\r\nThis application is an environment for robot programming and guiding. It is possible to change between robots and end-effectors, add targets, configure the path and simulate the movements. Please, select one option:\r\n\r\n<size=290><b>Tutorial</size></b>\r\nDetailed tutorials to get in touch with the station.\r\n\r\n<size=290><b>Normal</size></b>\r\nAll setttings are unlocked bt don't panic, you will have access to basic tutorials through the main menu.\r\n\r\n\r\n<size=290><b>Press to select:</size></b>\r\n";
        StartTutorial.text = "Tutorial";
        startNormal.text = "Normal";
        startAdvanced.text = "Advanced";

        // LanguageMenu
        ChooseLanguage.text = "Choose Language";
        LangEnglish.text = "English";
        LangSwedish.text = "Swedish";
        LangSpanish.text = "Spanish";

        //Backplate Buttons
        IntroButton.text = "INTRO";
        QRCodesButton.text = "QR CODES";
        AssemblyButton.text = "ASSEMBLY PIECES";
        TargetButton.text = "TARGETS";
        GripperButton.text = "GRIPPER";
        RobotButton.text = "ROBOT";

        //Intro 1 Menu
        Intro1Title.text = "1. INTRO";
        Intro1Description.text = "Welcome to the Mixed Reality Toolkit (MRTK) Intro! \r\n\r\nIn this section, we will dive deeper into the basic components of MRTK, such as Input Handling, Interactable Objects, and Spatial Mapping, so that you can then be ready to move on to more advanced topics.\r\n\r\nLet's get started!";

        //Intro 2 Menu
        Intro2Title.text = "1. INTRO";
        Intro2Description.text = "So you can see how MRTK works, I'll give you an example. \r\n\r\nDo you see the 3D element on the left side and the tab on the right side? \r\n-Grab it with one hand to move them.\r\n-Grab it with two hands to scale them.";

        //Intro 3 Menu
        Intro3Title.text = "1. INTRO";
        Intro3Description.text = "With MRTK you can access many script and prefabs. Here is another example of things that can be done:\r\n\r\n-Press any button and the colour of the square will change.\r\n\r\nThat's it, you know how basic of MRTK works!";

        //Intro 4 Menu
        Intro4Title.text = "1. INTRO";
        Intro4Description.text = "The last step you need to complete: \r\n-Put your palm up and select any option. \r\n\r\nThis is the Hand Menu and it has the access to the menus in the Normal and Advance part.\r\n";
        Intro4HandMenuText.text = "Hand Menu:";

        //Intro 5 Menu
        Intro5Title.text = "1. INTRO";
        Intro5Description.text = "Now, open the Config Menu:\r\n-Look at your palm and select Configuration Menu.\r\n-Select the Pin button to leave the menu in one place.\r\n\r\nYou should now see a menu with options: Language, Only Virtual Robot, Go Home and Help. \r\n\r\n-Press for example the Language button.\r\n-Select the language you prefer.\r\n\r\nAll the menus and advices in the project now are in that language.\r\n\r\n";

        //Intro 6 Menu
        Intro6Title.text = "1. INTRO";
        Intro5Description.text = "Apart from the Hand Menu in the left hand, in the Tutorial and Normal Mode, in your right hand you have the guidance menu.\r\n\r\nThis menu shows the instructions for the assembly.\r\n\r\nTo open it:\r\n-Look at your right hand.\r\n-Press the NEXT button.\r\n\r\nNow you know the guidance menu.\r\n\r\nAnd that's all thank you for doing the Intro!.";

        //QR Code 1
        QR1Title.text = "2. QR CODES";
        QR1Description.text = "Welcome to the new section: QR Codes! \r\n\r\nAs you can see, there are two QR codes around the robot, their purpose is to create the station.\r\n\r\nIn order to create the Station follow the instructions:\r\n-Look at your palm and select Station Menu. \r\n-Then, press the \"QR Code Robot\" button. \r\n-Look at the one on the left side of the robot.\r\n-Press the \"Confirm\" button.\r\n\r\n";

        //QR Code 2
        QR2Title.text = "2. QR CODES";
        QR2Description.text = "There is another QR code, for the WorkObject:\r\n-Select \"QR Code WorkObject\" option in Station Menu.\r\n-Look at the WorkObject in front of the robot.\r\n-Press the \"Confirm\" button.\r\n-Press  \"QR Search Turn Off\" to deactivate the scanner.\r\n\r\nIf you want to change the position or orientation of the WorkObject, move the QR Code and follow the steps again.\r\n\r\nThank you for complete this part!";

        //Assembly 1
        Assembly1Title.text = "3. ASSEMBLY PIECES";
        Assembly1Description.text = "Welcome to the third section: Assembly Pieces! \r\n\r\nIn the left side, there are 7 tetris pieces,  you can touch and grab them as learned in the Intro.\r\n\r\nUsing vuforia we can track this pieces: \r\n-Open the Station Menu and select \"Turn On Object Recognition\"\r\n-Look at the piece and a copy in white colour with some targets in it and two buttons should appear.\r\n-For picking up the piece select \" Pick Up Confirm\" button.\r\n-For placing the piece select \"Place Confirm\" button.\r\n \r\n-Select \"Turn Off Object Recognition\" to stop scanning the pieces.\r\n";
        
        //Targets 1
        Target1Title.text = "4. TARGETS";
        Target1Description.text = "Welcome to the new section: Targets! \r\n\r\nDo you remember the Hand Menu? Press the Target Menu option.\r\n\r\nAt the bottom of the menu it appears in which path you are and the arrows let you move to the previous or next one. With the buttons, you are able to add, as well as delete one or all existing targets, and the same for the paths.\r\n\r\nOnce you complete a path, you can see how it looks:\r\n-Open the Hand Menu and select Robot Menu.\r\n-To show the trajectory of the path select \"Send Targets\" and then,  \"Preview Path\" and when finished, select \"Clear Trajectory Preview\" to close it.";

        //Targets 2
        Target2Title.text = "4. TARGETS";
        Target2Description.text = "Do you see four white balls in front of the real robot? \r\n-Grab one of them and the Target Configuration Menu will appear.\r\n\r\nIn the Target Config Menu:\r\n-Select X,Y,Z to change the position of the target.\r\n-In the Target Menu, make sure \"Preview\" is toggled.\r\n-Select Roll, Pitch and Yaw to change the rotation. \r\n-Move the sliders to select the type of movement, speed and accuracy the robot will do to reach that point. \r\n-Move the last slider to open or close the gripper.\r\n\r\nIf you want to move to a specific target:\r\n-Grab the target with your hand.\r\n-Open the Robot Menu and press \"Move at Target\" button.";

        //Gripper 1
        Gripper1Title.text = "5. GRIPPER";
        Gripper1Description.text = "Welcome to the gripper section! \r\n\r\nAs you can see, the end-effector of the robot is a white and blue SCHUNK gripper. This tool can be changed:\r\n\r\n-Select  the Robot Menu and then, press \"Change Tool\" button.\r\n-Press the \"Change Tool URI\" option and erase the current URI.\r\n-Write any other option available (the options appear in the menu.";

        //Robot 2
        Robot2Title.text = "5. ROBOT";
        Robot2Description.text = "-To stop the simulation there are 2 options:\r\n       -If you want to continue after stop, select \"Pause Robot\" and then, \"Resume Robot\" to continue.\r\n       -If you want to cancel the simulation just press \"Stop Sim\".\r\n\r\n-To hide/show the virtual robot select the toggle \"Real Robot\".\r\n\r\n-Press \"Home Position\" to move the robot to the safe position.\r\n\r\nAnd that's all, thank you for completing the Tutorial!.";

        //Robot 1
        Robot1Title.text = "6. ROBOT";
        Robot1Description.text = "Welcome to the final section: the robot.\r\n\r\nIn the previous lessons we have learned how to generate the virtual robot and how to change the gripper. In this one we will explain the Robot Menu. \r\n\r\n-Look at your palm and select the Robot Menu.\r\n\r\nLet's explain the different options: \r\n\r\n-To perform a path, select \"Send Target\" and then, \"Start Path\".\r\n\r\n-To perform all paths, select \"Send Targets\" and then, \"Start Process\".\r\n\r\n";

        //Advice Normal Mode
        AdviceNormalDescription.text = "In order to start with the guide, open\r\nthe Guidance Menu and press Next";

        //Advice Robot Menu
        AdviceRobotDescription.text = "To activate the Jog Mode and thus be able to change the position of the robot, first deactivate the switch \"Real Robot\" so that you only have the virtual robot. Once you have done this, press the Jog Robot button again.";

        //Advice Jog
        AdviceJogDescription.text = "In order to control the Sliders correctly, move them slowly to the desired position, so that you can see how the Robot gradually changes its position.";

        //Error Display
        ErrorDisplayDescription.text = "One of the targets is not reachable\r\n\r\nCheck position and rotation of the targets";

        //Guidance Menu
        GuidanceChangePhoto.text = "Change Photo";
        GuidanceChangeText.text = "Change Text";
        GuidanceTitle.text = "GUIDANCE MENU";



    }


    public void changeToSwedish()
    {
        inEnglish = false;
        inSwedish = true;
        inSpanish = false;

        // Hand menu 
        HandConfigMenu.text = "Konfigurationsmeny";
        HandRobotMenu.text = "Robotmeny";
        HandTargetMenu.text = "Målgruppsmeny";
        HandStationMenu.text = "Stationsmeny";
        HandGuidanceMenu.text = "Meny för vägledning";

        // Configuration menu 
        ConfigTitle.text = "KONFIGURATIONSMENY";
        ConfigLanguage.text = "Språk";
        ConfigGoHome.text = "Gå hem";
        ConfigOnlyVirtual.text = "Endast virtuell\r\nRobot";
        ConfigHelp.text = "Hjälp";

        // Station menu
        StationTitle.text = "STATIONSMENY";
        StatQRCodeRobot.text = "Robot med\r\nQR-kod";
        StatQRCodeWorkobject.text = "QR-kod\r\narbetsobjekt";
        StatQRCodeSearchTurnOFF.text = "Sökning av QR-koder\r\nStäng av";
        StatTurnOnVuforia.text = "Slå på\r\nVuforia";
        StatTurnOffVuforia.text = "Stäng av\r\nVuforia";
        StatHelp.text = "Hjälp";

        // Robot menu 
        RobotTitle.text = "ROBOTMENY";
        RobotSendTargets.text = "Skicka mål";
        RobotStartPath.text = "Startbana";
        RobotStartProcess.text = "Starta processen";
        RobotStopSim.text = "Stoppa sim";
        RobotHomePosition.text = "Hemmaposition";
        RobotMoveAtTarget.text = "Flytta\r\npå målet";
        RobotJogRobot.text = "Jog Robot";
        RobotChangeTool.text = "Ändra verktyg";
        RobotHelp.text = "Hjälp";
        RobotToggleRealRobot.text = "Riktig Robot";
        RobotResume.text = "Återuppta Robot";
        RobotPause.text = "Pausera Robot";
        PreviewPath.text = "Förhandsgranskningssökväg";
        ClearTrajectoryPreview.text = "Förhandsgranska\r\ntydlig bana";


        // Target menu 
        TargetTitle.text = "MÅLMENY";
        TargetAddTarget.text = "Lägg till mål";
        TargetAddPath.text = "Lägg till bana";
        TargetAddPath.text = "Lägg till sökvägen\r\nFörsta positionen";
        TargetAddPathEnd.text = "Lägg till bana\r\nSista position";
        TargetDeleteAllTargets.text = "Ta bort\r\nalla mål";
        TargetDeleteSelectedTargets.text = "Ta bort\r\nvalda mål";
        TargetDeleteSelectedPath.text = "Ta bort\r\nmarkerad sökväg";
        TargetDeleteAllPaths.text = "Ta bort\r\nmarkerade banor";
        TargetTogglePreview.text = "Förhandsgranska\r\nVerktyg";
        TargetPreviousPath.text = "Föregående väg";
        TargetNextPath.text = "Nästa väg";
        TargetHelp.text = "Hjälp";
        TargetAfterFinishing.text = "Paus efter\r\navslut";



        // StartMenu
        StartDescription.text = "<size=400><b>Välkommen till Mixed Reality för monteringsprocesser</b></size>\r\n\r\n\r\nDenna applikation är en miljö för programmering och styrning av robotar. Det är möjligt att växla mellan robotar och ändmekanismer, lägga till mål, konfigurera banan och simulera rörelserna. Välj ett alternativ::\r\n\r\n<size=290><b>Handledning</size></b>\r\nDetaljerade handledningar för att komma i kontakt med stationen.\r\n\r\n<size=290><b>Normal</size></b>\r\nAlla inställningar är olåsta, men ingen panik, du har tillgång till grundläggande handledning via huvudmenyn.\r\n\r\n\r\n<size=290><b>Tryck på för att välja:</size></b>\r\n";
        StartTutorial.text = "Handledning";
        startNormal.text = "Normalt";
        startAdvanced.text = "Avancerad";

        // LanguageMenu
        ChooseLanguage.text = "Välj språk";
        LangEnglish.text = "Engelska";
        LangSwedish.text = "Svenska";
        LangSpanish.text = "Spanska";

        //BackPlateButtons
        IntroButton.text = "INLEDNING";
        QRCodesButton.text = "QR-KODER";
        AssemblyButton.text = "MONTERINGSBITAR";
        TargetButton.text = "MÅLTAVLOR";
        GripperButton.text = "GRIPPER";
        RobotButton.text = "ROBOT";

        //Intro 1 Menu
        Intro1Title.text = "1. INLEDNING";
        Intro1Description.text = "Välkommen till Mixed Reality Toolkit (MRTK) Intro!\r\n\r\nI denna sektion kommer vi att gå djupare in på de grundläggande komponenterna i MRTK, såsom hantering av input, interaktiva objekt och spatial kartläggning, så att du sedan är redo att gå vidare till mer avancerade ämnen.\r\n\r\nLåt oss börja!";

        //Intro 2 Menu
        Intro2Title.text = "1. INLEDNING";
        Intro2Description.text = "För att du ska kunna se hur MRTK fungerar ska jag ge dig ett exempel. \r\n\r\nSer du 3D-elementet på vänster sida och fliken på höger sida? \r\n-Ta tag i dem med en hand för att flytta dem.\r\n-Ta tag i dem med två händer för att skala dem.";

        //Intro 3 Menu
        Intro3Title.text = "1. INLEDNING";
        Intro3Description.text = "Med MRTK har du tillgång till många skript och prefabs. Här är ett annat exempel på saker som kan göras:\r\n\r\n-Tryck på en knapp och färgen på kvadraten ändras.\r\n\r\nNu vet du hur MRTK fungerar!";
        
        //Intro 4 Menu
        Intro4Title.text = "1. INLEDNING";
        Intro4Description.text = "Det sista steget som du måste genomföra: \r\n-Håll upp handflatan och välj ett valfritt alternativ. \r\n\r\nDetta är handmenyn och den ger tillgång till menyerna i den avancerade lägesdelen.\r\n";
        Intro4HandMenuText.text = "Handmeny:";

        //Intro 5 Menu
        Intro5Title.text = "1. INLEDNING";
        Intro5Description.text = "Öppna nu konfigurationsmenyn:\r\n-Titta på din handflata och välj Konfigurationsmeny.\r\nVälj Pin-knappen för att lämna menyn på ett ställe.\r\n\r\nDu bör nu se en meny med alternativ: Språk, Endast virtuell robot, Go Home och Help. \r\n\r\n-Tryck till exempel på knappen Språk.\r\n-Välj det språk du föredrar.\r\n\r\nAlla menyer och råd i projektet är nu på det språket.\r\n\r\n";

        //Intro 6 Menu
        Intro6Title.text = "1. INLEDNING";
        Intro6Description.text = "Förutom handmenyn i vänster hand, i handledning och normalt läge, har du vägledningsmenyn i höger hand.\r\n\r\nDenna meny visar instruktionerna för monteringen.\r\n\r\nFör att öppna den:\r\n-Titta på din högra hand.\r\n-Dryck på knappen NEXT (nästa).\r\n\r\nNu känner du till vägledningsmenyn.\r\n\r\nOch det var allt Tack för att du gjorde Intro!.";

        //QR Code 1
        QR1Title.text = "2. QR-KODER";
        QR1Description.text = "Välkommen till den nya sektionen: QR-koder! \r\n\r\nSom du kan se finns det två QR-koder runt roboten, deras syfte är att skapa stationen.\r\n\r\nFör att skapa stationen följer du instruktionerna:\r\n-Titta på din handflata och välj Station Menu. \r\n-Tryck sedan på knappen \"QR Code Robot\". \r\n-Se på den som finns på robotens vänstra sida.\r\n-Tryck på knappen \"Confirm\" (bekräfta).\r\n";

        //QR Code 2
        QR2Title.text = "2. QR-KODER";
        QR2Description.text = "Det finns en annan QR-kod för WorkObject:\r\n-Välj alternativet \"QR Code WorkObject\" i stationsmenyn.\r\n-Titta på WorkObject framför roboten.\r\n-Tryck på knappen \"Confirm\".\r\n-Tryck på \"QR Search Turn Off\" för att inaktivera skannern.\r\n\r\nOm du vill ändra WorkObjects position eller orientering flyttar du QR-koden och följer stegen igen.\r\n\r\nTack för att du har slutfört den här delen!\r\n\r\n";

        //Assembly 1
        Assembly1Title.text = "3. MONTERINGSBITAR";
        Assembly1Description.text = "Välkommen till det tredje avsnittet: Monteringsdelar! \r\n\r\nPå vänster sida finns det 7 tetrisbitar, du kan röra vid dem och ta tag i dem som du lärde dig i introduktionen.\r\n\r\nMed hjälp av vuforia kan vi spåra dessa bitar: \r\n-Öppna stationsmenyn och välj \"Turn On Object Recognition\".\r\n-Titta på pjäsen och en kopia i vit färg med några mål i den och två knappar bör visas.\r\n-För att plocka upp pjäsen väljer du knappen \"Pick Up Confirm\".\r\n-För att placera pjäsen väljer du knappen \"Place Confirm\" (placera bekräfta).\r\n \r\n-Välj \"Turn Off Object Recognition\" för att sluta skanna bitarna.\r\n";

        //Targets 1
        Target1Title.text = "4. MÅLTAVLOR";
        Target1Description.text = "Välkommen till den nya sektionen: Mål! \r\n\r\nMinns du handmenyn? Tryck på alternativet Target Menu.\r\n\r\nLängst ner i menyn visas i vilken bana du befinner dig och pilarna låter dig gå till föregående eller nästa bana. Med knapparna kan du lägga till samt ta bort ett eller alla befintliga mål, och detsamma gäller för banorna.\r\n\r\nNär du har slutfört en bana kan du se hur den ser ut:\r\n-Öppna handmenyn och välj Robotmeny.\r\n-För att visa banans bana väljer du \"Send Targets\" och sedan \"Preview Path\" och när du är klar väljer du \"Clear Trajectory Preview\" för att stänga den.\r\n\r\n";

        //Targets 2
        Target2Title.text = "4. MÅLTAVLOR";
        Target2Description.text = "Ser du fyra vita bollar framför den riktiga roboten? \r\n-Ta tag i en av dem och menyn för målkonfiguration visas.\r\n\r\nI målkonfigurationsmenyn:\r\n-Välj X,Y,Z för att ändra målets position.\r\n-Se till att \"Förhandsgranskning\" är aktiverat i målmenyn.\r\n-Välj Roll, Pitch och Yaw för att ändra rotationen. \r\n-Flytta reglagen för att välja typ av rörelse, hastighet och noggrannhet som roboten ska göra för att nå den aktuella punkten. \r\n-Flytta det sista reglaget för att öppna eller stänga gripdonet.\r\n\r\nOm du vill förflytta dig till ett specifikt mål:\r\n-Grip målet med handen.\r\n-Öppna robotmenyn och tryck på knappen \"Flytta till målet\".\r\n\r\n";

        //Gripper 1
        Gripper1Title.text = "5. GRIPPER";
        Gripper1Description.text = "Välkommen till gripersektionen! \r\n\r\nSom du kan se är robotens ändfunktion en vit och blå SCHUNK-greppare. Det här verktyget kan bytas ut:\r\n\r\n-Välj robotmenyn och tryck sedan på knappen \"Change Tool\".\r\n-Tryck på alternativet \"Change Tool URI\" och radera den aktuella URI:n.\r\n-Skriv något annat alternativ som är tillgängligt (alternativen visas i menyn.";

        //Robot 2
        Robot2Title.text = "5. ROBOT";
        Robot2Description.text = "-För att stoppa simuleringen finns det två alternativ:\r\n       -Om du vill fortsätta efter stoppet väljer du \"Pause Robot\" och sedan \"Resume Robot\" för att fortsätta.\r\n       -Om du vill avbryta simuleringen trycker du bara på \"Stop Sim\".\r\n\r\n-Om du vill dölja/visa den virtuella roboten väljer du \"Real Robot\".\r\n\r\n-Tryck på \"Home Position\" för att flytta roboten till en säker position.\r\n\r\nOch det var allt, tack för att du slutförde denna handledning!.";

        //Robot 1
        Robot1Title.text = "6. ROBOT";
        Robot1Description.text = "Välkommen till det sista avsnittet: roboten.\r\n\r\nI de tidigare lektionerna har vi lärt oss hur man genererar den virtuella roboten och hur man ändrar gripdonet. I den här kommer vi att förklara robotmenyn. \r\n\r\n-Se på din handflata och välj Robotmenyn.\r\n\r\nLåt oss förklara de olika alternativen: \r\n\r\n-För att utföra en bana väljer du \"Send Target\" (skicka mål) och sedan \"Start Path\" (starta bana).\r\n\r\n-För att utföra alla banor väljer du \"Send Targets\" och sedan \"Start Process\".\r\n";

        //Advice Normal Mode
        AdviceNormalDescription.text = "För att börja med vägledningen öppnar du\r\nvägledningsmenyn och trycker på Nästa.";

        //Advice Robot Menu
        
        AdviceRobotDescription.text = "För att aktivera Jog-läget och på så sätt kunna ändra robotens position, avaktivera först kontakten \"Real Robot\" så att du bara har den virtuella roboten. När du har gjort detta trycker du på knappen Jog Robot igen.";

        //Advice Jog
        
        AdviceJogDescription.text = "För att kunna styra reglagen på rätt sätt ska du flytta dem långsamt till önskat läge, så att du kan se hur roboten gradvis ändrar sitt läge.";

        //Error Display
        
        ErrorDisplayDescription.text = "Ett av målen kan inte nås\r\n\r\nKontrollera målens position och rotation";

        //Guidance Menu
        GuidanceChangePhoto.text = "Ändra foto";
        GuidanceChangeText.text = "Ändra text";
        GuidanceTitle.text = "MENY FÖR VÄGLEDNING";
        
    }


    public void changeToSpanish()
    {
        inEnglish = false;
        inSwedish = false;
        inSpanish = true;

        // Hand menu 
        HandConfigMenu.text = "Menú de\r\nconfiguración";
        HandRobotMenu.text = "Robot Menu";
        HandTargetMenu.text = "Menú Objetivo ";
        HandStationMenu.text = "Menú Estación";
        HandGuidanceMenu.text = "Meny Guía";

        // Configuration menu 
        ConfigTitle.text = "MENÚ DE CONFIGURACIÓN";
        ConfigLanguage.text = "Idioma";
        ConfigGoHome.text = "Ir a Home";
        ConfigOnlyVirtual.text = "Sólo Robot\r\nVirtual";
        ConfigHelp.text = "Ayuda";

        // Station menu
        StationTitle.text = "MENÚ DE LA ESTACIÓN";
        StatQRCodeRobot.text = "Código QR\r\ndel Robot";
        StatQRCodeWorkobject.text = "Código QR\r\ndel Workobject";
        StatQRCodeSearchTurnOFF.text = "Desactivar búsqueda\r\ncódigo QR";
        StatTurnOnVuforia.text = "Activar\r\nVuforia";
        StatTurnOffVuforia.text = "Desactivar\r\nVuforia";
        StatHelp.text = "Ayuda";

        // Robot menu 
        RobotTitle.text = "MENÚ DEL ROBOT";
        RobotSendTargets.text = "Enviar Objetivos";
        RobotStartPath.text = "Empezar Trayectoria";
        RobotStartProcess.text = "Empezar Proceso";
        RobotStopSim.text = "Parar Sim";
        RobotHomePosition.text = "Posición Home";
        RobotMoveAtTarget.text = "Mover hacia\r\nel objetivo";
        RobotJogRobot.text = "Jog Robot";
        RobotChangeTool.text = "Cambiar Herramienta";
        RobotHelp.text = "Ayuda";
        RobotToggleRealRobot.text = "Robot Real";
        RobotResume.text = "Continuar con el\r\nRobot";
        RobotPause.text = "Pausar\r\nRobot";
        PreviewPath.text = "Previsualización\r\nTrayectoria";
        ClearTrajectoryPreview.text = "Borrar previsualización\r\nde la trayetoria";



        // Target menu 
        TargetTitle.text = "MENÚ DEL OBJETIVO";
        TargetAddTarget.text = "Añadir Objetivo";
        TargetAddPath.text = "Añadir trayectoria al principio";
        TargetDeleteAllTargets.text = "Borrar todos\r\nlos objetivos";
        TargetDeleteSelectedTargets.text = "Borrar los\r\nobjetivos seleccionados";
        TargetDeleteSelectedPath.text = "Borrar las\r\ntrayectorias seleccionados";
        TargetHelp.text = "Ayuda";
        TargetAddPath.text = "Añadir trayectoria en\r\nprimera posición";
        TargetAddPathEnd.text = "Añadir trayectoria en\r\núltima posición";
        TargetDeleteAllPaths.text = "Borrar todos\r\nlos caminos";
        TargetTogglePreview.text = "Previsualización";
        TargetPreviousPath.text = "Trayectoria Anterior";
        TargetNextPath.text = "Siguiente Trayectoria";
        TargetAfterFinishing.text = "Pausar después\r\nde terminar";



        // StartMenu
        StartDescription.text = "<size=400><b>Bienvenido a la Realidad Mixta para procesos de montaje</b></size>\r\n\r\n\r\nEsta aplicación es un entorno para programar y controlar robots. Es posible cambiar entre robots y mecanismos finales, añadir objetivos, configurar la trayectoria y simular los movimientos. Seleccione una opción:\r\n\r\n<size=290><b>Tutorial</size></b>\r\nInstrucciones detalladas para ponerse en contacto con la estación.\r\n\r\n<size=290><b>Normal</size></b>\r\nTodos los ajustes están desbloqueados, pero no te asustes, tienes acceso a tutoriales básicos a través del menú principal.\r\n\r\n\r\n<size=290><b>Pulse para seleccionar:</size></b>\r\n";
        StartTutorial.text = "Tutorial";
        startNormal.text = "Normal";
        startAdvanced.text = "Avanzado";

        // LanguageMenu
        ChooseLanguage.text = "Elige el idioma";
        LangEnglish.text = "Inglés";
        LangSwedish.text = "Sueco";
        LangSpanish.text = "Español";

        //Backplate Buttons
        IntroButton.text = "INTRODUCCIÓN";
        QRCodesButton.text = "CÓDIGOS QR";
        AssemblyButton.text = "PIEZAS ENSAMBLAJE";
        TargetButton.text = "OBJETIVOS";
        GripperButton.text = "GRIPPER";
        RobotButton.text = "ROBOT";


        //Intro 1 Menu
        Intro1Title.text = "1. INTRO";
        Intro1Description.text = "¡Bienvenido a la introducción al Mixed Reality Toolkit (MRTK)!\r\n\r\nEn esta sección, profundizaremos en los componentes básicos de MRTK, como la gestión de entrada, objetos interactivos y el mapeo espacial, para que luego puedas estar preparado para avanzar hacia temas más avanzados.\r\n\r\n¡Comencemos!";

        //Intro 2 Menu
        Intro2Title.text = "1. INTRO";
        Intro2Description.text = "Para que veas cómo funciona MRTK, te pondré un ejemplo. \r\n\r\n¿Ves el elemento 3D a la izquierda y la pestaña a la derecha? \r\n-Cógelo con una mano para moverlos.\r\n-Cógelo con las dos manos para escalarlos.";

        //Intro 3 Menu
        Intro3Title.text = "1. INTRO";
        Intro3Description.text = "Con MRTK puedes acceder a muchos script y prefabs. Aquí hay otro ejemplo de cosas que se pueden hacer:\r\n\r\n-Pulse cualquier botón y el color del cuadrado va a cambiar.\r\n\r\nEso es todo, ¡ya sabes cómo funciona MRTK!";

        //Intro 4 Menu
        Intro4Title.text = "1. INTRO";
        Intro4Description.text = "El último paso que debe completar: \r\n-Poner la palma de la mano hacia arriba y seleccionar cualquier opción. \r\n\r\nEste es el menú de la mano y tiene el acceso a los menús en la parte del modo de avance.";
        Intro4HandMenuText.text = "Menú de Mano:";

        //Intro 5 Menu
        Intro5Title.text = "1. INTRO";
        Intro5Description.text = "Ahora, abra el Menú Config:\r\n-Mira la palma de tu mano y selecciona Menú Configuración.\r\n-Selecciona el botón Pin para dejar el menú en un solo lugar.\r\n\r\nAhora debería ver un menú con opciones: Idioma, Sólo robot Virtual, Ir a casa y Ayuda. \r\n\r\n-Pulsa por ejemplo el botón Idioma.\r\n-Selecciona el idioma que prefieras.\r\n\r\nTodos los menús y consejos del proyecto estarán ahora en ese idioma.\r\n\r\n";

        //Intro 6 Menu
        Intro6Title.text = "1. INTRO";
        Intro6Description.text = "Además del menú de la mano izquierda, en los modos Tutorial y Normal, en la mano derecha tiene el menú de guía.\r\n\r\nEste menú muestra las instrucciones para el montaje.\r\n\r\nPara abrirlo:\r\n-Mira tu mano derecha.\r\n-Pulsa el botón NEXT.\r\n\r\nAhora ya conoces el menú guía.\r\n\r\nY eso es todo ¡Gracias por hacer la Intro!.";

        //QR Code 1
        QR1Title.text = "2. QR CODES";
        QR1Description.text = "Bienvenido a la nueva sección: ¡Códigos QR! \r\n\r\nComo puedes ver, hay dos códigos QR alrededor del robot, su propósito es crear la estación.\r\n\r\nPara crear la Estación sigue las instrucciones:\r\n-Mira tu palm y selecciona Menú Estación. \r\n-Luego, presiona el botón \"Código QR Robot\". \r\n-Mira el del lado izquierdo del robot.\r\n-Pulsa el botón \"Confirmar\".\r\n";

        //QR Code 2
        QR2Title.text = "2. QR CODES";
        QR2Description.text = "Hay otro código QR, para el WorkObject:\r\n-Seleccione la opción \"Código QR WorkObject\" en el Menú Estación.\r\n-Mira el objeto de trabajo delante del robot.\r\n-Pulse el botón \"Confirmar\".\r\n-Pulse \"QR Search Turn Off\" para desactivar el escáner.\r\n\r\nSi desea cambiar la posición u orientación del Objeto de Trabajo, mueva el Código QR y siga los pasos de nuevo.\r\n\r\n¡Gracias por completar esta parte!\r\n\r\n";

        //Assembly 1
        Assembly1Title.text = "3. ASSEMBLY PIECES";
        Assembly1Description.text = "Bienvenido a la tercera sección: ¡Montaje de Piezas! \r\n\r\nEn el lado izquierdo, hay 7 piezas de tetris, puedes tocarlas y agarrarlas como aprendimos en la Intro.\r\n\r\nUsando vuforia podemos rastrear estas piezas: \r\n-Abre el menú de la estación y selecciona \"Activar reconocimiento de objetos\"\r\n-Mira la pieza y debería aparecer una copia en color blanco con algunos objetivos en ella y dos botones.\r\n-Para recoger la pieza seleccione el botón \"Confirmar Recogida\".\r\n-Para colocar la pieza seleccione el botón \"Colocar Confirmar\".\r\n \r\n-Seleccione \"Desactivar reconocimiento de objetos\" para dejar de escanear las piezas.\r\n";

        //Targets 1
        Target1Title.text = "4. TARGETS";
        Target1Description.text = "Bienvenido a la nueva sección: ¡Objetivos! \r\n\r\n¿Recuerdas el Menú Mano? Pulsa la opción Menú Objetivos.\r\n\r\nEn la parte inferior del menú aparece en qué trayectoria te encuentras y las flechas te permiten moverte a la anterior o a la siguiente. Con los botones, puedes añadir, así como eliminar uno o todos los objetivos existentes, y lo mismo para las trayectorias.\r\n\r\nUna vez que haya completado un trayecto, puede ver cómo queda:\r\n-Abra el Menú Mano y seleccione Menú Robot.\r\n-Para mostrar la trayectoria de la trayectoria selecciona \"Enviar Objetivos\" y luego, \"Vista Previa de la Trayectoria\" y cuando termines, selecciona \"Borrar Vista Previa de la Trayectoria\" para cerrarla.\r\n\r\n";

        //Targets 2
        Target2Title.text = "4. TARGETS";
        Target2Description.text = "¿Ves cuatro bolas blancas delante del robot real? \r\n-Coge una de ellas y aparecerá el Menú de Configuración de Objetivo.\r\n\r\nEn el Menú de Configuración de Objetivo:\r\n-Selecciona X,Y,Z para cambiar la posición del objetivo.\r\n-En el menú Target, asegúrate de que la opción \"Preview\" está activada.\r\n-Selecciona Roll, Pitch y Yaw para cambiar la rotación. \r\n-Mueve los deslizadores para seleccionar el tipo de movimiento, velocidad y precisión que hará el robot para llegar a ese punto. \r\n-Mueve el último deslizador para abrir o cerrar la pinza.\r\n\r\nSi quieres moverte hacia un objetivo concreto:\r\n-Agarra el objetivo con la mano.\r\n-Abre el Menú Robot y pulsa el botón \"Mover al Objetivo\".\r\n\r\n";

        //Gripper 1
        Gripper1Title.text = "5. GRIPPER";
        Gripper1Description.text = "¡Bienvenido a la sección de pinzas! \r\n\r\nComo puedes ver, el efector final del robot es una pinza SCHUNK blanca y azul. Esta herramienta se puede cambiar:\r\n\r\n-Seleccionar el Menú Robot y luego, pulsar el botón \"Cambiar Herramienta\".\r\n-Pulse la opción \"Cambiar URI de Herramienta\" y borre la URI actual.\r\n-Escribir cualquier otra opción disponible (las opciones aparecen en el menú.";

        //Robot 2
        Robot2Title.text = "5. ROBOT";
        Robot2Description.text = "-Para detener la simulación hay 2 opciones:\r\n       -Si desea continuar después de parar, seleccione \"Pausar Robot\" y luego, \"Reanudar Robot\" para continuar.\r\n       -Si quieres cancelar la simulación simplemente pulsa \"Parar Sim\".\r\n\r\n-Para ocultar/mostrar el robot virtual seleccione el conmutador \"Robot Real\".\r\n\r\n-Pulsa \"Home Position\" para mover el robot a la posición segura.\r\n\r\nY eso es todo, ¡gracias por completar el Tutorial!.";

        //Robot 1
        Robot1Title.text = "6. ROBOT";
        Robot1Description.text = "Bienvenido a la última sección: el robot.\r\n\r\nEn las lecciones anteriores hemos aprendido a generar el robot virtual y a cambiar la pinza. En ésta explicaremos el Menú Robot. \r\n\r\n-Mira tu palm y selecciona el Menú Robot.\r\n\r\nVamos a explicar las diferentes opciones: \r\n\r\n-Para realizar una trayectoria, selecciona \"Enviar Objetivo\" y luego, \"Iniciar Trayectoria\".\r\n\r\n-Para realizar todas las trayectorias, selecciona \"Enviar Objetivos\" y luego, \"Iniciar Proceso\".\r\n";

        //Advice Normal Mode
        AdviceNormalDescription.text = "Para empezar con la guía, abre\r\n el Menú Guía de la mano derecha";

        //Advice Robot Menu
        AdviceRobotDescription.text = "Para activar el Modo Jog y así poder cambiar la posición del robot, desactiva primero el interruptor \"Robot Real\" de forma que sólo tengas el robot virtual. Una vez hecho esto, pulsa de nuevo el botón Jog Robot.";

        //Advice Jog
        AdviceJogDescription.text = "Para controlar correctamente los Deslizadores, muévalos lentamente hasta la posición deseada, de modo que pueda ver cómo el Robot cambia gradualmente su posición.";

        //Error Display
        
        ErrorDisplayDescription.text = "Uno de los objetivos no es alcanzable\r\n\r\nCompruebe la posición y la rotación de los objetivos";

        //Guidance Menu
        GuidanceChangePhoto.text = "Cambiar foto";
        GuidanceChangeText.text = "Cambiar texto";
        GuidanceTitle.text = "MENÚ GUÍA";

    }

}

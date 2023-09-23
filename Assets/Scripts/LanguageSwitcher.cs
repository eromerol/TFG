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
        HandTargetMenu.text = "M�lgruppsmeny";
        HandStationMenu.text = "Stationsmeny";
        HandGuidanceMenu.text = "Meny f�r v�gledning";

        // Configuration menu 
        ConfigTitle.text = "KONFIGURATIONSMENY";
        ConfigLanguage.text = "Spr�k";
        ConfigGoHome.text = "G� hem";
        ConfigOnlyVirtual.text = "Endast virtuell\r\nRobot";
        ConfigHelp.text = "Hj�lp";

        // Station menu
        StationTitle.text = "STATIONSMENY";
        StatQRCodeRobot.text = "Robot med\r\nQR-kod";
        StatQRCodeWorkobject.text = "QR-kod\r\narbetsobjekt";
        StatQRCodeSearchTurnOFF.text = "S�kning av QR-koder\r\nSt�ng av";
        StatTurnOnVuforia.text = "Sl� p�\r\nVuforia";
        StatTurnOffVuforia.text = "St�ng av\r\nVuforia";
        StatHelp.text = "Hj�lp";

        // Robot menu 
        RobotTitle.text = "ROBOTMENY";
        RobotSendTargets.text = "Skicka m�l";
        RobotStartPath.text = "Startbana";
        RobotStartProcess.text = "Starta processen";
        RobotStopSim.text = "Stoppa sim";
        RobotHomePosition.text = "Hemmaposition";
        RobotMoveAtTarget.text = "Flytta\r\np� m�let";
        RobotJogRobot.text = "Jog Robot";
        RobotChangeTool.text = "�ndra verktyg";
        RobotHelp.text = "Hj�lp";
        RobotToggleRealRobot.text = "Riktig Robot";
        RobotResume.text = "�teruppta Robot";
        RobotPause.text = "Pausera Robot";
        PreviewPath.text = "F�rhandsgranskningss�kv�g";
        ClearTrajectoryPreview.text = "F�rhandsgranska\r\ntydlig bana";


        // Target menu 
        TargetTitle.text = "M�LMENY";
        TargetAddTarget.text = "L�gg till m�l";
        TargetAddPath.text = "L�gg till bana";
        TargetAddPath.text = "L�gg till s�kv�gen\r\nF�rsta positionen";
        TargetAddPathEnd.text = "L�gg till bana\r\nSista position";
        TargetDeleteAllTargets.text = "Ta bort\r\nalla m�l";
        TargetDeleteSelectedTargets.text = "Ta bort\r\nvalda m�l";
        TargetDeleteSelectedPath.text = "Ta bort\r\nmarkerad s�kv�g";
        TargetDeleteAllPaths.text = "Ta bort\r\nmarkerade banor";
        TargetTogglePreview.text = "F�rhandsgranska\r\nVerktyg";
        TargetPreviousPath.text = "F�reg�ende v�g";
        TargetNextPath.text = "N�sta v�g";
        TargetHelp.text = "Hj�lp";
        TargetAfterFinishing.text = "Paus efter\r\navslut";



        // StartMenu
        StartDescription.text = "<size=400><b>V�lkommen till Mixed Reality f�r monteringsprocesser</b></size>\r\n\r\n\r\nDenna applikation �r en milj� f�r programmering och styrning av robotar. Det �r m�jligt att v�xla mellan robotar och �ndmekanismer, l�gga till m�l, konfigurera banan och simulera r�relserna. V�lj ett alternativ::\r\n\r\n<size=290><b>Handledning</size></b>\r\nDetaljerade handledningar f�r att komma i kontakt med stationen.\r\n\r\n<size=290><b>Normal</size></b>\r\nAlla inst�llningar �r ol�sta, men ingen panik, du har tillg�ng till grundl�ggande handledning via huvudmenyn.\r\n\r\n\r\n<size=290><b>Tryck p� f�r att v�lja:</size></b>\r\n";
        StartTutorial.text = "Handledning";
        startNormal.text = "Normalt";
        startAdvanced.text = "Avancerad";

        // LanguageMenu
        ChooseLanguage.text = "V�lj spr�k";
        LangEnglish.text = "Engelska";
        LangSwedish.text = "Svenska";
        LangSpanish.text = "Spanska";

        //BackPlateButtons
        IntroButton.text = "INLEDNING";
        QRCodesButton.text = "QR-KODER";
        AssemblyButton.text = "MONTERINGSBITAR";
        TargetButton.text = "M�LTAVLOR";
        GripperButton.text = "GRIPPER";
        RobotButton.text = "ROBOT";

        //Intro 1 Menu
        Intro1Title.text = "1. INLEDNING";
        Intro1Description.text = "V�lkommen till Mixed Reality Toolkit (MRTK) Intro!\r\n\r\nI denna sektion kommer vi att g� djupare in p� de grundl�ggande komponenterna i MRTK, s�som hantering av input, interaktiva objekt och spatial kartl�ggning, s� att du sedan �r redo att g� vidare till mer avancerade �mnen.\r\n\r\nL�t oss b�rja!";

        //Intro 2 Menu
        Intro2Title.text = "1. INLEDNING";
        Intro2Description.text = "F�r att du ska kunna se hur MRTK fungerar ska jag ge dig ett exempel. \r\n\r\nSer du 3D-elementet p� v�nster sida och fliken p� h�ger sida? \r\n-Ta tag i dem med en hand f�r att flytta dem.\r\n-Ta tag i dem med tv� h�nder f�r att skala dem.";

        //Intro 3 Menu
        Intro3Title.text = "1. INLEDNING";
        Intro3Description.text = "Med MRTK har du tillg�ng till m�nga skript och prefabs. H�r �r ett annat exempel p� saker som kan g�ras:\r\n\r\n-Tryck p� en knapp och f�rgen p� kvadraten �ndras.\r\n\r\nNu vet du hur MRTK fungerar!";
        
        //Intro 4 Menu
        Intro4Title.text = "1. INLEDNING";
        Intro4Description.text = "Det sista steget som du m�ste genomf�ra: \r\n-H�ll upp handflatan och v�lj ett valfritt alternativ. \r\n\r\nDetta �r handmenyn och den ger tillg�ng till menyerna i den avancerade l�gesdelen.\r\n";
        Intro4HandMenuText.text = "Handmeny:";

        //Intro 5 Menu
        Intro5Title.text = "1. INLEDNING";
        Intro5Description.text = "�ppna nu konfigurationsmenyn:\r\n-Titta p� din handflata och v�lj Konfigurationsmeny.\r\nV�lj Pin-knappen f�r att l�mna menyn p� ett st�lle.\r\n\r\nDu b�r nu se en meny med alternativ: Spr�k, Endast virtuell robot, Go Home och Help. \r\n\r\n-Tryck till exempel p� knappen Spr�k.\r\n-V�lj det spr�k du f�redrar.\r\n\r\nAlla menyer och r�d i projektet �r nu p� det spr�ket.\r\n\r\n";

        //Intro 6 Menu
        Intro6Title.text = "1. INLEDNING";
        Intro6Description.text = "F�rutom handmenyn i v�nster hand, i handledning och normalt l�ge, har du v�gledningsmenyn i h�ger hand.\r\n\r\nDenna meny visar instruktionerna f�r monteringen.\r\n\r\nF�r att �ppna den:\r\n-Titta p� din h�gra hand.\r\n-Dryck p� knappen NEXT (n�sta).\r\n\r\nNu k�nner du till v�gledningsmenyn.\r\n\r\nOch det var allt Tack f�r att du gjorde Intro!.";

        //QR Code 1
        QR1Title.text = "2. QR-KODER";
        QR1Description.text = "V�lkommen till den nya sektionen: QR-koder! \r\n\r\nSom du kan se finns det tv� QR-koder runt roboten, deras syfte �r att skapa stationen.\r\n\r\nF�r att skapa stationen f�ljer du instruktionerna:\r\n-Titta p� din handflata och v�lj Station Menu. \r\n-Tryck sedan p� knappen \"QR Code Robot\". \r\n-Se p� den som finns p� robotens v�nstra sida.\r\n-Tryck p� knappen \"Confirm\" (bekr�fta).\r\n";

        //QR Code 2
        QR2Title.text = "2. QR-KODER";
        QR2Description.text = "Det finns en annan QR-kod f�r WorkObject:\r\n-V�lj alternativet \"QR Code WorkObject\" i stationsmenyn.\r\n-Titta p� WorkObject framf�r roboten.\r\n-Tryck p� knappen \"Confirm\".\r\n-Tryck p� \"QR Search Turn Off\" f�r att inaktivera skannern.\r\n\r\nOm du vill �ndra WorkObjects position eller orientering flyttar du QR-koden och f�ljer stegen igen.\r\n\r\nTack f�r att du har slutf�rt den h�r delen!\r\n\r\n";

        //Assembly 1
        Assembly1Title.text = "3. MONTERINGSBITAR";
        Assembly1Description.text = "V�lkommen till det tredje avsnittet: Monteringsdelar! \r\n\r\nP� v�nster sida finns det 7 tetrisbitar, du kan r�ra vid dem och ta tag i dem som du l�rde dig i introduktionen.\r\n\r\nMed hj�lp av vuforia kan vi sp�ra dessa bitar: \r\n-�ppna stationsmenyn och v�lj \"Turn On Object Recognition\".\r\n-Titta p� pj�sen och en kopia i vit f�rg med n�gra m�l i den och tv� knappar b�r visas.\r\n-F�r att plocka upp pj�sen v�ljer du knappen \"Pick Up Confirm\".\r\n-F�r att placera pj�sen v�ljer du knappen \"Place Confirm\" (placera bekr�fta).\r\n \r\n-V�lj \"Turn Off Object Recognition\" f�r att sluta skanna bitarna.\r\n";

        //Targets 1
        Target1Title.text = "4. M�LTAVLOR";
        Target1Description.text = "V�lkommen till den nya sektionen: M�l! \r\n\r\nMinns du handmenyn? Tryck p� alternativet Target Menu.\r\n\r\nL�ngst ner i menyn visas i vilken bana du befinner dig och pilarna l�ter dig g� till f�reg�ende eller n�sta bana. Med knapparna kan du l�gga till samt ta bort ett eller alla befintliga m�l, och detsamma g�ller f�r banorna.\r\n\r\nN�r du har slutf�rt en bana kan du se hur den ser ut:\r\n-�ppna handmenyn och v�lj Robotmeny.\r\n-F�r att visa banans bana v�ljer du \"Send Targets\" och sedan \"Preview Path\" och n�r du �r klar v�ljer du \"Clear Trajectory Preview\" f�r att st�nga den.\r\n\r\n";

        //Targets 2
        Target2Title.text = "4. M�LTAVLOR";
        Target2Description.text = "Ser du fyra vita bollar framf�r den riktiga roboten? \r\n-Ta tag i en av dem och menyn f�r m�lkonfiguration visas.\r\n\r\nI m�lkonfigurationsmenyn:\r\n-V�lj X,Y,Z f�r att �ndra m�lets position.\r\n-Se till att \"F�rhandsgranskning\" �r aktiverat i m�lmenyn.\r\n-V�lj Roll, Pitch och Yaw f�r att �ndra rotationen. \r\n-Flytta reglagen f�r att v�lja typ av r�relse, hastighet och noggrannhet som roboten ska g�ra f�r att n� den aktuella punkten. \r\n-Flytta det sista reglaget f�r att �ppna eller st�nga gripdonet.\r\n\r\nOm du vill f�rflytta dig till ett specifikt m�l:\r\n-Grip m�let med handen.\r\n-�ppna robotmenyn och tryck p� knappen \"Flytta till m�let\".\r\n\r\n";

        //Gripper 1
        Gripper1Title.text = "5. GRIPPER";
        Gripper1Description.text = "V�lkommen till gripersektionen! \r\n\r\nSom du kan se �r robotens �ndfunktion en vit och bl� SCHUNK-greppare. Det h�r verktyget kan bytas ut:\r\n\r\n-V�lj robotmenyn och tryck sedan p� knappen \"Change Tool\".\r\n-Tryck p� alternativet \"Change Tool URI\" och radera den aktuella URI:n.\r\n-Skriv n�got annat alternativ som �r tillg�ngligt (alternativen visas i menyn.";

        //Robot 2
        Robot2Title.text = "5. ROBOT";
        Robot2Description.text = "-F�r att stoppa simuleringen finns det tv� alternativ:\r\n       -Om du vill forts�tta efter stoppet v�ljer du \"Pause Robot\" och sedan \"Resume Robot\" f�r att forts�tta.\r\n       -Om du vill avbryta simuleringen trycker du bara p� \"Stop Sim\".\r\n\r\n-Om du vill d�lja/visa den virtuella roboten v�ljer du \"Real Robot\".\r\n\r\n-Tryck p� \"Home Position\" f�r att flytta roboten till en s�ker position.\r\n\r\nOch det var allt, tack f�r att du slutf�rde denna handledning!.";

        //Robot 1
        Robot1Title.text = "6. ROBOT";
        Robot1Description.text = "V�lkommen till det sista avsnittet: roboten.\r\n\r\nI de tidigare lektionerna har vi l�rt oss hur man genererar den virtuella roboten och hur man �ndrar gripdonet. I den h�r kommer vi att f�rklara robotmenyn. \r\n\r\n-Se p� din handflata och v�lj Robotmenyn.\r\n\r\nL�t oss f�rklara de olika alternativen: \r\n\r\n-F�r att utf�ra en bana v�ljer du \"Send Target\" (skicka m�l) och sedan \"Start Path\" (starta bana).\r\n\r\n-F�r att utf�ra alla banor v�ljer du \"Send Targets\" och sedan \"Start Process\".\r\n";

        //Advice Normal Mode
        AdviceNormalDescription.text = "F�r att b�rja med v�gledningen �ppnar du\r\nv�gledningsmenyn och trycker p� N�sta.";

        //Advice Robot Menu
        
        AdviceRobotDescription.text = "F�r att aktivera Jog-l�get och p� s� s�tt kunna �ndra robotens position, avaktivera f�rst kontakten \"Real Robot\" s� att du bara har den virtuella roboten. N�r du har gjort detta trycker du p� knappen Jog Robot igen.";

        //Advice Jog
        
        AdviceJogDescription.text = "F�r att kunna styra reglagen p� r�tt s�tt ska du flytta dem l�ngsamt till �nskat l�ge, s� att du kan se hur roboten gradvis �ndrar sitt l�ge.";

        //Error Display
        
        ErrorDisplayDescription.text = "Ett av m�len kan inte n�s\r\n\r\nKontrollera m�lens position och rotation";

        //Guidance Menu
        GuidanceChangePhoto.text = "�ndra foto";
        GuidanceChangeText.text = "�ndra text";
        GuidanceTitle.text = "MENY F�R V�GLEDNING";
        
    }


    public void changeToSpanish()
    {
        inEnglish = false;
        inSwedish = false;
        inSpanish = true;

        // Hand menu 
        HandConfigMenu.text = "Men� de\r\nconfiguraci�n";
        HandRobotMenu.text = "Robot Menu";
        HandTargetMenu.text = "Men� Objetivo ";
        HandStationMenu.text = "Men� Estaci�n";
        HandGuidanceMenu.text = "Meny Gu�a";

        // Configuration menu 
        ConfigTitle.text = "MEN� DE CONFIGURACI�N";
        ConfigLanguage.text = "Idioma";
        ConfigGoHome.text = "Ir a Home";
        ConfigOnlyVirtual.text = "S�lo Robot\r\nVirtual";
        ConfigHelp.text = "Ayuda";

        // Station menu
        StationTitle.text = "MEN� DE LA ESTACI�N";
        StatQRCodeRobot.text = "C�digo QR\r\ndel Robot";
        StatQRCodeWorkobject.text = "C�digo QR\r\ndel Workobject";
        StatQRCodeSearchTurnOFF.text = "Desactivar b�squeda\r\nc�digo QR";
        StatTurnOnVuforia.text = "Activar\r\nVuforia";
        StatTurnOffVuforia.text = "Desactivar\r\nVuforia";
        StatHelp.text = "Ayuda";

        // Robot menu 
        RobotTitle.text = "MEN� DEL ROBOT";
        RobotSendTargets.text = "Enviar Objetivos";
        RobotStartPath.text = "Empezar Trayectoria";
        RobotStartProcess.text = "Empezar Proceso";
        RobotStopSim.text = "Parar Sim";
        RobotHomePosition.text = "Posici�n Home";
        RobotMoveAtTarget.text = "Mover hacia\r\nel objetivo";
        RobotJogRobot.text = "Jog Robot";
        RobotChangeTool.text = "Cambiar Herramienta";
        RobotHelp.text = "Ayuda";
        RobotToggleRealRobot.text = "Robot Real";
        RobotResume.text = "Continuar con el\r\nRobot";
        RobotPause.text = "Pausar\r\nRobot";
        PreviewPath.text = "Previsualizaci�n\r\nTrayectoria";
        ClearTrajectoryPreview.text = "Borrar previsualizaci�n\r\nde la trayetoria";



        // Target menu 
        TargetTitle.text = "MEN� DEL OBJETIVO";
        TargetAddTarget.text = "A�adir Objetivo";
        TargetAddPath.text = "A�adir trayectoria al principio";
        TargetDeleteAllTargets.text = "Borrar todos\r\nlos objetivos";
        TargetDeleteSelectedTargets.text = "Borrar los\r\nobjetivos seleccionados";
        TargetDeleteSelectedPath.text = "Borrar las\r\ntrayectorias seleccionados";
        TargetHelp.text = "Ayuda";
        TargetAddPath.text = "A�adir trayectoria en\r\nprimera posici�n";
        TargetAddPathEnd.text = "A�adir trayectoria en\r\n�ltima posici�n";
        TargetDeleteAllPaths.text = "Borrar todos\r\nlos caminos";
        TargetTogglePreview.text = "Previsualizaci�n";
        TargetPreviousPath.text = "Trayectoria Anterior";
        TargetNextPath.text = "Siguiente Trayectoria";
        TargetAfterFinishing.text = "Pausar despu�s\r\nde terminar";



        // StartMenu
        StartDescription.text = "<size=400><b>Bienvenido a la Realidad Mixta para procesos de montaje</b></size>\r\n\r\n\r\nEsta aplicaci�n es un entorno para programar y controlar robots. Es posible cambiar entre robots y mecanismos finales, a�adir objetivos, configurar la trayectoria y simular los movimientos. Seleccione una opci�n:\r\n\r\n<size=290><b>Tutorial</size></b>\r\nInstrucciones detalladas para ponerse en contacto con la estaci�n.\r\n\r\n<size=290><b>Normal</size></b>\r\nTodos los ajustes est�n desbloqueados, pero no te asustes, tienes acceso a tutoriales b�sicos a trav�s del men� principal.\r\n\r\n\r\n<size=290><b>Pulse para seleccionar:</size></b>\r\n";
        StartTutorial.text = "Tutorial";
        startNormal.text = "Normal";
        startAdvanced.text = "Avanzado";

        // LanguageMenu
        ChooseLanguage.text = "Elige el idioma";
        LangEnglish.text = "Ingl�s";
        LangSwedish.text = "Sueco";
        LangSpanish.text = "Espa�ol";

        //Backplate Buttons
        IntroButton.text = "INTRODUCCI�N";
        QRCodesButton.text = "C�DIGOS QR";
        AssemblyButton.text = "PIEZAS ENSAMBLAJE";
        TargetButton.text = "OBJETIVOS";
        GripperButton.text = "GRIPPER";
        RobotButton.text = "ROBOT";


        //Intro 1 Menu
        Intro1Title.text = "1. INTRO";
        Intro1Description.text = "�Bienvenido a la introducci�n al Mixed Reality Toolkit (MRTK)!\r\n\r\nEn esta secci�n, profundizaremos en los componentes b�sicos de MRTK, como la gesti�n de entrada, objetos interactivos y el mapeo espacial, para que luego puedas estar preparado para avanzar hacia temas m�s avanzados.\r\n\r\n�Comencemos!";

        //Intro 2 Menu
        Intro2Title.text = "1. INTRO";
        Intro2Description.text = "Para que veas c�mo funciona MRTK, te pondr� un ejemplo. \r\n\r\n�Ves el elemento 3D a la izquierda y la pesta�a a la derecha? \r\n-C�gelo con una mano para moverlos.\r\n-C�gelo con las dos manos para escalarlos.";

        //Intro 3 Menu
        Intro3Title.text = "1. INTRO";
        Intro3Description.text = "Con MRTK puedes acceder a muchos script y prefabs. Aqu� hay otro ejemplo de cosas que se pueden hacer:\r\n\r\n-Pulse cualquier bot�n y el color del cuadrado va a cambiar.\r\n\r\nEso es todo, �ya sabes c�mo funciona MRTK!";

        //Intro 4 Menu
        Intro4Title.text = "1. INTRO";
        Intro4Description.text = "El �ltimo paso que debe completar: \r\n-Poner la palma de la mano hacia arriba y seleccionar cualquier opci�n. \r\n\r\nEste es el men� de la mano y tiene el acceso a los men�s en la parte del modo de avance.";
        Intro4HandMenuText.text = "Men� de Mano:";

        //Intro 5 Menu
        Intro5Title.text = "1. INTRO";
        Intro5Description.text = "Ahora, abra el Men� Config:\r\n-Mira la palma de tu mano y selecciona Men� Configuraci�n.\r\n-Selecciona el bot�n Pin para dejar el men� en un solo lugar.\r\n\r\nAhora deber�a ver un men� con opciones: Idioma, S�lo robot Virtual, Ir a casa y Ayuda. \r\n\r\n-Pulsa por ejemplo el bot�n Idioma.\r\n-Selecciona el idioma que prefieras.\r\n\r\nTodos los men�s y consejos del proyecto estar�n ahora en ese idioma.\r\n\r\n";

        //Intro 6 Menu
        Intro6Title.text = "1. INTRO";
        Intro6Description.text = "Adem�s del men� de la mano izquierda, en los modos Tutorial y Normal, en la mano derecha tiene el men� de gu�a.\r\n\r\nEste men� muestra las instrucciones para el montaje.\r\n\r\nPara abrirlo:\r\n-Mira tu mano derecha.\r\n-Pulsa el bot�n NEXT.\r\n\r\nAhora ya conoces el men� gu�a.\r\n\r\nY eso es todo �Gracias por hacer la Intro!.";

        //QR Code 1
        QR1Title.text = "2. QR CODES";
        QR1Description.text = "Bienvenido a la nueva secci�n: �C�digos QR! \r\n\r\nComo puedes ver, hay dos c�digos QR alrededor del robot, su prop�sito es crear la estaci�n.\r\n\r\nPara crear la Estaci�n sigue las instrucciones:\r\n-Mira tu palm y selecciona Men� Estaci�n. \r\n-Luego, presiona el bot�n \"C�digo QR Robot\". \r\n-Mira el del lado izquierdo del robot.\r\n-Pulsa el bot�n \"Confirmar\".\r\n";

        //QR Code 2
        QR2Title.text = "2. QR CODES";
        QR2Description.text = "Hay otro c�digo QR, para el WorkObject:\r\n-Seleccione la opci�n \"C�digo QR WorkObject\" en el Men� Estaci�n.\r\n-Mira el objeto de trabajo delante del robot.\r\n-Pulse el bot�n \"Confirmar\".\r\n-Pulse \"QR Search Turn Off\" para desactivar el esc�ner.\r\n\r\nSi desea cambiar la posici�n u orientaci�n del Objeto de Trabajo, mueva el C�digo QR y siga los pasos de nuevo.\r\n\r\n�Gracias por completar esta parte!\r\n\r\n";

        //Assembly 1
        Assembly1Title.text = "3. ASSEMBLY PIECES";
        Assembly1Description.text = "Bienvenido a la tercera secci�n: �Montaje de Piezas! \r\n\r\nEn el lado izquierdo, hay 7 piezas de tetris, puedes tocarlas y agarrarlas como aprendimos en la Intro.\r\n\r\nUsando vuforia podemos rastrear estas piezas: \r\n-Abre el men� de la estaci�n y selecciona \"Activar reconocimiento de objetos\"\r\n-Mira la pieza y deber�a aparecer una copia en color blanco con algunos objetivos en ella y dos botones.\r\n-Para recoger la pieza seleccione el bot�n \"Confirmar Recogida\".\r\n-Para colocar la pieza seleccione el bot�n \"Colocar Confirmar\".\r\n \r\n-Seleccione \"Desactivar reconocimiento de objetos\" para dejar de escanear las piezas.\r\n";

        //Targets 1
        Target1Title.text = "4. TARGETS";
        Target1Description.text = "Bienvenido a la nueva secci�n: �Objetivos! \r\n\r\n�Recuerdas el Men� Mano? Pulsa la opci�n Men� Objetivos.\r\n\r\nEn la parte inferior del men� aparece en qu� trayectoria te encuentras y las flechas te permiten moverte a la anterior o a la siguiente. Con los botones, puedes a�adir, as� como eliminar uno o todos los objetivos existentes, y lo mismo para las trayectorias.\r\n\r\nUna vez que haya completado un trayecto, puede ver c�mo queda:\r\n-Abra el Men� Mano y seleccione Men� Robot.\r\n-Para mostrar la trayectoria de la trayectoria selecciona \"Enviar Objetivos\" y luego, \"Vista Previa de la Trayectoria\" y cuando termines, selecciona \"Borrar Vista Previa de la Trayectoria\" para cerrarla.\r\n\r\n";

        //Targets 2
        Target2Title.text = "4. TARGETS";
        Target2Description.text = "�Ves cuatro bolas blancas delante del robot real? \r\n-Coge una de ellas y aparecer� el Men� de Configuraci�n de Objetivo.\r\n\r\nEn el Men� de Configuraci�n de Objetivo:\r\n-Selecciona X,Y,Z para cambiar la posici�n del objetivo.\r\n-En el men� Target, aseg�rate de que la opci�n \"Preview\" est� activada.\r\n-Selecciona Roll, Pitch y Yaw para cambiar la rotaci�n. \r\n-Mueve los deslizadores para seleccionar el tipo de movimiento, velocidad y precisi�n que har� el robot para llegar a ese punto. \r\n-Mueve el �ltimo deslizador para abrir o cerrar la pinza.\r\n\r\nSi quieres moverte hacia un objetivo concreto:\r\n-Agarra el objetivo con la mano.\r\n-Abre el Men� Robot y pulsa el bot�n \"Mover al Objetivo\".\r\n\r\n";

        //Gripper 1
        Gripper1Title.text = "5. GRIPPER";
        Gripper1Description.text = "�Bienvenido a la secci�n de pinzas! \r\n\r\nComo puedes ver, el efector final del robot es una pinza SCHUNK blanca y azul. Esta herramienta se puede cambiar:\r\n\r\n-Seleccionar el Men� Robot y luego, pulsar el bot�n \"Cambiar Herramienta\".\r\n-Pulse la opci�n \"Cambiar URI de Herramienta\" y borre la URI actual.\r\n-Escribir cualquier otra opci�n disponible (las opciones aparecen en el men�.";

        //Robot 2
        Robot2Title.text = "5. ROBOT";
        Robot2Description.text = "-Para detener la simulaci�n hay 2 opciones:\r\n       -Si desea continuar despu�s de parar, seleccione \"Pausar Robot\" y luego, \"Reanudar Robot\" para continuar.\r\n       -Si quieres cancelar la simulaci�n simplemente pulsa \"Parar Sim\".\r\n\r\n-Para ocultar/mostrar el robot virtual seleccione el conmutador \"Robot Real\".\r\n\r\n-Pulsa \"Home Position\" para mover el robot a la posici�n segura.\r\n\r\nY eso es todo, �gracias por completar el Tutorial!.";

        //Robot 1
        Robot1Title.text = "6. ROBOT";
        Robot1Description.text = "Bienvenido a la �ltima secci�n: el robot.\r\n\r\nEn las lecciones anteriores hemos aprendido a generar el robot virtual y a cambiar la pinza. En �sta explicaremos el Men� Robot. \r\n\r\n-Mira tu palm y selecciona el Men� Robot.\r\n\r\nVamos a explicar las diferentes opciones: \r\n\r\n-Para realizar una trayectoria, selecciona \"Enviar Objetivo\" y luego, \"Iniciar Trayectoria\".\r\n\r\n-Para realizar todas las trayectorias, selecciona \"Enviar Objetivos\" y luego, \"Iniciar Proceso\".\r\n";

        //Advice Normal Mode
        AdviceNormalDescription.text = "Para empezar con la gu�a, abre\r\n el Men� Gu�a de la mano derecha";

        //Advice Robot Menu
        AdviceRobotDescription.text = "Para activar el Modo Jog y as� poder cambiar la posici�n del robot, desactiva primero el interruptor \"Robot Real\" de forma que s�lo tengas el robot virtual. Una vez hecho esto, pulsa de nuevo el bot�n Jog Robot.";

        //Advice Jog
        AdviceJogDescription.text = "Para controlar correctamente los Deslizadores, mu�valos lentamente hasta la posici�n deseada, de modo que pueda ver c�mo el Robot cambia gradualmente su posici�n.";

        //Error Display
        
        ErrorDisplayDescription.text = "Uno de los objetivos no es alcanzable\r\n\r\nCompruebe la posici�n y la rotaci�n de los objetivos";

        //Guidance Menu
        GuidanceChangePhoto.text = "Cambiar foto";
        GuidanceChangeText.text = "Cambiar texto";
        GuidanceTitle.text = "MEN� GU�A";

    }

}

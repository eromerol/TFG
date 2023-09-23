using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Say : MonoBehaviour
{
    // Change the language with the LanguageChanger
    [SerializeField]
    private LanguageSwitcher languageArranger;

    public int language = 0; //0 = English, 1 = Swedish, 2 = Spanish

    public void onSelectSwedish()
    {

        language = 1;

        languageArranger.changeToSwedish();
    }

    public void onSelectEnglish()
    {

        language = 0;

        languageArranger.changeToEnglish();
    }

    public void onSelectSpanish()
    {

        language = 2;

        languageArranger.changeToSpanish();
    }

}

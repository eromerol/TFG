using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLanguage : MonoBehaviour
{
	
	public static GameLanguage gl;
	public string currentLanguage = "en";

	private bool FirstTime = true;
	
	Dictionary<string, string> langID;
    Dictionary<string, string> swedishID;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (FirstTime == true)
		{
            gl = this;

            if (PlayerPrefs.HasKey("GameLanguage"))
            {
                currentLanguage = PlayerPrefs.GetString("GameLanguage");
            }
            else
            {
                ResetLanguage();
            }

            Debug.Log("Current language: " + currentLanguage);

            WordDefine();
			FirstTime = false;
        }
    }
    public void Setlanguage(string langCode)
    {
        PlayerPrefs.SetString("GameLanguage", langCode);
        currentLanguage = langCode;
        UpdateLanguage();
    }


    public void ResetLanguage()
    {
        Setlanguage("en");
    }
	
	public string Say(string text){
		
		switch(currentLanguage){
			case "es" :
				return FindInDict(langID, text);
			case "sv":
				return FindInDict(swedishID, text);
			default :
				return text;
		}
	}
	
	public string FindInDict(Dictionary<string, string> selectedLang, string text){
		if(selectedLang.ContainsKey(text))
			return selectedLang[text];
		else
			return "Untranslated";
	}
	
	public void WordDefine(){
		
		//Español (es)
		langID = new Dictionary<string, string>()
		{
			{"English", "Inglés"},
			{"Some word.", "Alguna palabra."},
			{"Choose color", "Escoge un color"},
			{"Spanish", "Español"},
            {"Swedish", "Sueco"},
            {"Reset", "Resetear"},
            {"Close", "Cerrar"},
            {"Reset Language Preference", "Resetear preferencia en el idioma"},
			{"Choose Language", "Escoge un idioma"},
		};


        //Swedish (sv)
        swedishID = new Dictionary<string, string>()
        {
            {"English", "Engelska"},
            {"Spanish", "Spanska"},
            {"Swedish", "Svenska"},
            {"Reset", "Återställ"},
            {"Close", "Stäng"},
            {"Choose Language", "Välj språk"},
        };
    }

    public void UpdateLanguage()
    {
        TextMeshPro[] texts = FindObjectsOfType<TextMeshPro>();
        foreach (TextMeshPro text in texts)
        {
            text.text = Say(text.text);
        }
    }

}

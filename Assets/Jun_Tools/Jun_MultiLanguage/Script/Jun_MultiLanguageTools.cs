﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Jun_MultiLanguageTools
{

    public static void SetLanguage (this Text text,Jun_Language language)
    {
        if(text != null && language != null)
        {
            text.text = language.languageText;

            if(language.fontSize > 0)
                text.fontSize = language.fontSize;
        }
    }
}

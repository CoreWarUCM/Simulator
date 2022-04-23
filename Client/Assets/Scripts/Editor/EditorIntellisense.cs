using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.BaseCommands;
using TMPro;
using UnityEngine;

public class EditorIntellisense : MonoBehaviour
{

    public void checkStyles(ref string text, string lastText)
    {
        List<string> lines = text.Split('\n').ToList();
        List<string> lastLines = lastText.Split('\n').ToList();
        text = "";

        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (!lastLines.Contains(line))
                checkLine(ref line);
            text += line + '\n';
        }
    }

    private void checkLine(ref string line)
    {
        
    }
}

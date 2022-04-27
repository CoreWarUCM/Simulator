using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EditorIntellisense : MonoBehaviour
{
    enum Styles
    {
        Normal,
        Comment,
        Code,
        Intruction,
        Immediate,
        Direct,
        Error
    }

    [FormerlySerializedAs("_styles")] [SerializeField]
    private string[] styles;

    public void CheckStyles(ref string text, string lastText)
    {
        List<string> lines = text.Split('\n').ToList();
        List<string> lastLines = lastText.Split('\n').ToList();
        text = "";

        for (int i = 0; i < lines.Count; i++)
        { 
            var line = lines[i];
            if (!lastLines.Contains(line))
                 line = CheckLine(line);
            text += line + '\n';
        }
    }

    private string CheckLine(string line)
    {
        line = Regex.Replace(line, "<.*?>", string.Empty);
        
        InstructionCheck(ref line);
        
        CommentCheck(ref line);

        return line;
    }

    private void CommentCheck(ref string line)
    {
        int index = line.IndexOf(';');
        if(index == -1)
            return;
    
        string preComment = String.Empty;
        if (index != 0)
            preComment = line.Substring(0, index);
        string postComment = Regex.Replace(line.Substring(index),"<.*?>",String.Empty);
        // string postComment = line.Substring(index);

        postComment = InsertStyle(postComment, styles[(int)Styles.Comment]);
        line = preComment + postComment;
    }
    
    private void InstructionCheck(ref string line)
    {
        int index = line.IndexOf("name", StringComparison.Ordinal);
        if(index == -1)
            return;
    
        string preInstruction = String.Empty;
        if (index != 0)
            preInstruction = line.Substring(0, index);
        string instruction = line.Substring(index,4);
        string postInstruction = line.Substring(index + 4);

        instruction = InsertStyle(instruction, styles[(int)Styles.Intruction]);
        line = preInstruction + instruction + postInstruction;
    }
    
    private void ImmediateCheck(ref string line)
    {
        
    }
    
    private void DirectCheck(ref string line)
    {
        
    }
    
    private void ErrorCheck(ref string line)
    {
        
    }

    private string InsertStyle(string substring, string style)
    {
        return "<style=" + style + ">" + substring + "</style>";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Unfinished class, "Intellisense" for the in game editor
/// Works with a callback that passes the text as parameter and modifies it
/// </summary>
public class EditorIntellisense
{
    /// <summary>
    /// Enum to support array access
    /// USE CAREFULLY
    /// MUST MATCH THE ARRAY INITIALIZED IN EDITOR
    /// </summary>
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

    // List of styles names, initialized in editor
    [SerializeField]
    private string[] styles;

    /// <summary>
    /// Receives the text by reference and the before it was modified
    /// Because redcode works line by line we divide the text in lines
    /// And check one by one. The lastText is used to skip unchanged lines.
    /// </summary>
    /// <param name="text">New text to modify</param>
    /// <param name="lastText">Old text to use as check</param>
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

    /// <summary>
    /// Unfinished, calls all the types check and return the final line
    /// </summary>
    /// <param name="line">original line</param>
    /// <returns> modified line</returns>
    private string CheckLine(string line)
    {
        line = Regex.Replace(line, "<.*?>", string.Empty);
        
        InstructionCheck(ref line);
        
        CommentCheck(ref line);

        return line;
    }

    /// <summary>
    /// Checks if it can find a ';' and converts the rest to comment
    /// </summary>
    /// <param name="line">original line</param>
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
    
    /// <summary>
    /// Unfinished
    /// </summary>
    /// <param name="line">reference to the line</param>
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
    
    /// <summary>
    /// Unfinished
    /// </summary>
    /// <param name="line">reference to the line</param>
    private void ImmediateCheck(ref string line)
    {
        
    }
    
    /// <summary>
    /// Unfinished
    /// </summary>
    /// <param name="line">reference to the line</param>
    private void DirectCheck(ref string line)
    {
        
    }
    
    /// <summary>
    /// Unfinished
    /// </summary>
    /// <param name="line">reference to the line</param>
    private void ErrorCheck(ref string line)
    {
        
    }

    /// <summary>
    /// Assist method to insert a tag in the substring
    /// </summary>
    /// <param name="substring">substring to insert the tag</param>
    /// <param name="style">tag to inster</param>
    /// <returns></returns>
    private string InsertStyle(string substring, string style)
    {
        return "<style=" + style + ">" + substring + "</style>";
    }
}

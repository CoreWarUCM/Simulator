
public class Virus
{
    private string _path;
    private string _name;
    private string _author;
    private string[] _rawData;
    private bool validVirus;

    public Virus(string path, string name, string author, string[] rawData, bool isValid = true)
    {
        _path = path;
        _name = name;
        _author = author;
        _rawData = rawData;
        validVirus = isValid;
    }

    public bool isValidVirus()
    {
        return validVirus;
    }

    public string GetPath()
    {
        return _path;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetAuthor()
    {
        return _author;
    }

    public string[] GetRawData()
    {
        return _rawData;
    }
}

public struct VirusPair
{
    public Virus A;
    public Virus B;

    public VirusPair(Virus a = null, Virus b = null)
    {
        A = a;
        B = b;
    }

    public bool IsValidPair()
    {
        return A != null && B != null;
    }

    public void Clear()
    {
        A = null;
        B = null;
    }
}
namespace ECommerce.Infrastructure.Operations;

public static class NameOperation
{
    public static String CharacterRegulatory(string name)
    {
        return name
            .Replace("\"", "")
            .Replace("!", "")
            .Replace("'", "")
            .Replace("^", "")
            .Replace("+", "")
            .Replace("%", "")
            .Replace("&", "")
            .Replace("/", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("=", "")
            .Replace("?", "")
            .Replace("_", "")
            .Replace("", "")
            .Replace("@", "")
            .Replace("€", "")
            .Replace("¨", "")
            .Replace("~", "")
            .Replace(",", "")
            .Replace(";", "")
            .Replace(":", "")
            .Replace(".", "-")
            .Replace("Ö", "o")
            .Replace("ö", "o")
            .Replace("ı", "i")
            .Replace("İ", "i")
            .Replace("Ü", "u")
            .Replace("ü", "u")
            .Replace("Ğ", "g")
            .Replace("ğ", "g")
            .Replace("ğ", "g")
            .Replace("ğ", "g")
            .Replace("æ", "")
            .Replace("ß", "")
            .Replace("Ş", "s")
            .Replace("ş", "s")
            .Replace("Ç", "c")
            .Replace("ç", "c")
            .Replace("<", "")
            .Replace(">", "")
            .Replace("|", "");
    }
}


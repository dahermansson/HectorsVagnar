namespace HectorsVagnar
{
    public class Installningar
    {
        public string Epost { get; set; } = string.Empty;
        public string MailMall { get; set; } = """
            Hej!

            {lastatEllerEj}

            {lok}

            {Vagnar}

            Hälsningar
            Förnamn Efternamn
            Förare Stad 
            123-456 78 90
            """;
    }
}

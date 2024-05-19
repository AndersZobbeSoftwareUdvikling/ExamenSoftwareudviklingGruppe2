using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Forbrugsenhed_Sammenligner_gruppe_2
{
    public class APICallerAdresseIDObjects
    {
        public int id { get; set; }
        public int status { get; set; }
        public int darstatus { get; set; }
        public int vejkode { get; set; }
        public string vejnavn { get; set; }
        public string adresseringsvejnavn { get; set; }
        public int husnr { get; set; }
        public int etage { get; set; }
        public int dør {  get; set; }
        public string supplerendebynavn { get; set; }
        public int postnr { get; set; }
        public string postnrnavn{ get; set; }
        public int stormodtagerpostnr { get; set; }
        public string stormodtagerpostnrnavn { get; set; }
        public int kommunekode { get; set; }
        public string adgangsadresseid { get; set; }
        public int x { get; set; }
        public int y { get; set; }  
        public string href { get; set; }
        public string  betegnelse { get; set; }
    }

    public class APICallerJordstykkeObjects
    {
        public DateTime datafordelerOpdateringstid { get; set; }
        public string adressebetegnelse { get; set; }
        public int etagebetegnelse { get; set; }
        public int forretningshændelse { get; set; }
        public int forretningsområde { get; set; }
        public int forretningsproces {  get; set; }
        public int  id_lokalid { get; set; }
        public string id_namespace { get; set; }
        public DateTime registreringFra { get; set; }
        public string registreringsaktør {  get; set; }
        public int status { get; set; }
        public DateTime virkingFra { get; set; }
        public string virkningsaktør { get; set; }
        public int jordstykke { get; set; }
    }

    public class APICallerBFENummerObjects
    {
        public DateTime datafordelerOpdateringstid { get; set; }
        public string forretningshændelse { get; set; }
        public int gru009Vandforsyning { get; set; }
        public int  gru010Afløbsforhold {  get; set; }
        public string husnummer {  get; set; }
        public int kommunekode {  get; set; }
        public string registreringsaktør { get; set; }
        public int status { get; set; }
        public class bestemFastEjendom
        {
            public int bfeNummer { get; set; }
        }
    }

    public class APICallAdresseId
    {
        private const string URL = "https://api.dataforsyningen.dk/adresser?q=Seebladsgade%201&struktur=mini";

        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync(URL).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    // Deserialize the JSON response
                    var dataObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<APICallerAdresseIDObjects>>(responseData);
                    foreach (var dataObject in dataObjects)
                    {
                        // Decide how to handle the data here (print to console, save to file, etc.)
                        Console.WriteLine($"AdresseID: {dataObject.id}, Vejnavn: {dataObject.vejnavn}, Hus nummer: {dataObject.husnr}, Postnummer: {dataObject.postnr}");
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to fetch data. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

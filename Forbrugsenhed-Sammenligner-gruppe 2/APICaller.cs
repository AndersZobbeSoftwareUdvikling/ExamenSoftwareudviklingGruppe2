using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Web;

namespace Forbrugsenhed_Sammenligner_gruppe_2
{
    public class APICallerAdresseIDObjects
    {
        public string? id { get; set; }
        public int? status { get; set; }
        public int? darstatus { get; set; }
        public int? vejkode { get; set; }
        public string? vejnavn { get; set; }
        public string? adresseringsvejnavn { get; set; }
        public int? husnr { get; set; }
        public string? etage { get; set; }
        public int? dør { get; set; }
        public string? supplerendebynavn { get; set; }
        public int? postnr { get; set; }
        public string? postnrnavn { get; set; }
        public int? stormodtagerpostnr { get; set; }
        public string? stormodtagerpostnrnavn { get; set; }
        public int? kommunekode { get; set; }
        public string? adgangsadresseid { get; set; }
        public double? x { get; set; }
        public double? y { get; set; }
        public string? href { get; set; }
        public string? betegnelse { get; set; }
    }


    public class APICallerJordstykkeObjects
    {
        public DateTime? datafordelerOpdateringstid { get; set; }
        public string? adressebetegnelse { get; set; }
        public int? etagebetegnelse { get; set; }
        public int? forretningshændelse { get; set; }
        public string? forretningsområde { get; set; }
        public int? forretningsproces { get; set; }
        public string? id_lokalid { get; set; }
        public string? id_namespace { get; set; }
        public DateTime? registreringFra { get; set; }
        public string? registreringsaktør { get; set; }
        public int? status { get; set; }
        public DateTime? virkingFra { get; set; }
        public string? virkningsaktør { get; set; }
        public Husnummer? husnummer { get; set; }
    }

    public class Husnummer
    {
        public int? jordstykke { get; set; }
    }

    public class APICallerBFENummerObjects
    {
        public DateTime? datafordelerOpdateringstid { get; set; }
        public string? forretningshændelse { get; set; }
        public int? gru009Vandforsyning { get; set; }
        public int? gru010Afløbsforhold { get; set; }
        public string? husnummer { get; set; }
        public int? kommunekode { get; set; }
        public string? registreringsaktør { get; set; }
        public int? status { get; set; }
        public int? bfeNummer { get; set; }

    }

    public class APICallerBygningsInfoObjects
    {
        public string? byg021BygningensAnvendelse { get; set; }
        public string? byg030Vandforsyning { get; set; }
        public string? byg031Afløbsforhold { get; set; }
        public string? byg032YdervæggensMateriale { get; set; }
        public string? byg033Tagdækningsmateriale { get; set; }
        public string? byg034SupplerendeYdervæggensMateriale { get; set; }
        public string? byg035SupplerendeTagdækningsMateriale { get; set; }
        public string? byg036AsbestholdigtMateriale { get; set; }
        public string? byg037KildeTilBygningensMaterialer { get; set; }
        public string? byg056Varmeinstallation { get; set; }
        public string? byg057Opvarmningsmiddel { get; set; }
        public string? byg058SupplerendeVarme { get; set; }
        public string? byg111StormrådetsOversvømmelsesSelvrisiko { get; set; }
        public string? byg150Gulvbelægning { get; set; }
        public double? byg151Frihøjde { get; set; }
        public string? grund { get; set; }
        public string? husnummer { get; set; }
        public string? id_lokalid { get; set; }
        public string? id_namespace { get; set; }
        public string? kommunekode { get; set; }
    }
    public class etageList
    {

    }
    public class ejerlejlighed
    {

    }
    public class bygningPåFremmedGrundList
    {

    }

    public class APICallerEnhedInfoObjects
    {
        public string? bygning {  get; set; }
        public string? enh026EnhedensSamledeAreal { get; set; }
        public string? enh032Toiletforhold { get; set; }
        public string? enh033Badeforhold { get; set; }
        public string? enh034Køkkenforhold { get; set; }
        public string? enh035Energiforsyning { get; set; }
        public string? enh045Udlejningsforhold { get; set; }
        public string? enh046OffentligStøtte { get; set; }
        public string? enh051Varmeinstallation { get; set; }
        public string? enh052Opvarmningsmiddel { get; set; }
        public string? enh053SupplerendeVarme { get; set; }
        public string? enh067Støjisolering { get; set; }
        public string? etage { get; set; }
        public string? opgang { get; set; }

    }




    public class APICallAdresseId : IAPICall
    {
        //private const string URL = "https://api.dataforsyningen.dk/adresser?q=Seebladsgade%201&struktur=mini";
        

        public List<Dictionary<string, string>> Apicall(List<string> input)
        {
            List<Dictionary<string, string>> _output = new List<Dictionary<string, string>>();
            string URL = BuildURL(input);

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

                    var dataObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<APICallerAdresseIDObjects>>(responseData);
                    foreach(var dataobject in dataObjects)
                    {
                        Dictionary<string, string> Temp = new Dictionary<string, string>();
                        Temp.Add("ID", dataobject.id.ToString());
                        Temp.Add("Vej", dataobject.vejnavn);
                        Temp.Add("etage", dataobject.etage);
                        Temp.Add("doer", dataobject.dør.ToString());
                        Temp.Add("post", dataobject.postnr.ToString());
                        Temp.Add("by", dataobject.postnrnavn);
                        _output.Add(Temp);
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
            return _output;
        }

        public string BuildURL(List<string> input)
        {
            string url = "https://api.dataforsyningen.dk/adresser?q=";
            foreach (string i in input) 
            {
                url = url + i + "%";
            }
            url = url.Remove(url.Length - 1);
            url = url + "&struktur=mini";
            return url;
        }
    }

    public class APICallJordstykke:IAPICall
    {
        //private const string URL = "https://services.datafordeler.dk/DAR/DAR/2.0.0/rest/adresse?id=66a973e3-a800-4e8d-869a-879621bcf3bc&username=QRUSLIHSDE&password=SOFTWAREKval!tet2024";
        public List<Dictionary<string, string>> Apicall(List<string> input)
        {
            List<Dictionary<string, string>> _output = new List<Dictionary<string, string>>();
            string URL = BuildURL(input);

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

                    var dataObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<APICallerJordstykkeObjects>>(responseData);
                    foreach (var dataObject in dataObjects)
                    {
                        Dictionary<string, string> Temp = new Dictionary<string, string>();
                        Temp.Add("Jordstykke", dataObject.husnummer.jordstykke.ToString());
                        _output.Add(Temp);
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
            return _output;
        }
        public string BuildURL(List<string> input)
        {
            string url = "https://services.datafordeler.dk/DAR/DAR/2.0.0/rest/adresse?id=";
            foreach (string i in input)
            {
                url = url + i + "%";
            }
            url = url.Remove(url.Length - 1);
            url = url + "&username=QRUSLIHSDE&password=SOFTWAREKval!tet2024";
            return url;
        }

    }

    public class APICallBFENummer : IAPICall
    {
        public List<Dictionary<string, string>> Apicall(List<string> input)
        {
            List<Dictionary<string, string>> _output = new List<Dictionary<string, string>>();
            string URL = BuildURL(input);

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

                    var dataObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<APICallerBFENummerObjects>>(responseData);
                    foreach (var dataObject in dataObjects)
                    {
                        Dictionary<string, string> Temp = new Dictionary<string, string>();
                        Temp.Add("Husnummer", dataObject.husnummer.ToString());
                        Temp.Add("BFEnummer",dataObject.bfeNummer.ToString());
                        _output.Add(Temp);
                        
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
            return _output;
        }

        public string BuildURL(List<string> input)
        {
            string url = "https://services.datafordeler.dk//BBR/BBRPublic/1/rest/grund?jordstykke=";
            foreach (string i in input)
            {
                url = url + i + "%";
            }
            url = url.Remove(url.Length - 1);
            url = url + "&username=QRUSLIHSDE&password=SOFTWAREKval!tet2024";
            return url;
        }
    }
    public class APICallBygningsInfo : IAPICall
    {
        public List<Dictionary<string, string>> Apicall(List<string> input)
        {
            List<Dictionary<string, string>> _output = new List<Dictionary<string, string>>();
            string URL = BuildURL(input);

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

                    var dataObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<APICallerBygningsInfoObjects>>(responseData);
                    foreach (var dataObject in dataObjects)
                    {
                        Dictionary<string, string> Temp = new Dictionary<string, string>();
                        Temp.Add("byg021BygningensAnvendelse", dataObject.byg021BygningensAnvendelse);
                        Temp.Add("husnummer", dataObject.husnummer.ToString());
                        Temp.Add("byg150Gulvbelægning", dataObject.byg150Gulvbelægning);
                        Temp.Add("byg033Tagdækningsmateriale", dataObject.byg033Tagdækningsmateriale);
                        Temp.Add("byg030Vandforsyning", dataObject.byg030Vandforsyning);
                        Temp.Add("byg031Afløbsforhold", dataObject.byg031Afløbsforhold);
                        Temp.Add("byg032YdervæggensMateriale", dataObject.byg032YdervæggensMateriale);
                        Temp.Add("byg034SupplerendeYdervæggensMateriale", dataObject.byg034SupplerendeYdervæggensMateriale);
                        Temp.Add("byg035SupplerendeTagdækningsMateriale", dataObject.byg035SupplerendeTagdækningsMateriale);
                        Temp.Add("byg036AsbestholdigtMateriale", dataObject.byg036AsbestholdigtMateriale);
                        Temp.Add("byg037KildeTilBygningensMaterialer", dataObject.byg037KildeTilBygningensMaterialer);
                        Temp.Add("byg056Varmeinstallation", dataObject.byg056Varmeinstallation);
                        Temp.Add("byg058SupplerendeVarme", dataObject.byg058SupplerendeVarme);
                        Temp.Add("byg057Opvarmningsmiddel", dataObject.byg057Opvarmningsmiddel);
                        Temp.Add("byg111StormrådetsOversvømmelsesSelvrisiko", dataObject.byg111StormrådetsOversvømmelsesSelvrisiko);
                        Temp.Add("byg151Frihøjde", dataObject.byg151Frihøjde.ToString());
                        Temp.Add("grund", dataObject.grund);
                        
                        _output.Add(Temp);
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
            return _output;
        }

        public string BuildURL(List<string> input)
        {
            string url = "https://services.datafordeler.dk/BBR/BBRPublic/1/rest/bygning?husnummer=";
            foreach (string i in input)
            {
                url = url + i + "%";
            }
            url = url.Remove(url.Length - 1);
            url = url + "&username=QRUSLIHSDE&password=SOFTWAREKval!tet2024";
            return url;
        }
    }
    public class APICallEnhedInfo : IAPICall
    {
        public List<Dictionary<string, string>> Apicall(List<string> input)
        {
            List<Dictionary<string, string>> _output = new List<Dictionary<string, string>>();
            string URL = BuildURL(input);

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

                    var dataObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<APICallerEnhedInfoObjects>>(responseData);
                    foreach (var dataObject in dataObjects)
                    {
                        Dictionary<string, string> Temp = new Dictionary<string, string>();
                        Temp.Add("opgang", dataObject.opgang);
                        Temp.Add("enh035Energiforsyning", dataObject.enh035Energiforsyning);
                        Temp.Add("enh046OffentligStøtte", dataObject.enh046OffentligStøtte);
                        Temp.Add("bygning", dataObject.bygning);
                        Temp.Add("enh067Støjisolering", dataObject.enh067Støjisolering);
                        Temp.Add("enh026EnhedensSamledeAreal", dataObject.enh026EnhedensSamledeAreal);
                        Temp.Add("enh032Toiletforhold", dataObject.enh032Toiletforhold);
                        Temp.Add("enh033Badeforhold", dataObject.enh033Badeforhold);
                        Temp.Add("enh034Køkkenforhold", dataObject.enh034Køkkenforhold);
                        Temp.Add("enh045Udlejningsforhold", dataObject.enh045Udlejningsforhold);
                        Temp.Add("enh051Varmeinstallation", dataObject.enh051Varmeinstallation);
                        Temp.Add("enh053SupplerendeVarme", dataObject.enh053SupplerendeVarme);
                        Temp.Add("enh052Opvarmningsmiddel", dataObject.enh052Opvarmningsmiddel);
                        Temp.Add("etage", dataObject.etage);
                        _output.Add(Temp);

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
            return _output;
        }

        public string BuildURL(List<string> input)
        {
            string url = "https://services.datafordeler.dk/BBR/BBRPublic/1/rest/enhed?AdresseIdentificerer=";
            foreach (string i in input)
            {
                url = url + i + "%";
            }
            url = url.Remove(url.Length - 1);
            url = url + "&username=QRUSLIHSDE&password=SOFTWAREKval!tet2024";
            return url;
        }
    }

    public interface IAPICall
    {
        public List<Dictionary<string, string>> Apicall(List<string>input);

        abstract string BuildURL(List<string>input);

    }
}
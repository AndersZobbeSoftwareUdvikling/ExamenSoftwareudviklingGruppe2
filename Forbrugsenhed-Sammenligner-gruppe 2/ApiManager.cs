using ExcelDataReader;
using System.Text.Encodings;
using System.Data;

namespace Forbrugsenhed_Sammenligner_gruppe_2
{
    public interface IApiManager
    {
        public string runApi(Dictionary<string, string> parameters);
    }
    public class ApiManager : IApiManager
    {
        TranslateBBRCodes _translator = new TranslateBBRCodes();
        public IAPICall[] Apis = new IAPICall[]
        {
             new APICallAdresseId(), new APICallJordstykke(), new APICallBFENummer(), new APICallBygningsInfo(), new APICallEnhedInfo()
        };
        public List<string> AcepteretByggeAndelvendelser = new List<string>
        {
            "120","121","122","130","131","132","140","150","190","510"
        }; 

        public string runApi(Dictionary<string, string> parameters)
        {
            string output = "";

            List<string> list = new List<string>();
            list.Add(parameters["Vej"]);
            list.Add(parameters["Hus"]);
            list.Add(parameters["Etage"]);
            list.Add(parameters["Doer"]);
            list.Add(parameters["By"]);
            list.Add(parameters["Post"]);

            var answerToAdressId = Apis[0].Apicall(list);
            var correctId = answerToAdressId;
            foreach (KeyValuePair<string, string> keyValuePair in parameters)
            {
                if (correctId.Count == 1)
                {
                    break;
                }
                foreach (Dictionary<string,string> response in answerToAdressId)
                {
                    if(response[keyValuePair.Key] != keyValuePair.Value)
                    {
                        correctId.Remove(response);
                        if (correctId.Count == 1)
                        {
                            break;
                        }
                    }
                }
                
            }
            
            var answerToFindJordstykke = Apis[1].Apicall(new List<string> { correctId.First()["ID"] });
            
            var answerToFindBFE = Apis[2].Apicall(new List<string> { answerToFindJordstykke.First()["Jordstykke"] });
            var actualAnswer = Apis[3].Apicall(new List<string> { answerToFindBFE.First()["Husnummer"] });

            foreach (Dictionary<string, string> i in actualAnswer)
            {
                bool IsAHouse = true;
                string houseInfo = "";
                foreach (KeyValuePair<string, string> keyValuePair in i)
                {
                    houseInfo = houseInfo + translator(keyValuePair)+ "\n";
                    if(keyValuePair.Key == "byg021BygningensAnvendelse")
                    {

                        if (!AcepteretByggeAndelvendelser.Contains(keyValuePair.Value))
                        {
                            IsAHouse = false;
                        }
                    }
                }
                if (IsAHouse)
                {
                    output = output+ houseInfo + "\n";
                }
            }
            var a= Apis[4].Apicall(new List<string> { correctId.First()["ID"] });
            foreach (Dictionary<string, string> i in a)
            {
                foreach (KeyValuePair<string, string> keyValuePair in i)
                {
                    output = output + translator(keyValuePair);
                }
            }

            return output;

        }

        

        public string translator(KeyValuePair<string, string> input)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            string name = _translator.TranslateToBBRCodes(input.Key);
            FileStream stream = File.Open("C:\\Users\\Stefan\\Documents\\GitHub\\SoftwareKvalitetExamen-Gruppe2\\Forbrugsenhed-Sammenligner-gruppe-2\\BBRKodelister20230606_1.xlsx", FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();

            excelReader.Close();
            
            foreach (DataRow row in result.Tables[0].Rows)
            {
                string[] AnsRow = new string[3];
                int i = 0;

                foreach (DataColumn col in result.Tables[0].Columns)
                {
                    AnsRow[i] = row[col.ColumnName].ToString();
                    i++;
                }
                if (AnsRow[0] == name && AnsRow[1] == input.Value)
                {
                    return name +" = "+ AnsRow[2];
                }
            }
            return name + " = " + input.Value;



        }

    }

    public interface ITranslateBBRCodes
    {

    }

    public class TranslateBBRCodes : ITranslateBBRCodes
    {
        Dictionary<string, string> ApiToBBRTranslator = new Dictionary<string, string>
        {
            {"byg021BygningensAnvendelse","BygAnvendelse" },
            {"byg030Vandforsyning","BygVandforsyning" },
            {"byg031Afløbsforhold","BygAfløbsforhold" },
            {"byg032YdervæggensMateriale","YdervaeggenesMateriale" },
            {"byg033Tagdækningsmateriale","Tagdaekningsmateriale" },
            {"byg034SupplerendeYdervæggensMateriale","SupplerendeYdervaeggensmateriale" },
            {"byg035SupplerendeTagdækningsMateriale","SupplerendeTagdaekningsMateriale" },
            {"byg036AsbestholdigtMateriale","AsbestholdigtMateriale" },
            {"byg037KildeTilBygningensMaterialer","KildeTilOplysninger" },
            {"byg056Varmeinstallation","Varmeinstallation" },
            {"byg057Opvarmningsmiddel","Opvarmingsmiddel" },
            {"byg058SupplerendeVarme","SupplerendeVarme" },
            {"byg150Gulvbelægning","Gulvbelægning" }
        };
        public string TranslateToBBRCodes(string key)
        {
            string output ="";
            if (ApiToBBRTranslator.TryGetValue(key, out output))
            {
                return output;
            }
            return key;
        }
    }





}

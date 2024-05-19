using System.Text.RegularExpressions;

namespace Forbrugsenhed_Sammenligner_gruppe_2
{

    public interface IParser
    {
        public Dictionary<string, string> Parse(Dictionary<string, string> adress);

    }
    public class AddressParser : IParser
    {
        
        public Dictionary<string, string> Parse(Dictionary<string,string> adress)
        {
            Dictionary<string, string> _result = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in adress) 
            {
                double value = 0;
                if (double.TryParse(pair.Value,out value))
                {
                    _result.Add(pair.Key, value.ToString());

                }
                else if(Regex.IsMatch(pair.Value, @"^[a-zA-ZÆØÅæøå ]+$"))
                {
                    _result.Add(pair.Key, pair.Value);
                }
                else
                {
                    _result.Add(pair.Key, "");
                }
                    
                
            }
            return _result;
        }
    }

}

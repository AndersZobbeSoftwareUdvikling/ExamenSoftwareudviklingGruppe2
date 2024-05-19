using System.Collections;
using FluentAssertions;
using Forbrugsenhed_Sammenligner_gruppe_2;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace Forbrugsenhed_Sammenligner_Test
{
    public class UnitTest1
    {
        
        [Theory]
        [ClassData(typeof(AdressData))]
        public void AdresseGetsParsedCorectly(Dictionary<string, string> input, Dictionary<string, string> output)
        {
            //Arange
            AddressParser addressParser = new AddressParser();

            //Act
            var result = addressParser.Parse(input);

            //Asert
            result.Should().BeEquivalentTo(output);
        }
        [Theory]
        [ClassData(typeof(CompareTestData))]
        public void AdressesGetsCompariredCorrectly(Dictionary<string, string> addressOne, Dictionary<string, string> addressTwo, Dictionary<string, string> correctComparison)
        {

            //Arange
            AddressCompare addressCompare = new AddressCompare();

            //Act
            addressCompare.Compare(addressOne, addressTwo);

            //Asert
            addressCompare.result.Should().BeEquivalentTo(correctComparison);
        }
        
    }
    public class AdressData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            List<object[]> result = new List<object[]>();
            using (var reader = new StreamReader(@"C:\Users\Ander\source\repos\SoftwareKvalitetExamen-Gruppe2\Forbrugsenhed-Sammenligner-Test\TestData\class-data-xl.csv"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    Dictionary<string, string> adressbreakdown = new Dictionary<string, string>();
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    adressbreakdown.Add("vejnavn", values[2]);
                    adressbreakdown.Add("husnummer", values[3]);
                    adressbreakdown.Add("etage", values[4]);
                    adressbreakdown.Add("doernummer", values[5]);
                    adressbreakdown.Add("by", values[6]);
                    adressbreakdown.Add("postnummer", values[7]);

                    Dictionary<string, string> adressInput = new Dictionary<string, string>();

                    line = reader.ReadLine();
                    values = line.Split(';');
                    adressInput.Add("vejnavn", values[2]);
                    adressInput.Add("husnummer", values[3]);
                    adressInput.Add("etage", values[4]);
                    adressInput.Add("doernummer", values[5]);
                    adressInput.Add("by", values[6]);
                    adressInput.Add("postnummer", values[7]);

                    result.Add(new object[] { adressInput, adressbreakdown });
                }
            }
            foreach (object[] adresspair in result)
            {
                yield return adresspair;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class CompareTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            List<object[]> Data = new List<object[]>();
            using (var reader = new StreamReader(@"C:\Users\Ander\source\repos\SoftwareKvalitetExamen-Gruppe2\Forbrugsenhed-Sammenligner-Test\TestData\Test-compare-data.csv"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    Dictionary<string, string> addressonebreakdown = new Dictionary<string, string>();
                    Dictionary<string, string> addresstwobreakdown = new Dictionary<string, string>();
                    Dictionary<string, string> comparison = new Dictionary<string, string>();
                    string[] line = reader.ReadLine().Split(";");

                    for (int i = 0; i+1 < line.Length; i = i + 2)
                    {
                        addressonebreakdown.TryAdd(line[i], line[i + 1]);
                    }

                    line = reader.ReadLine().Split(";");

                    for (int i = 0; i+1 < line.Length; i = i + 2)
                    {
                        addresstwobreakdown.TryAdd(line[i], line[i + 1]);
                    }

                    line = reader.ReadLine().Split(";");

                    for (int i = 0; i+1 < line.Length; i = i + 2)
                    {
                        comparison.TryAdd(line[i], line[i + 1]);
                    }

                    Data.Add(new object[] { addressonebreakdown, addresstwobreakdown, comparison });
                }
            }
            foreach (object[] comparisonpair in Data)
            {
                yield return comparisonpair;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
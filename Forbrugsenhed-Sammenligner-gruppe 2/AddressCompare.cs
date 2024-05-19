namespace Forbrugsenhed_Sammenligner_gruppe_2
{

    public interface IAddressCompare
    {
        public Dictionary<string, string> result { get; set; }
        public void Compare(Dictionary<string, string> AdressOne, Dictionary<string, string> AdressTwo);

    }
    public class AddressCompare : IAddressCompare
    {
        private Dictionary<string, string> _result = new Dictionary<string, string>();
        public Dictionary<string, string> result
        {
            get
            {
                return _result;
            }

            set { }
        }
        public void Compare(Dictionary<string, string> AdressOne, Dictionary<string, string> AdressTwo)
        {
            Console.WriteLine("im comparing");
        }
    }
}

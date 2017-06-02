using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MathToWords
{
    public static class ConfigReader
    {
        public static Dictionary<string, string> GetWords()
        {
            FileStream stream = new FileStream(Environment.CurrentDirectory + @"\Operations.xml", FileMode.Open, FileAccess.Read);
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            XmlNodeList operations = document.GetElementsByTagName("operation");
            Dictionary<string, string> output = new Dictionary<string, string>();
            //Environment.CurrentDirectory + @"\Operations.xml"
        }

        private static List<Operation> GetOperationsCommon(string path)
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            XmlNodeList operations = document.GetElementsByTagName("operation");
            List<Operation> output = new List<Operation>();
            foreach (XmlNode node in operations)
            {
                string operChar = node.SelectSingleNode("operChar").InnerText;
                if (operChar == null)
                {
                    // throw error. operChar was not found
                }

                int operPrec;
                try
                {
                    operPrec = Int32.Parse(node.SelectSingleNode("operPrec").InnerText);
                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentException("The given XML file did not contain a valid operChar element for some operation.", nameof(path));
                }
                catch (FormatException)
                {
                    throw new ArgumentException("An operPrec value found in the given XML file is not a valid integer. operChar: " + operChar, nameof(path));
                }

                string operAssoc = node.SelectSingleNode("operAssoc").InnerText;
                if (operAssoc == null)
                {
                    throw new ArgumentException("The given XML file did not contain a valid operAssoc element for this operChar: " + operChar, nameof(path));
                }

                string operText = node.SelectSingleNode("operText").InnerText;
                if (operText == null)
                {
                    throw new ArgumentException("The given XML file did not contain a valid operText element for this operChar: " + operChar, nameof(path));
                }
            }
        }
    }

    public class Operation
    {
        #region Members definition
        private string _operationChar;
        public string OperationChar
        {
            get { return _operationChar; }
            set { _operationChar = value; }
        }

        private int _operationPrecedence;
        public int OperationPrecedence
        {
            get { return _operationPrecedence; }
            set { _operationPrecedence = value; }
        }

        private string _operationAssociation;
        public string OperationAssociation
        {
            get { return _operationAssociation; }
            set { _operationAssociation = value; }
        }

        private string _operationText;
        public string OperationText
        {
            get { return _operationText; }
            set { _operationText = value; }
        }
        #endregion

        #region Constructors definition
        public Operation(string operChar, int operPrec, string operAssoc, string operText)
        {
            this.OperationChar = operChar;
            this.OperationPrecedence = operPrec;
            this.OperationAssociation = operAssoc;
            this.OperationText = operText;
        }
        #endregion
    }
}

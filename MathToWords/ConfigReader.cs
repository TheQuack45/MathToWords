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
        #region Methods definition
        public static List<string> GetCharacters()
        {
            List<Operation> operations = GetOperationsCommon(Environment.CurrentDirectory + @"\Operations.xml");
            List<string> output = new List<string>();

            foreach (Operation operation in operations)
            {
                output.Add(operation.OperationChar);
            }

            return output;
        }

        public static Dictionary<string, int> GetPrecedences()
        {
            List<Operation> operations = GetOperationsCommon(Environment.CurrentDirectory + @"\Operations.xml");
            Dictionary<string, int> output = new Dictionary<string, int>();

            foreach (Operation operation in operations)
            {
                output.Add(operation.OperationChar, operation.OperationPrecedence);
            }

            return output;
        }

        public static int GetPrecedenceFor(string token)
        {
            return GetPrecedences()[token];
        }

        public static Dictionary<string, Operation.ASSOCIATIVITY> GetAssociations()
        {
            List<Operation> operations = GetOperationsCommon(Environment.CurrentDirectory + @"\Operations.xml");
            Dictionary<string, Operation.ASSOCIATIVITY> output = new Dictionary<string, Operation.ASSOCIATIVITY>();

            foreach (Operation operation in operations)
            {
                output.Add(operation.OperationChar, operation.OperationAssociation);
            }

            return output;
        }

        public static Operation.ASSOCIATIVITY GetAssociationFor(string token)
        {
            return GetAssociations()[token];
        }

        public static Dictionary<string, string> GetWords()
        {
            List<Operation> operations = GetOperationsCommon(Environment.CurrentDirectory + @"\Operations.xml");
            Dictionary<string, string> output = new Dictionary<string, string>();

            foreach (Operation operation in operations)
            {
                output.Add(operation.OperationChar, operation.OperationText);
            }

            return output;
        }

        public static string GetWordsFor(string token)
        {
            return GetWords()[token];
        }

        public static Dictionary<string, int> GetOperandCounts()
        {
            List<Operation> operations = GetOperationsCommon(Environment.CurrentDirectory + @"\Operations.xml");
            Dictionary<string, int> output = new Dictionary<string, int>();

            foreach (Operation operation in operations)
            {
                output.Add(operation.OperationChar, operation.OperandCount);
            }

            return output;
        }

        public static int GetOperandCountFor(string token)
        {
            return GetOperandCounts()[token];
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

                int operandCount;
                try
                {
                    operandCount = Int32.Parse(node.SelectSingleNode("operands").InnerText);
                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentException("The given XML file did not contain a valid operands element for this operChar: " + operChar, nameof(path));
                }
                catch (FormatException)
                {
                    throw new ArgumentException("An operPrec value found in the given XML file is not a valid integer. operChar: " + operChar, nameof(path));
                }

                output.Add(new Operation(operChar, operPrec, operAssoc, operText, operandCount));
            }

            return output;
        }
        #endregion Methods definition
    }

    public class Operation
    {
        #region Static members definition
        public enum ASSOCIATIVITY { Left, Right };

        public static readonly Dictionary<string, ASSOCIATIVITY> OperatorAssoc = new Dictionary<string, ASSOCIATIVITY>()
        {
            { "Left", ASSOCIATIVITY.Left },
            { "Right", ASSOCIATIVITY.Right },
        };
        #endregion

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

        private ASSOCIATIVITY _operationAssociation;
        public ASSOCIATIVITY OperationAssociation
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

        private int _operandCount;
        public int OperandCount
        {
            get { return _operandCount; }
            set { _operandCount = value; }
        }
        #endregion

        #region Constructors definition
        public Operation(string operChar, int operPrec, ASSOCIATIVITY operAssoc, string operText, int operandCount)
        {
            this.OperationChar = operChar;
            this.OperationPrecedence = operPrec;
            this.OperationAssociation = operAssoc;
            this.OperationText = operText;
            this.OperandCount = operandCount;
        }

        public Operation(string operChar, int operPrec, string operAssoc, string operText, int operandCount)
        {
            this.OperationChar = operChar;
            this.OperationPrecedence = operPrec;
            this.OperationAssociation = OperatorAssoc[operAssoc];
            this.OperationText = operText;
            this.OperandCount = operandCount;
        }
        #endregion
    }
}

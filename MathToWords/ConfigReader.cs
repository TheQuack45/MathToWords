using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MathToWords
{
    /// <summary>
    /// Static class that provides methods for accessing values from the operations config file.
    /// </summary>
    public static class ConfigReader
    {
        #region Methods definition
        /// <summary>
        /// Gets a list of the characters for all supported operations.
        /// </summary>
        /// <returns>List of characters for all supported operations.</returns>
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

        /// <summary>
        /// Gets a list of the precedences for all supported operations.
        /// </summary>
        /// <returns>List of precedences for all supported operations.</returns>
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

        /// <summary>
        /// Gets the precedence for the given operation, as specified by the operation's character.
        /// </summary>
        /// <param name="token">Character of the operation to get the precedence for.</param>
        /// <returns>The precedence for the given operation.</returns>
        public static int GetPrecedenceFor(string token)
        {
            return GetPrecedences()[token];
        }

        /// <summary>
        /// Gets a list of the associations for all supported operations.
        /// </summary>
        /// <returns>List of associations for all supported operations.</returns>
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

        /// <summary>
        /// Gets the association for the given operation, as specified by the operation's character.
        /// </summary>
        /// <param name="token">Character of the operation to get the precedence for.</param>
        /// <returns>The association of the given operation.</returns>
        public static Operation.ASSOCIATIVITY GetAssociationFor(string token)
        {
            return GetAssociations()[token];
        }

        /// <summary>
        /// Gets a list of the word formats for all supported operations.
        /// </summary>
        /// <returns>List of word formats for all supported operations.</returns>
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

        /// <summary>
        /// Gets the word format for the given operation, as specified by the operation's character.
        /// </summary>
        /// <param name="token">Character of the operation to get the word format for.</param>
        /// <returns>The word format of the given operation.</returns>
        public static string GetWordsFor(string token)
        {
            return GetWords()[token];
        }

        /// <summary>
        /// Gets a list of operand counts for all supported operations.
        /// </summary>
        /// <returns>List of operand counts for all supported operations.</returns>
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

        /// <summary>
        /// Gets the operand count for the given operation, as specified by the operation's character.
        /// </summary>
        /// <param name="token">Character of the operation to get the operand count for.</param>
        /// <returns>The operand count of the given operation.</returns>
        public static int GetOperandCountFor(string token)
        {
            return GetOperandCounts()[token];
        }

        /// <summary>
        /// Parses the XML file at the given path into a list of Operation objects.
        /// </summary>
        /// <param name="path">Path of the XML file to parse from.</param>
        /// <returns>A list of instances of the Operation class as specified by the given XML file.</returns>
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathToWords
{
    /// <summary>
    /// Represents a supported mathematical operation and its attributes.
    /// </summary>
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

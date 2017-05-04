using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsMis.Models.Console.Common
{
    public class ExceptionTranslater : Exception
    {
        private string _message;
        private string _stackTrace;
        private int _number;
        public ExceptionTranslater(Exception objEx)
        {
            this._message = TranslateMessage(objEx);
            this.Source = objEx.Source;
            this._stackTrace = objEx.StackTrace;
            if (objEx.GetType().ToString().Equals("System.Data.SqlClient.SqlException"))
            {
                this._number = ((System.Data.SqlClient.SqlException)objEx).Number;
            }
            else
            {
                this._number = -1;
            }
        }
        public static string TranslateMessage(Exception objEx)
        {
            string message = string.Empty;
            if (objEx.GetType().ToString().Equals("System.Data.SqlClient.SqlException"))
            {
                int exNum = ((System.Data.SqlClient.SqlException)objEx).Number;
                switch (exNum)
                {
                    case 17:
                        message = "Database server could not be reached.";
                        break;
                    case 18456:
                        message = "Invalid user name or password.";
                        break;
                    case 4060:
                        message = "Could not open connection with the database server.";
                        break;
                    case 208:
                        message = "Could not find the requested catalog table.";
                        break;
                    case 2627:
                    case 2601:
                        message = "Can not create duplicate records.";
                        break;
                    case 547:
                        message = "The record cannot be deleted/modified/created. Operation violates the data integrity.";
                        break;
                    case 201:
                        message = "Stored Procedure requires some parameters which were either not supplied or are invalid.";
                        break;
                    case 2812:
                        message = "Invalid procedure call.";
                        break;
                    case 8144:
                        message = "Stored Procedure has extra arguments specified.";
                        break;
                    case 8143:
                        message = "Duplicate parameters supplied while calling a procedure.";
                        break;
                    case 8145:
                        message = "Invalid parameter name supplied.";
                        break;
                    case 8114:
                        message = "Wrong data type supplied.";
                        break;
                    case 170:
                        message = "The query has an invalid syntax.";
                        break;
                    default:
                        message = objEx.Message;
                        break;
                }
            }
            else
            {
                message = objEx.Message;
            }
            if (objEx.Data != null && objEx.Data.Contains("Source"))
            {
                message = string.Format("{0}.. Source: {1}", message, objEx.Data["Source"]);
            }
            return message;
        }
        public override string Message
        {
            get
            {
                return _message;
            }
        }
        public override string StackTrace
        {
            get
            {
                return _stackTrace;
            }
        }
        public int Number
        {
            get
            {
                return _number;
            }
        }
    }
}
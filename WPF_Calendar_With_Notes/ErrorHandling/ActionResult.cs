using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calendar_With_Notes
{
    public class ActionResult
    {
        private static readonly Lazy<ActionResult> _success = new Lazy<ActionResult>(() => new ActionResult());

        private ActionResult(bool isSuccess = true, string errorMsg = "", ErrorType errorType = ErrorType.None)
        {
            this._isSuccess = isSuccess;
            this._errorMsg = errorMsg;
            this._errorType = errorType;
        }

        private bool _isSuccess;
        /// <summary>
        /// Determines whether result is success
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return _isSuccess;
            }
        }

        private string _errorMsg;
        /// <summary>
        /// String contains the Error massage
        /// </summary>
        public string ErrorMsg { get { return _errorMsg; } }

        private ErrorType _errorType;
        /// <summary>
        /// Type of Error
        /// </summary>
        public ErrorType ErrorType { get { return _errorType; } }

        public static ActionResult CreateSuccessResult()
        {
            return _success.Value;
        }

        public static ActionResult CreateFailResult(string errorMsg, ErrorType errorType)
        {
            return new ActionResult(false, errorMsg, errorType);
        }
    }

    public enum ErrorType { None, Unknown, DataAlreadyPresent, DataSavingFailedWhileAdding, DataSavingFailedWhileRemoving };

}

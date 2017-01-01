using System.Collections.Generic;

namespace Ca.Scta.Dal.Cqrs.Base
{
    public class GenericResult<T>
    {
        public ErrorReason? ErrorReason { get; private set; }
        private List<string> _errors;
        public T Data { get; private set; }
        public bool Succeeded { get; private set; }

        protected GenericResult(T data, bool succeeded, ErrorReason? errorReason)
        {
            Data = data;
            Succeeded = succeeded;
            ErrorReason = errorReason;

        }

        public static GenericResult<T> Success(T data)
        {
            return new GenericResult<T>(data, true,null);
        }

        public static GenericResult<T> Failure(ErrorReason errorReason)
        {
            return new GenericResult<T>(default(T),false,errorReason);
        }
        public void AddError(string error)
        {
            if(_errors==null)
                _errors=new List<string>();
            _errors.Add(error);
        }

        public List<string> GetErrorList()
        {
            return _errors;
        }

        public string GetErrorString()
        {
            var errorString = string.Join("; ", _errors);
            return errorString;
        }
        
    }
    
    public class GenericIntResult : GenericResult<int> {
        public GenericIntResult(int data, bool succeeded, ErrorReason? errorReason) : base(data, succeeded, errorReason)
        {
        }
    }
}
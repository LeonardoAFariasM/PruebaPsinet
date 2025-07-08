using System;

namespace Domain.Exceptions
{
    public class TaskConflictException : Exception
    {
        public TaskConflictException() { }

        public TaskConflictException(string message) : base(message) { }

        public TaskConflictException(string message, Exception inner) : base(message, inner) { }
    }
}

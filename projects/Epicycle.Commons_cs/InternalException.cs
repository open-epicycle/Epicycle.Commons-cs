using System;

// Author: untrots

namespace Epicycle.Commons
{
	/// <summary>
	/// An exception that symbolizes that something internal unexpectedly didn't work and there is nothing to do about that.
	/// </summary>
	public class InternalException : Exception
	{
		/// <summary>
		/// C-tor.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public InternalException(string message, Exception innerException = null) : base(message, innerException) { }
	}
}


﻿namespace Domain.Common.Exception
{
	public class BusinessException : System.Exception
	{
		public BusinessException(string message) : base(message)
		{
		}
	}
}

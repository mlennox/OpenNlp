﻿using System;

namespace OpenNLP.Tools.Trees.TRegex.Tsurgeon
{
    /// <summary>
    /// Something has gone wrong internally in Tsurgeon
    /// 
    /// @author John Bauer
    /// </summary>
    public class TsurgeonRuntimeException : Exception
    {
        /// <summary>
        /// Creates a new exception with a message.
        /// </summary>
        /// <param name="message">the message for the exception</param>
        public TsurgeonRuntimeException(string message) : base(message)
        {
        }
    }
}
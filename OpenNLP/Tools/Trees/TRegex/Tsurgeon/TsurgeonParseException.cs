﻿using System;

namespace OpenNLP.Tools.Trees.TRegex.Tsurgeon
{
    /// <summary>
    ///  A runtime exception that indicates something went wrong parsing a
    /// Tsurgeon expression.  The purpose is to make those exceptions
    /// unchecked exceptions, as there are only a few circumstances in
    /// which one could recover.
    /// 
    /// @author John Bauer
    /// </summary>
    public class TsurgeonParseException : Exception
    {
        public TsurgeonParseException(string message) : base(message)
        {
        }

        public TsurgeonParseException(string message, Exception cause) : base(message, cause)
        {
        }
    }
}
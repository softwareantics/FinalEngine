﻿namespace FinalEngine.Logging
{
    public interface ILogFormatter
    {
        string GetFormattedLog(LogType type, string message);
    }
}
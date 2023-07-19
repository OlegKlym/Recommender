using System;
namespace Recommender.Core.Services
{
    public interface ILoggingService
    {
        void Error(string message);

        void Error(Exception e, string message);

        void Error(string format, params object[] args);

        void Error(Exception e, string format, params object[] args);

        void Info(string message);

        void Info(Exception e, string message);

        void Info(string format, params object[] args);

        void Info(Exception e, string format, params object[] args);
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using log4net;
using InventoryManagement.Web.Extensions;

namespace InventoryManagement.Web.Helpers
{

    public class Logger
    {

        public static void Initialize()
        {
            if (Loggers == null)
            {
                log4net.Config.XmlConfigurator.Configure();
                System.Web.Hosting.HostingEnvironment.Cache["log"] = new List<log4net.ILog>() { LogManager.GetLogger("GeneralLog") };
            }
        }

        protected static List<log4net.ILog> Loggers
        {
            get
            {

                return (List<log4net.ILog>)System.Web.Hosting.HostingEnvironment.Cache["log"];
            }

        }


        #region methods

        private static bool CheckPathForEnabled(string path)
        {
            path = path.ToCleanString().ToLower();
            return true;
        }
        public static void Log(Log4NetLevel lvl
            , Guid? processId
            , Exception ex
            , string body
            , [CallerMemberName] string memberName = ""
            , [CallerFilePath] string sourceFilePath = ""
            , [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!CheckPathForEnabled(sourceFilePath)) { return; }
            var msg = string.Format("{0} {1} {2} {3} {4}", processId.GetValueOrDefault(Guid.Empty), sourceFilePath, memberName, sourceLineNumber, body.ToCleanString());
            if (ex != null)
            {
                msg = string.Format("{0}\r\nEx Message:{1}\r\nStack Trace{2}", msg, ex.Message, ex.StackTrace);
                if (ex.InnerException != null)
                {
                    msg = string.Format("{0}\r\n Inner Exception Message:{1}\r\nInner Exception Stack Trace{2}", msg, ex.InnerException.Message, ex.InnerException.StackTrace);
                    if (ex.InnerException.InnerException != null)
                    {
                        msg = string.Format("{0}\r\n Inner Exception Message:{1}\r\nInner Exception Stack Trace{2}", msg, ex.InnerException.InnerException.Message, ex.InnerException.InnerException.StackTrace);
                    }
                }
            }

            if (Loggers == null || Loggers.Count == 0)
            {
                Console.WriteLine("{0}: {1}", lvl.ToCleanString(), msg);
                return;
            }
            else
            {
                for (int i = 0; i < Loggers.Count; i++)
                {
                    Loggers[i].Log(lvl, msg);
                }
            }

        }      

        #endregion


        #region overloads

        #region errors

        public static void LogError(Exception ex, string msg, [CallerMemberName] string memberName = "(Unknown)"
                              , [CallerFilePath] string sourceFilePath = "(Unknown)"
                               , [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(Log4NetLevel.Error
                , ex
                , msg
                , memberName
                , sourceFilePath
                , sourceLineNumber);
        }

        public static void LogError(Guid? processId, Exception ex, string msg, [CallerMemberName] string memberName = "(Unknown)"
                      , [CallerFilePath] string sourceFilePath = "(Unknown)"
                       , [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(Log4NetLevel.Error
                , processId
                , ex
                , msg
                , memberName
                , sourceFilePath
                , sourceLineNumber);

        }

        #endregion

        #region debug

        public static void LogDebug(string msg, [CallerMemberName] string memberName = "(Unknown)"
                     , [CallerFilePath] string sourceFilePath = "(Unknown)"
                      , [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(Log4NetLevel.Debug, msg, memberName, sourceFilePath, sourceLineNumber);
        }

        public static void LogDebug(Guid? processId, string msg, [CallerMemberName] string memberName = "(Unknown)"
                     , [CallerFilePath] string sourceFilePath = "(Unknown)"
                      , [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(Log4NetLevel.Debug
                , processId
                , msg
                , memberName
                , sourceFilePath
                , sourceLineNumber);
        }
        
        #endregion

        #region start/end

        public static void LogStart([CallerMemberName] string memberName = "(Unknown)"
             , [CallerFilePath] string sourceFilePath = "(Unknown)"
              , [CallerLineNumber] int sourceLineNumber = 0)
        {
            LogDebug("starting", memberName, sourceFilePath, sourceLineNumber);
        }


        public static void LogStart(Guid? processId, [CallerMemberName] string memberName = "(Unknown)"
              , [CallerFilePath] string sourceFilePath = "(Unknown)"
               , [CallerLineNumber] int sourceLineNumber = 0)
        {
            LogDebug(processId, "starting", memberName, sourceFilePath, sourceLineNumber);
        }
     

        public static void LogEnd([CallerMemberName] string memberName = "(Unknown)"
                     , [CallerFilePath] string sourceFilePath = "(Unknown)"
                      , [CallerLineNumber] int sourceLineNumber = 0)
        {
            LogDebug("completed", memberName, sourceFilePath, sourceLineNumber);
        }

        public static void LogEnd(Guid? processId, [CallerMemberName] string memberName = "(Unknown)"
              , [CallerFilePath] string sourceFilePath = "(Unknown)"
               , [CallerLineNumber] int sourceLineNumber = 0)
        {
            LogDebug(processId, "completed", memberName, sourceFilePath, sourceLineNumber);
        }      

        #endregion

        #region general

        public static void Log(string body
            , [CallerMemberName] string memberName = ""
            , [CallerFilePath] string sourceFilePath = ""
            , [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(Log4NetLevel.Info
                , Guid.Empty
                , null
                , body
                , memberName
                , sourceFilePath
                , sourceLineNumber);
        }

        public static void Log(Guid? processId
            , string body
            , [CallerMemberName] string memberName = ""
            , [CallerFilePath] string sourceFilePath = ""
            , [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(Log4NetLevel.Info
                , processId
                , null
                , body
                , memberName
                , sourceFilePath
                , sourceLineNumber);
        }


        public static void Log(Log4NetLevel lvl
            , string body
            , [CallerMemberName] string memberName = "(Unknown)"
            , [CallerFilePath] string sourceFilePath = "(Unknown)"
            , [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(lvl
                , Guid.Empty
                , null
                , body
                , memberName
                , sourceFilePath
                , sourceLineNumber);
        }

        public static void Log(Log4NetLevel lvl
            , Exception ex
            , string body
            , [CallerMemberName] string memberName = ""
            , [CallerFilePath] string sourceFilePath = ""
            , [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            Log(lvl
                , Guid.Empty
                , ex
                , body
                , memberName
                , sourceFilePath
                , sourceLineNumber);

        }


        public static void Log(Log4NetLevel lvl
            , Guid? processId
            , string body
            , [CallerMemberName] string memberName = "(Unknown)"
            , [CallerFilePath] string sourceFilePath = "(Unknown)"
            , [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log(lvl
                , processId
                , null
                , body
                , memberName
                , sourceFilePath
                , sourceLineNumber);
        }



      

        #endregion


        #endregion
    }
}

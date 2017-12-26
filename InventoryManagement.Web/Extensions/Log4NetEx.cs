using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using InventoryManagement.Web.Extensions;

namespace InventoryManagement.Web.Extensions
{
    public enum Log4NetLevel{
         Debug,        
         Error,        
         Fatal,        
         Info,        
         Warn
    }

    public static class Log4NetEx
    {
        public static void Log(this ILog log, Log4NetLevel lvl, string body, params object[] objs){
            string logout =  (objs == null || objs.Length == 0)?body.ToCleanString():string.Format(body,objs);
            switch (lvl)
            {
                case Log4NetLevel.Debug:
                    log.Debug(logout);
                    break;
                case Log4NetLevel.Error:
                    log.Error(logout);
                    break;
                case Log4NetLevel.Warn:
                    log.Warn(logout);
                    break;
                case Log4NetLevel.Info:
                    log.Info(logout);
                    break;
                case Log4NetLevel.Fatal:
                    log.Fatal(logout);
                    break;
            }
           
        }
        public static void Info(this ILog log, string body, params object[] objs)
        {
            if (objs == null || objs.Length == 0)
            {
                log.Info(body);
                return;
            }
            log.Info(string.Format(body, objs));
        }

        public static void Warn(this ILog log, string body, params object[] objs)
        {
            if (objs == null || objs.Length == 0)
            {
                log.Warn(body);
                return;
            }
            log.Warn(string.Format(body, objs));
        }

        public static void Debug(this ILog log, string body, params object[] objs)
        {
            if (objs == null || objs.Length == 0)
            {
                log.Debug(body);
                return;
            }
            log.Debug(string.Format(body, objs));
        }

        public static void Error(this ILog log, string body, params object[] objs)
        {
            if (objs == null || objs.Length == 0)
            {
                log.Error(body);
                return;
            }
            log.Error(string.Format(body, objs));
        }

        public static void Fatal(this ILog log, string body, params object[] objs)
        {
            if (objs == null || objs.Length == 0)
            {
                log.Fatal(body);
                return;
            }
            log.Fatal(string.Format(body, objs));
        }
    }
}
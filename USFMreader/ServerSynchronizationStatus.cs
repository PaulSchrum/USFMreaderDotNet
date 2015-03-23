using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USFMreader
{
   public enum ServerSynchronizationStatus
   {
      NotDownloaded,
      NeedsUpdating,
      UpToDate
   }

   public static class ServerSynchronizationStatusExtensions
   {
      public static String ToStringNaturalLanguage(this ServerSynchronizationStatus st)
      {
         switch(st)
         {
            case ServerSynchronizationStatus.NotDownloaded:
               {
                  return "Not Downloaded";
               }
            case ServerSynchronizationStatus.NeedsUpdating:
               {
                  return "Needs Updating";
               }
            case ServerSynchronizationStatus.UpToDate:
               {
                  return "Up To Date";
               }
            default:
               {
                  throw new Exception("Malformed ServerSynhronizationStatus enumeration.");
               }
         }
      }
   }
}

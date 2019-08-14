using System;
namespace NetCoreAuthJwtMySql.Utils
{
    public static class DateUtil
    {
        public static int ConvertToTimeStamp(DateTime dateTime)
        {
            var timeStamp = (int)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return timeStamp;
        }
    }
}

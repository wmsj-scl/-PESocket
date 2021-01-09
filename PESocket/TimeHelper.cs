using System;

namespace PESocket
{
    public static class TimeHelper
    {
        /// <summary>
        /// 获取时间戳Timestamp  
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
            return timeStamp;
        }

        /// <summary>
        /// 时间戳Timestamp转换成日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = ((long)timeStamp * 10000000);
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return targetDt;
        }
    }
}

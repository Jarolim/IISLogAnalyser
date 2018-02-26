using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public class IISLogProperties
    {
        [IISLogDateAttribute("date")]
        public DateTimeOffset Date;
        [IISLogTimeAttribute("time")]
        public DateTimeOffset Time;
        [IISLogStringAttribute("s-ip")]
        public string ServerIpAddress;
        [IISLogStringAttribute("cs-method")]
        public string Method;
        [IISLogStringAttribute("cs-uri-stem")]
        public string UriStem;
        [IISLogStringAttribute("cs-uri-query")]
        public string UriQuery;
        [IISLogIntAttribute("s-port")]
        public int Port;
        [IISLogStringAttribute("cs-username")]
        public string Username;
        [IISLogStringAttribute("c-ip")]
        public string ClientIpAddress;
        [IISLogStringAttribute("cs(User-Agent)")]
        public string Agent;
        [IISLogStringAttribute("cs(Referer)")]
        public string Referer;
        [IISLogIntAttribute("sc-status")]
        public int Status;
        [IISLogIntAttribute("sc-substatus")]
        public int SubStatus;
        [IISLogIntAttribute("sc-win32-status")]
        public int Win32Status;
        [IISLogIntAttribute("time-taken")]
        public int TimeTaken;
        [IISLogIntAttribute("sc-bytes")]
        public int BytesSent;
        [IISLogIntAttribute("cs-bytes")]
        public int BytesReceived;
        [IISLogStringAttribute("host")]
        public string Host;
    }
}

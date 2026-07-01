using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UBSS.Helpers;
using UBSS.Logic;

namespace UBSS.PacketProcessing
{
    //Packet 20104
    class LoginOkMessage : Message
    {
        private long m_vAccountId;
        private string m_vPassToken;
        private string m_vFacebookId;
        private string m_vGamecenterId;
        private int m_vServerMajorVersion;
        private int m_vServerBuild;
        private int m_vContentVersion;
        private string m_vServerEnvironment;
        private int m_vSessionCount;
        private int m_vDaysSinceStartedPlaying;
        private string m_vServerTime;
        private int m_vPlayTimeSeconds;
        private string m_vAccountCreatedDate;
        private int m_vStartupCooldownSeconds;
        private string m_vCountryCode;
        
        public LoginOkMessage(Client client) : base (client)
        {
            SetMessageType(20104);
            SetMessageVersion(1);

            Unknown11 = "someid2";//"108457211027966753069";
        }

        public override void Encode()
        {
            List<Byte> pack = new List<Byte>();

            /*pack.AddInt64(m_vAccountId);
            pack.AddInt64(m_vAccountId);
            pack.AddString(m_vPassToken);
            pack.AddString(m_vFacebookId);
            pack.AddString(m_vGamecenterId);
            pack.AddInt32(m_vServerMajorVersion);
            pack.AddInt32(m_vServerBuild);
            pack.AddInt32(m_vContentVersion);
            pack.AddString(m_vServerEnvironment);
            pack.AddInt32(m_vDaysSinceStartedPlaying);
            pack.AddInt32(m_vPlayTimeSeconds);
            pack.AddInt32(m_vSessionCount);
            pack.AddString("someid1");//"297484437009394";
            pack.AddString(m_vServerTime);
            pack.AddString(m_vAccountCreatedDate);
            pack.AddInt32(m_vStartupCooldownSeconds);
            pack.AddString("someid2");//"108457211027966753069";
            pack.AddString(m_vCountryCode);
            pack.AddString("");
            pack.AddInt32(1);
            pack.AddString("");
            pack.AddString("");
            pack.AddString("");*/

            pack.AddHexa("4E-88-00-00-D5-00-01-00-00-00-01-00-00-09-00-00-00-00-01-00-00-09-00-00-00-00-28-65-72-7A-6B-77-37-38-70-38-33-74-37-33-73-62-6D-36-63-34-6D-73-32-77-77-38-38-39-34-66-79-6E-6D-7A-63-33-78-79-6E-74-66-00-00-00-0F-34-36-37-36-30-36-38-32-36-39-31-33-36-38-38-00-00-00-0B-47-3A-33-32-35-33-37-38-36-37-31-00-00-00-02-00-00-00-43-00-00-00-01-00-00-00-04-70-72-6F-64-00-00-00-B0-00-00-71-85-00-00-00-17-00-00-00-0F-31-30-33-31-32-31-33-31-30-32-34-31-32-32-32-00-00-00-0D-31-34-39-39-35-32-37-30-38-34-38-33-31-00-00-00-0D-31-34-39-37-35-31-33-39-33-38-30-30-30-00-00-00-00-FF-FF-FF-FF-00-00-00-02-46-52-FF-FF-FF-FF-00-00-00-01-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF-FF");

            SetData(pack.ToArray());
        }

        public void SetAccountCreatedDate(string date)
        {
            m_vAccountCreatedDate = date;
        }

        public void SetAccountId(long id)
        {
            m_vAccountId = id;
        }

        public void SetContentVersion(int version)
        {
            m_vContentVersion = version;
        }

        public void SetCountryCode(string code)
        {
            m_vCountryCode = code;
        }

        public void SetDaysSinceStartedPlaying(int days)
        {
            m_vDaysSinceStartedPlaying = days;
        }

        public void SetFacebookId(string id)
        {
            m_vFacebookId = id;
        }

        public void SetGamecenterId(string id)
        {
            m_vGamecenterId = id;
        }

        public void SetPassToken(string token)
        {
            m_vPassToken = token;
        }

        public void SetPlayTimeSeconds(int seconds)
        {
            m_vPlayTimeSeconds = seconds;
        }

        public void SetServerBuild(int build)
        {
            m_vServerBuild = build;
        }

        public void SetServerEnvironment(string env)
        {
            m_vServerEnvironment = env;
        }

        public void SetServerMajorVersion(int version)
        {
            m_vServerMajorVersion = version;
        }

        public void SetServerTime(string time)
        {
            m_vServerTime = time;
        }

        public void SetSessionCount(int count)
        {
            m_vSessionCount = count;
        }

        public void SetStartupCooldownSeconds(int seconds)
        {
            m_vStartupCooldownSeconds = seconds;
        }

        public String Unknown9 { get; set; } //32 39 37 34 38 34 34 33 37 30 30 39 33 39 34
        public String Unknown11 { get; set; }
    }
}

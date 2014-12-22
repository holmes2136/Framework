using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CountryFilterService.DataAccess;

namespace CountryFilterService.VO
{
    public class TS_Work_List
    {
        private int clientSn;


        private string nowIP; 
        private string lastTimeIP;
        private string com_cphone;
        private string thisTimeCountry;
        private string lastTimeCountry;
        private int IsFinish;
        private string cname;
        public int  pageNumberCount;
       

        
        
        /// <summary>
        /// 搜尋
        /// </summary>
        public DataTable Search(int startRecord,int maxRecord,string cname,string com_cphone,int IsFinish,string publishday) {

            return null;
            
        }


        public int GetTotleRowsCount(int startRecord, int maxRecord, string cname, string com_cphone, int IsFinish, string publishday)
        {

            return this.pageNumberCount;
        }


        #region

        public string LastTimeIP
        {
            get { return lastTimeIP; }
            set { lastTimeIP = value; }
        }
        

        public string ThisTimeCountry
        {
            get { return thisTimeCountry; }
            set { thisTimeCountry = value; }
        }
        

        public string LastTimeCountry
        {
            get { return lastTimeCountry; }
            set { lastTimeCountry = value; }
        }
        

        public int IsFinish1
        {
            get { return IsFinish; }
            set { IsFinish = value; }
        }


        public string Cname
        {
            get { return cname; }
            set { cname = value; }
        }
        

        public string Com_cphone
        {
            get { return com_cphone; }
            set { com_cphone = value; }
        }


        public int ClientSn
        {
            get { return clientSn; }
            set { clientSn = value; }
        }

        public string NowIP
        {
            get { return nowIP; }
            set { nowIP = value; }
        }

        #endregion

    }
}
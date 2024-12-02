using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace ResultComp
{
    public partial class SelectServer : Form
    {
        static SelectEvent selEvent ;
        public SelectServer()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MDBInterface.serverName =tbxServerName.Text;
            selEvent = new SelectEvent();
            //this.Close();
            selEvent.Show();


        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (MDBInterface.ServerAccessOK(tbxServerName.Text))
            {
                lblMessage.Text = "接続成功!!";
                lblMessage.BackColor = SystemColors.Control;
            } else
            {
                lblMessage.Text = "     接続できません。\n \nServer名を確認してください。\n\n\n  ";
                lblMessage.BackColor = Color.IndianRed;
            }


        }
    }
    public static class MDBInterface
    {
        public const string magicWord = "\\SQLEXPRESS;Encrypt=True;TrustServerCertificate=True;";
        public const string magicHead = "Persist Security Info=False;User ID=Sw;Password=;Initial Catalog=Sw;Server=";

        public static string serverName = "";
        private static int poolLength = 50; 
        public static int GetPoolLength {  get { return poolLength; } }
        private static int maxLane4Heat, maxLane4SemiFinal, maxLane4Final, maxLane4TimeFinal;
        public static void GetNoOfLanes(ref int heat, ref int semiFinal, ref int final, ref int timeFinal)
        {
            heat = maxLane4Heat;
            semiFinal = maxLane4SemiFinal;
            final = maxLane4Final;
            timeFinal = maxLane4TimeFinal;
        }
        private static string eventName="";
        private static int maxPrgNo; // 表示用競技番号の最大値
        private static int maxUID;   // 競技番号の最大値
        private static int[] raceNobyUID;
        private static int[] UIDFromRaceNo;
        private static int[] genderbyUID;
        public static int[] classNumberbyUID;

        const int NUMSTYLE = 8;
        const int NUMDISTANCE = 8;


        readonly static string[] ShumokuTable = new string[NUMSTYLE]
            { "","自由形","背泳ぎ","平泳ぎ","バタフライ", "個人メドレー", "リレー", "メドレーリレー" };
        readonly static string[] distanceTable = new string[NUMDISTANCE] { "","  25m","  50m",
        " 100m"," 200m"," 400m", " 800m", "1500m" };
        readonly static string[] GenderStr = new string[5] { "","男子", "女子", "混成", "混合"  };


        public static string GetEventName() { return eventName; }
        
        private static void RedimProgramTableArray()
        {
            int ubound = maxUID + 1;
            classNumberbyUID = new int[ubound];

        }
        public static void ReadProgramTable(int eventNo)
        {

            InitPrgNoArray(eventNo);
            InitUIDArray(eventNo);
            string connectionString = MDBInterface.GetConnectionString();
            string sqlQuery = "select 競技番号, 表示用競技番号, 種目コード, 距離コード, 性別コード from プログラム ";
            int raceNo;
            int uid;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            raceNo = Convert.ToInt32(reader["表示用競技番号"]);
                            uid = Convert.ToInt32(reader["競技番号"]);

                        }
                    }
                }
            }
        } 

        public static void InitPrgNoArray(int eventNo)
        {
            string connectionString = MDBInterface.GetConnectionString();
            string sqlQuery = "select  競技番号 from プログラム where 競技番号=(select MAX(競技番号) from プログラム where 大会番号= " + eventNo +
                ") and 大会番号=" + eventNo;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maxPrgNo = Convert.ToInt32(reader["競技番号"]);
                            Array.Resize<int> (ref UIDFromRaceNo,maxPrgNo+1)
                        }
                    }
                }
            }
        }
        public static void InitUIDArray(int eventNo)
        {
            string connectionString = MDBInterface.GetConnectionString();
            string sqlQuery = "select  表示用競技番号 from プログラム where 表示用競技番号=(select MAX(表示用競技番号) from プログラム where 大会番号= " + eventNo +
                ") and 大会番号=" + eventNo;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                maxUID = Convert.ToInt32(reader["表示用競技番号"]);
                            }
                        }
                    }
                }
                
            } catch (Exception ex) {
                MessageBox.Show("error in NoOfLanes\n"+ ex.Message);
            }


        }
        public static void InitEventInfo(int eventNo )
        {
            string connectionString = MDBInterface.GetConnectionString();
            string sqlQuery = "select  * from 大会設定 where 大会番号=" + eventNo;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                maxLane4Heat = Convert.ToInt32(reader["使用水路予選"]);
                                maxLane4TimeFinal = Convert.ToInt32(reader["使用水路タイム決勝"]);
                                maxLane4Final = Convert.ToInt32(reader["使用水路決勝"]);
                                maxLane4SemiFinal = Convert.ToInt32(reader["使用水路準決勝"]);
                                eventName = "" + reader["大会名1"].ToString();
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("error in NoOfLanes\n"+ ex.Message);
            }
        }
       public static string GetEventName(int eventNo)
        {
            string connectionString = MDBInterface.GetConnectionString();
            string sqlQuery = "select 大会名1 from 大会設定 where 大会番号=" + eventNo;
            string eventName = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventName = "" + reader["大会名1"].ToString();
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("error in GetEventName\n"+ ex.Message);
            }
            return eventName;

        }

        public static string GetConnectionString()
        {
            return magicHead + serverName + magicWord;
        }
        public static bool ServerAccessOK(string sName)
        {
            serverName = sName;
            string connectionString = GetConnectionString();
            string sqlQuery = "select * from クラス";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}

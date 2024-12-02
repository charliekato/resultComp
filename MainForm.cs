// copied from ResultSYS
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Data.SqlClient;

namespace ResultComp
{


    public partial class MainForm : Form
    {
        const int STDWIDTH = 1200; //was 1200
        const int STDHEIGHT = 800; //was 800

        const string fontName = "MS UI Gothic";
        private bool stopPoling = false;
        private bool firstRun = true;

        string connectionString = "";
//        public int timerInterval = 1;
//        private bool relayFlag = false;

        private int[] lapCounter = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] prgNofromLane = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] uidFromLane = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //private int[] kumifromLane = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //private string[] namefromLane = new string[10] { "", "", "", "", "", "", "", "", "", "" };


        private int[,] lapTime = new int[10,31] ;
        private int[] arrivalOrder = new int[10] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        private int[] goalTimeArray = new int[10] { 999999, 999999, 999999, 999999, 999999, 999999, 999999, 999999, 999999, 999999, };

        private string[] reasonString = new string[7] { "", "棄権", "失格", "途中棄権", "OPEN", "OP(失格)", "OP(棄権)" };



        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerL0;
        private System.Windows.Forms.Timer timerL1;
        private System.Windows.Forms.Timer timerL2;
        private System.Windows.Forms.Timer timerL3;
        private System.Windows.Forms.Timer timerL4;
        private System.Windows.Forms.Timer timerL5;
        private System.Windows.Forms.Timer timerL6;
        private System.Windows.Forms.Timer timerL7;
        private System.Windows.Forms.Timer timerL8;
        private System.Windows.Forms.Timer timerL9;
        private EventHandler ev1;
        private EventHandler evl1, evl2, evl3, evl4, evl5, evl6, evl7, evl8, evl9, evl0;
        public static bool monitorEnable=false;
        public static bool debugMode = false;
        private int maxLaneNo;
        private int maxLane4Heat=10, maxLane4SemiFinal=10, maxLane4Final=10, maxLane4TimeFinal= 10;
        private int eventNumber;
        private Label mylabel;
        private int FirstPrgNo;
        private int LastPrgNo;
        // for label (mainly lane label) position...
        private Thread readThread=null;
        
        private timeData tmd;

        public static int[] occupied = new int[10];


        public bool enableAutoRun = true;

        private void InitLapCounter()
        {

            int ix;
            for ( ix=1; ix<10; ix++)
            {
                lapCounter[ix] = 0;
            }
        }
        public int get_lap_interval()
        {
            return 2 * evtDB.get_pool_length();
        }
        /*-- this is currently not used....
        public int how_many_laps(int uid)
        {
            int distance = Convert.ToInt32(program_db.get_distance_from_uid(uid).Substring(0, 4));
            int laps = distance / get_lap_interval();
            return laps;
        }
        --*/
        public void set_program_number(int prgNo)
        {
            this.tbxPrgNo.Text = align(prgNo);
        }
        public int get_program_number()
        {
            return Convert.ToInt32(this.tbxPrgNo.Text);
        }
        public void set_kumi_number(int kumi)
        {
            this.tbxKumi.Text = align2(kumi);
        }
        public int get_kumi_number()
        {
            return Convert.ToInt32(this.tbxKumi.Text);
        }


        private int GetMax(int a,int b, int c, int d)
        {
            int maxNo = 0;
            if (maxNo<a) maxNo= a;
            if (maxNo<b) maxNo= b;
            if (maxNo<c) maxNo= c;
            if (maxNo<d) maxNo= d;
            return maxNo;
        }
        public MainForm(int eventNo)
        {
            InitializeComponent();

            this.eventNumber = eventNo;
            MDBInterface.InitEventInfo(eventNo);
            MDBInterface.GetNoOfLanes(ref maxLane4Heat,ref maxLane4SemiFinal,ref maxLane4Final,ref maxLane4TimeFinal);

            maxLaneNo = GetMax(maxLane4Heat,maxLane4SemiFinal,maxLane4Final,maxLane4TimeFinal);
            MDBInterface.ReadProgramTable(eventNo);

            InitMainForm();

        }
       
        private void btnQuit_Click(object sender, EventArgs e)
        {
            stopPoling=true;
            this.Close();

        }

        private void LayoutHeader()
        {
            Font myFont = new Font(fontName, 13*Width/STDWIDTH);
            int fontSize = 25*this.Width/STDWIDTH; // was  30
            Font nameFont = new Font(fontName, fontSize); // was 23

            Controls["lblPrgNo"].Left = 10*Width/STDWIDTH;
            Controls["lblPrgNo"].Top = 3*Width/STDWIDTH;
            //            Controls["lblPrgNo"].Width = 25;
            Controls["lblPrgNo"].Height = 20*Width/STDWIDTH;
            Controls["lblPrgNo"].Font = myFont;

            Controls["tbxPrgNo"].Width = 60*Width/STDWIDTH;
            Controls["tbxPrgNo"].Height = 22*Width/STDWIDTH;
            Controls["tbxPrgNo"].Left = 5*Width/STDWIDTH;
            Controls["tbxPrgNo"].Top = 25*Width/STDWIDTH;
            Controls["tbxPrgNo"].Font = myFont;
            Controls["lblHyphen"].Left = 75*Width/STDWIDTH;
            Controls["lblHyphen"].Top = 25*Width/STDWIDTH;
            Controls["lblHyphen"].Height = 25*Width/STDWIDTH;
            Controls["lblHyphen"].Font = myFont;
            Controls["lblKumi"].Height = 20*Width/STDWIDTH;
            Controls["lblKumi"].Left = 95*Width/STDWIDTH;
            Controls["lblKumi"].Top = 3*Width/STDWIDTH;
            Controls["lblKumi"].Font = myFont;
            Controls["tbxKumi"].Width = 40*Width/STDWIDTH;
            Controls["tbxKumi"].Height = 22*Width/STDWIDTH;
            Controls["tbxKumi"].Left = 95*Width/STDWIDTH;
            Controls["tbxKumi"].Top = 25*Width/STDWIDTH;
            Controls["tbxKumi"].Font = myFont;

            this.cbxMonitorEnable.Location = new System.Drawing.Point(1364*Width/STDWIDTH, 17);
            this.lblPending.Location = new System.Drawing.Point(1000*Width/STDWIDTH, 2);
          

        }
        private void layout_button()
        {
            int top = 2*Width/STDWIDTH;
            int left = 150*Width/STDWIDTH;
            int width = 35*Width/STDWIDTH;
            int height = 20*Width/STDWIDTH;
            int space = 4*Width/STDWIDTH;
            Controls["btnShow"].Left = left;
            Controls["btnShow"].Top = top;
            Controls["btnShow"].Height = height;
            Controls["btnShow"].Width = width;
            Controls["btnShowPrev"].Left = left + width + space;
            Controls["btnShowPrev"].Top = top;
            Controls["btnShowPrev"].Height = height;
            Controls["btnShowPrev"].Width = width;
            Controls["btnShowNext"].Left = 2 * (width + space) + left;
            Controls["btnShowNext"].Top = top;
            Controls["btnShowNext"].Height = height;
            Controls["btnShowNext"].Width = width;
            Controls["lblRaceName0"].Top = 30*Width/STDWIDTH;
            Controls["lblRaceName0"].Left = /*2 * (width + space) +*/ left;
        }
        private void LayoutLabel()
        {

            int topMargin = this.Height / 12;  // 88

            int buttomMargin = this.Height / 15; //53
            int leftMargin = this.Width / 80;     //15
            int rightMargin = this.Width / 100;     //12 
            int laneNoWidth = this.Width / 30;   //60
            int nameWidth = (this.Width - leftMargin - rightMargin - laneNoWidth) / 4; //220
//            int relayNameWidth = nameWidth - 5;
//            int shozokuWidth = nameWidth - 5;
            int laneHeight = (this.Height - topMargin - buttomMargin) / maxLaneNo;
            int raceNameHeight = 14*Height/STDHEIGHT; // laneHeight * 3 / 10;
            //      laneHeight = 100;
            int aoWidth = 25 * this.Width / STDWIDTH;
            //int fontSizeKana = 12*Width/STDWIDTH;
            int timeWidth = 140 * this.Width / STDWIDTH;
            int fontSize4Rly = 16*Width/STDWIDTH;
            int fontSizeShozoku = 23*Width/STDWIDTH;
            int fontsize4relayTeam = 24*Width/STDWIDTH;
            int fontsize4RaceName = 17 * Width / STDWIDTH;
            int fontSize = 30*this.Width/STDWIDTH; // was  30
            Font nameFont = new Font(fontName, fontSize); // was 23
            int timeFontSize = 32 * this.Width / STDWIDTH;
            Font timeFont = new Font(fontName, timeFontSize); // was 23
            int stimeFontSize = 32 * this.Width / STDWIDTH;
            Font stimeFont = new Font(fontName, stimeFontSize); // was 23
            Font relayTeamFont = new Font(fontName, fontsize4relayTeam);
            Font shozokuFont = new Font(fontName, fontSizeShozoku);
            Font raceNameFont = new Font(fontName, fontsize4RaceName);
            Font smallNameFont = new Font(fontName, fontSize4Rly);
            int halfLaneHeight = fontSize + 4;  // was laneHeight/2
            int relaySwimmerNameHeight=18*Width/STDWIDTH;
            int shozokuNameHeight = 20 * Width / STDWIDTH;
            int gameRecordPosX = GAMERECORDPOSX*Width/STDWIDTH;
            int newRecordPosX = NEWRECORDNOTICEPOSX*Width/STDWIDTH;

            int laneNo;
            int ypos = topMargin;

            for (laneNo = 1; laneNo <= maxLaneNo; laneNo++)
            {
                
                int xpos = leftMargin;
                int yposr = topMargin + (laneHeight * (laneNo - 1))+2;
                int yposk = yposr + raceNameHeight;
                if (maxLaneNo==10)
                {

                    create_lblName("lblLane", laneNo, xpos, ypos, nameFont, ""+(laneNo-1)  );
                } else
                {
                    create_lblName("lblLane", laneNo, xpos, ypos, nameFont, ""+laneNo  );

                }
                xpos = xpos + laneNoWidth;
                create_lblName("lblName", laneNo, xpos, ypos, nameFont, "");
                create_lblName("lblRealyTeamName", laneNo, xpos, ypos, relayTeamFont);
                xpos += nameWidth;
                int xpos4relay = xpos - 8; ////------
                xpos += nameWidth;
                xpos += fontSize * 2;
                for (int swimmerOder = 1; swimmerOder < 5; swimmerOder++)
                {
                    int myypos1 = ypos + shozokuNameHeight+9; //-----
                    int myypos2 = myypos1 + shozokuNameHeight;
                    create_lblName("lblRelaySwimmer" + swimmerOder, laneNo, xpos4relay, ypos + 2, smallNameFont, "");
                    create_lblName("lblLapTime_"  + swimmerOder + "_", laneNo, xpos4relay, myypos1 ,smallNameFont);
                    create_lblName("lblLapTimekk_"  + swimmerOder + "_", laneNo, xpos4relay, myypos2 ,smallNameFont);
                }

                create_lblName("lblArrivalOrder", laneNo, xpos4relay, ypos-2, timeFont, "");
                xpos4relay += aoWidth;
                create_lblName("lblTime", laneNo, xpos4relay, ypos-2, stimeFont, "");
                create_lblName("lblLapTime", laneNo, xpos4relay-(fontSize/2), ypos+halfLaneHeight, timeFont, "");//2023/03/07

            }

        }

        private void create_lblName(string head, int laneNo, int x, int y, Font myFont, string txt = "")
        {

            this.mylabel = new Label();
            this.mylabel.AutoSize = true;
            this.mylabel.Location = new System.Drawing.Point(x, y);
            this.mylabel.Name = head + laneNo;
            //this.mylabel.Size = new System.Drawing.Size(w, h);
            // this.mylabel.TabIndex = 19;
            this.mylabel.Text = txt;
            this.mylabel.Font = myFont;
            this.Controls.Add(this.mylabel);
        }

        public void show_swimmer_name(int laneNo, int swimmerID)
        {
            if (swimmerID == 0)
            {
                this.Controls["lblName" + laneNo].Text = "";
            } else {
                this.Controls["lblName" + laneNo].Text = swmDB.get_name(swimmerID);
            }

        }
        public void show_reason_and_set_occupied(int laneNo, int reason_code)
        {
            if (reason_code == 0)
            {
                occupied[laneNo] = 1;
                return;
            }
            if (reason_code == 1)
            {
                Controls["lblTime" + laneNo].Text = " 棄権";
                Controls["lblTime" + laneNo].ForeColor = Color.Black;
            }
            if (reason_code == 2)
            {
                Controls["lblTime" + laneNo].Text = " 失格";
//                Controls["lblTime" + laneNo].BackColor = Color.FromArgb(240, 240, 240);
            }
        }
        private string get_race_name(int uid)
        {
            string returnStr = string.Empty;
            returnStr= ""+program_db.get_class_from_uid(uid) + program_db.get_gender_from_uid(uid) + " " ;
            returnStr +=   program_db.get_distance_from_uid(uid) + program_db.get_shumoku_from_uid(uid) + " " + program_db.get_phase_from_uid(uid);
            return returnStr;


        }
        private void show_header(int uid, int prgNo, int laneNo, int kumi)
        {

            Controls["lblRaceName" + laneNo].Text = "No." + prgNo + "   " + get_race_name(uid) +" "+ kumi+"組";
            Controls["lblGameRecord" + laneNo].Text = "大会記録 : " + misc.timeint2str(program_db.GetBestRecordFromUID(uid));

        }



        private void set_quit_button()
        {
            btnQuit.Location = new Point(this.Width - 65*Width/STDWIDTH, 10);
            btnQuit.Size = new Size(55*Width/STDWIDTH, 25*Width/STDWIDTH);
            btnQuit.Show();
            btnQuit.Font = new Font(fontName, 12*Width/STDWIDTH);
        }
        private void set_start_button()
        {
            this.btnStart.Name = "btnStart";
            this.btnStart.TabIndex = 24;
            this.btnStart.Text = "取込開始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            btnStart.Location = new Point(this.Width - 125*Width/STDWIDTH, 10);
            btnStart.Size = new Size(55*Width/STDWIDTH, 25*Width/STDWIDTH);
            btnStart.Font = new Font(fontName, 8*Width/STDWIDTH);
            btnStart.Show();
            btnStart.Visible = true;
        }
        private void set_lbl2xpoolLength()
        {
            lbl2xpoolLength.Location = new Point(this.Width - 60*Width/STDWIDTH, 38*Width/STDWIDTH);
            lbl2xpoolLength.Size = new Size(40*Width/STDWIDTH, 25*Width/STDWIDTH);
            lbl2xpoolLength.Show();
            lbl2xpoolLength.Font = new Font(fontName, 12*Width/STDWIDTH);
            lbl2xpoolLength.Text = Convert.ToString(get_lap_interval());
        }
        private void set_lblLapInterval()
        {
            lblLapInterval.Location = new Point(this.Width - 142*Width/STDWIDTH, 40*Width/STDWIDTH);

            lblLapInterval.Show();
            lblLapInterval.Font = new Font(fontName, 12*Width/STDWIDTH);
        }
        private void InitMainForm()
        {
            const int outerMarginX = 0;
            const int outerMarginY = 0;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - outerMarginX;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - outerMarginY;

            this.Text = MDBInterface.GetEventName();
            //            MessageBox.Show(" width: " + Width + "   Height: " + Height);
            // in my surface   width=1196 height=791
            InitLapCounter(); //--

            LayoutHeader();
            LayoutLabel();
            layout_button();
            //set_txtBoxTimer();
            set_quit_button();
            set_start_button();
            set_lblLapInterval();
            set_lbl2xpoolLength();
            set_program_number(1);
            set_kumi_number(1);
            firstRun = false;
            FirstPrgNo = 1;

            //serial_interface.readandFifoPush();
            cmdFileIo.init();
            show_lane_order();
            //check_cmd_file_loop();
            if (readThread==null)
            {
                if (!debugMode) { 
                    readThread = new Thread(serial_interface.readandFifoPush);
                    readThread.Start();
                }
            }
            read_serial();
            register_timer();

        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }


        private void show_lane_order()
        {
            int prgNo;
            int kumi;
            int uid;
            int maxPrgNo = program_db.get_max_program_no();

            init_array(occupied);
            prgNo = get_program_number();
            kumi = get_kumi_number();
            if (prgNo > maxPrgNo)
            {
                MessageBox.Show("該当のレースはありません。最終レースを表示します。");
                prgNo = maxPrgNo;
                kumi = 1;
                uid = program_db.get_uid_from_prgno(prgNo);
                while (result_db.race_exist(uid, kumi)) kumi++;
                kumi--;
                set_program_number(prgNo);
                set_kumi_number(kumi);
            }
            uid = program_db.get_uid_from_prgno(prgNo);
            if (!result_db.race_exist(uid, kumi)) MessageBox.Show("該当のレースはありません。");
            else
            {
                if (kumi == 1)
                {
                    uid = program_db.get_uid_from_prgno(prgNo);
                    while (mdb_interface.can_go_with_prev(uid, kumi))
                    {
                        program_db.dec_race_number(ref prgNo);
                        uid = program_db.get_uid_from_prgno(prgNo);
                    }
                }
            }
            set_program_number(prgNo);
            set_kumi_number(kumi);
            show();
        }
        private static bool already_occupied(int[] array, int laneNo) { return (array[laneNo] > 0); }

        private void clear_lane_info()
        {
            for (int lane = 1; lane <= maxLaneNo; lane++)
            {

                clear_time(lane);
                Controls["lblRaceName" + lane].Text = "";
                Controls["lblName" + lane].Text = "";
                Controls["lblGameRecord" + lane].Text = "";
                //Controls["lblKana" + lane].Text = "";
                for (int swimOrder = 1; swimOrder < 5; swimOrder++)
                {
                    Controls["lblLapTime_" + swimOrder + "_" + lane].Text = "";
                    Controls["lblLapTimekk_" + swimOrder + "_" + lane].Text = "";
                    Controls["lblRelaySwimmer" + swimOrder + lane].Text = "";
                    //Controls["lblRelaySwimmerKana" + swimOrder + lane].Text = "";
                }
            }
        }



        private void show()
        {
            int prgNo = get_program_number();
            int kumi = get_kumi_number();
            int[] swimmerID = new int[10];
            int uid = program_db.get_uid_from_prgno(prgNo);
            int prevUID = uid;
            //string connectionString = magicWord + dbfilename;
            if (timer != null)
                timer.Tick -= ev1;


            LastPrgNo = prgNo;
            FirstPrgNo = prgNo;
            show_header(uid, prgNo, 0,kumi);

            int laneNo;
            int lastOccupiedLane = 0;
            bool togetherflag = false;

            int first_lane = 0;
            clear_lane_info();
            InitLapCounter();
            do
            {
                OleDbConnection conn = new OleDbConnection(connectionString);
                using (conn)
                {
                    String sql = "SELECT UID, 選手番号, 組, 水路, 事由入力ステータス, " +
                                        "第１泳者, 第２泳者, 第３泳者, 第４泳者 " +
                                        "FROM 記録マスター WHERE 組=" + kumi + " AND UID=" + uid + ";";
                    OleDbCommand comm = new OleDbCommand(sql, conn);
                    conn.Open();
                    prgNo = program_db.get_race_number_from_uid(uid);
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            laneNo = Convert.ToInt32(dr["水路"]);
                            //if (dr["選手番号"] == DBNull.Value) continue;
                            if (laneNo > 9) continue;
                            swimmerID[laneNo] = misc.if_not_null(dr["選手番号"]);
                            if (swimmerID[laneNo] > 0)
                            {
                                prgNofromLane[laneNo] = prgNo;
                                if (first_lane == 0) first_lane = laneNo;

                                if (prevUID != uid) {  //change header
                                    togetherflag = true;
                                    show_header(uid, prgNo, laneNo,kumi);
                                    prevUID = uid;
                                }
                                uidFromLane[laneNo] = uid;

                                ExcelConnection.program_append(prgNo, program_db.get_class_from_uid(uid),program_db.get_gender_from_uid(uid),
                                    program_db.get_distance_from_uid(uid),program_db.get_shumoku_from_uid(uid), program_db.get_phase_from_uid(uid));

                                if (program_db.is_relay(uid))
                                {
//                                    relayFlag = true;
                                    ExcelConnection.append( prgNo, kumi, laneNo, tmDB.get_name(swimmerID[laneNo]));
                                } else
                                {
//                                    relayFlag = false;
                                    show_swimmer_name(laneNo, swimmerID[laneNo]);
                                    ExcelConnection.append( prgNo, kumi, laneNo, swmDB.get_name(swimmerID[laneNo]));
                                }
                                show_reason_and_set_occupied(laneNo, Convert.ToInt32(dr["事由入力ステータス"]));
                                lastOccupiedLane = laneNo;
                            } else
                            {

                            }
                        }
                    }
                }
                if (result_db.race_exist(uid, kumi + 1)) break;
                prevUID = uid;
                uid = program_db.get_next_uid(uid);

            } while (can_go_with_next(uid, prevUID, kumi, lastOccupiedLane));
            LastPrgNo = prgNo;
            if (togetherflag)
            {
                Controls["lblRaceName" + first_lane].Text = Controls["lblRaceName0"].Text;
                Controls["lblGameRecord" + first_lane].Text = Controls["lblGameRecord0"].Text;
                Controls["lblRaceName0"].Text = "合同レース";
                Controls["lblGameRecord0"].Text = "";
            }
            lane_monitor.init_lane_monitor(); /* bug fix 2023/10/2 */
        }
        private static bool can_go_with_next(int uid, int prevUID, int kumi, int prevLastLane)
        {
            if (kumi > 1) return false;

            if (!program_db.is_same_distance_style(uid, prevUID)) return false;
            //          if (result_db.race_exist(uid, 2)) return false;
            if (result_db.get_first_occupied_lane(uid, 1) <= prevLastLane) return false;
            return true;
        }

        private void btnShow_click(object sender, EventArgs e)
        {
            show_lane_order();
        }

        private void btnShowPrev_Click(object sender, EventArgs e)
        {
            int prgNo = get_program_number();
            int kumi = get_kumi_number();
            result_db.get_prev_race(ref prgNo, ref kumi);

            set_program_number(prgNo);
            set_kumi_number(kumi);
            show_lane_order();
        }
        public void show_next_race(object sender, EventArgs e)
        {
            int prgNo = LastPrgNo;
            int kumi = get_kumi_number();

            while (true)
            {
                int uid = program_db.get_uid_from_prgno(prgNo);
                kumi++;
                if (result_db.race_exist(uid, kumi))
                {
                    set_program_number(program_db.get_race_number_from_uid(uid));
                    set_kumi_number(kumi);
                    show_lane_order();
                    return;
                }
                if (program_db.inc_race_number(ref prgNo) == false)
                {
                    //stop_monitor();
                    MessageBox.Show("最終レースです。");
                    return;
                }

                kumi = 0;
            }
        }
        private void btnShowNext_Click(object sender, EventArgs e)
        {
            show_next_race(sender, e);


        }
        private string align(int n)
        {
            if (n < 10) return "  " + n;
            if (n < 100) return " " + n;
            return "" + n;
        }
        private string align2(int n)
        {
            if (n < 10) return " " + n;

            return "" + n;
        }

        private void lap_case1(int laneNo,int lapCount, int intTime, int[,] lapTime)
        {
            if (lapCount == 1) { 
                Controls["lblLapTime_2_" + laneNo].Text = misc.timeint2str(intTime);
            }
            
            if (lapCount == 2)
            {
                Controls["lblLapTime_4_" + laneNo].Text = misc.timeint2str(intTime);
                Controls["lblLapTimekk_4_" + laneNo].Text = "(" + misc.timeint2str(misc.substract_time(intTime, lapTime[laneNo, 1]))+")";
            }
        }
        // lap_case2 ... distanc==200 and short cource,  distance==400 and long cource 
        private void lap_case2(int laneNo,int lapCount,int intTime, int[,] lapTime)
        {
            Controls["lblLapTime_" + lapCount + "_" + laneNo].Text = misc.timeint2str(intTime);
            if (lapCount>1)
            {
                Controls["lblLapTimekk_"+ lapCount+ "_"+laneNo].Text = 
                   "(" + misc.timeint2str(misc.substract_time(intTime, lapTime[laneNo, lapCount-1]) )+")";
            }
        }
        /*
         lap_case3 ...  distance=400 and short couse. or distance==800 and long course.
          short course case....
          distance  : 50 100 150 200 250 300 350 400(goal)
          lapCount  :  1   2   3   4   5   6   7   8
          bylapCount:  0   1   1   2   2   3   3   4
        
          long course case...
          distance  :100 200 300 400 500 600 700 800
          lapCount  :  1   2   3   4   5   6   7   8
          bylapCount:  0   1   1   2   2   3   3   4
 
        */
        private void lap_case3(int laneNo,int lapCount,int intTime, int[,] lapTime)
        {

            if ((lapCount % 2) == 1) return;
            int bylapCount = lapCount >> 1;
            Controls["lblLapTime_" +bylapCount+"_"+ laneNo].Text = misc.timeint2str(intTime);
            if (bylapCount>1)
            {
                Controls["lblLapTimekk_"+ bylapCount+ "_"+laneNo].Text = 
                   "(" + misc.timeint2str(misc.substract_time(intTime, lapTime[laneNo, lapCount-2]) )+")";
            } 
        }
        // lap_case4 ... distance=1500... or distance==800 and short course
        private void lap_case4(int laneNo, int lapCount, int intTime, int[,] lapTime)
        {
            if (lapCount > 4)
            {
                int lc;
                for (lc = 1; lc < 4; lc++)
                {
                    int lcp1 = lc + 1;
                    Controls["lblLapTime_" + lc + "_" + laneNo].Text = Controls["lblLapTime_" + lcp1 + "_" + laneNo].Text;
                    Controls["lblLapTimekk_" + lc + "_" + laneNo].Text = Controls["lblLapTimekk_" + lcp1 + "_" + laneNo].Text;

                }
                Controls["lblLapTime_4_" + laneNo].Text = misc.timeint2str(intTime);
                Controls["lblLapTimekk_4_" + laneNo].Text =
                   "(" + misc.timeint2str(misc.substract_time(intTime, lapTime[laneNo, lapCount - 1])) + ")";
            }
            else lap_case2(laneNo, lapCount, intTime, lapTime);

        }


        private void write_lap_2(int laneNo, int intTime,int thisDistance, int goalDistance, int[,] lapTime, int lapCount, int lapInterval)
        {
            if (goalDistance <= 50) return;
            if (goalDistance==100)
            {
                if (lapInterval == 100) return;
                lap_case1(laneNo, lapCount, intTime, lapTime);
            }
            else if (goalDistance==200)
            {
                if (lapInterval == 100)
                {
                    lap_case1(laneNo,lapCount, intTime, lapTime);
                } else/* lapInterval==50 , that is, short cource*/ lap_case2(laneNo,lapCount,intTime,lapTime);
            }
            else if (goalDistance==400)
            {
                if (lapInterval == 100) lap_case2(laneNo, lapCount, intTime, lapTime);
                else /* (lapInterval == 50)*/ lap_case3(laneNo,lapCount,intTime,lapTime);
            }
            else if (goalDistance==800)
            {
                if (lapInterval == 100) lap_case3(laneNo, lapCount, intTime, lapTime);
                if (lapInterval == 50) lap_case4(laneNo, lapCount, intTime, lapTime);
            }
            else
            {
                lap_case4(laneNo,lapCount,intTime,lapTime);
            }

        }
        private void write_time(int intGoalTime, int laneNo,ushort orderOfArrival,string distance,bool fin=false, int gameRecord=0)
        {
	    string goalTime=misc.timeint2str(intGoalTime);
            if (fin)
            {

                if (intGoalTime<gameRecord)
                {
                    Controls["lblTime" + laneNo].Text = goalTime+"大会新";
                    Controls["lblTime" + laneNo].ForeColor = System.Drawing.Color.Red;
                } else {
                    Controls["lblTime" + laneNo].Text = goalTime+"  Fin";
                    Controls["lblTime" + laneNo].ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                Controls["lblTime" + laneNo].ForeColor = System.Drawing.Color.Black;
                Controls["lblTime" + laneNo].Text = goalTime + "  " + distance ;
                enable_timer(laneNo);
            }

            Controls["lblArrivalOrder" + laneNo].Text = "" + orderOfArrival;

        }
        private void write_lap(int laptime,int laneNo)
        {
            Controls["lblLapTime" + laneNo].Text = "(" +
                misc.timeint2str(laptime) + ")";
        }
        private void clear_time(int laneNo)
        {
            Controls["lblTime" + laneNo].Text = "";
            Controls["lblLapTime" + laneNo].Text = "";
            Controls["lblArrivalOrder" + laneNo].Text = "";
        }
        private void erase_lane0(object s, EventArgs e)
        {
            timerL0.Tick -= evl0;
            timerL0.Enabled = false;
            clear_time(0);
        }
        private void erase_lane1(object s, EventArgs e)
        {
            timerL1.Tick -= evl1;
            timerL1.Enabled = false;
            clear_time(1);
        }
        private void erase_lane2(object s, EventArgs e)
        {
            timerL2.Tick -= evl2;
            timerL2.Enabled = false;
            clear_time(2);
        }
        private void erase_lane3(object s, EventArgs e)
        {
            timerL3.Tick -= evl3;
            timerL3.Enabled = false;
            clear_time(3);
        }
        private void erase_lane4(object s, EventArgs e)
        {
            timerL4.Tick -= evl4;
            timerL4.Enabled = false;
            clear_time(4);
        }
        private void erase_lane5(object s, EventArgs e)
        {
            timerL5.Tick -= evl5;
            timerL5.Enabled = false;
            clear_time(5);
        }
        private void erase_lane6(object s, EventArgs e)
        {
            timerL6.Tick -= evl6;
            timerL6.Enabled = false;
            clear_time(6);
        }
        private void erase_lane7(object s, EventArgs e)
        {
            timerL7.Tick -= evl7;
            timerL7.Enabled = false;
            clear_time(7);
        }
        private void erase_lane8(object s, EventArgs e)
        {
            timerL8.Tick -= evl8;
            timerL8.Enabled = false;
            clear_time(8);
        }
        private void erase_lane9(object s, EventArgs e)
        {
            timerL9.Tick -= evl9;
            timerL9.Enabled = false;
            clear_time(9);
        }
        private void enable_timer(int laneNo)
        {
            if (laneNo==0)
            {
                timerL0.Tick += evl0;
                timerL0.Enabled = true;
            }
            if (laneNo==1)
            {
                timerL1.Tick += evl1;
                timerL1.Enabled = true;
            }
            if (laneNo==2)
            {
                timerL2.Tick += evl2;
                timerL2.Enabled = true;
            }
            if (laneNo==3)
            {
                timerL3.Tick += evl3;
                timerL3.Enabled = true;
            }
            if (laneNo==4)
            {
                timerL4.Tick += evl4;
                timerL4.Enabled = true;
            }
            if (laneNo==5)
            {
                timerL5.Tick += evl5;
                timerL5.Enabled = true;
            }
            if (laneNo==6)
            {
                timerL6.Tick += evl6;
                timerL6.Enabled = true;
            }
            if (laneNo==7)
            {
                timerL7.Tick += evl7;
                timerL7.Enabled = true;
            }
            if (laneNo==8)
            {
                timerL8.Tick += evl8;
                timerL8.Enabled = true;
            }
            if (laneNo==9)
            {
                timerL9.Tick += evl9;
                timerL9.Enabled = true;
            }
        }

        private void init_array(int[] array, int value=0)
        {
            int ix;
            for (ix = 0; ix < 10; ix++)
            {
                array[ix] = value;
            }
        }
        private bool is_race_comp()
        {
            int laneNo;
            for (laneNo = 0; laneNo < 10; laneNo++)
            {
                if ((occupied[laneNo] == 1) && (lane_monitor.Is_goal(laneNo) == false))
                {
                    return false;
                }
            }
            return true;
        }

        private void tbxKumi_TextChanged(object sender, EventArgs e)
        {

        }

        private int get_distance_from_lane(int laneNo)
        {
            int uid = uidFromLane[laneNo];
            string distance = program_db.get_distance_from_uid(uid);
            if (distance == null) return 0;
            int strlen = distance.Length;
            return Convert.ToInt32(distance.Substring(0, strlen-1));

        }
        private async void read_serial()
        {
            int intTime = 111111;
            int laneNo = 0;
            int arrivalOrder = 0;
            int lapInterval = evtDB.Get_lap_interval();
            bool goalFlag=false;
            //bool goalFlag; //determined by distance
            tmd = serial_interface.tmd;

            while (true)
            {
              //  if (!monitorEnable) break;
                bool rc;
                rc = tmd.pop(ref intTime, ref laneNo, ref arrivalOrder,ref goalFlag);
                if (rc)
                {
                    if (intTime==0)
                    {
                        setupFileIo.writeLog("line868: reset comes.");
                        // actually never happens
                    }
                    else
                    {
                        lapCounter[laneNo]++;
                        //string mytime = misc.timeint2str(intTime);
                        
                        string distance;
                        int intDistance = lapCounter[laneNo] * lapInterval;
                        int goalDistance = get_distance_from_lane(laneNo);
                        int gameRecord = program_db.GetBestRecordFromUID(uidFromLane[laneNo]);
                        if (goalDistance>0)
                        {

                            distance = "" +intDistance + "m";
                            //goalFlag = (intDistance == goalDistance);
                            ExcelConnection.insert_time(prgNofromLane[laneNo], get_kumi_number(), laneNo, intTime, distance);
                            write_time(intTime, laneNo,  (ushort) arrivalOrder,distance,goalFlag,gameRecord);
                            write_lap_2(laneNo, intTime, intDistance, goalDistance, lapTime, lapCounter[laneNo],lapInterval);
                            if (lapCounter[laneNo]>1)
                            {
                                write_lap(misc.substract_time(intTime, lapTime[laneNo,lapCounter[laneNo]-1]),laneNo);
                            }
                            if (goalFlag)
                            {
                                lane_monitor.Set_goal(laneNo);
                                lapCounter[laneNo] = 0;
                                ExcelConnection.insert_time(prgNofromLane[laneNo], get_kumi_number(), laneNo,intTime);
                            }
                            lapTime[laneNo,lapCounter[laneNo]] = intTime;
                        } 
                    }
                }
                await Task.Delay(300);
                if (stopPoling) return;
                check_cmd_file();
                if (is_race_comp())
                {
                    lane_monitor.init_lane_monitor();
                    //InitLapCounter();
                    timer.Tick += ev1;
                    timer.Interval = 1000*Form1.get_interval_2_next_race();
                    timer.Enabled = true;
                }
            }
                    ///---check---
           
        }
        private async void check_cmd_file_loop()
        {
            while (true) { 
                await Task.Delay(300);
                if (stopPoling) return;
                check_cmd_file();
            }
        }

        private void check_cmd_file()
        {
            int kumi=0, prgNo=0;
            bool resultFlag=false;
            if (cmdFileIo.get_prgNo_kumi_from_cmd_file( ref prgNo, ref kumi, ref resultFlag))
            {
                int currentPrgNo = get_program_number();
                int currentKumi = get_kumi_number();
                if ((prgNo != currentPrgNo) || (kumi != currentKumi))
                {
                    set_program_number(prgNo);
                    set_kumi_number(kumi);
                    show();
                }
                if (resultFlag)
                {
                    show_record();
                    calc_arrival_order();
                    show_arrival_order();
                }
            }
        }

        private void calc_arrival_order()
        {
            int laneNo1, laneNo2;
            init_array(arrivalOrder, 999999);
            for (laneNo1 = 1; laneNo1 < maxLaneNo; laneNo1++)
            {
                for (laneNo2=laneNo1+1; laneNo2 < maxLaneNo;laneNo2++)
                {
                    if (goalTimeArray[laneNo1] > goalTimeArray[laneNo2]) arrivalOrder[laneNo1]++;
                    if (goalTimeArray[laneNo1] < goalTimeArray[laneNo2]) arrivalOrder[laneNo2]++;
                }
            }
        }
        private void show_arrival_order()
        {
            int laneNo;
            for (laneNo = 1; laneNo < maxLaneNo;laneNo++)
            {

                if (arrivalOrder[laneNo]<999999)
                {
                    Controls["lblArrivalOrder" + laneNo].Text = "" + arrivalOrder[laneNo];
                }
            }

        }

        public bool is_occupied(int laneNo)
        {
            int prgNo = get_program_number();
            int kumi = get_kumi_number();
            int swimmerID;

            //string connectionString = magicWord + dbfilename;
            bool rc = false;
            int uid = program_db.get_uid_from_prgno(prgNo);
            OleDbConnection conn = new OleDbConnection(connectionString);
            using (conn)
            {
                String sql = "SELECT UID, 選手番号, 組, 水路, 事由入力ステータス,ゴール " +
                                    "FROM 記録マスター WHERE 組=" + kumi + " AND UID=" + uid +
                                    " AND 水路=" + laneNo + ";";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();

                using (OleDbDataReader dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        swimmerID = misc.if_not_null(dr["選手番号"]);
                        if (swimmerID > 0)
                        {
                            if (Convert.ToInt32(dr["事由入力ステータス"]) == 0)
                            {
                                rc = true;
                            }
                        }
                    }
                }
            }
            return rc;

        }
        public bool show_record()
        {
            int prgNo = get_program_number();
            int kumi = get_kumi_number();
            int swimmerID;

            //string connectionString = magicWord + dbfilename;
            bool rc = true;
            int laneNo;

            init_array(arrivalOrder,1);
            init_array(goalTimeArray,999999);
            while (prgNo <= LastPrgNo)
            {
                int uid = program_db.get_uid_from_prgno(prgNo);
                OleDbConnection conn = new OleDbConnection(connectionString);
                using (conn)
                {
                    String sql = "SELECT UID, 選手番号, 組, 水路, 事由入力ステータス,ゴール " +
                                        "FROM 記録マスター WHERE 組=" + kumi + " AND UID=" + uid + ";";
                    OleDbCommand comm = new OleDbCommand(sql, conn);
                    conn.Open();

                    using (OleDbDataReader dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            laneNo = Convert.ToInt32(dr["水路"]);
                            if (laneNo > 10) continue;
                            swimmerID = misc.if_not_null(dr["選手番号"]);
                            if (swimmerID > 0)
                            {
                                int reasonCode = Convert.ToInt32(dr["事由入力ステータス"]);
                                if (reasonCode == 0)
                                {
                                    if (dr["ゴール"] == DBNull.Value) rc = false;
                                    else
                                    {
                                        string goalTime = Convert.ToString(dr["ゴール"]);
                                        if (goalTime == "") rc = false;
                                        else
                                        {
                                            goalTimeArray[laneNo]=misc.timestr2int(goalTime);
                                            bool newRecord = (goalTimeArray[laneNo] < program_db.GetBestRecordFromUID(uid));
                                            Controls["lblTime" + laneNo].Text = goalTime;
                                        }
                                    }


                                } else
                                {
                                        Controls["lblTime" + laneNo].Text = reasonString[reasonCode];

                                }
                            }
                        }
                    }
                }
                program_db.inc_race_number(ref prgNo);
            }
            return rc;
        }

        private void set_labeltimer()
        {
            /////----------------------------------lapAliveTime
            int dispTime = 1000*Form1.get_lap_alive_time();

            timerL0 = new System.Windows.Forms.Timer();
            timerL1 = new System.Windows.Forms.Timer();
            timerL2 = new System.Windows.Forms.Timer();
            timerL3 = new System.Windows.Forms.Timer();
            timerL4 = new System.Windows.Forms.Timer();
            timerL5 = new System.Windows.Forms.Timer();
            timerL6 = new System.Windows.Forms.Timer();
            timerL7 = new System.Windows.Forms.Timer();
            timerL8 = new System.Windows.Forms.Timer();
            timerL9 = new System.Windows.Forms.Timer();
            timerL0.Interval = dispTime; // should be updated to 5000 or so.
            timerL1.Interval = dispTime; // should be updated to 5000 or so.
            timerL2.Interval = dispTime; // should be updated to 5000 or so.
            timerL3.Interval = dispTime; // should be updated to 5000 or so.
            timerL4.Interval = dispTime; // should be updated to 5000 or so.
            timerL5.Interval = dispTime; // should be updated to 5000 or so.
            timerL6.Interval = dispTime; // should be updated to 5000 or so.
            timerL7.Interval = dispTime; // should be updated to 5000 or so.
            timerL8.Interval = dispTime; // should be updated to 5000 or so.
            timerL9.Interval = dispTime; // should be updated to 5000 or so.
        }

        private void register_timer()
        {
                timer = new System.Windows.Forms.Timer();
                set_labeltimer();
                timer.Interval = Form1.get_interval_2_next_race();
                timer.Enabled = false;
                ev1 = new EventHandler(show_next_race);
                register_event();

        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!monitorEnable) start_monitor();
            else stop_monitor();
        }        
        private void stop_monitor()
        {
            Controls["btnStart"].Text = "取込開始";
            lblPending.Visible = true;
            monitorEnable = false;
        }

        private void start_monitor()
        {
            Controls["btnStart"].Text = "取込停止";
            lblPending.Visible = false;
            monitorEnable = true;
//            register_timer();

        }

        private void register_event()
        {
            evl1 = new EventHandler(erase_lane1);
            evl2 = new EventHandler(erase_lane2);
            evl3 = new EventHandler(erase_lane3);
            evl4 = new EventHandler(erase_lane4);
            evl5 = new EventHandler(erase_lane5);
            evl6 = new EventHandler(erase_lane6);
            evl7 = new EventHandler(erase_lane7);
            evl8 = new EventHandler(erase_lane8);
            evl9 = new EventHandler(erase_lane9);
            evl0 = new EventHandler(erase_lane0);
        }
}
    public static class serial_interface {
        public static bool threadStop = false;
        public static SerialPort _serialPort=(SerialPort)null;

        public static timeData tmd=new timeData();
        public static bool open_serial_port(string portName)
        {
            if (_serialPort != null) return true;
            try
            {

                _serialPort = new SerialPort();
                _serialPort.PortName = portName;
                _serialPort.BaudRate = 9600;
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "Even");
                _serialPort.DataBits = 7;
                _serialPort.ReadTimeout = -1;
                _serialPort.Open();
                return true;

            } catch (Exception /*ex*/)
            {
                //MessageBox.Show(ex.Message);
                
                _serialPort=null;
                return false;
            }
        }

        private static bool is_start(byte[] timedt)
        {
            return ((timedt[13] == 'S')); // (timedt[0]=='A'  ) & (timedt[1]=='R')
        }
        private static bool is_lap(byte[] timedt)
        {
            return (timedt[13] == 'L');
        }
        private static bool is_goal(byte[] timedt)
        {
            return (timedt[13] == 'G');
        }


        public static void readandFifoPush()
        {
            byte[] buffer = new byte[100];
            byte[] charbyte = new byte[20];
            const byte stx = 2;
            const byte etx = 3;
            int howmanyread;
            int counter = -1;
            int laneNo = 0;
            
            string mytime = "";
            string mytimePrev = "";
            int laneNoPrev = 0;
            try
            {
                while (true)
                {
                    try
                    {
                        howmanyread =  _serialPort.Read(buffer, 0, 54); // 54=18*3
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        howmanyread = 0;
                    }
                    for (int j = 0; j < howmanyread; j++)
                    {
                        if (buffer[j] == stx)
                        {
                            counter = 0;
                        }
                        else if (buffer[j] == etx)
                        {
                            counter = -1;
                            if (is_start(charbyte))
                            {
                                if(MainForm.monitorEnable) tmd.push();
                            }
                            int orderOfArrival;
                            laneNo = charbyte[2] - 48;
                            orderOfArrival = charbyte[3] - '0';

                            if (is_lap(charbyte))
                            {
                                mytime = Encoding.ASCII.GetString(charbyte, 5, 8);
                                if ((mytime!=mytimePrev)||(laneNo!=laneNoPrev)) {
                                    if (MainForm.monitorEnable)
                                    tmd.push(misc.timestr2int(mytime), laneNo, orderOfArrival,false);

                                }
                                mytimePrev = mytime;
                                laneNoPrev = laneNo;

                            }
                            if (is_goal(charbyte))
                            {
                                if ((orderOfArrival <= 0) || (orderOfArrival >=11)) {
                                    orderOfArrival = 1;
                                    setupFileIo.writeLog("Invalid order of arrival.");
                                    
                                }

                                mytime = Encoding.ASCII.GetString(charbyte, 5, 8);
                                if ((mytime != mytimePrev) || (laneNo != laneNoPrev))
                                {
                                    if (MainForm.monitorEnable)
                                    tmd.push(misc.timestr2int(mytime), laneNo, orderOfArrival,true);
                                }
                                mytimePrev = mytime;
                                laneNoPrev = laneNo;
                            }

                        } else if (counter >= 0)
                        {
                            charbyte[counter++] = buffer[j];
                            //Console.Write("{0} ,{1}", buffer[j], Encoding.ASCII.GetString(buffer, j,1));
                            if (counter > 16)
                            {
                                setupFileIo.writeLog("error counter reaches 17.");
                                counter = -1;
                            }
                        }

                        if (threadStop) return;
                    }
                    if (threadStop) return;
                }


            }
            catch (TimeoutException e) {
                Console.WriteLine(e.Message);
            }
        }    

    }
    public  static class lane_monitor
    {
        private static bool[] goaled=new bool[10];
        public static void init_lane_monitor()
        {
            int ix;
            for (ix=0; ix<10; ix++)
            {
                goaled[ix] = false;
            }
        }
        public static void Set_goal(int laneNo) => goaled[laneNo] = true;
        public static bool Is_goal(int laneNo) => goaled[laneNo];

    }
    public static class mdb_interface
    {

        public static bool can_go_with_prev(int uid, int kumi)
        {
            int prgNo;
            int prevuid;
            int minLaneNumber = result_db.get_first_occupied_lane(uid, kumi);

            if (minLaneNumber == 1) return false;
            prgNo = program_db.get_race_number_from_uid(uid);
            if (prgNo == 1) return false;
            program_db.dec_race_number(ref prgNo);
            if (prgNo == 0) return false;
            prevuid = program_db.get_uid_from_prgno(prgNo);
            if (result_db.race_exist(prevuid, 2)) return false;
            if (!program_db.is_same_distance_style(uid, prevuid)) return false;
            if (minLaneNumber > result_db.get_last_occupied_lane(prevuid, 1)) return true;
            return false;
        }
        
    }
 
    public class cmdFileIo
    {

        private static string cmdFile = "IsisCmd.Txt"; //file name is always IsisCmd.Txt which is the rule of SEIKO
        private static int prevPrgNo, prevKumi;
        private static bool prevResultFlag;
        public static bool cmdNotFound = false;

        public static void set_cmd_file(string cmdFileName)
        {
            cmdFile = cmdFileName;
        }
        public static void init()
        {
            if (cmdNotFound) return;
            try
            {
                using (StreamReader reader = new StreamReader(cmdFile, System.Text.Encoding.GetEncoding("sjis")))
                {
                    string line = "";
                    if ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(':');
                        prevPrgNo = Int32.Parse(words[1]);
                        prevKumi = Int32.Parse(words[2]);
                        prevResultFlag = (words[0] == "R");

                    }

                }
            }
            catch
            {
                MessageBox.Show(cmdFile + "(command file) cannot be opened.");
                cmdNotFound = true;
            }
         }
        public static bool get_prgNo_kumi_from_cmd_file(ref int prgNo, ref int Kumi, ref bool resultFlag)
        {
            if (cmdNotFound) return false;
            try {
                using (StreamReader reader = new StreamReader(cmdFile, System.Text.Encoding.GetEncoding("sjis")))
                {
                    string line = "";
                    if ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(':');

                        prgNo = Int32.Parse(words[1]);
                        Kumi = Int32.Parse(words[2]);
                        resultFlag = (words[0] == "R");
                        if ((prgNo !=prevPrgNo)||(Kumi!=prevKumi)||(resultFlag!=prevResultFlag))
                        {
                            prevPrgNo = prgNo;
                            prevKumi = Kumi;
                            prevResultFlag=resultFlag;
                            return true;
                        }
                        return false;
                    } 
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }
     }
 
    public  class timeData
    {
        Queue<int> dataFifo = new Queue<int>();
        private static int timeDataEncode(int intTime, int laneNo, int orderOfArrival, bool goalFlag)
        {
            int returnValue;
            if (goalFlag)
            {
                returnValue=(laneNo * 1000000 + (orderOfArrival * 10000000) + intTime+100000000);

            }
            else
            {
                returnValue=(laneNo * 1000000 + (orderOfArrival * 10000000) + intTime);
            }
            return returnValue;

        }
        private static void timeDataDecode(int timedata, ref int intTime, ref int laneNo, ref int orderOfArrival,ref bool goalFlag)
        {
            goalFlag = (timedata >= 100000000);
            timedata = timedata % 100000000;
            
            orderOfArrival = timedata / 10000000;
            laneNo = (timedata % 10000000) / 1000000;
            intTime = timedata % 1000000;
        }
        public void push()
        {
            dataFifo.Enqueue(0);
        }
        public void push(int intTime, int laneNo, int orderOfArrival,bool goalFlag)
        {
            dataFifo.Enqueue(timeDataEncode(intTime, laneNo, orderOfArrival,goalFlag));
        }
        public bool pop(ref int intTime, ref int laneNo, ref int orderOfArrival,ref bool goalFlag)
        {
            if (dataFifo.Count>0)
            {
                timeDataDecode(dataFifo.Dequeue(), ref intTime, ref laneNo, ref orderOfArrival, ref goalFlag);
                return true;
            }
            return false;
            
        }
    }

}
//------------------------trash--------------------------------

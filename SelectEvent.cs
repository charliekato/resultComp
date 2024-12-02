using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ResultComp
{
    public partial class SelectEvent : Form
    {
        public SelectEvent()
        {
            InitializeComponent();
            ShowEventList();

        }
        public void ShowEventList()
        {
            string connectionString = MDBInterface.GetConnectionString();
            string sqlQuery = "select * from 大会設定";
            listEvent.Items.Clear();
           
            listEvent.DrawMode = DrawMode.OwnerDrawFixed;
            listEvent.DrawItem += (sender, e) =>
            {
                e.DrawBackground();
                if (e.Index >= 0)
                {
                    string item = listEvent.Items[e.Index].ToString();
                    string[] parts = item.Split('|'); // "|" 区切りでアイテムを分割

                    // カラムごとに描画
                    int x = e.Bounds.Left;
                    e.Graphics.DrawString(parts[0], e.Font, Brushes.Black, x, e.Bounds.Top);
                    x += 60; // カラムの位置調整
                    e.Graphics.DrawString(parts[1], e.Font, Brushes.Black, x, e.Bounds.Top);
                    x += 680;
                    e.Graphics.DrawString(parts[2], e.Font, Brushes.Black, x, e.Bounds.Top);
                    x += 550;
                    e.Graphics.DrawString(parts[3], e.Font, Brushes.Black, x, e.Bounds.Top);
                }
                e.DrawFocusRectangle();
            };
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while ( reader.Read())
                            {
                                string eventNo;
                                string eventName;
                                string eventDate;
                                string eventStart;
                                string eventEnd;
                                string eventVenue;
                                eventNo = ""+reader["大会番号"];
                                eventName = "" + reader["大会名1"];
                                eventVenue = "" + reader["開催地"];
                                eventStart = "" + reader["始期間"];
                                eventEnd = "" + reader["終期間"];
                                if (eventStart == eventEnd)
                                {
                                    eventDate = Program.Space(6) + eventStart + Program.Space(6);
                                } else
                                {
                                    eventDate = eventStart + "～" + eventEnd;
                                }
                                string showStr = Program.Right(eventNo, 3) + "|"+
                                    eventName + "|" +
                                    eventVenue + "|" +
                                    eventDate;
                                listEvent.Items.Add(showStr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー [ ShowEvnetList ] \n" + ex.Message);
            }


        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            var selectedItem = listEvent.SelectedItem;
            int eventNo;
            if (selectedItem != null)
            {
                eventNo= Int32.Parse(selectedItem.ToString().Substring(0, 3));
                MessageBox.Show("大会番号 > " + eventNo);
                MainForm mainForm = new MainForm(eventNo);
                mainForm.Show();

            }
            else
            {
                MessageBox.Show("大会が選択されていません");
            }
        }
    }
}

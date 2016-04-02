
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace HomeWrok5
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        private static String DB_NAME = "User_DB.db";
        private static String TABLE_NAME = "User";
        private static String SQL_CREATE_TABLE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " (Key TEXT,Value TEXT);";
        private static String SQL_QUERY_VALUE = "SELECT * FROM " + TABLE_NAME;// + " WHERE Key = (?);"; 
        private static String SQL_INSERT = "INSERT INTO " + TABLE_NAME + " VALUES(?,?);";
        private static String SQL_UPDATE = "UPDATE " + TABLE_NAME + " SET Value = ? WHERE Key = ?";
        private static String SQL_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Key = ?";

        public BlankPage1()
        {
            this.InitializeComponent();
            SQLiteConnection conn = new SQLiteConnection(DB_NAME);
            using (var statement = conn.Prepare(SQL_CREATE_TABLE))
            {
                statement.Step();
            }
        }
        //void zx()
        //{
        //    SQLiteConnection conn = new SQLiteConnection("User_DB");
        //    using (var statement = conn.Prepare("CREATE TABLE IF NOT EXISTS User_DB (key TEXT.Value TEXT)"))
        //    {
        //        statement.Step();
        //    }

        //    using (var statement = conn.Prepare("SELECT*FROM User"))
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        while (statement.Step() == SQLiteResult.ROW)
        //        {

        //        }
        //    }
        //}
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(DB_NAME);
            if (num.Text == "")
                new MessageDialog("请输入账号");
            else if (key.Password == "")
                new MessageDialog("请输入密码");
            else
            {
                using (var statement = conn.Prepare(SQL_QUERY_VALUE))
                {
                    StringBuilder sb = new StringBuilder();
                    while (statement.Step() == SQLiteResult.ROW)
                    {
                        for (int i = 0; i < statement.DataCount; i++)
                            sb.AppendLine(statement[i].ToString());

                    }
                    await new MessageDialog(sb.ToString()).ShowAsync();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}

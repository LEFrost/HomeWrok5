using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace HomeWrok5
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string uri = "http://apis.baidu.com/showapi_open_bus/showapi_joke/joke_text";
        public MainPage()
        {
            this.InitializeComponent();
        }

        public async Task<string> myHttpClient()
        {
            string content = "";
            try
            {

                return await Task.Run(() =>
                {

                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("apikey", "6e4b2f5b59fc77691942c8ba6f3bf7e3");
                    HttpResponseMessage response;
                    response = httpClient.GetAsync(new Uri(uri)).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        content = response.Content.ReadAsStringAsync().Result;
                    return content;
                });
            }
            catch
            {
                new MessageDialog("好像没有网路");
                return "";
            }
        }
        public async void writeFile(string filename, string content)
        {
            StorageFolder fold = ApplicationData.Current.LocalFolder;
            StorageFile file = await fold.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, content);
        }
        public async Task<string> readFile(string filename)
        {
            var fold = ApplicationData.Current.LocalFolder;
            StorageFile file = await fold.GetFileAsync(filename);
            string result = await FileIO.ReadTextAsync(file);
            return result;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string content = await myHttpClient();
            JObject json = JObject.Parse(content);
            //display.Text = json.ToString();
            writeFile("json", json.ToString());

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            display.Text = await readFile("json");
        }
    }
}

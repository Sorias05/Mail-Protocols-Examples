using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;


namespace _04_http_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string folderName = "Files";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            string url = $"https://source.unsplash.com/random/{txtWidth.Text}x{txtHeight.Text}/?{txtCategory.Text}&1";

            HttpClient client = new HttpClient();

            string name = Guid.NewGuid().ToString();

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = $"Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            dlg.Title = "Save an Image File";
            dlg.FileName = $"{name}";

            if (dlg.ShowDialog() == true)
            {
                string folderPath = Path.GetDirectoryName(dlg.FileName);
                string fileName = Path.GetFileName(dlg.FileName);
                string dest = Path.Combine(folderPath, $"{fileName}");
                //MessageBox.Show(folderName);
                WebClient webClient = new WebClient();

                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;

                webClient.DownloadFileAsync(new Uri(url), dest);
                lbHistory.Items.Add($"{DateTime.Now.ToString()} {dest}");
            }
            
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.InitialDirectory = "c:\\";
            //dlg.ValidateNames = false;
            //dlg.CheckFileExists = false;
            //dlg.CheckPathExists = true;
            //dlg.FileName = "Directory.";
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    folderName = Path.GetDirectoryName(dlg.FileName);
            //}



            //try
            //{
            //    var response = await client.GetAsync(url);
            //    MessageBox.Show($"Response status: {response.StatusCode}");

            //    //response.Content.ReadAsStream

            //    if (!Directory.Exists(folderName))
            //        Directory.CreateDirectory(folderName);

            //    using FileStream stream = File.Create(dest);
            //    {
            //        await response.Content.CopyToAsync(stream);
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message );
            //}

            
        }

        

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Title = $"Progress: {e.ProgressPercentage}%";
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WebApiClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = WebRequest.Create(txtUrl.Text.Trim()) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Credentials = new NetworkCredential(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            byte[] postData = Encoding.UTF8.GetBytes(txtPostData.Text.Trim());
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
            }
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                txtResult.Text = reader.ReadToEnd();
            }
        }
    }
}

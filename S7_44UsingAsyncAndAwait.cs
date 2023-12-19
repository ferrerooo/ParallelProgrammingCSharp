using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form1
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Version1: blocking call

        public int CalculateValue()
        {
            Thread.Sleep(5000);
            return 123;
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            int n = CalculateValue();
            LblResult.Text = n.ToString();
        }
        

        Version2
        public Task<int> CalculateValueAsync()
        {
            return Task.Factory.StartNew(() => 
            {
                Thread.Sleep(5000);
                return 123;
            });
        }
        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            var calcuation = CalculateValueAsync();
            calcuation.ContinueWith(t => 
            {
                LblResult.Text = t.Result.ToString();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        

        Version 3: async await pattern
        public async Task<int> CalculateValueAsync()
        {
            await Task.Delay(5000);
            return 123;
        }
        private async void BtnCalculate_Click(object sender, EventArgs e)
        {
            int value  = await CalculateValueAsync();
            LblResult.Text = value.ToString();
        }
        

        // Version 4: a little complex case of async await pattern
        public async Task<int> CalculateValueAsync()
        {
            await Task.Delay(5000);
            return 123;
        }
        private async void BtnCalculate_Click(object sender, EventArgs e)
        {
            int value  = await CalculateValueAsync();
            LblResult.Text = value.ToString();

            await Task.Delay(5000);

            using (var wc = new WebClient())
            {
                String data = await wc.DownloadStringTaskAsync("http://google.com/robots.txt");
                Lblresult.Text = data.Split('\n')[0].Trim();
            }
        }

    }

}

*/

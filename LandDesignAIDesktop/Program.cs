using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using System.ClientModel;
using LandDesignAIDesktop.Forms;

namespace LandDesignAIDesktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using (var splash = new SplashForm())
            {
                splash.Show();
                splash.Refresh();

                System.Threading.Thread.Sleep(2000);
            }


            Application.Run(new Form1());
        }
    }
}
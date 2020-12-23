using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using UIControl;

namespace SetDefaultMixer
{
    class Program
    {
        readonly static string mixer = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("appSettings")["mixerurl"];
        readonly static string show = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("appSettings")["show"];
        readonly static string snapshot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("appSettings")["snapshot"];
        static void Main(string[] args)
        {
            if (mixer == null || show == null || snapshot == null) { Environment.Exit(1); }
            else
            {
                ConnectionClass.InitMixer(mixer);
                ConnectionClass.OpenMixer();
                if (ConnectionClass.isOpen)
                {
                    DataClass DataCon = new DataClass();
                    Task.Delay(100).Wait();
                    DataCon.ChangeSnapshot(show, snapshot);
                    ConnectionClass.KeepAlive();
                    Task.Delay(100).Wait();
                    ConnectionClass.KeepAlive();
                    ConnectionClass.CloseMixer();
                    Task.Delay(100).Wait();
                    if (args.Length > 0)
                    {
                        int arguments = int.Parse(args[0]);
                        if (arguments == 1)
                        {
                            Console.WriteLine("Programm executed.");
                            Console.ReadLine();
                        }
                    }
                    Environment.Exit(0);

                }
            }
        }
    }
}

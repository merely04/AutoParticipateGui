using AutoParticipateGui.Models;

namespace AutoParticipateGui.Controllers
{
    public class DataController
    {
        static DataController()
        {
            Settings = new Settings();
        }
        
        public static Settings Settings { get; private set; }
    }
}
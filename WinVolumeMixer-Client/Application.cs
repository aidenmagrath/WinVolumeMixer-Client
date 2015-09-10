using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinVolumeMixer.Client
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Volume { get; set; }
        public bool Muted { get; set; }
        public static object Current { get; internal set; }
    }
}

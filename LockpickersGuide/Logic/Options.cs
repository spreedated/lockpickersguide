using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LockpickersGuide.Logic
{
    internal static class Options
    {
        internal static Models.Options Instance { get; private set; } = null;

        private static readonly string optionsFilepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "options.json");

        internal static void Initialize()
        {
            if (File.Exists(optionsFilepath))
            {
                Load();
                return;
            }
            CreateBlank();
            Load();
        }

        internal static void Load()
        {
            StringBuilder json = new();

            using (FileStream s = File.Open(optionsFilepath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader w = new(s))
                {
                    while (!w.EndOfStream)
                    {
                        json.Append(w.ReadLine());
                    }
                }
            }

            Instance = JsonConvert.DeserializeObject<Models.Options>(json.ToString());
        }

        internal static void Save()
        {
            //string json = JsonConvert.SerializeObject(k, Formatting.Indented);

            //using (FileStream s = File.Create(optionsFilepath))
            //{
            //    using (StreamWriter w = new(s))
            //    {
            //        w.Write(json);
            //    }
            //}
        }

        internal static void CreateBlank()
        {
            string json = JsonConvert.SerializeObject(new Models.Options(), Formatting.Indented);

            using (FileStream s = File.Create(optionsFilepath))
            {
                using (StreamWriter w = new(s))
                {
                    w.Write(json);
                }
            }
        }
    }
}

using Newtonsoft.Json;
using System.IO;
using System.Text;
using static LockpickersGuide.Logic.Variables;

namespace LockpickersGuide.Logic
{
    internal static class Options
    {
        internal static Models.Options Instance { get; private set; } = null;

        internal static void Initialize()
        {
            if (File.Exists(OptionsFilepath))
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

            using (FileStream s = File.Open(OptionsFilepath, FileMode.Open, FileAccess.Read, FileShare.Read))
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
            string json = JsonConvert.SerializeObject(Instance, Formatting.Indented);

            using (FileStream s = File.Create(Variables.OptionsFilepath))
            {
                using (StreamWriter w = new(s))
                {
                    w.Write(json);
                }
            }
        }

        internal static void CreateBlank()
        {
            string json = JsonConvert.SerializeObject(new Models.Options(), Formatting.Indented);

            using (FileStream s = File.Create(OptionsFilepath))
            {
                using (StreamWriter w = new(s))
                {
                    w.Write(json);
                }
            }
        }
    }
}

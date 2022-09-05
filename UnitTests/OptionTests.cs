using LockpickersGuide.Logic;
using NUnit.Framework;
using System;
using System.Linq;
using Moq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace UnitTests
{
    public class OptionTests
    {
        private readonly string optionsFile = Path.Combine(Variables.testFilePath, "options.json");

        [SetUp]
        public void SetUp()
        {
            LockpickersGuide.Logic.Variables.OptionsFilepath = optionsFile;
        }

        [Test]
        public void CreateBlankTest()
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => { Options.CreateBlank(); });
                Assert.That(File.Exists(optionsFile), Is.True);
                Assert.DoesNotThrow(() => { this.VerifyOptionJsonParsable(); });
            });
        }

        [Test]
        public void LoadTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(File.Exists(optionsFile), Is.False);
                Assert.DoesNotThrow(() => { Options.CreateBlank(); });
                Assert.That(File.Exists(optionsFile), Is.True);
                Assert.That(Options.Instance, Is.EqualTo(default(LockpickersGuide.Models.Options)));
                Assert.DoesNotThrow(() => { Options.Load(); });
                Assert.DoesNotThrow(() => { this.VerifyOptionJsonParsable(); });
            });
        }

        [Test]
        public void SaveTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(File.Exists(optionsFile), Is.False);
                Assert.DoesNotThrow(() => { Options.CreateBlank(); Options.Load(); });
            });

            Options.Instance.DatabaseCredentials.Host = "testhost";
            Options.Instance.DatabaseCredentials.Password = "verySecure";

            LockpickersGuide.Models.Options result = null;

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => { Options.Save(); });
                Assert.That(File.Exists(optionsFile), Is.True);
                Assert.That(Options.Instance, Is.Not.EqualTo(default(LockpickersGuide.Models.Options)));
                Assert.DoesNotThrow(() => { result = this.VerifyOptionJsonParsable(); });
                Assert.That(result.DatabaseCredentials.Host, Is.EqualTo("testhost"));
                Assert.That(result.DatabaseCredentials.Password, Is.EqualTo("verySecure"));
            });
        }

        [Test]
        public void InitializeTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Options.Instance, Is.EqualTo(default(LockpickersGuide.Models.Options)));
                Assert.That(File.Exists(optionsFile), Is.False);
                Assert.DoesNotThrow(() => { Options.Initialize(); });
                Assert.That(File.Exists(optionsFile), Is.True);
            });
        }

        private LockpickersGuide.Models.Options VerifyOptionJsonParsable()
        {
            StringBuilder s = new();
            using (FileStream f = File.Open(optionsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader r = new(f))
                {
                    while (!r.EndOfStream)
                    {
                        s.Append(r.ReadLine() + "\n");
                    }
                }
            }

            LockpickersGuide.Models.Options res = JsonConvert.DeserializeObject<LockpickersGuide.Models.Options>(s.ToString());

            Assert.That(res, Is.Not.Null);

            return res;
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(optionsFile))
            {
                File.Delete(optionsFile);
            }
        }
    }
}
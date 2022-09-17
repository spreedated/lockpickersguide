using LockpickersGuide.Logic;
using NUnit.Framework;
using System;
using System.Linq;
using Moq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using LockpickersGuide.Datastructure;
using LockpickersGuide.Models;
using NUnit.Framework.Constraints;

namespace UnitTests
{
    public class DatastructureTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void LocktypeHashsetTest()
        {
            HashSetLockpicker<Locktype> h = new(new LocktypeComparer());

            Locktype l = new()
            {
                DatabaseId = 1,
                Name = "MyLock"
            };

            Locktype f = new()
            {
                DatabaseId = 1,
                Name = "MyLock"
            };

            h.Add(l);
            h.Add(f);
            h.Add(l);

            Assert.Multiple(() =>
            {
                Assert.That(l.Equals(f), Is.True);
                Assert.That(h.Count, Is.EqualTo(1));
            });

            Locktype c1 = new();
            Locktype c2 = new();

            Assert.That(c1.Equals(c2), Is.True);
        }

        [Test]
        public void BeltHashsetTest()
        {
            HashSetLockpicker<Belt> h = new(new BeltComparer());

            Belt l = new()
            {
                DatabaseId = 1,
                Description = "Some Description",
                Imagelink = "http://none.de",
                Name = "Black"
            };

            Belt f = new()
            {
                DatabaseId = 1,
                Description = "Some Description",
                Imagelink = "http://none.de",
                Name = "Black"
            };

            h.Add(l);
            h.Add(f);
            h.Add(l);

            Assert.Multiple(() =>
            {
                Assert.That(l.Equals(f), Is.True);
                Assert.That(h.Count, Is.EqualTo(1));
            });

            Belt c1 = new();
            Belt c2 = new();

            Assert.That(c1.Equals(c2), Is.True);
        }

        [Test]
        public void CountryHashsetTest()
        {
            HashSetLockpicker<Country> h = new(new CountryComparer());

            Country l = new()
            {
                DatabaseId = 1,
                Iso3 = "III",
                Iso = "II",
                Name = "None",
                Nicename = "None"
            };

            Country f = new()
            {
                DatabaseId = 1,
                Iso3 = "III",
                Iso = "II",
                Name = "None",
                Nicename = "None"
            };

            h.Add(l);
            h.Add(f);
            h.Add(l);

            Assert.Multiple(() =>
            {
                Assert.That(l.Equals(f), Is.True);
                Assert.That(h.Count, Is.EqualTo(1));
            });

            Country c1 = new();
            Country c2 = new();

            Assert.That(c1.Equals(c2), Is.True);
        }

        [Test]
        public void BrandHashsetTest()
        {
            HashSetLockpicker<Brand> h = new(new BrandComparer());

            Brand l = new()
            {
                DatabaseId = 1,
                Description = "Some Description",
                Name = "Great Brand",
                Website = "http://some.thing",
                AltName = null,
                City = "None",
                Country = new Country()
                {
                    DatabaseId = 1,
                    Iso3 = "III",
                    Iso = "II",
                    Name = "None",
                    Nicename = "None"
                },
                Founded = 2022
            };

            Brand f = new()
            {
                DatabaseId = 1,
                Description = "Some Description",
                Name = "Great Brand",
                Website = "http://some.thing",
                AltName = null,
                City = "None",
                Country = new Country()
                {
                    DatabaseId = 1,
                    Iso3 = "III",
                    Iso = "II",
                    Name = "None",
                    Nicename = "None"
                },
                Founded = 2022
            };

            h.Add(l);
            h.Add(f);
            h.Add(l);

            Assert.Multiple(() =>
            {
                Assert.That(l.Equals(f), Is.True);
                Assert.That(h.Count, Is.EqualTo(1));
            });

            Brand c1 = new();
            Brand c2 = new();

            Assert.That(c1.Equals(c2), Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            
        }
    }
}
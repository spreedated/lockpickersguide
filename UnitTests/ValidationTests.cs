using LockpickersGuide.Logic;
using NUnit.Framework;
using System;
using System.Linq;
using Moq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using LockpickersGuide.Models;
using System.ComponentModel.DataAnnotations;

namespace UnitTests
{
    public class ValidationTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void CollectionLockValidationTest()
        {
            CollectionLocks f = new()
            {
                DatabaseId = "AF5B",
                BindingOrder = null,
                Brand = new Brand()
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
                },
                Core = new Core()
                {
                    DatabaseId = 1,
                    Name = "Something"
                },
                Description = null,
                Model = "Something",
                Modelname = "Something",
                Type = new Locktype()
                {
                    DatabaseId = 1,
                    Name = "MyLock"
                },
                Picked = true,
                Price = 148.25d,
                Ownership = false
            };

            Assert.That(f.Validate(new ValidationContext(f)).Count(), Is.EqualTo(0));

            CollectionLocks ff = new()
            {
                DatabaseId = "AF5B",
                BindingOrder = null,
                Brand = new Brand()
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
                },
                Description = null,
                Model = "Something",
                Modelname = "Something",
                Type = new Locktype()
                {
                    DatabaseId = 1,
                    Name = "MyLock"
                },
                Picked = true,
                Price = 148.25d,
                Ownership = false
            };

            Assert.That(ff.Validate(new ValidationContext(ff)).Count(), Is.EqualTo(1));
        }

        [TearDown]
        public void TearDown()
        {
           
        }
    }
}
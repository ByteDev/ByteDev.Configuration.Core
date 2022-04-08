using System.Linq;
using ByteDev.Collections;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace ByteDev.Configuration.Core.IntTests
{
    [TestFixture]
    public class ConfigurationExtensionsTests
    {
        private IConfiguration _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ConfigurationBuilder()
                .AddAppSettingsJsonFile()
                .Build();
        }

        [TestFixture]
        public class GetSectionSettings : ConfigurationExtensionsTests
        {
            [Test]
            public void WhenSectionExist_ThenBindToObject()
            {
                var result = _sut.GetSectionSettings<TestAppSettings>("ApplicationSettings");

                Assert.That(result.ExistingString, Is.EqualTo("Value exists"));
                Assert.That(result.ExistingBool, Is.True);
                Assert.That(result.ExistingNumber, Is.EqualTo(10));
            }

            [Test]
            public void WhenSectionNotExist_ThenReturnNull()
            {
                var sut = new ConfigurationBuilder().Build();

                var result = sut.GetSectionSettings<TestAppSettings>("ApplicationSettings");

                Assert.That(result, Is.Null);
            }
        }

        [TestFixture]
        public class GetApplicationSettings : ConfigurationExtensionsTests
        {
            [Test]
            public void WhenApplicationSettingsExist_ThenBindToObject()
            {
                var result = _sut.GetApplicationSettings<TestAppSettings>();

                Assert.That(result.ExistingString, Is.EqualTo("Value exists"));
                Assert.That(result.ExistingBool, Is.True);
                Assert.That(result.ExistingNumber, Is.EqualTo(10));
            }

            [Test]
            public void WhenApplicationSettingsNotExist_ThenReturnNull()
            {
                var sut = new ConfigurationBuilder().Build();

                var result = sut.GetApplicationSettings<TestAppSettings>();

                Assert.That(result, Is.Null);
            }
        }

        [TestFixture]
        public class GetApplicationSettingsValue : ConfigurationExtensionsTests
        {
            [Test]
            public void WhenJsonStringExists_ThenReturnValue()
            {
                var result = _sut.GetApplicationSettingsValue<string>("ExistingString");

                Assert.That(result, Is.EqualTo("Value exists"));
            }

            [Test]
            public void WhenJsonStringNotExists_ThenReturnsNull()
            {
                var result = _sut.GetApplicationSettingsValue<string>("NotExistingString");

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenJsonBoolExists_ThenReturnValue()
            {
                var result = _sut.GetApplicationSettingsValue<bool>("ExistingBool");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenJsonBoolNotExists_ThenReturnFalse()
            {
                var result = _sut.GetApplicationSettingsValue<bool>("NotExistingBool");

                Assert.That(result, Is.False);
            }
            
            [Test]
            public void WhenJsonNumberExists_ThenReturnValue()
            {
                var result = _sut.GetApplicationSettingsValue<int>("ExistingNumber");
                
                Assert.That(result, Is.EqualTo(10));
            }

            [Test]
            public void WhenJsonNumberNotExists_ThenReturnZero()
            {
                var result = _sut.GetApplicationSettingsValue<int>("NotExistingNumber");
                
                Assert.That(result, Is.Zero);
            }

            [Test]
            public void WhenJsonArrayExists_ThenReturnValue()
            {
                var result = _sut.GetApplicationSettingsValue<string[]>("ExistingArray");
                
                Assert.That(result.Length, Is.EqualTo(3));
                Assert.That(result.First(), Is.EqualTo("John"));
                Assert.That(result.Second(), Is.EqualTo("Anna"));
                Assert.That(result.Third(), Is.EqualTo("Peter"));
            }

            [Test]
            public void WhenJsonArrayNotExists_ThenReturnNull()
            {
                var result = _sut.GetApplicationSettingsValue<string[]>("NotExistingArray");

                Assert.That(result, Is.Null);
            }
        }
    }

    public class TestAppSettings
    {
        public string ExistingString { get; set; }

        public bool ExistingBool { get; set; } 

        public int ExistingNumber { get; set; }
    }
}
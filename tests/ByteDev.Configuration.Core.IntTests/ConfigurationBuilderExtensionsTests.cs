using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace ByteDev.Configuration.Core.IntTests
{
    [TestFixture]
    public class ConfigurationBuilderExtensionsTests
    {
        private ConfigurationBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ConfigurationBuilder();
        }

        [TestFixture]
        public class AddAppSettingsJsonFile : ConfigurationBuilderExtensionsTests
        {
            [Test]
            public void WhenDefaultAppSettingFileExists_ThenAddFileSettings()
            {
                _sut.AddAppSettingsJsonFile();

                var result = _sut.Build();

                Assert.That(result.GetValue<Uri>("KeyVaultUri"), Is.EqualTo(new Uri("https://localhost/kvapi/")));
            }

            [Test]
            public void WhenAppSettingsEnvironmentFileExists_ThenAddFileSettings()
            {
                _sut.AddAppSettingsJsonFile(new ConfigurationFileOptions
                {
                    Environment = "uat"
                });

                var result = _sut.Build();

                Assert.That(result.GetValue<Uri>("KeyVaultUri"), Is.EqualTo(new Uri("https://uathost/kvapi/")));
            }

            [Test]
            public void WhenAppSettingsEnvironmentFileNotExists_ThenThrowException()
            {
                _sut.AddAppSettingsJsonFile(new ConfigurationFileOptions
                {
                    Environment = "myenv"
                });

                Assert.Throws<FileNotFoundException>(() => _sut.Build());
            }
        }
    }
}
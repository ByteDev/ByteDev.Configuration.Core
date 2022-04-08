using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace ByteDev.Configuration.Core.UnitTests
{
    [TestFixture]
    public class ConfigurationExtensionsTests
    {
        private IConfiguration _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new InMemoryConfigurationBuilder().Build();
        }

        [TestFixture]
        public class GetApplicationSettingsValue : ConfigurationExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenKeyIsNullOrEmpty_ThenThrowException(string key)
            {
                Assert.Throws<ArgumentException>(() => _sut.GetApplicationSettingsValue<string>(key));
            }
        }
    }
}
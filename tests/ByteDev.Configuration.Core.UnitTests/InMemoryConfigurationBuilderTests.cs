using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace ByteDev.Configuration.Core.UnitTests
{
    [TestFixture]
    public class InMemoryConfigurationBuilderTests
    {
        private InMemoryConfigurationBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new InMemoryConfigurationBuilder();
        }

        [TestFixture]
        public class WithApplicationSetting : InMemoryConfigurationBuilderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenKeyIsNullOrEmpty_ThenThrowException(string key)
            {
                Assert.Throws<ArgumentException>(() => _sut.WithApplicationSetting(key, "Some string"));
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("Value1")]
            public void WhenValidKey_ThenAdd(string value)
            {
                _sut.WithApplicationSetting("Key1", value);

                var result = _sut.Build();

                Assert.That(result.GetValue<string>("ApplicationSettings:Key1"), Is.EqualTo(value));
            }
        }

        [TestFixture]
        public class WithSetting : InMemoryConfigurationBuilderTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenKeyIsNullOrEmpty_ThenThrowException(string key)
            {
                Assert.Throws<ArgumentException>(() => _sut.WithSetting(key, "Some string"));
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("Value1")]
            public void WhenValidKey_ThenAdd(string value)
            {
                _sut.WithSetting("Key1", value);

                var result = _sut.Build();

                Assert.That(result.GetValue<string>("Key1"), Is.EqualTo(value));
            }
        }

        [TestFixture]
        public class Clear : InMemoryConfigurationBuilderTests
        {
            [Test]
            public void WhenCalled_ThenRemovesSettings()
            {
                _sut.WithSetting("Key1", "Value1");

                Assert.That(_sut.Build().GetValue<string>("Key1"), Is.EqualTo("Value1"));

                _sut.Clear();

                Assert.That(_sut.Build().GetValue<string>("Key1"), Is.Null);
            }
        }

        [TestFixture]
        public class Build : InMemoryConfigurationBuilderTests
        {
            [Test]
            public void WhenApplicationSettingsAreSet_ThenReturnConfig()
            {
                var uri = new Uri("https://www.google.com/");
                var guid = Guid.NewGuid();

                _sut.WithApplicationSetting("SomeString", "Test 123");
                _sut.WithApplicationSetting("SomeInt", 100);
                _sut.WithApplicationSetting("SomeUri", uri);
                _sut.WithApplicationSetting("SomeGuid", guid);

                var result = _sut.Build();

                Assert.That(result.GetValue<string>("ApplicationSettings:SomeString"), Is.EqualTo("Test 123"));
                Assert.That(result.GetValue<int>("ApplicationSettings:SomeInt"), Is.EqualTo(100));
                Assert.That(result.GetValue<Uri>("ApplicationSettings:SomeUri"), Is.EqualTo(uri));
                Assert.That(result.GetValue<Guid>("ApplicationSettings:SomeGuid"), Is.EqualTo(guid));
            }

            [Test]
            public void WhenSettingsAreSet_ThenReturnConfig()
            {
                var uri = new Uri("https://www.google.com/");
                var guid = Guid.NewGuid();

                _sut.WithSetting("SomeString", "Test 123");
                _sut.WithSetting("SomeInt", 100);
                _sut.WithSetting("SomeUri", uri);
                _sut.WithSetting("SomeGuid", guid);

                var result = _sut.Build();

                Assert.That(result.GetValue<string>("SomeString"), Is.EqualTo("Test 123"));
                Assert.That(result.GetValue<int>("SomeInt"), Is.EqualTo(100));
                Assert.That(result.GetValue<Uri>("SomeUri"), Is.EqualTo(uri));
                Assert.That(result.GetValue<Guid>("SomeGuid"), Is.EqualTo(guid));
            }
        }
    }
}
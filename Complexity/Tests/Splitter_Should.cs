using System;
using System.Collections.Generic;
using FluentAssertions;
using Infrastructure;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Splitter_Should
    {
        [Test]
        public void AcceptItself()
        {
            var strings = new[] {"aa", "bb", "cc"};
            var (s,key) = strings.SafeConcat();
            if (s.TrySafeSplit(key, out var  xs))
                xs.Count.Should().Be(strings.Length);
        }

        [Test]
        public void ThrowOnEmptyList()
        {
            Action usage = () => new List<string>().SafeConcat();
            usage.Should().Throw<ArgumentException>();
        }
        [Test]
        public void ThrowOnSingleElement()
        {
            Action usage = () => new List<string>(){"a"}.SafeConcat();
            usage.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ReturnFalse_WhenSplitWithNonExistingKey()
        {
            var strings = new[] { "aa", "bb", "cc" };
            var (result, _) = strings.SafeConcat();
            result.TrySafeSplit(new Splitter.Key(Guid.NewGuid()), out _).Should().BeFalse();
        }

        [TestCase("a","b")]
        [TestCase("a","b","c","d")]
        public void SplitAndConcat(params string[] strings)
        {
            var (s, k) = strings.SafeConcat();
            s.TrySafeSplit(k, out var result).Should().BeTrue();
            result.Should().BeEquivalentTo(strings);

        }
    }
}
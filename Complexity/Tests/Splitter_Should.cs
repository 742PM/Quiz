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
        public void ReturnEmptyStringOnEmptyList()
        {
            new List<string>().SafeConcat(out _)
            .Should().BeEquivalentTo("");
        }
        [Test]
        public void NotChangeAnythingOnSingleElement()
        {
            new List<string>(){"a"}.SafeConcat(out var key).SafeSplit(key).Should().BeEquivalentTo("a");
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
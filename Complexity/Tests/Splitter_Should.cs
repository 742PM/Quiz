using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Infrastructure;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Storage_Should
    {

        [Test]
        public void Throw_OnNull()
        {
            Action creation = () => Storage.Concat(null);
            creation.Should()
                    .Throw<ArgumentException>();
        }

        [Test]
        public void ReturnEmptyArray_OnEmptyArray()
        {
            Storage.Concat()
                   .Split()
                   .Should()
                   .BeEquivalentTo(new string[0]);
        }
        


        [TestCase("a","b")]
        [TestCase("a","b","c","d")]
        public void SplitAndConcat(params string[] strings)
        {
            Storage.Concat(strings)
                   .Split()
                   .Should()
                   .BeEquivalentTo(strings);
        }

        [Test]
        public void Map()
        {
            Storage.Concat("a", "b").Map(s => s.ToUpper()).Split().Should().BeEquivalentTo("A", "B");
        }

        [Test]
        public void MapMany()
        {
            new[] { Storage.Concat("a", "b"), Storage.Concat("c", "d") }
                .MapMany(arr => arr.Select(s => s.ToUpper()).ToArray())
                .Should()   
                .BeEquivalentTo(new[] { Storage.Concat("A", "B"), Storage.Concat("C", "D") });
        }
    }
}
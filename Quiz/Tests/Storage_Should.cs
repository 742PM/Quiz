using System;
using System.Linq;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Extensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
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

        /// <summary>
        /// Если честно, я черт знает, как описать здесь нужное поведение.
        /// Функция не должна менять ключ, использованный для склейки. Но в принципе может и менять, если не сильно.
        /// Оставлю так и буду надеяться, что никто не будет пользоваться этим методом.
        /// С <see cref="Domain.Entities.TaskGenerators.TemplateTaskGenerator"/> он работает адекватно и верно, так там
        /// строка меняется вне ключа.
        /// </summary>
        [Test]
        public void Throw_WhenMappedWithBreakingFunctions()
        {
            Action mapping = () => Storage.Concat("a", "b","c").Map(_ => "_");
            mapping.Should().Throw<ArgumentException>();
        }

        [Test]
        public void NotChangeAnything_WhenMappedWithIdentity()
        {
            Storage.Concat("a", "b").Map(StorageExtensions.Identity).Split().Should().BeEquivalentTo("a", "b");
        }

        [Test]
        public void MapMany()
        {
            new[] { Storage.Concat("a", "b"), Storage.Concat("c", "d") }
                .MapMany(arr => arr.Select(s => s.ToUpper()).ToArray())
                .Should()   
                .BeEquivalentTo(Storage.Concat("A", "B"), Storage.Concat("C", "D"));
        }
    }
}
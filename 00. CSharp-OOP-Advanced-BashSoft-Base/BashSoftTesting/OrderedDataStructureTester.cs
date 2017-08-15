using NUnit.Framework;

namespace BashSoftTesting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BashSoft.Contracts;
    using BashSoft.DataStructures;

    [TestFixture]
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;

        [SetUp]
        public void Initialize()
        {
            this.names = new SimpleSortedList<string>();
        }

        [Test]
        public void TestEmptyConstructor()
        {
            this.names = new SimpleSortedList<string>();
            Assert.AreEqual(16, this.names.Capacity);
            Assert.AreEqual(0, this.names.Size);
        }

        [Test]
        public void TestConstructorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(20);
            Assert.AreEqual(20, this.names.Capacity);
            Assert.AreEqual(0, this.names.Size);

        }

        [Test]
        public void TestConstructorWithInitialComparer()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(16, this.names.Capacity);
            Assert.AreEqual(0, this.names.Size);

        }

        [Test]
        public void TestConstructorWithAllParameters()
        {
            this.names = new SimpleSortedList<string>(30, StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(30, this.names.Capacity);
            Assert.AreEqual(0, this.names.Size);

        }

        [Test]
        public void TestAddIncreasesSize()
        {
            this.names.Add("Someone");

            Assert.AreEqual(1, this.names.Size);
        }

        [Test]
        public void TestAddNullThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.Add(null));
        }

        [Test]
        [TestCase("Rosen,Georgi,Balkan")]
        public void TestAddUnsortedDataIsHeldSorted(string listString)
        {
            // Arrange
            var list = listString.Split(',')
                .ToArray();

            var sortedCollection = new[] {"Rosen", "Georgi", "Balkan"};

            // Act
            for (int i = 0; i < list.Length; i++)
            {
                this.names.Add(list[i]);
            }

            // Assert
            CollectionAssert.AreEqual(sortedCollection, list);
        }

        [Test]
        [TestCase("R,G,F,R,T,H,Y,S,A,W,V,K,I,X,P,M,T")]
        public void TestAddingMoreThanInitialCapacity(string listString)
        {
            // Arrange
            var list = listString.Split(',')
                .ToArray();

            // Act
            for (int i = 0; i < list.Length; i++)
            {
                this.names.Add(list[i]);
            }

            //Assert
            Assert.AreNotEqual(16, this.names.Capacity);
            Assert.AreEqual(17, this.names.Size);
        }

        [Test]
        [TestCase("R,G")]
        public void TestAddingAllFromCollectionIncreasesSize(string listString)
        {
            // Arrange
            var list = listString.Split(',')
                .ToArray();

            // Act

            this.names.AddAll(list);

            //Assert

            Assert.AreEqual(2, this.names.Size);
        }

        [Test]
        public void TestAddingAllFromNullThrowsException()
        {
            
            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => this.names.AddAll(null));
        }

        [Test]
        [TestCase("Rosen,Georgi,Balkan")]
        public void TestAddAllKeepsSorted(string listString)
        {
            // Arrange
            var list = listString.Split(',')
                .ToArray();

            var sortedCollection = new string[] {"Balkan", "Georgi", "Rosen" };

            // Act

            this.names.AddAll(list);
            var index = 0;

            // Assert
            foreach (var name in names)
            {
                var actual = name;
                var expected = sortedCollection[index++];
                Assert.AreEqual(expected, actual);
            }
            
        }

        [Test]
        [TestCase("Rosen,Georgi,Balkan")]
        public void TestAddAllKeepsSortedWithAssertCollection(string listString)
        {
            // Arrange
            var list = listString.Split(',')
                .ToArray();

            var sortedCollection = new string[] { "Balkan", "Georgi", "Rosen" };

            // Act

            this.names.AddAll(list);
            
            // Assert
            CollectionAssert.AreEqual(sortedCollection,this.names, Comparer<string>.Create((x, y) => x.CompareTo(y)));

        }

        [Test]
        [TestCase("Rosen,Georgi,Balkan")]
        public void TestRemoveValidElementDecreasesSize(string listString)
        {
            // Arrange
            var list = listString.Split(',')
                .ToArray();

            var elementToRemove = "Ivan";

            //Act
            this.names.AddAll(list);
            this.names.Add(elementToRemove);
            this.names.Remove(elementToRemove);

            //Assert
            Assert.AreEqual(list.Length, this.names.Size);
        }

        [Test]
        [TestCase("Rosen,Georgi,Balkan")]
        public void TestRemoveValidElementRemovesSelectedOne(string listString)
        {
            // Arrange
            var list = listString.Split(',')
                .ToArray();

            var elementToRemove = "Ivan";
            var anotherElement = "Nasko";

            //Act
            this.names.AddAll(list);
            this.names.Add(elementToRemove);
            this.names.Add(anotherElement);
            this.names.Remove(elementToRemove);

            //Assert
            CollectionAssert.DoesNotContain(this.names, elementToRemove);
        }

        [Test]
        [TestCase("Rosen,Georgi,Balkan")]
        public void TestRemovingNullThrowsException(string listString)
        {
            // Arrange
            var list = listString.Split(',')
                .ToArray();

            //Act
            this.names.AddAll(list);

            //Assert
            Assert.Throws<ArgumentNullException>(() => this.names.Remove(null));
        }

        [Test]
        [TestCase("Rosen,Georgi,Balkan")]
        public void TestJoinWithNull(string listString)
        {

            // Arrange
            var list = listString.Split(',')
                .ToArray();

            //Act
            this.names.AddAll(list);

            //Assert
            Assert.Throws<ArgumentNullException>(() => this.names.JoinWith(null));
        }

        [Test]
        [TestCase("Rosen,Georgi,Balkan")]
        public void TestJoinWorksFine(string listString)
        {

            // Arrange
            var list = listString.Split(',')
                .ToArray();

            var expectedString = "Balkan, Georgi, Rosen" ;

            //Act
            this.names.AddAll(list);
           var actualString = this.names.JoinWith(", ");

            //Assert
            Assert.AreEqual(expectedString,actualString);
        }
    }
}

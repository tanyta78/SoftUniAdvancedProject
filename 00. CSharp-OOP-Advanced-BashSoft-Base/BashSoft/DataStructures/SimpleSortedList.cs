namespace BashSoft.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using BashSoft.Contracts;

    public class SimpleSortedList<T> : ISimpleOrderedBag<T>
        where T : IComparable<T>
    {
        private const int DefaultSize = 16;

        private T[] innerCollection;
        private IComparer<T> comparison;

        public SimpleSortedList(int capacity, IComparer<T> comparer)
        {
            this.comparison = comparer;
            this.InitializeInnerCollection(capacity);
        }

        public SimpleSortedList(int capacity)
            : this(capacity, Comparer<T>.Create((x, y) => x.CompareTo(y)))
        {
        }

        public SimpleSortedList(IComparer<T> comparer)
            : this(DefaultSize, comparer)
        {
        }

        public SimpleSortedList()
            : this(DefaultSize)
        {
        }

        public int Size { get; private set; }

        public void Add(T element)
        {
            if (this.innerCollection.Length == this.Size)
            {
                this.Resize();
            }

            this.innerCollection[this.Size] = element;
            this.Size++;
            Array.Sort(this.innerCollection, 0, this.Size, this.comparison);

            /*  BubbleSort(this.innerCollection, this.comparison);
             SelectionSort(this.innerCollection, this.comparison);
             InsertionSort(this.innerCollection, this.comparison);
             to do QuickSort*/
        }

        /*
                private void InsertionSort(T[] collection, IComparer<T> comparer)
                {
                    var equalityComparer = comparer ?? Comparer<T>.Default;
                    for (var counter = 0; counter < collection.Length - 1; counter++)
                    {
                        var index = counter + 1;
                        while (index > 0)
                        {
                            if (equalityComparer.Compare(collection[index - 1], collection[index]) > 0)
                            {
                                var temp = collection[index - 1];
                                collection[index - 1] = collection[index];
                                collection[index] = temp;
                            }

                            index--;
                        }
                    }
                }

                private void SelectionSort(T[] collection, IComparer<T> comparer)
                {
                    T temp;

                    for (int i = 0; i < collection.Length - 1; i++)
                    {
                        for (int j = i; j < collection.Length - 1; j++)
                        {
                            T first = collection[i];
                            T other = collection[j + 1];

                            if (comparer.Compare(first, other) < 0)
                            {
                                temp = collection[i];
                                collection[i] = collection[j + 1];
                                collection[j + 1] = temp;
                            }
                        }
                    }
                }

                private void BubbleSort(T[] collection, IComparer<T> comparer)
                {
                    bool isRunning = true;
                    while (isRunning)
                    {
                        isRunning = false;
                        for (int j = 0; j < collection.Length - 1; j++)
                        {
                            T first = collection[j];
                            T other = collection[j + 1];
                            if (comparer.Compare(first, other) > 0)
                            {
                                collection[j] = other;
                                collection[j + 1] = first;
                                isRunning = true;
                            }
                        }
                    }
                }
        */

        public void AddAll(ICollection<T> collection)
        {
            if (this.Size + collection.Count >= this.innerCollection.Length)
            {
                this.MultiResize(collection);
            }

            foreach (var element in collection)
            {
                this.innerCollection[this.Size] = element;
                this.Size++;
            }

            Array.Sort(this.innerCollection, 0, this.Size, this.comparison);

            // BubbleSort(this.innerCollection, this.comparison);

            // SelectionSort(this.innerCollection, this.comparison);
        }

        public string JoinWith(string joiner)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var element in this)
            {
                sb.Append(element);
                sb.Append(joiner);
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Size; i++)
            {
                yield return this.innerCollection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void InitializeInnerCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative!");
            }

            this.innerCollection = new T[capacity];
        }

        private void MultiResize(ICollection<T> collection)
        {
            int newSize = this.innerCollection.Length * 2;
            while (this.Size + collection.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newCollection = new T[newSize];
            Array.Copy(this.innerCollection, newCollection, this.Size);

            this.innerCollection = newCollection;
        }

        private void Resize()
        {
            T[] newCollection = new T[this.Size * 2];
            Array.Copy(this.innerCollection, newCollection, this.Size);
            this.innerCollection = newCollection;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.PriorityQueue
{
    //public class PriorityQueue<TPriorityItem, TItem> : IPriorityQueue<TPriorityItem> where TPriorityItem : IPriorityItem<TItem>
    public class PriorityQueue<TPriorityKey, TPriorityItem> : IPriorityQueue<TPriorityKey, TPriorityItem> where TPriorityItem : IPriorityItem<TPriorityKey, TPriorityItem>
    {
        #region Heap Private fields

        private const int HighestItemIndex = 1;

        private readonly TPriorityItem[] HeapArray;
        private readonly int ArraySize;

        private int ItemsCount;

        #endregion

        #region Heap Implementation
        //////////////////////////////////////////////////////////////////////////////////////////////
        // Implementation base on:                                                                  //
        // http://cs.anu.edu.au/~Alistair.Rendell/Teaching/apac_comp3600/module2/binary_heaps.xhtml //
        //                                                                                          //
        // By Dominik Janiec in 2013                                                                //
        //////////////////////////////////////////////////////////////////////////////////////////////

        private void MaxHeapInsert(TPriorityItem item)
        {
            ++ItemsCount;
            HeapArray[ItemsCount] = item;
            HeapSiftUp(ItemsCount);
        }

        private TPriorityItem HeapMaximum()
        {
            return HeapArray[HighestItemIndex];
        }

        private TPriorityItem HeapExtractMax()
        {
            TPriorityItem MostImportatnItem = HeapMaximum();

            HeapArray[HighestItemIndex] = HeapArray[ItemsCount];
            HeapArray[ItemsCount] = default(TPriorityItem);
            --ItemsCount;

            MaxHeapify(HighestItemIndex);

            return MostImportatnItem;
        }

        #region Helper Heap Function

        private void HeapSiftUp(int currentItemIndex)
        {
            int parentItemIndex = ParentIndex(currentItemIndex);
            while (currentItemIndex > HighestItemIndex &&
                HeapArray[parentItemIndex].IsMoreImportantThan(HeapArray[currentItemIndex]) == false)
            {
                SwapItemsByIndex(currentItemIndex, parentItemIndex);

                currentItemIndex = parentItemIndex;
                parentItemIndex = ParentIndex(currentItemIndex);
            }
        }

        private void MaxHeapify(int currentItemIndex)
        {
            int largestIndex;

            int leftItemIndex = LeftChildIndex(currentItemIndex);
            int rightItemIndex = RightChildIndex(currentItemIndex);

            if (leftItemIndex <= ItemsCount &&
                HeapArray[leftItemIndex].IsMoreImportantThan(HeapArray[currentItemIndex]))
            {
                largestIndex = leftItemIndex;
            }
            else
            {
                largestIndex = currentItemIndex;
            }

            if (rightItemIndex <= ItemsCount &&
                HeapArray[rightItemIndex].IsMoreImportantThan(HeapArray[largestIndex]))
            {
                largestIndex = rightItemIndex;
            }

            if (largestIndex != currentItemIndex)
            {
                SwapItemsByIndex(currentItemIndex, largestIndex);
                MaxHeapify(largestIndex);
            }
        }

        private int ParentIndex(int itemIndex) { return (int)(itemIndex / 2); }
        private int LeftChildIndex(int itemIndex) { return 2 * itemIndex; }
        private int RightChildIndex(int itemIndex) { return (2 * itemIndex) + 1; }

        private void SwapItemsByIndex(int firstItemIndex, int secondItemIndex)
        {
            TPriorityItem tempItem = HeapArray[secondItemIndex];
            HeapArray[secondItemIndex] = HeapArray[firstItemIndex];
            HeapArray[firstItemIndex] = tempItem;
        }

        #endregion

        #endregion


        #region Public Interface - Properties

        public int Count { get { return ItemsCount; } }
        public int MaxElements { get { return ArraySize; } }

        public bool IsFull { get { return ItemsCount == ArraySize; } }
        public bool IsEmpty { get { return ItemsCount == 0; } }

        #endregion

        #region Public Interface - Methods

        public bool Insert(TPriorityItem item)
        {
            if (IsFull == false)
            {
                MaxHeapInsert(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public TPriorityItem PullHighest()
        {
            if (IsEmpty == false)
            {
                return HeapExtractMax();
            }
            else
            {
                throw new InvalidOperationException("Can NOT Pull from an empty queue.");
            }
        }

        public TPriorityItem PeekHighest()
        {
            if (IsEmpty == false)
            {
                return HeapMaximum();
            }
            else
            {
                throw new InvalidOperationException("Can NOT Peek at an empty queue.");
            }
        }

        public void Clear()
        {
            Array.Clear(HeapArray, 0, ArraySize + HighestItemIndex);
            ItemsCount = 0;
        }

        #endregion


        #region Constructors

        public PriorityQueue(int size)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException("size", size, "Can NOT be smaller than 1");
            }
            if (size == Int32.MaxValue)
            {
                throw new ArgumentOutOfRangeException("size", size, String.Format("HAVE to be smaller than {0}", Int32.MaxValue));
            }

            HeapArray = new TPriorityItem[size + HighestItemIndex];

            ArraySize = size;
            ItemsCount = 0;
        }

        #region Filling Constructor

        public PriorityQueue(IEnumerable<TPriorityItem> collection, int? size = null)
        {
            if (size.HasValue == true)
            {
                if (size.Value < 1)
                {
                    throw new ArgumentOutOfRangeException("size", size, "Can NOT be smaller than 1");
                }
                if (size.Value == Int32.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("size", size, String.Format("HAVE to be smaller than {0}", Int32.MaxValue));
                }
            }
            else
            {
                if (collection == null)
                {
                    throw new ArgumentNullException("collection", "Indeterminable size of queue, because also parameter \"size\" has NO value.");
                }
                if (collection.Any() == false)
                {
                    throw new ArgumentException("Indeterminable size of queue. Parameter \"collection\" is empty, and also parameter \"size\" has NO value.");
                }
            }

            List<TPriorityItem> collectionList = new List<TPriorityItem>(0);
            if ((collection == null) == false)
            {
                if (size.HasValue == false)
                {
                    collectionList = collection.ToList();
                }
                else
                {
                    collectionList = collection.Take(size.Value).ToList();
                }
            }

            if (size.HasValue == true)
            {
                HeapArray = new TPriorityItem[size.Value + HighestItemIndex];
                ArraySize = size.Value;
            }
            else
            {
                ArraySize = collectionList.Count;
                HeapArray = new TPriorityItem[ArraySize + HighestItemIndex];
            }

            foreach (TPriorityItem item in collectionList)
            {
                MaxHeapInsert(item);
                ++ItemsCount;
            }
        }

        #endregion

        #endregion
    }
}

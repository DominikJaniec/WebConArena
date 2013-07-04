using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.PriorityQueue
{
    public class PriorityQueue<TPriorityKey, TPriorityItem> : IPriorityQueue<TPriorityKey, TPriorityItem> where TPriorityItem : IPriorityItem<TPriorityKey, TPriorityItem>
    {
        #region Heap Private fields

        private const int HighestItemIndex = 1;

        private readonly TPriorityItem[] HeapArray;
        private readonly int ArraySize;

        private int ItemsCount;

        private Dictionary<TPriorityItem, int> HeapArrayIndexes;

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
            HeapArrayIndexes.Add(item, ItemsCount);

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
            HeapArrayIndexes.Remove(MostImportatnItem);
            --ItemsCount;

            MaxHeapify(HighestItemIndex);

            return MostImportatnItem;
        }

        private void HeapChangePriority(int itemIndex)
        {
            int parentItemIndex = ParentIndex(itemIndex);
            if (parentItemIndex >= HighestItemIndex &&
                HeapArray[parentItemIndex].IsMoreImportantThan(HeapArray[itemIndex]) == false)
            {
                HeapSiftUp(itemIndex);
            }
            else
            {
                MaxHeapify(itemIndex);
            }
        }

        private void HeapRemoveAt(int itemIndex)
        {
            TPriorityItem Item = HeapArray[itemIndex];

            HeapArray[itemIndex] = HeapArray[ItemsCount];
            HeapArray[ItemsCount] = default(TPriorityItem);
            HeapArrayIndexes.Remove(Item);
            --ItemsCount;

            if (itemIndex < ItemsCount)
            {
                HeapChangePriority(itemIndex);
            }
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

            HeapArrayIndexes[HeapArray[firstItemIndex]] = firstItemIndex;
            HeapArrayIndexes[HeapArray[secondItemIndex]] = secondItemIndex;
        }

        #endregion

        #endregion


        #region Public Interface - Properties

        public int Count { get { return ItemsCount; } }
        public int Size { get { return ArraySize; } }

        public bool IsFull { get { return ItemsCount >= ArraySize; } }
        public bool IsEmpty { get { return ItemsCount <= 0; } }

        public bool IsReadOnly { get { return false; } }

        #endregion

        #region Public Interface - Methods

        public bool ItemPriorityChanged(TPriorityItem item)
        {
            if (Contains(item))
            {
                HeapChangePriority(HeapArrayIndexes[item]);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Insert(TPriorityItem item)
        {
            if (IsFull == true || Contains(item))
            {
                return false;
            }
            else
            {
                MaxHeapInsert(item);
                return true;
            }
        }

        public void Add(TPriorityItem item)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Can NOT Add to full queue.");
            }
            else if (Contains(item))
            {
                throw new InvalidOperationException("Can NOT Add item multiple times.");
            }
            else
            {
                Insert(item);
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

        public bool Contains(TPriorityItem item)
        {
            return HeapArrayIndexes.ContainsKey(item);
        }

        public bool Remove(TPriorityItem item)
        {
            if (Contains(item))
            {
                HeapRemoveAt(HeapArrayIndexes[item]);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            Array.Clear(HeapArray, 0, ArraySize + HighestItemIndex);
            HeapArrayIndexes.Clear();
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

            ArraySize = size;
            ItemsCount = 0;

            HeapArray = new TPriorityItem[ArraySize + HighestItemIndex];
            HeapArrayIndexes = new Dictionary<TPriorityItem, int>(ArraySize);
        }

        public PriorityQueue(IEnumerable<TPriorityItem> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "Indeterminable size of queue, because also parameter \"size\" has NO value.");
            }
            if (collection.Any() == false)
            {
                throw new ArgumentException("Indeterminable size of queue. Parameter \"collection\" is empty, and also parameter \"size\" has NO value.");
            }

            List<TPriorityItem> collectionList = collection.ToList();

            ArraySize = collectionList.Count;
            ItemsCount = 0;

            HeapArray = new TPriorityItem[ArraySize + HighestItemIndex];
            HeapArrayIndexes = new Dictionary<TPriorityItem, int>(ArraySize);

            foreach (TPriorityItem item in collectionList)
            {
                MaxHeapInsert(item);
                ++ItemsCount;
            }
        }

        #endregion

        #region Shoud not be Implemented?

        public IEnumerator<TPriorityItem> GetEnumerator()
        {
            return SourcePriorityItemList().AsReadOnly().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            IEnumerable source = SourcePriorityItemList().AsReadOnly();
            return source.GetEnumerator();
        }

        public void CopyTo(TPriorityItem[] array, int arrayIndex)
        {
            SourcePriorityItemList().CopyTo(array, arrayIndex);
        }

        private List<TPriorityItem> SourcePriorityItemList()
        {
            return HeapArrayIndexes.Select(kvp => kvp.Key).ToList();
        }

        #endregion
    }
}

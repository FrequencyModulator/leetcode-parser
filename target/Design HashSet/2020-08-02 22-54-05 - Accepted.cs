/*
Status: Accepted
Runtime: 252 ms
Memory: 47.8 MB
URL: http://leetcode.com/submissions/detail/375222791/
Submission DateTime: August 2, 2020 10:54:05 PM
*/
public class MyHashSet {

        private const int hashBase = 1009; // prime number -> less collisions
        private readonly LinkedList<int>[] buckets = new LinkedList<int>[hashBase];
        
        /** Initialize your data structure here. */
        public MyHashSet()
        {

        }

        public void Add(int key)
        {
            if(Contains(key))
                return;

            var hash = GetKeyHash(key);
            var b = buckets[hash];
            if(b == null)
            {
                buckets[hash] = new LinkedList<int>();
                b = buckets[hash];
            }
            b.AddLast(key);
        }

        public void Remove(int key)
        {
            if(!Contains(key))
                return;

            var b = GetKeyBucket(key);
            b.Remove(b.Find(key));
        }

        /** Returns true if this set contains the specified element */
        public bool Contains(int key)
        {
            var b = GetKeyBucket(key);
            if(b == null)
                return false;
            return b.Find(key) != null;            
        }

        private int GetKeyHash(int key) => Math.Abs(key) % hashBase;
        private LinkedList<int> GetKeyBucket(int key) => buckets[GetKeyHash(key)];
}

/**
 * Your MyHashSet object will be instantiated and called as such:
 * MyHashSet obj = new MyHashSet();
 * obj.Add(key);
 * obj.Remove(key);
 * bool param_3 = obj.Contains(key);
 */

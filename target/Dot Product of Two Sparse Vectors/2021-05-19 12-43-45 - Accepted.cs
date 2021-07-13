/*
Status: Accepted
Runtime: 448 ms
Memory: 45.7 MB
URL: http://leetcode.com/submissions/detail/495409964/
Submission DateTime: May 19, 2021 12:43:45 PM
*/
public class SparseVector {
    
    private Dictionary<int, int> values;
  
    public SparseVector(int[] nums) {
      if(nums == null)
        values = new Dictionary<int, int>(0);
      else
        values = nums.Select((num, i) => (i, num)).Where(p => p.num != 0).ToDictionary(p => p.i, p => p.num);
    }
      
    
    // Return the dotProduct of two sparse vectors
    public int DotProduct(SparseVector vec) {
      int result = 0;
      foreach(var p in values)
      {
        if(vec.values.ContainsKey(p.Key))
          result += p.Value * vec.values[p.Key];
      }
      
      return result;
    }
  
  
}

// Your SparseVector object will be instantiated and called as such:
// SparseVector v1 = new SparseVector(nums1);
// SparseVector v2 = new SparseVector(nums2);
// int ans = v1.DotProduct(v2);

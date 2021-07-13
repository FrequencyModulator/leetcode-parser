/*
Status: Accepted
Runtime: 132 ms
Memory: 36.7 MB
URL: http://leetcode.com/submissions/detail/489747449/
Submission DateTime: May 6, 2021 3:27:28 PM
*/
public class Solution {
    // if cummulative sum of elements i and j deffer on k then summ all elements from j to j is equal k
    public int SubarraySum(int[] nums, int k) {
      var map = new Dictionary<int, int>(nums.Length + 1){[0] = 1};      
      int sum = 0;
      int count = 0;
      for(int i = 0; i < nums.Length; i++)  
      {
        sum += nums[i];
        int diff = sum - k;
        if(map.ContainsKey(diff))
          count += map[diff];          
        if (map.ContainsKey(sum))
          map[sum] += 1; // important! ""+ 1" to count
        else
          map[sum] = 1;
      }
      
      return count;
    }
}

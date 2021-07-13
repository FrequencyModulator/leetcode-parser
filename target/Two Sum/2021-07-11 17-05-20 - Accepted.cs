/*
Status: Accepted
Runtime: 388 ms
Memory: 32.7 MB
URL: http://leetcode.com/submissions/detail/520981787/
Submission DateTime: July 11, 2021 5:05:20 PM
*/
public class Solution {
    public int[] TwoSum(int[] nums, int target) 
    {
      var diffs = new Dictionary<int, int>();
      for(int i = 0; i < nums.Length; i++)
      {
        int d = target - nums[i];
        if(diffs.ContainsKey(d))
          return new[] {diffs[d], i};
        else
          diffs.Add(nums[i], i);
      }
      
      return new int[0];
    }
}

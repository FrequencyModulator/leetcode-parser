/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/500076003/
Submission DateTime: May 29, 2021 6:31:09 PM
*/
public class Solution {
    public bool CheckSubarraySum(int[] nums, int k) 
    {
      // the similar as to find continues subarray with sum == k
      // sum(a[0] mod k....a[i] mod k) == sum(a[0] mod k....a[j] mod k) then sum(a[i]...a[j]) mod k == 0 so range [i,j]
      var sumMods = new Dictionary<int, int>(nums.Length){[0] = -1};
      int sum = 0;
      for(int i = 0; i < nums.Length; i++)
      {
        sum += nums[i];        
        if (k != 0)
          sum %= k;        
        if(sumMods.TryGetValue(sum, out var prev))
          return true;        
        sumMods[sum] = i;          
      }
      
      return false;
      
    }
}

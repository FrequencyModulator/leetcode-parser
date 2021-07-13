/*
Status: Runtime Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/389070547/
Submission DateTime: August 31, 2020 3:34:22 PM
*/
public class Solution {
    public int[] TwoSum(int[] nums, int target) 
    {
      if(nums != null && nums.Length > 1)
      {
        Dictionary<int, int> complements = new Dictionary<int, int>(nums.Length);
        for(int i = 0; i < nums.Length; i++)
        {
          int c = target - nums[i];
          if(complements.ContainsKey(c))
            return new[]{complements[c], i};
          complements.Add(nums[i], i);
        }
      }
        
      return new int[0];
        
    }
}

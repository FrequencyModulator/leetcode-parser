/*
Status: Accepted
Runtime: 348 ms
Memory: 32.2 MB
URL: http://leetcode.com/submissions/detail/431738536/
Submission DateTime: December 17, 2020 3:18:23 PM
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
          if(!complements.ContainsKey(nums[i]))
            complements.Add(nums[i], i);
        }
      }
        
      return new int[0];
        
    }
}

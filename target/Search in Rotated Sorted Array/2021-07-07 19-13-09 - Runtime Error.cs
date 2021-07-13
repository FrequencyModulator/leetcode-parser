/*
Status: Runtime Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/518989506/
Submission DateTime: July 7, 2021 7:13:09 PM
*/
public class Solution {
    public int Search(int[] nums, int target)
    {
      // 1. serach min (rotated point)
      int p = SearchRotateIndex(nums, 0, nums.Length - 1);
      // 2. search in proper part
      if(p == -1)
        return Search(nums, target, 0, nums.Length - 1);
      else if(nums[0] <= target)
        return Search(nums, target, 0, p - 1);
      else 
        return Search(nums, target, p, nums.Length - 1);
      
    }
    
    private int SearchRotateIndex(int[] nums, int s, int e)
    {
      int m = (s + e) / 2;
      if(s > e)
        return -1;
      
      if(nums[m] < nums[m - 1] && nums[m] < nums[m + 1])
        return m;
      
      if(nums[0] < nums[m])
        s = m + 1;
      else 
        e = m -1;
      return SearchRotateIndex(nums, s, e);
    }
  
  private int Search(int[] nums, int target, int s, int e)
  {
    while(s <= e)
    {
      int m = (s + e) / 2;
      if(nums[m] == target)
        return m;
      if(nums[m] < target)
        s = m + 1;
      else
        e = m - 1;
    }
    return -1;
  }
  
}

/*
Status: Accepted
Runtime: 248 ms
Memory: 32.9 MB
URL: http://leetcode.com/submissions/detail/501005091/
Submission DateTime: May 31, 2021 3:43:55 PM
*/
public class Solution {
    public int[] SearchRange(int[] nums, int target) {
      // binary search: left most and righ most.
        
      var leftMost = BinarySearch(nums, target, 0, 0);
      if(leftMost == -1)
        return new[]{-1, -1};
      var rightMost = BinarySearch(nums, target, 1, leftMost);
      return new[]{leftMost, rightMost};
    }
  
    private static int BinarySearch(int[] nums, int target, int mode, int l)
    {
      var res = -1;      
      int r = nums.Length - 1;
      while(l <= r)
      {
        int m = (l + r ) / 2;
        if(nums[m] > target)
          r = m - 1;
        else if(nums[m] <  target)
          l = m + 1;
        else
        {
          res = m;
          if(mode == 0) // left most
            r = m - 1;
          else
            l = m + 1;
        }
      }
      return res;
    }
}

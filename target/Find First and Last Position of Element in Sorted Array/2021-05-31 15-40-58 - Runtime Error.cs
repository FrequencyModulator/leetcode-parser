/*
Status: Runtime Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/501003877/
Submission DateTime: May 31, 2021 3:40:58 PM
*/
public class Solution {
    public int[] SearchRange(int[] nums, int target) {
      // binary search: left most and righ most.
        
      var leftMost = BinarySearch(nums, target, 0);
      var rightMost = BinarySearch(nums, target, 1);
      return new[]{leftMost, rightMost};
    }
  
    private static int BinarySearch(int[] nums, int target, int mode)
    {
      var res = -1;
      int l = 0;
      int r = nums.Length;
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

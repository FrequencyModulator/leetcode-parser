/*
Status: Accepted
Runtime: 232 ms
Memory: 34.3 MB
URL: http://leetcode.com/submissions/detail/436254168/
Submission DateTime: December 29, 2020 10:56:41 PM
*/
public class Solution {
    public int RemoveDuplicates(int[] nums) {
        if(nums.Length < 2)
        {
          return nums.Length;
        }
      
        int i = 0;
        int j = 1;   
        int p = 1;
        while(j < nums.Length)
        {
          while(j < nums.Length && nums[i] == nums[j]) j++;          
          if(j < nums.Length)
          {            
            nums[p++] = nums[j];
            i = j++;
          }                      
        }
      
      return p;
    }
}

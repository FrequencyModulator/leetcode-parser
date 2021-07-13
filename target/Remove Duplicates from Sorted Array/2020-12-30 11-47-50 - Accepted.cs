/*
Status: Accepted
Runtime: 248 ms
Memory: 34.3 MB
URL: http://leetcode.com/submissions/detail/436456648/
Submission DateTime: December 30, 2020 11:47:50 AM
*/
public class Solution {
    public int RemoveDuplicates(int[] nums) {
        if(nums.Length == 0)        
          return 0;        
              
        
        int i = 0;   
        int p = 1;
        for(int j = 1; j < nums.Length; j++)
        {
          if(nums[i] != nums[j])
          {
            nums[p++] = nums[j];
            i = j;
          }
        }
                      
        return p;
    }
}

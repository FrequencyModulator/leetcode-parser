/*
Status: Accepted
Runtime: 252 ms
Memory: 34.2 MB
URL: http://leetcode.com/submissions/detail/436455943/
Submission DateTime: December 30, 2020 11:45:20 AM
*/
public class Solution {
    public int RemoveDuplicates(int[] nums) {
        if(nums.Length < 2)
        {
          return nums.Length;
        }
              
        
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

/*
Status: Accepted
Runtime: 376 ms
Memory: 32.1 MB
URL: http://leetcode.com/submissions/detail/501944911/
Submission DateTime: June 2, 2021 11:45:02 AM
*/
public class Solution {
    public void NextPermutation(int[] nums) {
      int i = nums.Length - 2;
      
      while(i >=0 && nums[i] >= nums[i + 1]) // find max(i): a[i] < a[i+1]
        i--;
              
      if(i >= 0)
      {        
        // find max(j): a[i] < a[j]
        int j = nums.Length - 1;
        while(nums[j] <= nums[i])
          j--;
        // swap(a[i], a[j])
        int t = nums[i];
        nums[i] = nums[j];
        nums[j] = t;
      }        
      // reverse nums right subarray (i...]
      Array.Reverse(nums, i + 1, nums.Length - i - 1);
          
      
    }
}

/*
Status: Accepted
Runtime: 240 ms
Memory: 30.9 MB
URL: http://leetcode.com/submissions/detail/438149307/
Submission DateTime: January 3, 2021 4:40:15 PM
*/
public class Solution {
    public void Merge(int[] nums1, int m, int[] nums2, int n) {        
        
        int p1 = m - 1; 
        int p2 = n - 1; 
        int pw = m + n - 1;
        
        while(p1 >= 0 || p2 >= 0)
        {
          if(p1 < 0)
              nums1[pw--] = nums2[p2--];
          else if(p2 < 0)
              nums1[pw--] = nums1[p1--];
          else if (nums1[p1] >= nums2[p2])                                
              nums1[pw--] = nums1[p1--];
          else
              nums1[pw--] = nums2[p2--];
        }
      
    }
}

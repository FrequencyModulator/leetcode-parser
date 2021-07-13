/*
Status: Runtime Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/521590011/
Submission DateTime: July 12, 2021 8:41:18 PM
*/
public class Solution {
    public void Merge(int[] nums1, int m, int[] nums2, int n) 
    {
        int p = m + n - 1; // insert position (from the end of nums1(result) array)
        // given numbers positions
        n -= 1;
        m -= 1;
      
        while(n >= 0 || m >= 0)
        {
          int v1 = 0;
          if(n >= 0)
            v1 = nums1[n];
          
          int v2 = 0;
          if(m >= 0)
            v2 = nums2[m];
                    
          if(v1 >= v2)
          {
            nums1[p--] = v1;    
            n--;
          }
          else
          {
            nums1[p--] = v2;
            m--;
          }
                    
        }
    }
}

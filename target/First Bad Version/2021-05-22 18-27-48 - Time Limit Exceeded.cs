/*
Status: Time Limit Exceeded
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/496839990/
Submission DateTime: May 22, 2021 6:27:48 PM
*/
/* The isBadVersion API is defined in the parent class VersionControl.
      bool IsBadVersion(int version); */

public class Solution : VersionControl {
    public int FirstBadVersion(int n) {
      int l = 0;
      int r = n;
      while(l < r)
      {
        int mid = (l + r) / 2;        
        if(IsBadVersion(mid))         
          r = mid;   
        else
          l = mid + 1;                  
      }      
      return l;
    }
}

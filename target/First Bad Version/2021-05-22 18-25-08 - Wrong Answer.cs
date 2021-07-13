/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/496839259/
Submission DateTime: May 22, 2021 6:25:08 PM
*/
/* The isBadVersion API is defined in the parent class VersionControl.
      bool IsBadVersion(int version); */

public class Solution : VersionControl {
    public int FirstBadVersion(int n) {
      int l = 0;
      int r = n - 1;
      while(l < r)
      {
        int mid = (l + r) / 2;        
        if(!IsBadVersion(mid))
        {
          if(IsBadVersion(mid + 1))
             return mid + 1;
           l = mid + 1;
        }
        else
           r = mid;          
      }
      if(IsBadVersion(0))
        return 0;
      return -1;
    }
}

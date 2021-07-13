/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/471514894/
Submission DateTime: March 23, 2021 1:29:36 PM
*/
public class Solution {
    public int MaxArea(int h, int w, int[] horizontalCuts, int[] verticalCuts) {
      Array.Sort(horizontalCuts);
      Array.Sort(verticalCuts);
      
      var maxH = MaxCutSize(h, horizontalCuts);
      var maxW = MaxCutSize(w, verticalCuts);
                            
      // because of 2 <= h, w <= 10^9 
      // max multiplication is 10^18 what is less then long max value = 9,223,372,036,854,775,807
      double t = maxH * maxW;
      return (int)(t % (Math.Pow(10, 9) + 7));
    }
  
    private int MaxCutSize(int edge, int[] cuts)
    {
      int max = cuts[0];
      for(int i = 0; i < cuts.Length - 1; i++)
      {        
        max = Math.Max(max, cuts[i + 1] - cuts[i]);
      }
      max = Math.Max(max, edge - cuts[cuts.Length - 1]);
      return max;
    }
}



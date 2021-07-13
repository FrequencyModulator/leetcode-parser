/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/471602425/
Submission DateTime: March 23, 2021 6:06:01 PM
*/
public class Solution {
    public int MaxArea(int h, int w, int[] horizontalCuts, int[] verticalCuts) {
      //Array.Sort(horizontalCuts);
      //Array.Sort(verticalCuts);
      
      var maxH = MaxCutSize2(h, horizontalCuts);
      var maxW = MaxCutSize2(w, verticalCuts);
                                  
      return (int)((maxH * maxW) % new BigInteger(Math.Pow(10, 9) + 7));
    }
  
    private BigInteger MaxCutSize(int edge, int[] cuts)
    {
      int max = cuts[0];
      for(int i = 0; i < cuts.Length - 1; i++)
      {        
        max = Math.Max(max, cuts[i + 1] - cuts[i]);
      }
      max = Math.Max(max, edge - cuts[cuts.Length - 1]);
      return max;
    }
  
    private BigInteger MaxCutSize2(int edge, int[] cuts)
    {
      var maxSize = (s: 0, e: 0);      
      foreach(int c in cuts.Concat(new[]{edge}))
      {
        var cutSize = maxSize;
        if(c > cutSize.s && c < cutSize.e)
            cutSize.s = c;
        else if(c > cutSize.s &&  c > cutSize.e)
            cutSize = (cutSize.e, c);

        if(maxSize.e - maxSize.s < cutSize.e - cutSize.s)
            maxSize = cutSize;
      }
      
      return maxSize.e - maxSize.s;
    }
}



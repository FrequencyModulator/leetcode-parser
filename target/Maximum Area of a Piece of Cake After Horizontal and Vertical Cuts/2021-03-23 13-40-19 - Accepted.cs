/*
Status: Accepted
Runtime: 192 ms
Memory: 38.7 MB
URL: http://leetcode.com/submissions/detail/471518472/
Submission DateTime: March 23, 2021 1:40:19 PM
*/
public class Solution {
    public int MaxArea(int h, int w, int[] horizontalCuts, int[] verticalCuts) {
      Array.Sort(horizontalCuts);
      Array.Sort(verticalCuts);
      
      var maxH = MaxCutSize(h, horizontalCuts);
      var maxW = MaxCutSize(w, verticalCuts);
                            
      var t = new BigInteger(maxH) * new BigInteger(maxW);
      return (int)(t % new BigInteger(Math.Pow(10, 9) + 7));
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



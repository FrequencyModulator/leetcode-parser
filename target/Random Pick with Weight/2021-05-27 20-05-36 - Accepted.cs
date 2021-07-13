/*
Status: Accepted
Runtime: 184 ms
Memory: 47.9 MB
URL: http://leetcode.com/submissions/detail/499205325/
Submission DateTime: May 27, 2021 8:05:36 PM
*/
public class Solution {

    private int[] sums;
    private int totalSum = 0;
    private Random random = new Random();
  
    public Solution(int[] w) 
    {
      // Calc cumulative sums      
      sums = new int[w.Length];
      for(int i = 0; i < w.Length; i++)
      {
        sums[i] = totalSum + w[i];
        totalSum = sums[i];
      }
    }
    
    public int PickIndex() 
    {
      double rnd = random.NextDouble() * totalSum;
            
      int l = 0;
      int r = sums.Length;
      while(l < r)
      {
        // better to avoid the overflow
        int m = l + (r - l) / 2;
        if(rnd > sums[m])
          l = m + 1;
        else
          r = m; 
      }
      return l;                
    }
}

/**
 * Your Solution object will be instantiated and called as such:
 * Solution obj = new Solution(w);
 * int param_1 = obj.PickIndex();
 */

/*
Status: Accepted
Runtime: 144 ms
Memory: 35.5 MB
URL: http://leetcode.com/submissions/detail/503582797/
Submission DateTime: June 5, 2021 7:39:23 PM
*/
public class Solution {
    public int LeastInterval(char[] tasks, int n) 
    {
      // get chars freqs.
      int[] freqs = new int[26];
      foreach(var c in tasks)
        freqs[c - 'A']++;
      
      // sort by freqs
      Array.Sort(freqs);
      
      int fMax = freqs[25];
      int idles = (fMax - 1) * n; // max number of iddle units of time
      
      for(int i = 24; i >= 0 && idles > 0; i--)
      {
        idles -= Math.Min((fMax - 1), freqs[i]); // fill with nother tasks         
      }
      
      return Math.Max(0, idles) + tasks.Length;
      
    }
}

/*
Status: Accepted
Runtime: 152 ms
Memory: 24.9 MB
URL: http://leetcode.com/submissions/detail/457284837/
Submission DateTime: February 17, 2021 2:26:32 PM
*/
public class Solution {
    public int MinDifficulty(int[] jobDifficulty, int d) 
    {
      if(jobDifficulty.Length < d) 
        return -1;
      int[][] mem = new int[d][];
      for(int i = 0; i < d; i++)
      {
        mem[i] = new int[jobDifficulty.Length];
        Array.Fill(mem[i], -1);
      }
      return dfs(mem, jobDifficulty, d - 1, 0);
    }
  
    private int dfs(int[][] mem,  int[] jobDifficulty, int d, int idx)
    {
      int max = -1;
      
      if(d == 0)
      {        
        while(idx < jobDifficulty.Length) 
        {
          max = Math.Max(max, jobDifficulty[idx++]);
        }
        return max;
      }
      
      if(mem[d][idx] != -1) 
        return mem[d][idx];
      
      max = -1;
      int res = Int32.MaxValue;
      for(int i = idx; i < jobDifficulty.Length - d; i++)
      {
        max = Math.Max(max, jobDifficulty[i]);
        res = Math.Min(res, max + dfs(mem, jobDifficulty, d - 1, i + 1));        
      }      
      mem[d][idx] = res;
      
      return res;
    }
}

/*
Status: Runtime Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/489661123/
Submission DateTime: May 6, 2021 11:08:21 AM
*/
public class Solution {
    public int[][] KClosest(int[][] points, int K) {
      var distances = points.Select(p => (p: p, d: Math.Sqrt(p[0]*p[0] + p[1]*p[1]))).ToArray();
      
      // quick select K
      QSelect(distances, K, 0, distances.Length - 1);
      
      var closest = distances.Take(K).Select(d => d.p).ToArray();
      
      return closest;
        
    }
  
    private void QSelect((int[] p, double d)[] distances, int K, int b, int e)
    {
      int p = Pivot(distances, b - 1, b, e);
      if(p == K)
        return;
      else if (p > K)
        QSelect(distances, K, b, p - 1);
      else
        QSelect(distances, K, p + 1, e);
    }
  
  private int Pivot((int[] p, double d)[] distances, int i, int j, int p)
  {
    for(; j < p; j++)
    {
      if(distances[j].d <= distances[p].d)
      {
        i++;
        if(i != j)
          (distances[i], distances[p]) = (distances[p], distances[i]); // swap
      }
    }
    i++;
    (distances[i], distances[p]) = (distances[p], distances[i]); // swap p
    return i;      
  }
    
}

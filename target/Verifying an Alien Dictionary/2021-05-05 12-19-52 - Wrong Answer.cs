/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/489258298/
Submission DateTime: May 5, 2021 12:19:52 PM
*/
public class Solution {
    public bool IsAlienSorted(string[] words, string order) {
      var alienOrder = order.Select((l, i) => (l - 'a', i)).ToDictionary(p => p.Item1, p => p.Item2);
      
      for (int i = 0; i < words.Length - 1; i++)
      {
        var w1 = words[i];
        var w2 = words[i + 1];
        for (int j = 0; j < w1.Length; j++)
        {
          if(j > w2.Length - 1) // if w1 longer then w2 then w1 > w1 
            return false;
          
          if(w1[j] != w2[j])
          {
            if(alienOrder[w1[j] - 'a'] > alienOrder[w2[j] - 'a'])
              return false;
            return true;
          }
          
        }
      }
      return true;  
      
    }
}

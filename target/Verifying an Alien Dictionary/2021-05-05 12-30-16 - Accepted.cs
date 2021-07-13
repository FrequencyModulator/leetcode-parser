/*
Status: Accepted
Runtime: 96 ms
Memory: 25.8 MB
URL: http://leetcode.com/submissions/detail/489261837/
Submission DateTime: May 5, 2021 12:30:16 PM
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
          // If we do not find a mismatch letter between words[i] and words[i + 1],
          // we need to examine the case when words are like ("apple", "app").
          if(j >= w2.Length) 
            return false;
          
          if(w1[j] != w2[j]) // if find different 
          {
            if(alienOrder[w1[j] - 'a'] > alienOrder[w2[j] - 'a'])
              return false;
            // if we find the first different letter and they are sorted,
            // then there's no need to check remaining letters
            // abcefga > abcdfga because of "abc == abc" but "e > d"
            break;
          }
          
        }
      }
      return true;  
      
    }
}

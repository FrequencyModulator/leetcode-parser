/*
Status: Accepted
Runtime: 228 ms
Memory: 32.4 MB
URL: http://leetcode.com/submissions/detail/434865720/
Submission DateTime: December 26, 2020 3:37:25 PM
*/
public class Solution {
    public IList<string> LetterCombinations(string digits) {
      var result = new List<string>();  
      if(string.IsNullOrWhiteSpace(digits))
        return result;
        
      Traverse(result, "", digits, 0, 0);
      return result;
    }
  
    private static readonly Dictionary<char, char[]> DigitsChars = new Dictionary<char, char[]>
    {      
      ['2'] = new[]{'a', 'b', 'c'},
      ['3'] = new[]{'d', 'e', 'f'},
      ['4'] = new[]{'g', 'h', 'i'},
      ['5'] = new[]{'j', 'k', 'l'},
      ['6'] = new[]{'m', 'n', 'o'},
      ['7'] = new[]{'p', 'q', 'r', 's'},
      ['8'] = new[]{'t', 'u', 'v'},
      ['9'] = new[]{'w', 'x', 'y', 'z'},
    };
  
    private void Traverse(List<string> combinations, string combination, string digits, int digitIndex, int charIndex)
    {                 
      for(int i = digitIndex; i < digits.Length; i++)
      {
        for(int j = charIndex; j < DigitsChars[digits[i]].Length; j++)
        {          
          var nextCombination = combination + DigitsChars[digits[i]][j];
          if(nextCombination.Length == digits.Length)
            combinations.Add(nextCombination);       
          else
            Traverse(combinations, nextCombination, digits, i + 1, 0);         
        }
      }      
    }
}

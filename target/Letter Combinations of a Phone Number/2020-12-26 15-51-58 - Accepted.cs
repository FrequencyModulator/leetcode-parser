/*
Status: Accepted
Runtime: 232 ms
Memory: 32.5 MB
URL: http://leetcode.com/submissions/detail/434870352/
Submission DateTime: December 26, 2020 3:51:58 PM
*/
public class Solution {
    public IList<string> LetterCombinations(string digits) {
      var result = new List<string>();  
      if(string.IsNullOrWhiteSpace(digits))
        return result;
        
      Traverse(result, "", digits);
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
  
    private void Traverse(List<string> combinations, string combination, string digits)
    {                 
      if(digits.Length == 0)
        combinations.Add(combination);
      else
      {                
        var digitChars = DigitsChars[digits[0]];
        for(int j = 0; j < digitChars.Length; j++)
        {                                                    
            Traverse(combinations, combination + digitChars[j], digits.Substring(1));         
        }        
      }
    }
}

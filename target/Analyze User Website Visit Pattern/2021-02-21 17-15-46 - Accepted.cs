/*
Status: Accepted
Runtime: 320 ms
Memory: 36.7 MB
URL: http://leetcode.com/submissions/detail/458977672/
Submission DateTime: February 21, 2021 5:15:46 PM
*/
public class Solution {
    public IList<string> MostVisitedPattern(string[] username, int[] timestamp, string[] website) 
        {                                    
            if((timestamp?.Length ?? 0) < 3)
                return null;

            // sort sessions by timestamp O(NLogN)
            IEnumerable<(int t, string user, string website)> sessions = timestamp.Select((t, i) => (t, username[i], website[i])).OrderBy(s => s.t);

            // get users all visited sequences O(N)            
            Dictionary<string, List<string>> userWebsites = new Dictionary<string, List<string>>();
            foreach(var s in sessions)
            {
                if(!userWebsites.ContainsKey(s.user))
                    userWebsites.Add(s.user, new List<string>());
                userWebsites[s.user].Add(s.website);
            }

            // get 3-seqs freq
            Dictionary<(string, string, string), int> freq3Seqs = new Dictionary<(string, string, string), int>();
            int maxVisit = 0;
            string[] maxSequince = new string[3];
            foreach(var uws in userWebsites.Values.Where(l => l.Count > 2))
            {
                var user3Seqs = All3Sequences(uws);
                foreach(var u3s in user3Seqs)
                {
                    var key = (u3s[0], u3s[1], u3s[2]);                    
                    if(!freq3Seqs.ContainsKey(key))
                        freq3Seqs.Add(key, 0);                    
                    freq3Seqs[key]++;
                    
                    if(freq3Seqs[key] > maxVisit || (freq3Seqs[key] == maxVisit && LexiCompare(u3s, maxSequince) < 0))
                    {
                        maxSequince = u3s;
                        maxVisit = freq3Seqs[key];
                    }                    
                }
            }
            
            return maxSequince.ToList();
        }

        private int LexiCompare(string[] x, string[] y)
        {                        
            for(int i = 0; i < x.Length; i++)
            {
                var r = StringComparer.InvariantCultureIgnoreCase.Compare(x[i], y[i]);
                if(r != 0 )
                    return r;
            }
            return 0;
        }
        
        private IEnumerable<string[]> All3Sequences(IList<string> s)
        {            
            int start = 0;
            int end = (s?.Count ?? 0) - 1;
            if(end - start < 2)
                yield break;

            HashSet<(string, string, string)> uniq = new HashSet<(string, string, string)>();
            int limitPoint = end - 2;            
            for(int i = start; i<=limitPoint; i++)
            for(int j = i + 1; j <= limitPoint + 1; j++)
            for(int k = j + 1; k <= end; k++)
            {                
                var key = (s[i], s[j], s[k]);
                if(!uniq.Contains(key))
                {
                    uniq.Add(key);
                    yield return new[]{s[i], s[j], s[k]};
                }
            }

        }
}

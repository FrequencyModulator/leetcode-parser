'''
Status: Accepted
Runtime: 32 ms
Memory: 14.4 MB
URL: http://leetcode.com/submissions/detail/489724407/
Submission DateTime: May 6, 2021 2:16:58 PM
'''
class Solution:
    def addBinary(self, a: str, b: str) -> str:
      x = int(a, 2)
      y = int(b, 2)
      while y:
        x, y = x ^ y, (x & y) << 1
      return bin(x)[2:]
        

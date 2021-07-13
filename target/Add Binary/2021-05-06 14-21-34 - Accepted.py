'''
Status: Accepted
Runtime: 32 ms
Memory: 14.3 MB
URL: http://leetcode.com/submissions/detail/489725952/
Submission DateTime: May 6, 2021 2:21:34 PM
'''
class Solution:
    def addBinary(self, a: str, b: str) -> str:
      x, y = int(a, 2), int(b, 2)
      while y:
        x, y = x ^ y, (x & y) << 1
        """ more readable        
        a = x ^ y
        c = (x & y) << 1
        x = a
        y = c"""
      return bin(x)[2:]
        
